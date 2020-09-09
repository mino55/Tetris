using System.Collections.Generic;

namespace Tetris
{
    public class GameStats
    {
        public int Lines { get; private set; }
        public int Shapes { get; private set; }
        public int Score { get; private set; }
        public int Level { get; private set; }
        public int EffectLevel { get; private set; }
        public int MaxDropDelay { get; private set; }
        public int DropDelay { get; private set; }
        public int StartLevel { get; private set; }

        private readonly int _linesPerLevel;
        private readonly int _effectLevelLimit;
        private readonly int _speedIncreasePerEffectLevel;
        private readonly List<Tetriminos.Type> _history = null;

        public GameStats(int startLevel,
                         int linesPerLevel,
                         int effectLevelLimit,
                         int speedIncreasePerEffectLevel)
        {
            Lines = 0;
            Shapes = 0;
            Score = 0;
            Level = 0;
            MaxDropDelay = 1000;
            DropDelay = MaxDropDelay;
            StartLevel = startLevel;

            _linesPerLevel = linesPerLevel;
            _effectLevelLimit = effectLevelLimit;
            _speedIncreasePerEffectLevel = speedIncreasePerEffectLevel;
            _history = new List<Tetriminos.Type>();

            CalculateLevel();
            CalculateDropDelay();
        }

        public void ScoreLines(int lines) {
            Lines += lines;
            CalculateScore(lines);
            CalculateLevel();
            CalculateDropDelay();
        }

        public void RegisterTetrimino(Tetriminos.Type type) {
            Shapes += 1;
            _history.Add(type);
        }

        public Tetriminos.Type[] History()
        {
            return _history.ToArray();
        }

        private void CalculateScore(int lines){
            int score = 0;
            while(lines > 0)
            {
                if (lines >= 4)
                {
                    lines -= 4;
                    score += 800 * (Level + 1);
                    continue;
                }

                if (lines == 3)
                {
                    lines -= 3;
                    score += 500 * (Level + 1);
                    continue;
                }

                if (lines == 2)
                {
                    lines -= 2;
                    score += 300 * (Level + 1);
                    continue;
                }

                if (lines == 1)
                {
                    lines -= 1;
                    score += 100 * (Level + 1);
                    continue;
                }
            }
            Score += score;
        }

        private void CalculateLevel() {
            Level = StartLevel + (Lines / _linesPerLevel);
            if (Level <= _effectLevelLimit) EffectLevel = Level;
            else EffectLevel = _effectLevelLimit;
        }

        private void CalculateDropDelay() {
            DropDelay = MaxDropDelay - EffectLevel * _speedIncreasePerEffectLevel;
        }
    }
}
