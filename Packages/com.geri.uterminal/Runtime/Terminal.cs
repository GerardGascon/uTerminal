using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine; 

namespace uTerminal
{
    public static class Terminal
    {
        private static Dictionary<string, TerminalCommand> _commands = new Dictionary<string, TerminalCommand>();
        public static List<CommandInfo> allCommands;

        private static bool WasInitialized = false;

        /// <summary>
        /// Initializes the uTerminal.
        /// </summary>
        public static void Initialize()
        {
            if (WasInitialized) return;

            WasInitialized = true;
            allCommands = new List<CommandInfo>();
            _commands = new Dictionary<string, TerminalCommand>();

            var flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var methods = type.GetMethods(flags).Where(m => Attribute.IsDefined(m, typeof(uCommandAttribute)));

                foreach (var method in methods)
                {
                    var attribute = Attribute.GetCustomAttribute(method, typeof(uCommandAttribute)) as uCommandAttribute;

                    string path = "";
                    string tempName = attribute.name.Trim().Split(' ')[0];

                    if (ConsoleSettings.Instance.useNamespace)
                    {
                        path = (type.Namespace + "." + tempName).ToLower();

                        if (string.IsNullOrEmpty(type.Namespace))
                            path = (type.Name + "." + tempName).ToLower();
                    }
                    else
                    {
                        path = tempName;
                    }

                    if (!_commands.ContainsKey(path))
                    {
                        CommandInfo commandInfo = new CommandInfo(path, attribute.description);
                        Context context = new Context(method);

                        _commands.Add(path, new TerminalCommand(commandInfo, context));
                        allCommands.Add(commandInfo);

                        if (type.IsSubclassOf(typeof(MonoBehaviour)))
                        {
                            MonoBehaviour[] instances = MonoBehaviour.FindObjectsOfType<MonoBehaviour>().Where(m => type.IsAssignableFrom(m.GetType())).ToArray();
                            context.AddTargets(instances);
                        }
                        else if (type.BaseType == null)
                        {
                            object instance = Activator.CreateInstance(type);
                            context.AddTargets(new object[] { instance });
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"The path '{path}' already contains in the list of commands");
                        uTerminalDebug.Log($"The path '{path}' already contains in the list of commands", uTerminalDebug.Color.Orange);
                    }
                }
            }

            if (ConsoleSettings.Instance.showStartMessage)
                uTerminalDebug.Log(ConsoleSettings.Instance.startMessage, uTerminalDebug.Color.Blue);

            if (ConsoleSettings.Instance.showVersion)
                uTerminalDebug.Log($"uTerminal v{ConsoleSettings.Version}");
        }

        /// <summary>
        /// Adds a custom command to uTerminal.
        /// </summary>
        /// <param name="path">The path of the command.</param>
        /// <param name="description">The description of the command.</param>
        /// <param name="method">The method to execute for the command.</param>
        public static void AddCommand(string path, string description, Action<object[]> method)
        {
            if (_commands.ContainsKey(path))
                Debug.LogError($"The path '{path}' already contains in the list of commands");
            else
            {
                CommandInfo commandInfo = new CommandInfo(path, description);
                Context context = new Context(method);

                TerminalCommand command = new TerminalCommand(commandInfo, context);
                _commands.Add(path, command);

                allCommands.Add(commandInfo);
            }
        }

        /// <summary>
        /// Attempts to execute the specified uTerminal command.
        /// </summary>
        /// <param name="context">The uTerminal command to execute.</param>
        private static void TryExecuteCommand(string context)
        {
            uTerminalDebug.Log("> " + context, uTerminalDebug.Color.Blue);

            try
            {
                var commands = context.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string currentContext = commands[0];
                commands.RemoveAt(0);

                if (!_commands.ContainsKey(currentContext))
                    uTerminalDebug.Log("The command \"" + context + "\" was not found");
                else
                {
                    _commands[currentContext].context.Execute(commands.ToArray());
                }
            }
            catch (Exception e)
            {
                uTerminalDebug.Log(e.Message, uTerminalDebug.Color.Red);
            }
        }

        /// <summary>
        /// Executes the specified uTerminal command.
        /// </summary>
        /// <param name="context">The uTerminal command to execute.</param>
        public static void ExecuteCommand(string context)
        {
            TryExecuteCommand(context);
        }

        /// <summary>
        /// Executes the specified chat-based uTerminal command.
        /// </summary>
        /// <param name="context">The chat-based uTerminal command to execute.</param>
        /// <returns>True if the command is executed, false otherwise.</returns>
        public static bool ChatExecuteCommand(string context)
        {
            if (context.StartsWith(ConsoleSettings.Instance.chatCommandPrefix))
            {
                TryExecuteCommand(context.Replace(ConsoleSettings.Instance.chatCommandPrefix, ""));
                return true;
            }

            return false;
        }
    }
}
