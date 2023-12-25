namespace uTerminal
{
    public class CommandInfo
    {
        /// <summary>
        /// The path of the uTerminal command.
        /// </summary>
        public readonly string path;

        /// <summary>
        /// The description of the uTerminal command.
        /// </summary>
        public readonly string description;

        /// <summary>
        /// Initializes a new instance of the CommandInfo class with the specified path and description.
        /// </summary>
        /// <param name="path">The path of the uTerminal command.</param>
        /// <param name="description">The description of the uTerminal command.</param>
        public CommandInfo(string path, string description)
        {
            this.path = path;
            this.description = description;
        }
    }
}
