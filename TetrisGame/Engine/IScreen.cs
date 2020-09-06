namespace Tetris
{
    public interface IScreen
    {
        void Mount(Engine engine);

        void Input(string input, int dTime);

        string Render();

        void Unmount(Engine engine);
    }
}