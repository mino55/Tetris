namespace Tetris
{
  public class Block
  {
    private string _print;

    public Block()
    {
        _print = " B ";
    }

    public Block(string print)
    {
        _print = print;
    }

    public string Print()
    {
      return $"\u001b[31m{_print}\u001b[0m";
    }
  }
}