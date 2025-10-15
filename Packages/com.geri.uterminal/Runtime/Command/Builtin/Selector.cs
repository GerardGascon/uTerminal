 using System.Reflection;

namespace uTerminal
{
    /// <summary>
    /// Static class that defines built-in commands for behavior selection in uTerminal.
    /// </summary>
    public static class Selector
    {
        private static object[] Objects;
        private static object[] Parameters;
        private static MethodInfo ToInvoke;

        /// <summary>
        /// Initializes the Selector with the specified objects, parameters, and method to invoke.
        /// </summary>
        /// <param name="objects">The array of objects on which the method will be invoked.</param>
        /// <param name="parameters">The array of parameters to be passed to the invoked method.</param>
        /// <param name="toInvoke">The method to be invoked.</param>
        public static void Init(object[] objects, object[] parameters, MethodInfo toInvoke)
        {
            Objects = objects;
            Parameters = parameters;
            ToInvoke = toInvoke;
        }

        #region BuiltinCommands

        /// <summary>
        /// Selects and invokes a behavior based on the specified index.
        /// </summary>
        /// <param name="index">The index of the behavior to be selected and invoked.</param>
        [uCommand("select", "Selector of the multiple behavior")]
        public static void Select(int index)
        {
            if (Objects == null)
            {
                uTerminalDebug.Log("You cannot select an item because there is no item to select", uTerminalDebug.Color.Red);
                return;
            }

            if (index >= Objects.Length)
            {
                uTerminalDebug.Log("You need to select a valid item", uTerminalDebug.Color.Red);
                return;
            }

            ToInvoke.Invoke(Objects[index], Parameters);

            Objects = null;
            ToInvoke = null;
            Parameters = null;
        }

        /// <summary>
        /// Cancels the current selection, resetting the Selector's state.
        /// </summary>
        public static void Cancel()
        {
            Objects = null;
            ToInvoke = null;
            Parameters = null;
        }

        #endregion
    }
}
