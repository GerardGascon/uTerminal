using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using uTerminal.UI;
using UnityEngine;
using UnityEngine.UI;

namespace uTerminal
{
    public class AutoComplete : MonoBehaviour
    {
        [SerializeField] Transform _suggestionParent;
        [SerializeField] TextMeshProUGUI _autocompleteText, _autocompleteTextHighlight;
        [SerializeField] AutoCompleteText _suggestionPrefab;

        private List<AutoCompleteText> _suggestions;
        private uTerminal.UI.UIManager _uiManager;
        private bool _haveSuggestion = false;
        private string _currentSuggestion = "";

        private List<AutoCompleteText> _suggestionsList;
        private void Start()
        {
            _suggestions = new List<AutoCompleteText>();
            _uiManager = GetComponent<uTerminal.UI.UIManager>();

            _uiManager.inputCommand.onValueChanged.AddListener(AutoCompleteCheck);

            _suggestionsList = new List<AutoCompleteText>();
        }

        private void Update()
        {
            if (_haveSuggestion && Input.GetKeyDown(KeyCode.Tab))
            {
                StartCoroutine(_uiManager.SetCurretSuggestion(_currentSuggestion));
                ClearAutoComplete();
            }
        }

        public void AutoCompleteCheck(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                ClearAutoComplete();
                return;
            }
            ClearAutoComplete();

            var matchingSuggestions = Terminal.allCommands.FindAll(s => s.path.ToLower().Contains(_uiManager.inputCommand.text.ToLower().Trim()));
            var first = matchingSuggestions.FirstOrDefault();

            if (matchingSuggestions.Count <= 0)
                ShowAutoComplete(Terminal.allCommands);
            else
                ShowAutoComplete(matchingSuggestions);

            if (first != null)
            {
                if (_uiManager.inputCommand.text.Trim() != first.path)
                {
                    string replacement = new string(' ', _uiManager.inputCommand.text.Length);
                    string replaced = first.path.Replace(_uiManager.inputCommand.text.ToLower(), "");

                    if (first.path.StartsWith(_uiManager.inputCommand.text))
                        SetAutoCompleteText(replaced, replacement);

                    _haveSuggestion = true;
                    _currentSuggestion = first.path;
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

        public void ShowAutoComplete(List<CommandInfor> infors)
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

                temp.text.text = item.path;
                _suggestions.Add(temp);
            }
        }

        public void SetAutoCompleteText(string text, string replacement)
        {
            _autocompleteText.text = replacement + "<mark=#ff8a00>" + text;
            _autocompleteTextHighlight.text = replacement + text;
        }
    }
}
