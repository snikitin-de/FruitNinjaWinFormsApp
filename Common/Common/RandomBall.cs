namespace Common
{
    public class RandomBall : Ball
    {
        private int ballMinSize = 20;
        private int ballMaxSize = 70;
        private Random random = new Random();

        public RandomBall(int borderX, int borderY) : base()
        {
            var x = random.Next(ballMaxSize, borderX - ballMaxSize);
            var y = random.Next(ballMaxSize, borderY - ballMaxSize);

            color = GameColors.GetRandomBallColor();
            diameter = random.Next(ballMinSize, ballMaxSize);
            centerPoint.X = x;
            centerPoint.Y = y;
        }
    }
}
