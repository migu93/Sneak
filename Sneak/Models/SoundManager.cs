using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Sneak.Models
{
    public class SoundManager
    {
        private SoundPlayer eatSound;
        private SoundPlayer gameOverSound;

        public SoundManager()
        {
            // Инициализируем звуковые файлы из ресурсов
            eatSound = new SoundPlayer(Properties.Resources.EatSound);
            gameOverSound = new SoundPlayer(Properties.Resources.GameOverSound);
        }

        public void PlayEatSound()
        {
            eatSound.Play();
        }

        public void PlayGameOverSound()
        {
            gameOverSound.Play();
        }
    }
}
