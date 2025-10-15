using System;
using UnityEngine;
using System.Reflection; 

namespace uTerminal
{
    /// <summary>
    /// Class for managing the execution of uTerminal commands.
    /// </summary>
    public class Context
    {
        private object[] targets;
        private Action<object[]> method;
        private ParameterInfo[] parametersInfo;
        private bool isManuallyMethod;

        /// <summary>
        /// Initializes a new instance of the Context class with the specified method information.
        /// </summary>
        /// <param name="method">The method information.</param>
        public Context(MethodInfo method)
        {
            this.method = (args) =>
            {
                if (targets == null)
                    method.Invoke(null, args);
                else
                {
                    if (targets.Length > 1)
                    {
                        if (ConsoleSettings.Instance.useNamespace)
                            uTerminalDebug.Log("Please select an item, type \"uterminal.select\" and the item number", uTerminalDebug.Color.Orange);
                        else
                            uTerminalDebug.Log("Please select an item, type \"select\" and the item number", uTerminalDebug.Color.Orange);
                        int i = 0;
                        foreach (var item in targets)
                        {
                            if (item != null)
                            {
                                uTerminalDebug.Log(string.Format("[{0}] {1}", i, ((MonoBehaviour)item).name));
                                i++;
                            }
                        }

                        Selector.Init(targets, args, method);
                    }
                    else
                    {
                        method.Invoke(targets[0], args);
                    }
                }
            };

            parametersInfo = method.GetParameters();
        }

        /// <summary>
        /// Initializes a new instance of the Context class with the specified action.
        /// </summary>
        /// <param name="action">The action to be executed.</param>
        public Context(Action<object[]> action)
        {
            this.method = action;
            parametersInfo = action.GetMethodInfo().GetParameters();
            isManuallyMethod = true;
        }

        /// <summary>
        /// Executes the command with the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters to be passed to the command.</param>
        public void Execute(object[] parameters)
        {
            var args = isManuallyMethod ? parameters : ArgumentConverter.CreateParameters(parameters, parametersInfo);

            if (args.Length != parametersInfo.Length)
            {
                uTerminalDebug.Log("The parameter number does not match what is required.", uTerminalDebug.Color.Orange);
                return;
            } 

            method(args);
        }

        /// <summary>
        /// Adds targets to the context for methods that operate on multiple objects.
        /// </summary>
        /// <param name="targets">The array of target objects.</param>
        public void AddTargets(object[] targets)
        {
            this.targets = targets;
        }
    }
}
