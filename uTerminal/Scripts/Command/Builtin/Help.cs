namespace uTerminal
{
    public static class Help
    {
        #region BuiltinCommands
        [uCommand("help", "Display all available commands with description")]
        public static void HelpCommands() 
        {
            foreach (var item in Terminal.allCommands)
            {
                Console.Log($" <color=#FFA800>- <b> {item.path}</color></b>, {item.description}");
            }
        }
        #endregion
    }
}