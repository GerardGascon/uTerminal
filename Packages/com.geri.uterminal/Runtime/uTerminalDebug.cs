using uTerminal.Graphics;
using UnityEngine;

namespace uTerminal {
	public static class uTerminalDebug {
		public enum Color {
			Blue,
			Green,
			White,
			Orange,
			Red
		}

		private static uTerminalGraphics _ui;

		/// <summary>
		/// Initializes the uTerminalDebug with the specified uTerminalGraphics.
		/// </summary>
		/// <param name="uiManager">The uTerminalGraphics instance.</param>
		public static void Init(uTerminalGraphics uiManager) {
			_ui = uiManager;
		}

		/// <summary>
		/// Logs a message with optional showDateTime and stackTrace parameters.
		/// </summary>
		/// <param name="log">The message to log.</param>
		/// <param name="showDateTime">Whether to show the log timestamp.</param>
		/// <param name="stackTrace">The stack trace associated with the log.</param>
		public static void Log(string log, bool showDateTime = false, string stackTrace = "") {
			if (_ui) _ui.ProcessText(log, showDateTime, stackTrace);
		}

		/// <summary>
		/// Logs a message with a specified color.
		/// </summary>
		/// <param name="log">The message to log.</param>
		/// <param name="color">The color of the log message.</param>
		public static void Log(string log, Color color) {
			Log($"<color=#{GetColor(color)}>" + log);
		}

		/// <summary>
		/// Logs a message with a specified color and stack trace.
		/// </summary>
		/// <param name="log">The message to log.</param>
		/// <param name="stackTrace">The stack trace associated with the log.</param>
		/// <param name="color">The color of the log message.</param>
		public static void Log(string log, string stackTrace, LogType color) {
			Log($"<color=#{GetColor(color)}>" + log, true, stackTrace);
		}

		/// <summary>
		/// Gets the hexadecimal color code corresponding to the specified uTerminalDebug.Color.
		/// </summary>
		/// <param name="color">The uTerminalDebug.Color value.</param>
		/// <returns>The hexadecimal color code.</returns>
		private static string GetColor(Color color) {
			switch (color) {
				case Color.Blue: return "00CCFF";
				case Color.Green: return "47FF00";
				case Color.White: return "FFFFFF";
				case Color.Red: return "FF0D00";
				case Color.Orange: return "FFA800";
				default: return "";
			}
		}

		/// <summary>
		/// Gets the hexadecimal color code corresponding to the specified LogType.
		/// </summary>
		/// <param name="log">The LogType value.</param>
		/// <returns>The hexadecimal color code.</returns>
		private static string GetColor(LogType log) {
			switch (log) {
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