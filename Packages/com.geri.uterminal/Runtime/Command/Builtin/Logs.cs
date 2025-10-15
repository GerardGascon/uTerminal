using uTerminal.Graphics;

namespace uTerminal.Logs
{
    /// <summary>
    /// Static class that defines built-in commands for managing Unity console logs in uTerminal.
    /// </summary>
    public static class Logs
    {
        #region BuiltinCommands

        /// <summary>
        /// Shows Unity console logs during runtime.
        /// </summary>
        [uCommand("show", "Show Unity console logs in runtime")]
        public static void ShowLogs()
        {
            uTerminalGraphics.ShowUnityLogs = true;
        }

        /// <summary>
        /// Hides Unity console logs during runtime.
        /// </summary>
        [uCommand("hide", "Hide Unity console logs in runtime")]
        public static void HideLogs()
        {
            uTerminalGraphics.ShowUnityLogs = false;
        }

        #endregion
    }
}
