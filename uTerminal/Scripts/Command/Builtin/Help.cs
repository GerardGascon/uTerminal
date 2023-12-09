namespace uTerminal
{
    public static class Help
    {
        #region BuiltinCommands
        [uTerminal("help", "Display all available commands with description")]
        public static void HelpCommands() 
        {
            foreach (var item in Terminal.allCommands)
            {
                Console.Log(" - <b>" + item.name + ", path: " + item.path + "</b>, " + item.description);
            }
        }
        #endregion
    }
}