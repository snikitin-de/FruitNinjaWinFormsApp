using Common;
using Timer = System.Windows.Forms.Timer;

namespace FruitNinjaWinFormsApp
{
    public class FruitBall : MoveRandomBall
    {
        private int currentDestroyedTime = 0;
        private int timeToDestroy = 1200;
        private int minDiameter = 50;
        private int maxDiameter = 75;
        private int fruitStartXIndentation = 150;
        private float g = 2f;
        private Timer destroyedTimer = new Timer();

        public bool isSliced { get; set; }
        public double DestroyedAngle { get; set; }
        public int Diameter { get; set; }
        public Color Color { get; set; }

        public FruitBall(Color color, int borderX, int borderY) : base(borderX, borderY)
        {
            diameter = random.Next(minDiameter, maxDiameter);
            Diameter = diameter;

            var vx = random.Next(1, 5);
            var vy = random.Next(40, 50);

            speed.VX = vx;
            speed.VY = -(float)vy;
            centerPoint.X = random.Next(fruitStartXIndentation, borderX - fruitStartXIndentation);
            centerPoint.Y = borderY;

            this.color = color;
            Color = color;

            destroyedTimer.Tick += DestroyedTime_Tick;
            destroyedTimer.Interval = timeToDestroy;
        }

        private void DestroyedTime_Tick(object? sender, EventArgs e)
        {
            currentDestroyedTime += 100;
        }

        public void DrawSliced(Graphics graphics)
        {
            color = Color.FromArgb(255, color.R, color.G, color.B);

            var brush = new SolidBrush(color);
            var radius = diameter / 2;
            var rectangle = new Rectangle((int)centerPoint.X - radius, (int)centerPoint.Y - radius, diameter, diameter);

            graphics.FillPie(brush, rectangle, (float)DestroyedAngle, 180);  
            rectangle = new Rectangle((int)centerPoint.X - radius + 10, (int)centerPoint.Y - radius, diameter, diameter);
            graphics.FillPie(brush, rectangle, 180 + (float)DestroyedAngle, 180);

            destroyedTimer.Start();
        }

        public override void Move()
        {
            base.Move();

            speed.VY += g;
        }

        public override bool IsInsideBorders()
        {
            return (centerPoint.X > LeftSide() && centerPoint.X < RightSide()) &&
                   (centerPoint.Y > TopSide() && centerPoint.Y < BottomSide() + maxDiameter);
        }

        public PointF GetCenterPoint()
        {
            return centerPoint;
        }

        public float GetRadius()
        {
            return diameter / 2;
        }

        public bool IsDestroyed()
        {
            var isDestroyed = currentDestroyedTime >= timeToDestroy;

            if (isDestroyed)
            {
                destroyedTimer.Stop();
            }

            return isDestroyed;
        }

        public void SetMoveSlowly()
        {
            timer.Interval = 40;
        }
    }
}
