namespace AtomCCompiler
{
    /// <summary>
    /// Represents one compiler diagnostic message.
    /// This object keeps formatting logic in one place so every error looks the same.
    /// </summary>
    public sealed class CompilerError
    {
        /// <summary>
        /// Creates a new compiler diagnostic.
        /// </summary>
        /// <param name="position">Source position where the error was detected.</param>
        /// <param name="message">Human-readable explanation of the problem.</param>
        public CompilerError(SourcePosition position, string message)
        {
            Position = position;
            Message = message;
        }

        /// <summary>
        /// Gets the source position where the error happened.
        /// </summary>
        public SourcePosition Position { get; }

        /// <summary>
        /// Gets the human-readable error text.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Formats the error in a style that is easy to read in a terminal.
        /// </summary>
        /// <returns>A user-friendly diagnostic string.</returns>
        public override string ToString()
        {
            return $"error on line {Position.Line}, column {Position.Column}: {Message}";
        }
    }
}
