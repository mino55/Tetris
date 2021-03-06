using System;
using System.Collections.Generic;

namespace Tetris
{
    public class MenuSelections
    {
        private int _currentSelectionIndex = 0;
        private readonly List<string> _selections = new List<string>();
        private readonly Dictionary<string, bool> _selectionIsSetting = new Dictionary<string, bool>();
        private readonly Dictionary<string, string[]> _settingStates = new Dictionary<string, string[]>();
        private readonly Dictionary<string, int> _currentSettingStateIndex = new Dictionary<string, int>();

        public void AddPick(string name)
        {
            _selections.Add(name);
            _selectionIsSetting[name] = false;
        }

        public void AddSetting(string name, string[] states)
        {
            _selections.Add(name);
            _selectionIsSetting[name] = true;
            _currentSettingStateIndex[name] = 0;
            _settingStates[name] = states;
        }

        public string SelectedSettingCurrentState()
        {
            if (CurrentSelectionType() == Type.SETTING)
            {
                int stateIndex = _currentSettingStateIndex[CurrentSelection()];
                return _settingStates[CurrentSelection()][stateIndex];
            }

            return null;
        }

        public string CurrentSelection()
        {
            return _selections[_currentSelectionIndex];
        }

        public Type CurrentSelectionType()
        {
            if (_selectionIsSetting[CurrentSelection()]) return Type.SETTING;

            return Type.PICK;
        }

        public bool IsSelected(string name)
        {
            return _selections[_currentSelectionIndex] == name;
        }

        public string GetSettingState(string name)
        {
            int stateIndex = _currentSettingStateIndex[name];
            return _settingStates[name][stateIndex];
        }

        public void SetSettingState(string name, string state)
        {
            int stateIndex = Array.IndexOf(_settingStates[name], state);
            _currentSettingStateIndex[name] = stateIndex;
        }

        public void SelectNext()
        {
            if (_currentSelectionIndex < (_selections.Count - 1)) _currentSelectionIndex++;
            else _currentSelectionIndex = 0;
        }

        public void SelectPrevious()
        {
            if (_currentSelectionIndex > 0) _currentSelectionIndex--;
            else _currentSelectionIndex = _selections.Count - 1;
        }

        public void SelectedSettingNextState()
        {
            if (CurrentSelectionType() != Type.SETTING) return;

            string[] states = _settingStates[CurrentSelection()];
            int currentIndex = _currentSettingStateIndex[CurrentSelection()];

            if (currentIndex < (states.Length - 1)) currentIndex++;
            else currentIndex = 0;

            _currentSettingStateIndex[CurrentSelection()] = currentIndex;
        }

        public void SelectedSettingPreviousState()
        {
            if (CurrentSelectionType() != Type.SETTING) return;

            string[] states = _settingStates[CurrentSelection()];
            int currentIndex = _currentSettingStateIndex[CurrentSelection()];

            if (currentIndex > 0) currentIndex--;
            else currentIndex = states.Length - 1;

            _currentSettingStateIndex[CurrentSelection()] = currentIndex;
        }

        public enum Type
        {
            PICK,
            SETTING
        }
    }
}
