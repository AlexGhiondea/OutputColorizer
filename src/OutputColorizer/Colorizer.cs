using System;
using System.Collections.Generic;
using System.Text;

namespace OutputColorizer
{
    public static class Colorizer
    {
        private static IOutputWriter s_printer = new ConsoleWriter();

        private static Dictionary<string, ConsoleColor> s_consoleColorMap = InitializeColors();

        private static Dictionary<string, ConsoleColor> InitializeColors()
        {
            Dictionary<string, ConsoleColor> map = new Dictionary<string, ConsoleColor>(StringComparer.OrdinalIgnoreCase);
            foreach (ConsoleColor item in Enum.GetValues(typeof(ConsoleColor)))
            {
                // we do this to allow not explicitly matching the console color values
                // and to not have to parse the color every single time
                map.Add(item.ToString(), item);
            }
            return map;
        }

        /// <summary>
        /// The format is like this: [Color!text{params}]. Nesting is allowed
        /// </summary>
        public static void WriteLine(string message, params object[] args)
        {
            InternalWrite(message, args);
            s_printer.WriteLine(string.Empty);
        }

        /// <summary>
        /// The format is like this: [Color!text{params}]. Nesting is allowed
        /// </summary>
        public static void Write(string message, params object[] args)
        {
            InternalWrite(message, args);
        }

        public static void SetupWriter(IOutputWriter newWriter)
        {
            s_printer = newWriter;
        }

        private static void InternalWrite(string message, object[] args)
        {
            Stack<ConsoleColor> colors = new Stack<ConsoleColor>();
            Stack<int> parens = new Stack<int>();
            int unbalancedParens = 0;
            // intial state
            parens.Push(-1);

            Dictionary<string, int> argMap = CreateArgumentMap(message);
            for (int i = 0; i < message.Length; i++)
            {
                char currentChar = message[i];

                switch (currentChar)
                {
                    case '\\':
                        {
                            // if we have an escaped character, continue.
                            i++;
                            continue;
                        }
                    case '[':
                        {
                            // When we encounter a '[' it means we are probably going to change the color
                            // so we need to write what we had up to this point.

                            // pop the location of the last paren from the stack
                            // Write the message segment between the last paren and the current position
                            int previousParenIndex = parens.Pop();
                            WriteMessageSegment(message, args, argMap, previousParenIndex, i);

                            // Given a string that looks like [color! extracts the color and pushes it on the stack of colors
                            ParseColor(message, colors, ref i);

                            // keep track of the latest parens that you saw
                            unbalancedParens++;
                            parens.Push(i);

                            continue;
                        }
                    case ']':
                        {
                            // at this point, we know where the color ended.
                            // Write the message segment between the last paren and the current position
                            int matchingbracket = parens.Pop();
                            WriteMessageSegment(message, args, argMap, matchingbracket, i);

                            if (colors.Count == 0)
                            {
                                throw new FormatException($"Missing expected ']' ");
                            }
                            s_printer.ForegroundColor = colors.Pop();

                            parens.Push(i);
                            unbalancedParens--;
                            continue;
                        }
                }
            }

            // at this point, the closing bracket might not have been found!
            if (unbalancedParens != 0)
            {
                throw new FormatException($"Missing expected ']' ");
            }

            // write the last part, if any
            int finalParen = parens.Pop();
            WriteMessageSegment(message, args, argMap, finalParen, message.Length);
        }

        private static void WriteMessageSegment(string message, object[] args, Dictionary<string, int> argMap, int startIndex, int currentIndex)
        {
            // do we have anything to print?
            if (currentIndex - startIndex - 1 > 0)
            {
                string messageSegment = message.Substring(startIndex + 1, currentIndex - startIndex - 1);
                string content = RewriteString(argMap, messageSegment, args);
                s_printer.Write(content);
            }
        }

        private static void ParseColor(string message, Stack<ConsoleColor> colors, ref int currPos)
        {
            int textLength = message.Length;
            // find the color
            for (int pos = currPos + 1; pos < textLength; pos++)
            {
                if (message[pos] == '!')
                {
                    string colorString = message.Substring(currPos + 1, pos - currPos - 1);
                    ConsoleColor color;

                    if (!s_consoleColorMap.TryGetValue(colorString, out color))
                    {
                        throw new ArgumentException($"Unknown color {colorString}");
                    }

                    colors.Push(s_printer.ForegroundColor);
                    s_printer.ForegroundColor = color;

                    // set the position of the last character
                    currPos = pos;
                    break;
                }
            }
        }

        private static string RewriteString(Dictionary<string, int> argMap, string content, params object[] args)
        {
            // one way to do this is to map the original index to the new index in the args array.
            // {3} {0} {0} ==> {0} {1} {1}

            //                     old     new 
            // first thing, map from {3} to {0}
            //              map from {0} to {1}

            // rewrite the string based on this map
            // generate the args array based on this map

            //            Dictionary<string, int> argMap = CreateArgumentMap(content);

            StringBuilder sb = new StringBuilder();
            int textLength = content.Length;
            for (int pos = 0; pos < textLength; pos++)
            {
                if (content[pos] == '}')
                {
                    // '}' are escaped as '}}'
                    if (pos + 1 < textLength && content[pos + 1] == '}')
                    {
                        sb.Append('}'); sb.Append('}');
                        pos++;
                        continue;
                    }
                }
                if (content[pos] == '{')
                {
                    // '{' are escaped as '{{'
                    if (pos + 1 < textLength && content[pos + 1] == '{')
                    {
                        sb.Append('{'); sb.Append('{');
                        pos++;
                        continue;
                    }

                    int temp = pos;
                    while (temp < textLength && content[temp++] != '}')
                    {
                    }

                    string arg = content.Substring(pos + 1, temp - pos - 2);
                    sb.Append('{');
                    sb.Append(argMap[arg]);
                    sb.Append('}');

                    pos = temp - 1;
                    continue;
                }
                else if ((pos + 1 < textLength && content[pos] == '\\') &&
                    (content[pos + 1] == '[' || content[pos + 1] == ']'))
                {
                    // get rid of the escaped [ and ] as those are ok in regular strings
                    pos++;
                }

                // passthrough the content
                sb.Append(content[pos]);
            }

            //create the array.
            object[] argument = new object[argMap.Count];

            int argidx = 0;
            foreach (var item in argMap.Keys)
            {
                int argOrig = int.Parse(item);
                argument[argidx++] = args[argOrig];
            }

            return string.Format(sb.ToString(), argument);
        }

        private static Dictionary<string, int> CreateArgumentMap(string content)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();

            int argCount = 0;
            int textLength = content.Length;
            for (int i = 0; i < textLength; i++)
            {
                if (content[i] == '{')
                {
                    // '{' are escaped as '{{'
                    if (i + 1 < textLength && content[i + 1] == '{')
                    {
                        i++;
                        continue;
                    }

                    // find the matching closing curly bracket
                    int pos = i;
                    while (pos < textLength && content[pos++] != '}')
                    {
                    }

                    if (content[pos - 1] != '}') // did not find matching '}'
                        throw new ArgumentException(string.Format("Could not parse '{0}'", content));

                    string arg = content.Substring(i + 1, pos - i - 2);

                    int x;
                    if (!int.TryParse(arg, out x))
                    {
                        throw new ArgumentException(string.Format("Could not parse '{0}'", content));
                    }

                    if (!map.ContainsKey(arg))
                    {
                        map.Add(arg, argCount++);
                    }
                }
            }

            return map;
        }
    }
}
