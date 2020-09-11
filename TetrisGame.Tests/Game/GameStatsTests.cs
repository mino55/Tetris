using Xunit;

namespace Tetris.Tests
{
    public class GameStatsTests
    {
        private GameStats _gameStats;

        private GameStats GameStatsAtLevel(int level)
        {
            return new GameStats(startLevel: level,
                                 linesPerLevel: 10,
                                 effectLevelLimit: 10,
                                 speedIncreasePerEffectLevel: 90);
        }

        [Theory]
        [InlineData(0, 1000)]
        [InlineData(1, (1000 - 1 * 90))]
        [InlineData(4, (1000 - 4 * 90))]
        [InlineData(9, (1000 - 9 * 90))]
        [InlineData(12, (1000 - 10 * 90))]
        public void GameStatsConstructor_AtAnyLevel_CalculateDropDelay(int level, int dropDelay)
        {
            _gameStats = GameStatsAtLevel(level);

            Assert.Equal(dropDelay, _gameStats.DropDelay);
        }

        [Fact]
        public void ScoreLines_AddToLines()
        {
            _gameStats = GameStatsAtLevel(0);

            _gameStats.ScoreLines(1);

            Assert.Equal(1, _gameStats.Lines);
        }

        [Theory]
        [InlineData(1, 0, 100)]
        [InlineData(6, 0, (800 + 300))]
        [InlineData(5, 2, (800 * 3) + (100 * 3))]
        [InlineData(6, 3, (800 * 4) + (300 * 4))]
        [InlineData(11, 4, (800 * 2 * 5) + (500 * 5))]
        [InlineData(8, 12, (800 * 2 * 13))]
        [InlineData(2, 20, (300 * 21))]
        public void ScoreLines_ScoreGainedScalesWithLevels(int lines, int level, int score)
        {
            _gameStats = GameStatsAtLevel(level);

            _gameStats.ScoreLines(lines);

            Assert.Equal(score, _gameStats.Score);
        }

        [Fact]
        public void ScoreLines_AtLevelThreshold_UpTheLevel()
        {
            _gameStats = GameStatsAtLevel(0);
            _gameStats.ScoreLines(9);
            int levelBefore = _gameStats.Level;

            _gameStats.ScoreLines(1);
            int levelAfter = _gameStats.Level;

            Assert.Equal(levelAfter, levelBefore + 1);
        }

        [Fact]
        public void ScoreLines_BelowLevelThreshold_LevelRemainsSame()
        {
            _gameStats = GameStatsAtLevel(0);

            _gameStats.ScoreLines(10);

            Assert.Equal(1, _gameStats.Level);
        }

        [Fact]
        public void ScoreLines_PastLevelThreshold_SpeedIncreases()
        {
            _gameStats = GameStatsAtLevel(0);
            int dropDelayBefore = _gameStats.DropDelay;

            _gameStats.ScoreLines(10);
            int dropDelayAfter = _gameStats.DropDelay;

            Assert.Equal(dropDelayAfter, dropDelayBefore - 90);
        }

        [Fact]
        public void ScoreLines_BelowLevelThreshold_SpeedRemainsSame()
        {
            _gameStats = GameStatsAtLevel(0);
            int dropDelayBefore = _gameStats.DropDelay;

            _gameStats.ScoreLines(9);
            int dropDelayAfter = _gameStats.DropDelay;

            Assert.Equal(dropDelayAfter, dropDelayBefore);
        }

        [Fact]
        public void ScoreLines_LevelAboveEffectLevelLimit_UpTheLevel()
        {
            _gameStats = GameStatsAtLevel(0);

            _gameStats.ScoreLines(9);

            Assert.Equal(0, _gameStats.Level);
        }

        [Fact]
        public void ScoreLines_LevelAboveEffectLevelLimit_SpeedRemainsSame()
        {
            _gameStats = GameStatsAtLevel(10);
            int dropDelayBefore = _gameStats.DropDelay;

            _gameStats.ScoreLines(10);
            int dropDelayAfter = _gameStats.DropDelay;

            Assert.Equal(dropDelayAfter, dropDelayBefore);
        }

        [Fact]
        public void RegisterTetrimino_IncrementShapes()
        {
            _gameStats = GameStatsAtLevel(0);

            _gameStats.RegisterTetrimino(new Tetriminos.ShapeS().Type());

            Assert.Equal(1, _gameStats.Shapes);
        }

        [Fact]
        public void RegisterTetrimino_AddTetriminoTypeToHistory()
        {
            _gameStats = GameStatsAtLevel(0);

            _gameStats.RegisterTetrimino(new Tetriminos.ShapeS().Type());

            Assert.Single(_gameStats.History());
            Assert.Equal(Tetriminos.Type.SHAPE_S, _gameStats.History()[0]);
        }
    }
}
