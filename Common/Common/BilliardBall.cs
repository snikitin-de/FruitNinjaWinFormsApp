namespace Common
{
    public class BilliardBall : MoveRandomPointBall
    {
        private int ballSize = 50;
        public event EventHandler<HitEventArgs> OnHitted;

        public BilliardBall(int borderX, int borderY) : base(borderX, borderY)
        {
            diameter = ballSize;
        }

        public override void Move()
        {
            base.Move();

            if (centerPoint.X + speed.VX < LeftSide())
            {
                speed.VX = -speed.VX;
                OnHitted.Invoke(this, new HitEventArgs(Side.Left));
            }
            else if (centerPoint.X + speed.VX > RightSide())
            {
                speed.VX = -speed.VX;
                OnHitted.Invoke(this, new HitEventArgs(Side.Right));
            }
            else if (centerPoint.Y + speed.VY < TopSide())
            {
                speed.VY = -speed.VY;
                OnHitted.Invoke(this, new HitEventArgs(Side.Top));
            }
            else if (centerPoint.Y + speed.VY > BottomSide())
            {
                speed.VY = -speed.VY;
                OnHitted.Invoke(this, new HitEventArgs(Side.Bottom));
            }
        }
    }
}
