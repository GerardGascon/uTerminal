using uTerminal.UI;

namespace uTerminal.Logs
{
    public static class Logs
    {
        #region BuiltinCommands
        [uTerminal("show", "Show unity console logs in runtime")]
        public static void ShowLogs()
        {
            UIManager.ShowUnityLogs = true;
        }

        [uTerminal("hide", "Hide unity console logs in runtime")]
        public static void HideLogs()
        {
            UIManager.ShowUnityLogs = false;
        }
        #endregion
    }
}