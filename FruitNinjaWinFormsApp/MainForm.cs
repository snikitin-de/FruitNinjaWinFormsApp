using Common;
using System.Diagnostics;
using System.Media;
using Timer = System.Windows.Forms.Timer;

namespace FruitNinjaWinFormsApp
{
    public partial class MainForm : Form
    {
        private Image gifExplodingBomb;
        private List<FruitBall> fruitBalls = new List<FruitBall>();
        private List<PointF> swordPoints = new List<PointF>();
        private Random random = new Random();
        private Timer throwFruitTimer = new Timer();
        private Timer bananaFruitTimer = new Timer();
        private Stopwatch gameStopwatch = new Stopwatch();
        private int throwFruitMinInterval = 1000;
        private int throwFruitMaxInterval = 3000;
        private int renderInterval = 1;
        private int gameScore = 0;
        private int bananaCurrentTime = 0;
        private int bananaActiveTime = 10000;
        private bool isDrawSword = false;
        private bool isActiveBanana = false;

        public MainForm()
        {
            InitializeComponent();

            gameScoreLabel.Parent = renderPictureBox;
            gameScoreLabel.BackColor = Color.Transparent;
            gameTimerLabel.Parent = renderPictureBox;
            gameTimerLabel.BackColor = Color.Transparent;

            var renderTimer = new Timer();
            renderTimer.Interval = renderInterval;
            renderTimer.Enabled = true;
            renderTimer.Tick += (s, o) => { renderPictureBox.Refresh(); };

            throwFruitTimer.Interval = throwFruitMaxInterval;
            throwFruitTimer.Tick += FruitTimer_Tick;

            bananaFruitTimer.Interval = 1000;
            bananaFruitTimer.Tick += BananaFruitTimer_Tick;
        }

        // Включение двойной буферизации для всех элементов управления
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020;
                return createParams;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Загрузка анимированного изображения GIF
            gifExplodingBomb = Properties.Resources.explosion;

            bombPictureBox.Parent = renderPictureBox;
            bombPictureBox.BackColor = Color.Transparent;
            bombPictureBox.BringToFront();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            var soundGameStartPath = Properties.Resources.gameStart;

            using (var soundPlayer = new SoundPlayer(soundGameStartPath))
            {
                soundPlayer.Play();
            }

            throwFruitTimer.Start();
            gameStopwatch.Start();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !throwFruitTimer.Enabled)
            {
                new Thread(() =>
                {
                    var soundGameStartPath = Properties.Resources.gameStart;

                    using (var soundPlayer = new SoundPlayer(soundGameStartPath))
                    {
                        soundPlayer.Play();
                    }
                }).Start();

                throwFruitTimer.Start();
                gameScoreLabel.Text = "0";
                gameScore = 0;
                gameTimerLabel.ForeColor = Color.Yellow;
                gameStopwatch.Reset();
                gameStopwatch.Start();
                bananaFruitTimer.Stop();
                bananaCurrentTime = 0;
                isActiveBanana = false;
            }
        }

        private void FruitTimer_Tick(object? sender, EventArgs e)
        {
            var fruitsCount = random.Next(5, 10);
            var borderX = renderPictureBox.Width;
            var borderY = renderPictureBox.Height;

            for (int i = 0; i < fruitsCount; i++)
            {
                var color = GameColors.GetRandomFruitColor();
                var randomNumber = random.Next(101);
                FruitBall fruitBall;

                if (randomNumber < 10)
                {
                    fruitBall = new BananaFruitBall(color, borderX, borderY);
                }
                else if (randomNumber < 10 + 15)
                {
                    fruitBall = new BombBall(color, borderX, borderY);
                }
                else
                {
                    fruitBall = new FruitBall(color, borderX, borderY);
                }

                if (isActiveBanana)
                {
                    fruitBall.SetMoveSlowly();
                    gameTimerLabel.ForeColor = Color.FromArgb(255, 200, 233, 233);
                }

                fruitBalls.Add(fruitBall);
                fruitBall.Start();
            }

            new Thread(() =>
            {
                var soundThrowFruitPath = Properties.Resources.throwFruit;

                using (var soundPlayer = new SoundPlayer(soundThrowFruitPath))
                {
                    soundPlayer.Play();
                }
            }).Start();

            throwFruitTimer.Interval = random.Next(throwFruitMinInterval, throwFruitMaxInterval);
        }

        private void BananaFruitTimer_Tick(object? sender, EventArgs e)
        {
            if (bananaCurrentTime > bananaActiveTime)
            {
                bananaFruitTimer.Stop();
                isActiveBanana = false;
                gameTimerLabel.ForeColor = Color.Yellow;
            }

            bananaCurrentTime += 1000;
        }

        private void renderPictureBox_Paint(object sender, PaintEventArgs e)
        {
            var time = TimeSpan.FromMilliseconds(gameStopwatch.ElapsedMilliseconds);
            var minutes = time.Minutes;
            var seconds = time.Seconds;
            var milliseconds = time.Milliseconds;

            gameTimerLabel.Text = $"{minutes}:{seconds}.{milliseconds}";

            for (int i = 0; i < fruitBalls.Count; i++)
            {
                var centerPoint = fruitBalls[i].GetCenterPoint();

                if (fruitBalls[i].IsInsideBorders())
                {
                    if (fruitBalls[i].isSliced)
                    {
                        if (fruitBalls[i] is BombBall)
                        {
                            var diameter = fruitBalls[i].Diameter;
                            var radius = (int)fruitBalls[i].GetRadius();

                            bombPictureBox.Size = new Size(diameter, diameter);
                            bombPictureBox.Location = new Point((int)centerPoint.X - radius, (int)centerPoint.Y - radius);
                            bombPictureBox.Visible = true;

                            gameStopwatch.Stop();
                        }
                        else
                        {
                            fruitBalls[i].DrawSliced(e.Graphics);
                        }

                        if (fruitBalls[i].IsDestroyed())
                        {
                            fruitBalls.RemoveAt(i);
                        }
                    }
                    else
                    {
                        fruitBalls[i].Draw(e.Graphics);
                    }
                }
                else
                {
                    if (fruitBalls[i] is BombBall)
                    {
                        bombPictureBox.Visible = false;
                    }

                    fruitBalls.RemoveAt(i);
                }
            }

            var pen = new Pen(Color.Yellow, 4);

            if (swordPoints.Count > 1)
            {
                e.Graphics.DrawCurve(pen, swordPoints.ToArray());
            }
        }

        private void renderPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawSword)
            {
                swordPoints.Add(e.Location);
            }
        }

        private void renderPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawSword = true;
            swordPoints.Clear();
        }

        private void renderPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawSword = false;

            new Thread(() =>
            {
                var soundSwordSwipePath = Properties.Resources.swordSwipe;

                using (var soundPlayer = new SoundPlayer(soundSwordSwipePath))
                {
                    soundPlayer.PlaySync();
                }
            }).Start();

            foreach (var fruitBall in fruitBalls)
            {
                var centerPoint = fruitBall.GetCenterPoint();
                var isSliced = false;
                var intersectionPoints = new List<PointF>();

                foreach (var point in swordPoints)
                {
                    if (fruitBall.IsPointInside(point.X, point.Y, centerPoint.X, centerPoint.Y, fruitBall.GetRadius()))
                    {
                        intersectionPoints.Add(point);
                        isSliced = true;
                    }
                }

                if (isSliced)
                {
                    var startIntersectionPoint = intersectionPoints.First();
                    var finishIntersectionPoint = intersectionPoints.Last();
                    var intersectionAngleRadians = Math.Atan2(finishIntersectionPoint.Y - startIntersectionPoint.Y, finishIntersectionPoint.X - startIntersectionPoint.X);
                    var intersectionAngleDegrees = (180 / Math.PI) * intersectionAngleRadians;

                    fruitBall.isSliced = true;
                    fruitBall.DestroyedAngle = intersectionAngleDegrees;
                    gameScore++;
                    gameScoreLabel.Text = gameScore.ToString();

                    new Thread(() =>
                    {
                        var soundCleanSlicePath = Properties.Resources.cleanSlice;

                        using (var soundPlayer = new SoundPlayer(soundCleanSlicePath))
                        {
                            soundPlayer.Play();
                        }
                    }).Start();

                    if (fruitBall is BombBall)
                    {
                        throwFruitTimer.Stop();
                        StartAnimation();

                        new Thread(() =>
                        {
                            var soundBombExplodePath = Properties.Resources.bombExplode;

                            using (var soundPlayer = new SoundPlayer(soundBombExplodePath))
                            {
                                soundPlayer.Play();
                            }
                        }).Start();

                        MessageBox.Show("Игра окончена! Вы разрезали бомбу!", "Fruit Ninja");

                        new Thread(() =>
                        {
                            var soundGameOverPath = Properties.Resources.gameOver;

                            using (var soundPlayer = new SoundPlayer(soundGameOverPath))
                            {
                                soundPlayer.Play();
                            }
                        }).Start();

                        break;
                    }

                    if (fruitBall is BananaFruitBall)
                    {
                        isActiveBanana = true;
                        bananaCurrentTime = 0;
                        bananaFruitTimer.Start();
                    }
                }
            }

            swordPoints.Clear();
        }

        private void StartAnimation()
        {
            // Остановка предыдущей анимации, если она была
            StopAnimation();

            // Установка анимированного изображения в PictureBox
            bombPictureBox.Image = gifExplodingBomb;

            // Запуск анимации
            ImageAnimator.Animate(gifExplodingBomb, OnFrameChanged);
        }

        private void StopAnimation()
        {
            // Остановка анимации
            ImageAnimator.StopAnimate(gifExplodingBomb, OnFrameChanged);
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            // Обновление PictureBox для отображения следующего кадра
            bombPictureBox.Invalidate();
        }
    }
}