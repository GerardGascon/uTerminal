 namespace uTerminal
{
    /// <summary>
    /// Static class that defines built-in commands for providing help in uTerminal.
    /// </summary>
    public static class Help
    {
        #region BuiltinCommands

        /// <summary>
        /// Displays information about all available commands with their descriptions.
        /// </summary>
        [uCommand("help", "Display all available commands with description")]
        public static void HelpCommands()
        {
            foreach (var item in Terminal.allCommands)
            {
                uTerminalDebug.Log($" <color=#FFA800>- <b> {item.path}</color></b>, {item.description}");
            }
        }

        #endregion
    }
}
