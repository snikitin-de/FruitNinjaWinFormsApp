namespace Common
{
    public class Ball
    {
        protected Color color = GameColors.DefaultColorBall;
        protected PointF centerPoint = new PointF(0f, 0f);
        protected Speed speed = new Speed(10, 10);
        protected int diameter = 50;

        public Ball()
        {
        }

        public void Draw(Graphics graphics)
        {
            var brush = new SolidBrush(color);
            var radius = diameter / 2;
            var rectangle = new RectangleF(centerPoint.X - radius, centerPoint.Y - radius, diameter, diameter);

            graphics.FillEllipse(brush, rectangle);
        }

        public virtual void Move()
        {
            centerPoint.X += speed.VX;
            centerPoint.Y += speed.VY;
        }

        public bool IsPointInside(float x, float y, float x0, float y0, float r)
        {
            // Формула определения находится ли указанная точка в окружности
            return Math.Sqrt(Math.Pow(x - x0, 2) + Math.Pow(y - y0, 2)) <= r;
        }
    }
}
