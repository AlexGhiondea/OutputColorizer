using System;
using System.Collections.Generic;
using System.Text;

namespace OutputColorizer
{
    public static class Colorizer
    {
        private static IOutputWriter s_printer = new ConsoleWriter();

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

        private static void InternalWrite(string message, object[] args)
        {
            Stack<ConsoleColor> colors = new Stack<ConsoleColor>();
            Stack<int> parens = new Stack<int>();

            // intial state
            colors.Push(s_printer.ForegroundColor);
            parens.Push(-1);

            for (int i = 0; i < message.Length; i++)
            {
                char currentChar = message[i];

                switch (currentChar)
                {
                    case '\\':
                        {
                            // we need to check if we have an escaped [ at this point.
                            // if we do, go on, this will be covered by the rewriteString.
                            if (i + 1 < message.Length && (message[i + 1] == '[' || message[i + 1] == ']'))
                            {
                                i++;
                                continue;
                            }
                            break;
                        }
                    case '[':
                        {
                            // When we encounter a '[' it means we are probably going to change the color
                            // so we need to write what we had up to this point.

                            // pop the location of the last paren from the stack
                            int temp = parens.Pop();

                            // do we have anything to print?
                            if (i - temp - 1 > 0)
                            {
                                string content = RewriteString(message.Substring(temp + 1, i - temp - 1), args);
                                s_printer.Write(content);
                            }

                            // Given a string that looks like [color! extracts the color and pushes it on the stack of colors
                            ParseColor(message, colors, ref i);

                            // keep track of the latest parens that you saw
                            parens.Push(i);

                            continue;
                        }
                    case ']':
                        {
                            // at this point, we know where the color ended.
                            int matchingbracket = parens.Pop();

                            if (i - matchingbracket - 1 > 0)
                            {
                                //retrieve the content from the message
                                string content = message.Substring(matchingbracket + 1, i - matchingbracket - 1);

                                content = RewriteString(content, args);
                                s_printer.Write(content);
                            }

                            s_printer.ForegroundColor = colors.Pop();

                            parens.Push(i);
                            continue;
                        }
                }
            }

            // write the last part, if any
            s_printer.ForegroundColor = colors.Pop();
            int finalParen = parens.Pop();
            if (message.Length - finalParen - 1 > 0)
            {
                string finalContent = RewriteString(message.Substring(finalParen + 1, message.Length - finalParen - 1), args);

                s_printer.Write(finalContent);
            }
        }

        private static void ParseColor(string message, Stack<ConsoleColor> colors, ref int currPos)
        {
            // find the color
            for (int pos = currPos + 1; pos < message.Length; pos++)
            {
                if (message[pos] == '!')
                {
                    string colorString = message.Substring(currPos + 1, pos - currPos - 1);
                    ConsoleColor color;
                    if (!Enum.TryParse(colorString, out color))
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

        private static string RewriteString(string content, params object[] args)
        {
            // one way to do this is to map the original index to the new index in the args array.
            // {3} {0} {0} ==> {0} {1} {1}

            //                     old     new 
            // first thing, map from {3} to {0}
            //              map from {0} to {1}

            // rewrite the string based on this map
            // generate the args array based on this map

            Dictionary<string, int> map = new Dictionary<string, int>();

            int argCount = 0;
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == '{')
                {
                    int pos = i;
                    while (content[pos++] != '}') ;

                    string arg = content.Substring(i + 1, pos - i - 2);

                    if (!map.ContainsKey(arg))
                    {
                        map.Add(arg, argCount++);
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int pos = 0; pos < content.Length; pos++)
            {
                if (content[pos] == '{')
                {
                    int temp = pos;
                    while (content[temp++] != '}') ;

                    string arg = content.Substring(pos + 1, temp - pos - 2);
                    sb.Append("{");
                    sb.Append(map[arg]);
                    sb.Append("}");

                    pos = temp - 1;
                }
                else if (pos + 1 < content.Length && (content[pos] == '\\' && content[pos + 1] == '[' || content[pos + 1] == ']'))
                {
                    // get rid of the escaped [ and ] as those are ok in regular strings
                    pos++;
                    sb.Append(content[pos]);
                }
                else
                {
                    sb.Append(content[pos]);
                }
            }

            //create the array.
            object[] argument = new object[map.Count];

            int argidx = 0;
            foreach (var item in map.Keys)
            {
                int argOrig = int.Parse(item);
                argument[argidx++] = args[argOrig];
            }

            return string.Format(sb.ToString(), argument);
        }

        public static void SetupWriter(IOutputWriter newWriter)
        {
            s_printer = newWriter;
        }
    }
}
