 using System;
 using System.Linq;
 using System.Reflection;

 namespace uTerminal
{
    /// <summary>
    /// Static class that defines built-in commands for providing help in uTerminal.
    /// </summary>
    public static class Help
    {
        #region BuiltinCommands

        private static string GetFriendlyTypeName(Type type)
        {
            return type == typeof(int) ? "int"
                : type == typeof(string) ? "string"
                : type == typeof(bool) ? "bool"
                : type == typeof(float) ? "float"
                : type == typeof(double) ? "double"
                : type == typeof(object) ? "object"
                : type.Name;
        }

        /// <summary>
        /// Displays information about all available commands with their descriptions.
        /// </summary>
        [uCommand("help", "Display all available commands with description")]
        public static void HelpCommands()
        {
            foreach (TerminalCommand item in Terminal.AllCommands)
            {
                ParameterInfo[] paramTypes = item.context.parametersInfo;
                string paramList = paramTypes is { Length: > 0 }
                    ? string.Join(" ", paramTypes.Select(p => $"[{GetFriendlyTypeName(p.ParameterType)} {p.Name}]"))
                    : "";
                uTerminalDebug.Log(
                    $" <color=#FFA800>- <b> {item.infor.path} {paramList}</color></b>, {item.infor.description}");
            }
        }

        #endregion
    }
}
