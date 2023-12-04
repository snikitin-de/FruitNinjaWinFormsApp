namespace Common
{
    public class GameColors
    {
        public static Color GameBackground = Color.FromArgb(255, 250, 248, 239);
        public static Color ButtonStartBackground = Color.FromArgb(255, 0, 255, 127);
        public static Color ButtonStopBackground = Color.FromArgb(255, 255, 57, 0);
        public static Color FontColor = Color.FromArgb(255, 0, 178, 88);
        public static Color DefaultColorBall = Color.FromArgb(255, 205, 92, 92);
        public static Color FireworkNightSky = Color.FromArgb(255, 9, 7, 29);

        private static List<Color> ballsColor = new List<Color>() {
            Color.FromArgb(255, 205, 92, 92), // IndianRed
            Color.FromArgb(255, 60, 179, 113), // MediumSeaGreen
            Color.FromArgb(255, 233, 150, 122), // DarkSalmon
            Color.FromArgb(255, 30, 144, 255), // DodgerBlue
            Color.FromArgb(255, 255, 215, 0), // Gold
            Color.FromArgb(255, 204, 125, 255) // Heliotrope
        };

        private static List<Color> fruitColors = new List<Color>() {
            Color.FromArgb(255, 250, 160, 0), // Апельсин
            Color.FromArgb(255, 50, 205, 50), // Лайм
            Color.FromArgb(255, 231, 24, 66), // Клубника
            Color.FromArgb(255, 241, 236, 224), // Кокос
            Color.FromArgb(255, 230, 174, 37) // Ананас
        };

        public static Color GetRandomBallColor()
        {
            var random = new Random(); 
            var randomColorIndex = random.Next(0, ballsColor.Count);

            return ballsColor[randomColorIndex];
        }

        public static Color GetRandomFruitColor()
        {
            var random = new Random();
            var randomColorIndex = random.Next(0, fruitColors.Count);

            return fruitColors[randomColorIndex];
        }
    }
}
