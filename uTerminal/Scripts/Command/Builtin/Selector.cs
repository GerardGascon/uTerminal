using System.Diagnostics;
using System.Reflection; 

namespace uTerminal
{
    public static class Selector
    {
        private static object[] Objects;
        private static object[] Parameters;
        private static MethodInfo ToInvoke;

        public static void Init(object[] objects, object[] parameters, MethodInfo toInvoke)
        {
            Objects = objects;
            Parameters = parameters;
            ToInvoke = toInvoke;
        }

        #region BuiltinCommands
        [uCommand("select", "Selector of the multiple behaviour")]
        public static void Select(int index)
        {  
            if (Objects == null)
            {
                Console.Log("You cannot select an item because there is no item to select", Console.Color.Red);
                return;
            }

            if (index > Objects.Length)
            {
                Console.Log("You need to select a valid item", Console.Color.Red);
                return;
            }

            ToInvoke.Invoke(Objects[index], Parameters);

            Objects = null;
            ToInvoke = null;
            Parameters = null;
        } 

        public static void Cancel()
        {
            Objects = null;
            ToInvoke = null;
            Parameters = null;
        }
        #endregion
    }
}