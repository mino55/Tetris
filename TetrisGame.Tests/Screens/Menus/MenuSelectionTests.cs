using Xunit;

namespace Tetris
{
    public class MenuSelectionTests
    {
        private readonly MenuSelections _menuSelections;

        public MenuSelectionTests()
        {
            _menuSelections = new MenuSelections();
        }

        [Fact]
        public void AddPick_NoPick_AddsPick()
        {
            _menuSelections.AddPick("My Pick");

            string selection = _menuSelections.CurrentSelection();
            MenuSelections.Type selectionType = _menuSelections.CurrentSelectionType();

            Assert.Equal("My Pick", selection);
            Assert.Equal(MenuSelections.Type.PICK, selectionType);
        }

        [Fact]
        public void AddSetting_NoSetting_AddsSetting()
        {
            _menuSelections.AddSetting("My Setting", new string[] { "state A", "state B" });

            string selection = _menuSelections.CurrentSelection();
            string setting = _menuSelections.SelectedSettingCurrentState();
            MenuSelections.Type selectionType = _menuSelections.CurrentSelectionType();

            Assert.Equal("My Setting", selection);
            Assert.Equal("state A", setting);
            Assert.Equal(MenuSelections.Type.SETTING, selectionType);
        }

        [Theory]
        [InlineData(0, "Pick 1")]
        [InlineData(1, "Pick 2")]
        [InlineData(2, "Pick 3")]
        [InlineData(3, "Pick 1")]
        public void SelectNext_WithPicks_CyclesThroughPicks(int times, string expected)
        {
            _menuSelections.AddPick("Pick 1");
            _menuSelections.AddPick("Pick 2");
            _menuSelections.AddPick("Pick 3");

            for (int i = 0; i < times; i++) { _menuSelections.SelectNext(); }

            Assert.Equal(expected, _menuSelections.CurrentSelection());
        }

        [Theory]
        [InlineData(0, "Pick 1")]
        [InlineData(1, "Pick 3")]
        [InlineData(2, "Pick 2")]
        [InlineData(3, "Pick 1")]
        public void SelectPrevious_WithPicks_CyclesThroughPicksBackwards(int times, string expected)
        {
            _menuSelections.AddPick("Pick 1");
            _menuSelections.AddPick("Pick 2");
            _menuSelections.AddPick("Pick 3");

            for (int i = 0; i < times; i++) { _menuSelections.SelectPrevious(); }

            Assert.Equal(expected, _menuSelections.CurrentSelection());
        }

        [Theory]
        [InlineData(0, "Setting 1")]
        [InlineData(1, "Setting 2")]
        [InlineData(2, "Setting 3")]
        [InlineData(3, "Setting 1")]
        public void SelectNext_WithSettings_CyclesThroughSettings(int times, string expected)
        {
            _menuSelections.AddSetting("Setting 1", new string[] { "state A", "state B" });
            _menuSelections.AddSetting("Setting 2", new string[] { "state A", "state B" });
            _menuSelections.AddSetting("Setting 3", new string[] { "state A", "state B" });

            for (int i = 0; i < times; i++) { _menuSelections.SelectNext(); }

            Assert.Equal(expected, _menuSelections.CurrentSelection());
        }

        [Theory]
        [InlineData(0, "Setting 1")]
        [InlineData(1, "Setting 3")]
        [InlineData(2, "Setting 2")]
        [InlineData(3, "Setting 1")]
        public void SelectPrevious_WithSettings_CyclesThroughSettingsBackwards(int times, string expected)
        {
            _menuSelections.AddSetting("Setting 1", new string[] { "state A", "state B" });
            _menuSelections.AddSetting("Setting 2", new string[] { "state A", "state B" });
            _menuSelections.AddSetting("Setting 3", new string[] { "state A", "state B" });

            for (int i = 0; i < times; i++) { _menuSelections.SelectPrevious(); }

            Assert.Equal(expected, _menuSelections.CurrentSelection());
        }

        [Theory]
        [InlineData(0, "Setting 1")]
        [InlineData(1, "Pick 2")]
        [InlineData(2, "Setting 3")]
        [InlineData(3, "Setting 1")]
        public void SelectNext_WithBoth_CyclesThroughBoth(int times, string expected)
        {
            _menuSelections.AddSetting("Setting 1", new string[] { "state A", "state B" });
            _menuSelections.AddPick("Pick 2");
            _menuSelections.AddSetting("Setting 3", new string[] { "state A", "state B" });

            for (int i = 0; i < times; i++) { _menuSelections.SelectNext(); }

            Assert.Equal(expected, _menuSelections.CurrentSelection());
        }

        [Theory]
        [InlineData(0, "Pick 1")]
        [InlineData(1, "Pick 3")]
        [InlineData(2, "Setting 2")]
        [InlineData(3, "Pick 1")]
        public void SelectPrevious_WithBoth_CyclesThroughBothBackwards(int times, string expected)
        {
            _menuSelections.AddPick("Pick 1");
            _menuSelections.AddSetting("Setting 2", new string[] { "state A", "state B" });
            _menuSelections.AddPick("Pick 3");

            for (int i = 0; i < times; i++) { _menuSelections.SelectPrevious(); }

            Assert.Equal(expected, _menuSelections.CurrentSelection());
        }

        [Theory]
        [InlineData(0, "state A")]
        [InlineData(1, "state B")]
        [InlineData(2, "state C")]
        [InlineData(3, "state A")]
        public void SelectSettingNextState_CyclesThroughSettingStates(int times, string expected)
        {
            _menuSelections.AddSetting("My setting",
                                       new string[] { "state A", "state B", "state C" });

            for (int i = 0; i < times; i++) { _menuSelections.SelectedSettingNextState(); }

            Assert.Equal(expected, _menuSelections.SelectedSettingCurrentState());
        }

        [Theory]
        [InlineData(0, "state A")]
        [InlineData(1, "state C")]
        [InlineData(2, "state B")]
        [InlineData(3, "state A")]
        public void SelectSettingPreviousState_CyclesThroughSettingStatesBackwards(int times, string expected)
        {
            _menuSelections.AddSetting("My setting",
                                       new string[] { "state A", "state B", "state C" });

            for (int i = 0; i < times; i++) { _menuSelections.SelectedSettingPreviousState(); }

            Assert.Equal(expected, _menuSelections.SelectedSettingCurrentState());
        }
    }
}
