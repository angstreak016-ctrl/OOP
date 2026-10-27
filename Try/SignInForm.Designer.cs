namespace Try
{
    partial class SignInForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new Try.SignInForm.BorderedRoundButton();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.outlinedButton1 = new Try.OutlinedButton();
            this.outlinedButton2 = new Try.OutlinedButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Humato Heavy", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(125, 98);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(271, 33);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Humato Heavy", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(125, 176);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(271, 33);
            this.textBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Humato Heavy", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(122, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Humato Heavy", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(122, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.outlinedButton2);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(151, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(522, 350);
            this.panel1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gold;
            this.button1.BorderColor = System.Drawing.Color.Black;
            this.button1.BorderRadius = 20;
            this.button1.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Humato Heavy", 12F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(135, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(271, 46);
            this.button1.TabIndex = 4;
            this.button1.Text = "Log in";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.Font = new System.Drawing.Font("Humato Heavy", 20F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(722, 14);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.button2.Size = new System.Drawing.Size(49, 46);
            this.button2.TabIndex = 5;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Try.Properties.Resources.image_Photoroom__1_;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(144, 57);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // outlinedButton1
            // 
            this.outlinedButton1.BackColorEnd = System.Drawing.Color.Gold;
            this.outlinedButton1.BackColorStart = System.Drawing.Color.Peru;
            this.outlinedButton1.BorderRadius = 800;
            this.outlinedButton1.Font = new System.Drawing.Font("Humato Heavy", 26F, System.Drawing.FontStyle.Bold);
            this.outlinedButton1.ForeColor = System.Drawing.Color.White;
            this.outlinedButton1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.outlinedButton1.HoverOutlineEndColor = System.Drawing.Color.Magenta;
            this.outlinedButton1.HoverOutlineStartColor = System.Drawing.Color.Yellow;
            this.outlinedButton1.IsPillShape = false;
            this.outlinedButton1.Location = new System.Drawing.Point(649, 47);
            this.outlinedButton1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.outlinedButton1.Name = "outlinedButton1";
            this.outlinedButton1.OutlineColor = System.Drawing.Color.Black;
            this.outlinedButton1.OutlineWidth = 5F;
            this.outlinedButton1.Size = new System.Drawing.Size(45, 43);
            this.outlinedButton1.TabIndex = 7;
            this.outlinedButton1.Text = "x";
            this.outlinedButton1.UseVisualStyleBackColor = true;
            this.outlinedButton1.Click += new System.EventHandler(this.outlinedButton1_Click);
            // 
            // outlinedButton2
            // 
            this.outlinedButton2.BackColorEnd = System.Drawing.Color.Gold;
            this.outlinedButton2.BackColorStart = System.Drawing.Color.Peru;
            this.outlinedButton2.BorderRadius = 16;
            this.outlinedButton2.Font = new System.Drawing.Font("Humato Heavy", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outlinedButton2.ForeColor = System.Drawing.Color.White;
            this.outlinedButton2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.outlinedButton2.HoverOutlineEndColor = System.Drawing.Color.Magenta;
            this.outlinedButton2.HoverOutlineStartColor = System.Drawing.Color.Yellow;
            this.outlinedButton2.IsPillShape = false;
            this.outlinedButton2.Location = new System.Drawing.Point(126, 248);
            this.outlinedButton2.Name = "outlinedButton2";
            this.outlinedButton2.OutlineColor = System.Drawing.Color.Black;
            this.outlinedButton2.OutlineWidth = 5F;
            this.outlinedButton2.Size = new System.Drawing.Size(270, 46);
            this.outlinedButton2.TabIndex = 4;
            this.outlinedButton2.Text = "Log in";
            this.outlinedButton2.UseVisualStyleBackColor = true;
            this.outlinedButton2.Click += new System.EventHandler(this.outlinedButton2_Click);
            // 
            // SignInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.outlinedButton1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.Name = "SignInForm";
            this.Text = "SignInForm";
            this.TransparencyKey = System.Drawing.Color.RosyBrown;
            this.Load += new System.EventHandler(this.SignInForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private BorderedRoundButton button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private OutlinedButton outlinedButton1;
        private OutlinedButton outlinedButton2;
    }
}