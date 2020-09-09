namespace Tetris
{
    public class GameSettings
    {
        public int FPS { get; set; }
        public string Controlls { get; set; }

        public GameSettings()
        {
            Controlls = "simple ";
            FPS = 60;
        }
    }
}
