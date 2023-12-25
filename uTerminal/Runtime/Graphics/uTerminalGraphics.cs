using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace uTerminal.Graphics
{
    /// <summary>
    /// Class for handling uTerminal graphics.
    /// </summary>
    public class uTerminalGraphics : MonoBehaviour
    {
        [SerializeField] Transform contentParent;
        [SerializeField] ConsoleText textPrefab;
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] ClickDetection clickDetection;
        [SerializeField] ConsoleTextOptions textOptions;
        public TMP_InputField inputCommand;

        private List<ConsoleText> _texts;
        private List<string> _lastCommands = new List<string>();
        private int _current;
        private Dictionary<string, ConsoleText> _collapseTexts;
        private ConsoleTextOptions _currentTextOptions;
        private AutoComplete _autoComplete;

        public static bool ShowUnityLogs;

        private void Start()
        {
            _collapseTexts = new Dictionary<string, ConsoleText>();
            _autoComplete = GetComponent<AutoComplete>();

            Application.logMessageReceivedThreaded += (condition, stackTrace, type) =>
            {
                if (ShowUnityLogs)
                    uTerminalDebug.Log(condition, stackTrace, type);
            };

            clickDetection.onClick += () =>
            {
                OnCancel();
                _autoComplete.ClearAutoComplete();
            };

            inputCommand.onSelect.AddListener((s) =>
            {
                OnCancel();
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!string.IsNullOrEmpty(inputCommand.text))
                {
                    Terminal.ExecuteCommand(inputCommand.text);

                    _lastCommands.Add(inputCommand.text);
                    inputCommand.text = "";
                    inputCommand.Select();
                    inputCommand.ActivateInputField();
                    _current = _lastCommands.Count;
                }
            }

            if (Input.GetMouseButtonDown(0) && _currentTextOptions)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    OnCancel();
                    _autoComplete.ClearAutoComplete();
                }
            }

            if (_lastCommands.Count > 0)
            {
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    _current--;
                    if (_current <= 0) _current = 0;
                    StartCoroutine(SetCurretSuggestion(_lastCommands[_current]));
                }

                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    _current++;
                    if (_current >= _lastCommands.Count)
                    {
                        inputCommand.text = "";
                        _current = _lastCommands.Count;
                    }
                    else
                        StartCoroutine(SetCurretSuggestion(_lastCommands[_current]));
                }
            }
        }

        /// <summary>
        /// Sets the current suggestion in the input field.
        /// </summary>
        /// <param name="value">The suggestion text.</param>
        /// <returns>IEnumerator for WaitForSeconds.</returns>
        public IEnumerator SetCurretSuggestion(string value)
        {
            inputCommand.text = value;
            yield return new WaitForEndOfFrame();
            inputCommand.MoveTextEnd(false);
        }

        /// <summary>
        /// Handles cancellation actions.
        /// </summary>
        public void OnCancel()
        {
            if (_currentTextOptions)
                _currentTextOptions.gameObject.SetActive(false);
        }

        /// <summary>
        /// Handles click events on console text.
        /// </summary>
        /// <param name="console">The clicked console text.</param>
        public void OnClick(ConsoleText console)
        {
            var parentCanvas = transform.root.GetComponent<Canvas>();

            if (_currentTextOptions == null)
                _currentTextOptions = Instantiate(textOptions, parentCanvas.transform);
            else
                _currentTextOptions.gameObject.SetActive(true);

            _currentTextOptions.copy.onClick.RemoveAllListeners();
            _currentTextOptions.delete.onClick.RemoveAllListeners();

            _currentTextOptions.copy.onClick.AddListener(() =>
            {
                console.Copy();
                OnCancel();
            });

            _currentTextOptions.delete.onClick.AddListener(() =>
            {
                console.Delete();
                OnCancel();
            });

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                Input.mousePosition, parentCanvas.worldCamera,
                out Vector2 movePos);

            _currentTextOptions.transform.position = parentCanvas.transform.TransformPoint(movePos);
        }

        /// <summary>
        /// Generates a hash for stack trace.
        /// </summary>
        /// <param name="stackTrace">The stack trace string.</param>
        /// <returns>The hash string.</returns>
        private string StackTraceHash(string stackTrace)
        {
            int hashCode = stackTrace.GetHashCode();
            return hashCode.ToString("X");
        }

        /// <summary>
        /// Processes and displays console text.
        /// </summary>
        /// <param name="textToProcess">The text to display.</param>
        /// <param name="showDatetime">Flag to show datetime.</param>
        /// <param name="stackTrace">The stack trace associated with the text.</param>
        public void ProcessText(string textToProcess, bool showDatetime = false, string stackTrace = "")
        {
            if (_texts == null) _texts = new List<ConsoleText>();
            ConsoleText temp = null;

            if (!string.IsNullOrEmpty(stackTrace))
            {
                string key = StackTraceHash(stackTrace);

                if (_collapseTexts.ContainsKey(key))
                {
                    if (_collapseTexts[key])
                    {
                        _collapseTexts[key].count++;
                        _collapseTexts[key].counter.text = _collapseTexts[key].count.ToString();
                        _collapseTexts[key].counter.transform.parent.gameObject.SetActive(true);
                        temp = _collapseTexts[key];
                    }
                    else
                    {
                        _collapseTexts.Remove(key);
                    }
                }
            }

            if (temp == null)
            {
                temp = Instantiate(textPrefab);

                if (!string.IsNullOrEmpty(stackTrace))
                {
                    string key = StackTraceHash(stackTrace);

                    if (!_collapseTexts.ContainsKey(key))
                    {
                        _collapseTexts.Add(key, temp);
                        RectTransform rect = ((RectTransform)temp.text.transform);

                        rect.anchoredPosition = new Vector2(14.5f, 0);
                        rect.SetParent(temp.counter.transform, false);

                        rect.anchorMin = new Vector2(1, .5f);
                        rect.anchorMax = new Vector2(1, .5f);
                    }
                }

                temp.button.onClick.AddListener(() =>
                {
                    _autoComplete.ClearAutoComplete();
                    OnClick(temp);
                });
            }

            temp.transform.SetParent(contentParent, false);
            string text = "[" + DateTime.Now.ToString("HH:mm:ss") + "] ";
            string completeText = text + textToProcess;
            temp.text.text = showDatetime ? completeText : textToProcess;

            _texts.Add(temp);

            scrollRect.enabled = false;
            Invoke("UpdateScrollRect", 0.1f);
        }

        private void OnEnable()
        {
            inputCommand.Select();
            inputCommand.ActivateInputField();

            scrollRect.enabled = false;
            Invoke("UpdateScrollRect", 0.1f);
        }

        private void UpdateScrollRect()
        {
            scrollRect.normalizedPosition = new Vector2(0, 0);
            scrollRect.enabled = true;
        }

        #region BuiltinCommands
        [uCommand("clear", "Clear the console window of commands and any output generated by them")]
        public void Clear()
        {
            foreach (var item in _texts)
            {
                if (item)
                    Destroy(item.gameObject);
            }

            _texts.Clear();
            _collapseTexts.Clear();
            Invoke("UpdateScrollRect", 0.1f);
        }
        #endregion
    }
}
