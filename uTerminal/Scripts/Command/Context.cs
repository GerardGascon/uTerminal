using System;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace uTerminal
{
    public class Context
    {
        private object[] targets;
        private Action<object[]> method;
        private ParameterInfo[] parametersInfo;
        private bool isManuallyMethod;

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
                        Console.Log("Please select item, type \"ucommand.select\" and the item number", Console.Color.Orange);
                        int i = 0;
                        foreach (var item in targets)
                        {
                            if (item != null)
                            {
                                Console.Log(string.Format("[{0}] {1}", i, ((MonoBehaviour)item).name));
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

        public Context(Action<object[]> action)
        {
            this.method = action;
            parametersInfo = action.GetMethodInfo().GetParameters();
            isManuallyMethod = true; 
        }

        public void Execute(object[] parameters)
        { 
            var args = isManuallyMethod ? parameters : ArgumentConverter.CreateParameters(parameters, parametersInfo);

            if(args.Length != parametersInfo.Length)
            {
                Console.Log("The parameter number does not match what is required.", Console.Color.Orange); 
                return; 
            } 

            method(args);
        }

        public void AddTargets(object[] targets)
        {
            this.targets = targets;
        }
    }
}