namespace uTerminal
{
    /// <summary>
    /// Class for storing information about a uTerminal command and its execution context.
    /// </summary>
    public class TerminalCommand
    {
        /// <summary>
        /// The command information.
        /// </summary>
        public readonly CommandInfo infor;

        /// <summary>
        /// The execution context of the command.
        /// </summary>
        public readonly Context context;

        /// <summary>
        /// Initializes a new instance of the TerminalCommand class with the specified command information and context.
        /// </summary>
        /// <param name="infor">The command information.</param>
        /// <param name="context">The execution context of the command.</param>
        public TerminalCommand(CommandInfo infor, Context context)
        {
            this.infor = infor;
            this.context = context;
        }
    }
}
