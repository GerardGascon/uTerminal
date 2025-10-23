using UnityEngine;

namespace uTerminal.Graphics {
	internal static class InputAbstraction {
		public static bool DownPressed() {
#if ENABLE_INPUT_SYSTEM
			if (UnityEngine.InputSystem.Keyboard.current == null)
				return false;
			return UnityEngine.InputSystem.Keyboard.current[UnityEngine.InputSystem.Key.DownArrow]
				.wasPressedThisFrame;
#else
			return Input.GetKeyUp(KeyCode.DownArrow);
#endif
		}

		public static bool UpPressed() {
#if ENABLE_INPUT_SYSTEM
			if (UnityEngine.InputSystem.Keyboard.current == null)
				return false;
			return UnityEngine.InputSystem.Keyboard.current[UnityEngine.InputSystem.Key.UpArrow]
				.wasPressedThisFrame;
#else
			return Input.GetKeyUp(KeyCode.UpArrow);
#endif
		}

		public static bool MouseClicked() {
#if ENABLE_INPUT_SYSTEM
			if (UnityEngine.InputSystem.Mouse.current == null)
				return false;
			return UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame;
#else
			return Input.GetMouseButtonDown(0);
#endif
		}

		public static bool EnterPressed() {
#if ENABLE_INPUT_SYSTEM
			if (UnityEngine.InputSystem.Keyboard.current == null)
				return false;
			return UnityEngine.InputSystem.Keyboard.current[UnityEngine.InputSystem.Key.NumpadEnter]
				.wasPressedThisFrame;
#else
			return Input.GetKeyDown(KeyCode.KeypadEnter);
#endif
		}

		public static bool TabPressed() {
#if ENABLE_INPUT_SYSTEM
			if (UnityEngine.InputSystem.Keyboard.current == null)
				return false;
			return UnityEngine.InputSystem.Keyboard.current[UnityEngine.InputSystem.Key.Tab]
				.wasPressedThisFrame;
#else
			return Input.GetKeyDown(KeyCode.Tab);
#endif
		}

		public static bool ReturnPressed() {
#if ENABLE_INPUT_SYSTEM
			if (UnityEngine.InputSystem.Keyboard.current == null)
				return false;
			return UnityEngine.InputSystem.Keyboard.current[UnityEngine.InputSystem.Key.Enter]
				.wasPressedThisFrame;
#else
			return Input.GetKeyDown(KeyCode.Return);
#endif
		}

		public static Vector3 GetMousePosition() {
#if ENABLE_INPUT_SYSTEM
			if (UnityEngine.InputSystem.Mouse.current == null)
				return Vector3.zero;
			return UnityEngine.InputSystem.Mouse.current.position.ReadValue();
#else
			return Input.mousePosition;
#endif
		}

		public static bool OpenKeyDown() {
#if ENABLE_INPUT_SYSTEM
			if (UnityEngine.InputSystem.Keyboard.current == null)
				return false;
			return UnityEngine.InputSystem.Keyboard.current[ConsoleSettings.Instance.openTerminalKey]
				.wasPressedThisFrame;
#else
			return Input.GetKeyDown(ConsoleSettings.Instance.openTerminalKey);
#endif
		}
	}
}