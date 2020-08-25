namespace Tetris
{
    public interface IScreen
    {
        void Render(int dTime, string input, Engine engine);
    }
}