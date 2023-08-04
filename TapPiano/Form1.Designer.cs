namespace TapPiano
{
    partial class Form1
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
            this.sound1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.stopSound = new System.Windows.Forms.Button();
            this.run = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sound1
            // 
            this.sound1.Location = new System.Drawing.Point(13, 13);
            this.sound1.Name = "sound1";
            this.sound1.Size = new System.Drawing.Size(75, 23);
            this.sound1.TabIndex = 0;
            this.sound1.Text = "make Sound";
            this.sound1.UseVisualStyleBackColor = true;
            this.sound1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(340, 84);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(384, 255);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // stopSound
            // 
            this.stopSound.Location = new System.Drawing.Point(95, 12);
            this.stopSound.Name = "stopSound";
            this.stopSound.Size = new System.Drawing.Size(75, 23);
            this.stopSound.TabIndex = 2;
            this.stopSound.Text = "stop sound";
            this.stopSound.UseVisualStyleBackColor = true;
            this.stopSound.Click += new System.EventHandler(this.stopSound_click);
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(119, 84);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(75, 23);
            this.run.TabIndex = 3;
            this.run.Text = "run";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 84);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.run);
            this.Controls.Add(this.stopSound);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.sound1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sound1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button stopSound;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.TextBox textBox2;
    }
}

