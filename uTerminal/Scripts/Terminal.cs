using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using uTerminal;
using UnityEngine;
using Console = uTerminal.Console;
using Unity.VisualScripting;

public static class Terminal
{
    private static Dictionary<string, TerminalCommand> _commands = new Dictionary<string, TerminalCommand>();
    public static List<CommandInfor> allCommands;

    private static bool WasInitialized = false;
    public static void Initialize()
    {
        if (WasInitialized) return;

        WasInitialized = true;
        allCommands = new List<CommandInfor>();
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
                    CommandInfor commandInfo = new CommandInfor(path, attribute.name, attribute.description);
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
                    Console.Log($"The path '{path}' already contains in the list of commands", Console.Color.Orange);
                }
            }
        }

        if (ConsoleSettings.Instance.showStartMessage)
            Console.Log(ConsoleSettings.Instance.startMessage + ConsoleSettings.Version, Console.Color.Blue);

        if (ConsoleSettings.Instance.showVersion)
            Console.Log("Version " + ConsoleSettings.Version);
    }

    public static void AddCommand(string name, string path, string description, Action<object[]> method)
    {
        if (_commands.ContainsKey(path))
            Debug.LogError($"The path '{path}' already contains in the list of commands");
        else
        {
            CommandInfor commandInfo = new CommandInfor(path, name, description);
            Context context = new Context(method);

            TerminalCommand command = new TerminalCommand(commandInfo, context);
            _commands.Add(path, command);

            allCommands.Add(commandInfo);
        }
    }

    private static void TryExecuteCommand(string context)
    {
        Console.Log("> " + context, Console.Color.Blue);

        try
        {
            var commands = context.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            string currentContext = commands[0];
            commands.RemoveAt(0);

            if (!_commands.ContainsKey(currentContext))
                Console.Log("The command \"" + context + "\" was not found");
            else
            {
                _commands[currentContext].context.Execute(commands.ToArray());
            }
        }
        catch (Exception e)
        {
            Console.Log(e.Message, Console.Color.Red);
        }
    }

    public static void ExecuteCommand(string context)
    {
        TryExecuteCommand(context);
    }

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