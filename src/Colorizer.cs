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

            Lexer lex = new Lexer(message);
            Token[] tokens = lex.Tokenize();

            CheckFormat(lex);

            Dictionary<string, int> argMap = CreateArgumentMap(tokens, message);

            for (int currentTokenPosition = 0; currentTokenPosition < tokens.Length; currentTokenPosition++)
            {
                Token currentToken = tokens[currentTokenPosition];

                switch (currentToken.Kind)
                {
                    case TokenKind.String:
                        {
                            // write the text
                            string content = RewriteString(argMap, lex.GetValue(currentToken), args);
                            s_printer.Write(content);
                            break;
                        }
                    case TokenKind.CloseBracket:
                        {
                            s_printer.ForegroundColor = colors.Pop();
                            break;
                        }
                    case TokenKind.OpenBracket:
                        {
                            currentTokenPosition++; // move to the next token, which should be a string token.
                            currentToken = tokens[currentTokenPosition];

                            // This case is []
                            if (currentToken.Kind == TokenKind.CloseBracket)
                            {
                                string content = RewriteString(argMap, "\\[\\]", args);
                                s_printer.Write(content);
                                currentTokenPosition++;
                                break;
                            }

                            // The token is not a close paren -- we should check and see what is the next parameter
                            Token futureToken = tokens[currentTokenPosition + 1];
                            if (futureToken.Kind == TokenKind.ColorDelimiter)
                            {
                                if (s_consoleColorMap.TryGetValue(lex.GetValue(currentToken), out ConsoleColor color))
                                {
                                    colors.Push(s_printer.ForegroundColor);
                                    s_printer.ForegroundColor = color;
                                    currentTokenPosition++;  // skip over the ! token
                                    continue;
                                }
                            }
                            else if (futureToken.Kind == TokenKind.CloseBracket)
                            {
                                // check to see if we have a matching closing bracket (this can be covered by the check up-top)
                                // the user wanted to write the actual text '[noColor]'

                                // construct an escaped string 
                                string content = RewriteString(argMap, "\\[" + lex.GetValue(currentToken) + "\\]", args);
                                s_printer.Write(content);
                                currentTokenPosition++; // skip over the close bracket token
                            }

                            break;
                        }
                }
            }
        }

        private static void CheckFormat(Lexer lex)
        {
            Token[] tokens = lex.Tokenize();

            int brackets = 0;
            // check to see if the parens are balanced.
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Kind == TokenKind.OpenBracket) brackets++;
                if (tokens[i].Kind == TokenKind.CloseBracket) brackets--;

                // To nest you need to specify a color
                if (i > 0 && tokens[i].Kind == TokenKind.OpenBracket)
                {
                    if (tokens[i - 1].Kind == tokens[i].Kind)
                    {
                        throw new FormatException($"Invalid format at position {tokens[i].Start}");
                    }
                }

                if (tokens[i].Kind == TokenKind.ColorDelimiter && i > 1)
                {
                    string colorName = lex.GetValue(tokens[i - 1]);
                    if (!s_consoleColorMap.ContainsKey(colorName))
                    {
                        throw new ArgumentException($"Unknown color: {colorName}");
                    }
                }
            }

            if (brackets != 0)
            {
                throw new FormatException("Invalid format, unbalanced paranthesis in the string");
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
                    if (content[pos + 1] == '{')
                    {
                        sb.Append('{'); sb.Append('{');
                        pos++;
                        continue;
                    }

                    int temp = pos;
                    while (content[temp++] != '}')
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

                // if the original argument index points outside of the array, we should give an error
                if (argOrig < 0 || argOrig >= args.Length)
                {
                    throw new FormatException($"Invalid string format. Parameter at position {argOrig} could not be found in the argument array.");
                }

                argument[argidx++] = args[argOrig];
            }

            return string.Format(sb.ToString(), argument);
        }

        private static Dictionary<string, int> CreateArgumentMap(Token[] tokens, string content)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();

            int argCount = 0;

            foreach (Token tok in tokens)
            {
                // Skip over tokens that can't contain replacements.
                if (tok.Kind != TokenKind.String)
                {
                    continue;
                }

                int tokenTextLength = tok.End + 1;

                for (int i = tok.Start; i < tokenTextLength; i++)
                {
                    if (content[i] != '{')
                    {
                        continue;
                    }

                    // '{' are escaped as '{{'
                    if (i + 1 < tokenTextLength && content[i + 1] == '{')
                    {
                        i++;
                        continue;
                    }

                    // find the matching closing curly bracket
                    int pos = i;
                    while (pos < tokenTextLength && content[pos++] != '}') ;

                    if (content[pos - 1] != '}') // did not find matching '}'
                        throw new ArgumentException(string.Format("Could not parse '{0}'", content));

                    string arg = content.Substring(i + 1, pos - i - 2);

                    if (!int.TryParse(arg, out int x))
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
