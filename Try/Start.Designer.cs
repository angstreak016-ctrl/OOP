namespace Try
{
    partial class Start
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnStart = new Try.OutlinedButton();
            this.btnCapture = new Try.OutlinedButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(551, 348);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColorEnd = System.Drawing.Color.Gold;
            this.btnStart.BackColorStart = System.Drawing.Color.Peru;
            this.btnStart.BorderRadius = 16;
            this.btnStart.Font = new System.Drawing.Font("Humato Heavy", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.btnStart.HoverOutlineEndColor = System.Drawing.Color.Magenta;
            this.btnStart.HoverOutlineStartColor = System.Drawing.Color.Yellow;
            this.btnStart.IsPillShape = false;
            this.btnStart.Location = new System.Drawing.Point(584, 25);
            this.btnStart.Name = "btnStart";
            this.btnStart.OutlineColor = System.Drawing.Color.Black;
            this.btnStart.OutlineWidth = 2F;
            this.btnStart.Size = new System.Drawing.Size(187, 50);
            this.btnStart.SoundFilePath = "";
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click_1);
            // 
            // btnCapture
            // 
            this.btnCapture.BackColorEnd = System.Drawing.Color.Gold;
            this.btnCapture.BackColorStart = System.Drawing.Color.Peru;
            this.btnCapture.BorderRadius = 16;
            this.btnCapture.Font = new System.Drawing.Font("Humato Heavy", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapture.ForeColor = System.Drawing.Color.White;
            this.btnCapture.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.btnCapture.HoverOutlineEndColor = System.Drawing.Color.Magenta;
            this.btnCapture.HoverOutlineStartColor = System.Drawing.Color.Yellow;
            this.btnCapture.IsPillShape = false;
            this.btnCapture.Location = new System.Drawing.Point(584, 81);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.OutlineColor = System.Drawing.Color.Black;
            this.btnCapture.OutlineWidth = 2F;
            this.btnCapture.Size = new System.Drawing.Size(187, 50);
            this.btnCapture.SoundFilePath = "";
            this.btnCapture.TabIndex = 2;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click_1);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(783, 372);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnStart);
            this.Name = "Start";
            this.Text = "Start";
            this.Load += new System.EventHandler(this.Start_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private OutlinedButton btnStart;
        private OutlinedButton btnCapture;
    }
}