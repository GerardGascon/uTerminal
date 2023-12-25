using UnityEditor;
using UnityEngine;
 
namespace uTerminal.Editor
{
    [CustomEditor(typeof(ConsoleSettings))]
    public class ConsoleSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ConsoleSettings settings = (ConsoleSettings)target;

            GUILayout.Space(10);

            EditorGUILayout.HelpBox("A sophisticated in-game console specifically for invoking C# methods for Unity, Version " + ConsoleSettings.Version, MessageType.None);

            EditorGUILayout.LabelField(new GUIContent("Class naming"), EditorStyles.boldLabel);

            EditorGUILayout.HelpBox("Use this option to invoke the function either by specifying the class name or by omitting it.", MessageType.Info);

            settings.useNamespace = EditorGUILayout.Toggle("Use Namespace", settings.useNamespace);
            GUILayout.Space(10);

            EditorGUILayout.LabelField(new GUIContent("Other settings"), EditorStyles.boldLabel);

            settings.showStartMessage = EditorGUILayout.Toggle("Show Start Message", settings.showStartMessage);
            if (settings.showStartMessage)
            {
                GUILayout.Space(5);
                settings.startMessage = EditorGUILayout.TextArea(settings.startMessage, GUILayout.Height(100f));
                GUILayout.Space(5);
                settings.showVersion = EditorGUILayout.Toggle("Show Version", settings.showVersion);
            }
             
            settings.chatCommandPrefix = EditorGUILayout.TextField("Chat Command Prefix", settings.chatCommandPrefix);

            settings.openTerminalKey = (KeyCode)EditorGUILayout.EnumPopup("Open Terminal Key", (KeyCode)settings.openTerminalKey);  
        }
    }
}