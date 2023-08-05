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
            this.Logger = new System.Windows.Forms.TextBox();
            this.stopSound = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sound1
            // 
            this.sound1.Cursor = System.Windows.Forms.Cursors.Default;
            this.sound1.Location = new System.Drawing.Point(12, 246);
            this.sound1.Name = "sound1";
            this.sound1.Size = new System.Drawing.Size(75, 23);
            this.sound1.TabIndex = 0;
            this.sound1.Text = "make Sound";
            this.sound1.UseVisualStyleBackColor = true;
            this.sound1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Logger
            // 
            this.Logger.Enabled = false;
            this.Logger.HideSelection = false;
            this.Logger.Location = new System.Drawing.Point(340, 84);
            this.Logger.Multiline = true;
            this.Logger.Name = "Logger";
            this.Logger.ReadOnly = true;
            this.Logger.Size = new System.Drawing.Size(384, 255);
            this.Logger.TabIndex = 1;
            this.Logger.TextChanged += new System.EventHandler(this.Logger_TextChanged);
            // 
            // stopSound
            // 
            this.stopSound.Location = new System.Drawing.Point(93, 246);
            this.stopSound.Name = "stopSound";
            this.stopSound.Size = new System.Drawing.Size(75, 23);
            this.stopSound.TabIndex = 2;
            this.stopSound.Text = "stop sound";
            this.stopSound.UseVisualStyleBackColor = true;
            this.stopSound.Click += new System.EventHandler(this.stopSound_click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "440",
            "128"});
            this.comboBox1.Location = new System.Drawing.Point(99, 58);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Base frequency";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.stopSound);
            this.Controls.Add(this.Logger);
            this.Controls.Add(this.sound1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_handle);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sound1;
        private System.Windows.Forms.TextBox Logger;
        private System.Windows.Forms.Button stopSound;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}

