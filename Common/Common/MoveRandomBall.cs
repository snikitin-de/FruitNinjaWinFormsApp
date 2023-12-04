using Timer = System.Windows.Forms.Timer;

namespace Common
{
    public class MoveRandomBall : RandomBall
    {
        protected int moveInterval = 20;
        protected int minSpeed = 5;
        protected int maxSpeed = 10;
        protected int borderX;
        protected int borderY;
        protected Random random = new Random();
        protected Timer timer;

        public MoveRandomBall(int borderX, int borderY) : base(borderX, borderY)
        {
            this.borderX = borderX;
            this.borderY = borderY;
            timer = new Timer();
            timer.Interval = moveInterval;
            timer.Tick += Timer_Tick;

            var vx = random.Next(minSpeed, maxSpeed) * GetRandomDirection();
            var vy = random.Next(minSpeed, maxSpeed) * GetRandomDirection();

            speed.VX = vx;
            speed.VY = vy;
        }

        protected virtual void Timer_Tick(object? sender, EventArgs e)
        {
            Move();
        }

        public virtual bool IsInsideBorders()
        {
            return (centerPoint.X > LeftSide() && centerPoint.X < RightSide()) && 
                    (centerPoint.Y > TopSide() && centerPoint.Y < BottomSide());
        }

        protected int GetRandomDirection()
        {
            return random.Next(101) <= 50 ? -1 : 1;
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void UpdateBorders(int borderX, int borderY)
        {
            this.borderX = borderX;
            this.borderY = borderY;
        }

        public int LeftSide()
        {
            return diameter / 2;
        }

        public int RightSide()
        {
            return borderX - diameter / 2;
        }

        public int TopSide()
        {
            return diameter / 2;
        }

        public int BottomSide()
        {
            return borderY - diameter / 2;
        }
    }
}
