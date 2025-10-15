using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace uTerminal.Graphics
{
    /// <summary>
    /// Class for handling uTerminal auto-completion graphics.
    /// </summary>
    public class AutoComplete : MonoBehaviour
    {
        [SerializeField] Transform _suggestionParent;
        [SerializeField] TextMeshProUGUI _autocompleteText, _autocompleteTextHighlight;
        [SerializeField] AutoCompleteText _suggestionPrefab;

        private List<AutoCompleteText> _suggestions;
        private uTerminalGraphics _uiManager;
        private bool _haveSuggestion = false;
        private string _currentSuggestion = "";

        private List<AutoCompleteText> _suggestionsList;

        private void Start()
        {
            _suggestions = new List<AutoCompleteText>();
            _uiManager = GetComponent<uTerminalGraphics>();

            _uiManager.inputCommand.onValueChanged.AddListener(AutoCompleteCheck);

            _suggestionsList = new List<AutoCompleteText>();
        }

        private void Update()
        {
            if (_haveSuggestion && InputAbstraction.TabPressed())
            {
                StartCoroutine(_uiManager.SetCurretSuggestion(_currentSuggestion));
                ClearAutoComplete();
            }
        }

        /// <summary>
        /// Checks for auto-completion based on user input.
        /// </summary>
        /// <param name="input">The user input.</param>
        public void AutoCompleteCheck(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                ClearAutoComplete();
                return;
            }
            ClearAutoComplete();

            var matchingSuggestions = Terminal.AllCommands.FindAll(s => s.infor.path.ToLower().Contains(_uiManager.inputCommand.text.ToLower().Trim()));
            var first = matchingSuggestions.FirstOrDefault();

            if(_uiManager.inputCommand.text.Contains(" "))
                ClearAutoComplete();
            else
            {
                if (matchingSuggestions.Count <= 0)
                    ShowAutoComplete(Terminal.AllCommands);
                else
                    ShowAutoComplete(matchingSuggestions);
            }
             
            if (first != null)
            {
                if (_uiManager.inputCommand.text.Trim() != first.infor.path)
                {
                    string replacement = new string(' ', _uiManager.inputCommand.text.Length);
                    string replaced = first.infor.path.Replace(_uiManager.inputCommand.text.ToLower(), "");

                    if (first.infor.path.StartsWith(_uiManager.inputCommand.text))
                        SetAutoCompleteText(replaced, replacement);

                    _haveSuggestion = true;
                    _currentSuggestion = first.infor.path;
                }
                else
                {
                    ClearAutoComplete();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_uiManager.inputCommand.text))
                    ClearAutoComplete();
            }
        }

        /// <summary>
        /// Clears auto-completion suggestions.
        /// </summary>
        public void ClearAutoComplete()
        {
            _autocompleteText.text = string.Empty;
            _autocompleteTextHighlight.text = string.Empty;

            foreach (var item in _suggestions)
            {
                item.gameObject.SetActive(false);
                _suggestionsList.Add(item);
            }

            _suggestions.Clear();
            _haveSuggestion = false;
        }

        /// <summary>
        /// Displays auto-completion suggestions based on the provided command information.
        /// </summary>
        /// <param name="infors">The list of command information.</param>
        public void ShowAutoComplete(List<TerminalCommand> infors)
        {
            foreach (var item in infors.Take(7).ToList())
            {
                AutoCompleteText temp = null;
                if (_suggestionsList.Count > 0)
                {
                    temp = _suggestionsList[0];
                    _suggestionsList.RemoveAt(0);

                    temp.gameObject.SetActive(true);
                }
                else
                    temp = Instantiate(_suggestionPrefab);

                temp.transform.SetParent(_suggestionParent, false);

                temp.button.onClick.RemoveAllListeners();
                temp.button.onClick.AddListener(() =>
                {
                    _uiManager.inputCommand.text = temp.text.text;
                    ClearAutoComplete();
                });

                temp.text.text = item.infor.path;
                _suggestions.Add(temp);
            }
        }

        /// <summary>
        /// Sets the auto-completion text for highlighting suggestions.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="replacement">The replacement text for highlighting.</param>
        public void SetAutoCompleteText(string text, string replacement)
        {
            _autocompleteText.text = replacement + "<mark=#ff8a00>" + text;
            _autocompleteTextHighlight.text = replacement + text;
        }
    }
}
