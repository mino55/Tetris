namespace Tetris
{
    class ColorHelper
    {
        public Color defaultColor = Color.WHITE;

        public string ColorString(string str, Color color)
        {
            string startColor = ColorToString(color);
            string endColor = ColorToString(defaultColor);
            return $"{startColor}{str}{endColor}";
        }

        private string ColorToString(Color color)
        {
            switch(color)
            {
            case Color.WHITE:
                return "\u001b[0m";
            case Color.DARKGRAY:
                return "\u001b[40m";
            case Color.RED:
                return "\u001b[31m";
            case Color.GREEN:
                return "\u001b[32m";
            case Color.ORANGE:
                return "\u001b[33m";
            case Color.BLUE:
                return "\u001b[34m";
            case Color.PURPLE:
                return "\u001b[35m";
            case Color.TEAL:
                return "\u001b[36m";
            case Color.LIGHTGRAY:
                return "\u001b[37m";
            default:
                return "";
            }
        }
    }
}