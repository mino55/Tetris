namespace Tetris
{
    public class GameSettings
    {
        public int FPS { get; set; }
        public string Controlls { get; set; }
        public string Color { get; set; }
        public string Unicode { get; set; }

        public GameSettings()
        {
            Controlls = "simple ";
            FPS = 60;
            Color = "none";
            Unicode = "limited";
        }
    }
}
