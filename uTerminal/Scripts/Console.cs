using uTerminal.UI;
using UnityEngine;

namespace uTerminal
{
    public static class Console
    { 
        public enum Color
        {
            Blue, Green, White, Orange, Red
        }

        private static UIManager _ui;
        public static void Log(string log)
        {
            Log(log, false, "");
        }

        public static void Log(string log, bool showDataTime, string stackTrace)
        {
            if (_ui == null) _ui = GameObject.FindObjectOfType<UIManager>();
            if (_ui) _ui.ProcessText(log, showDataTime, stackTrace);  
        }

        public static void Log(string log, Color color)
        {
            Log($"<color=#{GetColor(color)}>" + log);
        }

        public static void Log(string log, string stackTrace, LogType color)
        { 
            Log($"<color=#{GetColor(color)}>" + log,  true, stackTrace);
        }

        private static string GetColor(Color color)
        {
            switch (color)
            {
                case Color.Blue: return "00CCFF";
                case Color.Green: return "47FF00";
                case Color.White: return "FFFFFF";
                case Color.Red: return "FF0D00";
                case Color.Orange: return "FFA800";
                default: return "";
            }
        }

        private static string GetColor(LogType log)
        {
            switch (log)
            {
                case LogType.Warning: return GetColor(Color.Orange);
                case LogType.Error:
                case LogType.Exception:
                    return GetColor(Color.Red);
                case LogType.Log: return GetColor(Color.White);
                default: return "";
            }
        }
    }
}