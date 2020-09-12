namespace Tetris
{
    public class ColorHelper
    {
        public Color _defaultColor = Color.WHITE;
        public bool ColorEnabled { get; set; }

        public ColorHelper(bool colorEnabled = true)
        {
            ColorEnabled = colorEnabled;
        }

        public string ColorString(string str, Color color)
        {
            if (!ColorEnabled) return str;

            string startColor = ColorToString(color);
            string endColor = ColorToString(_defaultColor);
            return $"{startColor}{str}{endColor}";
        }

        private string ColorToString(Color color)
        {
            return color switch
            {
                Color.WHITE => "\u001b[0m",
                Color.DARKGRAY => "\u001b[40m",
                Color.RED => "\u001b[31m",
                Color.GREEN => "\u001b[32m",
                Color.ORANGE => "\u001b[33m",
                Color.BLUE => "\u001b[34m",
                Color.PURPLE => "\u001b[35m",
                Color.TEAL => "\u001b[36m",
                Color.LIGHTGRAY => "\u001b[37m",
                _ => "",
            };
        }
    }
}
