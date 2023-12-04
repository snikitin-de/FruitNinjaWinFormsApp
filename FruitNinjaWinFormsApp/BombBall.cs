namespace FruitNinjaWinFormsApp
{
    public class BombBall : FruitBall
    {
        public BombBall(Color color, int borderX, int borderY) : base(color, borderX, borderY)
        {
            this.color = Color.FromArgb(255, 0, 0, 0); // Бомба
        }
    }
}
