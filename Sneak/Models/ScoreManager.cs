using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneak.Models
{
    public class ScoreManager
    {
        public int CurrentScore { get; private set; }
        public int HighScore { get; private set; }
        private string highScoreFilePath = "highscore.txt";

        public ScoreManager()
        {
            CurrentScore = 0;
            LoadHighScore();
        }

        public void AddScore(int points)
        {
            CurrentScore += points;
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                SaveHighScore();
            }
        }

        private void LoadHighScore()
        {
            if (File.Exists(highScoreFilePath))
            {
                string highScoreText = File.ReadAllText(highScoreFilePath);
                int.TryParse(highScoreText, out int loadedHighScore);
                HighScore = loadedHighScore;
            }
            else
            {
                HighScore = 0;
            }
        }

        private void SaveHighScore()
        {
            File.WriteAllText(highScoreFilePath, HighScore.ToString());
        }

        public void ResetScore()
        {
            CurrentScore = 0;
        }
    }
}
