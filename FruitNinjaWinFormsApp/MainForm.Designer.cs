namespace FruitNinjaWinFormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            renderPictureBox = new PictureBox();
            gameScoreLabel = new Label();
            bombPictureBox = new PictureBox();
            gameTimerLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)renderPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bombPictureBox).BeginInit();
            SuspendLayout();
            // 
            // renderPictureBox
            // 
            renderPictureBox.Dock = DockStyle.Fill;
            renderPictureBox.Image = Properties.Resources.background;
            renderPictureBox.InitialImage = null;
            renderPictureBox.Location = new Point(0, 0);
            renderPictureBox.Name = "renderPictureBox";
            renderPictureBox.Size = new Size(960, 719);
            renderPictureBox.TabIndex = 0;
            renderPictureBox.TabStop = false;
            renderPictureBox.Paint += renderPictureBox_Paint;
            renderPictureBox.MouseDown += renderPictureBox_MouseDown;
            renderPictureBox.MouseMove += renderPictureBox_MouseMove;
            renderPictureBox.MouseUp += renderPictureBox_MouseUp;
            // 
            // gameScoreLabel
            // 
            gameScoreLabel.AutoSize = true;
            gameScoreLabel.Font = new Font("Impact", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            gameScoreLabel.ForeColor = Color.Yellow;
            gameScoreLabel.Location = new Point(12, 9);
            gameScoreLabel.Name = "gameScoreLabel";
            gameScoreLabel.Size = new Size(32, 36);
            gameScoreLabel.TabIndex = 1;
            gameScoreLabel.Text = "0";
            // 
            // bombPictureBox
            // 
            bombPictureBox.Location = new Point(109, 12);
            bombPictureBox.Name = "bombPictureBox";
            bombPictureBox.Size = new Size(50, 50);
            bombPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            bombPictureBox.TabIndex = 2;
            bombPictureBox.TabStop = false;
            // 
            // gameTimerLabel
            // 
            gameTimerLabel.AutoSize = true;
            gameTimerLabel.Font = new Font("Impact", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            gameTimerLabel.ForeColor = Color.Yellow;
            gameTimerLabel.Location = new Point(818, 9);
            gameTimerLabel.Name = "gameTimerLabel";
            gameTimerLabel.Size = new Size(130, 36);
            gameTimerLabel.TabIndex = 3;
            gameTimerLabel.Text = "0:00.000";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 719);
            Controls.Add(gameTimerLabel);
            Controls.Add(bombPictureBox);
            Controls.Add(gameScoreLabel);
            Controls.Add(renderPictureBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fruit Ninja";
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            KeyDown += MainForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)renderPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)bombPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox renderPictureBox;
        private Label gameScoreLabel;
        private PictureBox bombPictureBox;
        private Label gameTimerLabel;
    }
}