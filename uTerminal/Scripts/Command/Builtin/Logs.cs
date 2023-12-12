using uTerminal.UI;

namespace uTerminal.Logs
{
    public static class Logs
    {
        #region BuiltinCommands
        [uCommand("show", "Show unity console logs in runtime")] 
        public static void ShowLogs()
        {
            uTerminal.UI.UIManager.ShowUnityLogs = true;
        } 

        [uCommand("hide", "Hide unity console logs in runtime")]
        public static void HideLogs()
        { 
            uTerminal.UI.UIManager.ShowUnityLogs = false;
        }
        #endregion
    }
}