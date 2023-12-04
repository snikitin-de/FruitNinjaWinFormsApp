namespace Common
{
    public class MoveRandomPointBall : MoveRandomBall
    {
        private bool isCaught = false;

        public MoveRandomPointBall(int borderX, int borderY) : base(borderX, borderY)
        {
            var minSpeed = -10;
            var maxSpeed = 10;
            var vx = random.Next(minSpeed, maxSpeed) * GetRandomDirection();
            var vy = random.Next(minSpeed, maxSpeed) * GetRandomDirection();

            speed.VX = vx == 0 ? maxSpeed : vx;
            speed.VY = vy == 0 ? maxSpeed : vy;

            this.borderX = borderX;
            this.borderY = borderY;
        }

        public override void Move()
        {
            base.Move();
            
            if (centerPoint.Y < TopSide() || centerPoint.Y > BottomSide())
            {
                speed.VY = -speed.VY;
            }
            else if (centerPoint.X < LeftSide() || centerPoint.X > RightSide())
            {
                speed.VX = -speed.VX;
            }
        }

        public bool IsMouseCaught(int mouseX, int mouseY)
        {
            var radius = diameter / 2;
            var x0 = centerPoint.X;
            var y0 = centerPoint.Y;

            if (!isCaught &&
                IsPointInside(mouseX, mouseY, x0, y0, radius))
            {
                isCaught = true;
                return true;
            }

            return false;
        }
    }
}
