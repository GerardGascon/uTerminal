using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace uTerminal {
	public static class Terminal {
		private static Dictionary<string, TerminalCommand> _commands = new();
		public static List<CommandInfo> allCommands;

		/// <summary>
		/// Initializes the uTerminal.
		/// </summary>
		public static void Initialize() {
			allCommands = new List<CommandInfo>();
			_commands = new Dictionary<string, TerminalCommand>();

			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				SearchForAttributes(assembly);
			}

			if (ConsoleSettings.Instance.showStartMessage)
				uTerminalDebug.Log(ConsoleSettings.Instance.startMessage, uTerminalDebug.Color.Blue);

			if (ConsoleSettings.Instance.showVersion)
				uTerminalDebug.Log($"uTerminal v{ConsoleSettings.Version}");
		}

		private static void SearchForAttributes(Assembly assembly) {
			const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

			foreach (Type type in assembly.GetTypes()) {
				IEnumerable<MethodInfo> methods = type.GetMethods(flags).Where(m => Attribute.IsDefined(m, typeof(uCommandAttribute)));

				SearchForAttributes(methods, type);
			}
		}

		private static void SearchForAttributes(IEnumerable<MethodInfo> methods, Type type) {
			foreach (MethodInfo method in methods) {
				uCommandAttribute attribute = Attribute.GetCustomAttribute(method, typeof(uCommandAttribute)) as uCommandAttribute;

				string path = GetCommandPath(attribute, type);

				if (_commands.ContainsKey(path)) {
					Debug.LogWarning($"The path '{path}' already contains in the list of commands");
					uTerminalDebug.Log($"The path '{path}' already contains in the list of commands",
						uTerminalDebug.Color.Orange);
					continue;
				}

				CommandInfo commandInfo = new(path, attribute!.description);
				Context context = new(method);

				_commands.Add(path, new TerminalCommand(commandInfo, context));
				allCommands.Add(commandInfo);

				if (type.IsSubclassOf(typeof(MonoBehaviour))) {
					object[] instances = Object
						.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
						.Where(m => type.IsAssignableFrom(m.GetType())).ToArray<object>();
					context.AddTargets(instances);
				} else if (type.BaseType == null) {
					object instance = Activator.CreateInstance(type);
					context.AddTargets(new[] { instance });
				}
			}
		}

		private static string GetCommandPath(uCommandAttribute attribute, Type type) {
			string tempName = attribute.name.Trim().Split(' ')[0];

			if (!ConsoleSettings.Instance.useNamespace)
				return tempName;

			if (string.IsNullOrEmpty(type.Namespace))
				return (type.Name + "." + tempName).ToLower();
			return (type.Namespace + "." + tempName).ToLower();
		}

		/// <summary>
		/// Adds a custom command to uTerminal.
		/// </summary>
		/// <param name="path">The path of the command.</param>
		/// <param name="description">The description of the command.</param>
		/// <param name="method">The method to execute for the command.</param>
		public static void AddCommand(string path, string description, Action<object[]> method) {
			if (_commands.ContainsKey(path))
				Debug.LogError($"The path '{path}' already contains in the list of commands");
			else {
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
		private static void TryExecuteCommand(string context) {
			uTerminalDebug.Log("> " + context, uTerminalDebug.Color.Blue);

			try {
				var commands = context.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
				string currentContext = commands[0];
				commands.RemoveAt(0);

				if (!_commands.ContainsKey(currentContext))
					uTerminalDebug.Log("The command \"" + context + "\" was not found");
				else {
					_commands[currentContext].context.Execute(commands.ToArray());
				}
			}
			catch (Exception e) {
				uTerminalDebug.Log(e.Message, uTerminalDebug.Color.Red);
			}
		}

		/// <summary>
		/// Executes the specified uTerminal command.
		/// </summary>
		/// <param name="context">The uTerminal command to execute.</param>
		public static void ExecuteCommand(string context) {
			TryExecuteCommand(context);
		}

		/// <summary>
		/// Executes the specified chat-based uTerminal command.
		/// </summary>
		/// <param name="context">The chat-based uTerminal command to execute.</param>
		/// <returns>True if the command is executed, false otherwise.</returns>
		public static bool ChatExecuteCommand(string context) {
			if (context.StartsWith(ConsoleSettings.Instance.chatCommandPrefix)) {
				TryExecuteCommand(context.Replace(ConsoleSettings.Instance.chatCommandPrefix, ""));
				return true;
			}

			return false;
		}
	}
}