using System;
using System.Collections.Generic;
using System.IO;

namespace AtomCCompiler
{
    /// <summary>
    /// Coordinates the front-end workflow.
    /// Right now the workflow is intentionally small: read the file, tokenize it, then print the tokens.
    /// </summary>
    public sealed class Compiler
    {
        /// <summary>
        /// Runs the Atom C compiler front-end for a single source file.
        /// </summary>
        /// <param name="filePath">Path to the source file that should be tokenized.</param>
        /// <returns>0 on success, non-zero when an error occurs.</returns>
        public int Run(string filePath)
        {
            // A friendly file error is better than letting the runtime throw a low-level exception.
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.Error.WriteLine("error: source file path is empty.");
                return 1;
            }

            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"error: input file not found: {filePath}");
                return 1;
            }

            try
            {
                string source = File.ReadAllText(filePath);
                var lexer = new Lexer(source);
                IReadOnlyList<Token> tokens = lexer.Tokenize();

                TokenPrinter.Print(tokens);
                return 0;
            }
            catch (LexerException exception)
            {
                // All lexer diagnostics are already formatted in a compiler-friendly style.
                Console.Error.WriteLine(exception.Error.ToString());
                return 1;
            }
            catch (IOException exception)
            {
                Console.Error.WriteLine($"error: could not read source file: {exception.Message}");
                return 1;
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.Error.WriteLine($"error: cannot access source file: {exception.Message}");
                return 1;
            }
        }
    }
}
