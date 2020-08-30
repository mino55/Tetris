namespace Tetris
{
    public interface IScreen
    {
        string Render(int dTime, string input, Engine engine);
    }
}