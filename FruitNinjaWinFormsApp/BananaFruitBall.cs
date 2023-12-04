namespace FruitNinjaWinFormsApp
{
    public class BananaFruitBall : FruitBall
    {
        public BananaFruitBall(Color color, int borderX, int borderY) : base(color, borderX, borderY)
        {
            this.color = Color.FromArgb(255, 255, 255, 0); // Банан
        }
    }
}
