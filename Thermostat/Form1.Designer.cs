namespace Thermostat
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
            this.configButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.label4 = new System.Windows.Forms.Label();
            this.motionTimeInput = new System.Windows.Forms.NumericUpDown();
            this.structuresPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.onButton = new System.Windows.Forms.RadioButton();
            this.offButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.motionTimeInput)).BeginInit();
            this.SuspendLayout();
            // 
            // configButton
            // 
            this.configButton.Location = new System.Drawing.Point(677, 11);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(75, 23);
            this.configButton.TabIndex = 6;
            this.configButton.Text = "Config";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.statusLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.configButton);
            this.groupBox1.Controls.Add(this.menuStrip1);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(758, 40);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(338, 27);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(54, 13);
            this.statusLabel.TabIndex = 18;
            this.statusLabel.Text = "No Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "MotiTemp";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(3, 16);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(752, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(248, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Minutes without motion until inactive:";
            // 
            // motionTimeInput
            // 
            this.motionTimeInput.Location = new System.Drawing.Point(472, 63);
            this.motionTimeInput.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.motionTimeInput.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.motionTimeInput.Name = "motionTimeInput";
            this.motionTimeInput.Size = new System.Drawing.Size(120, 20);
            this.motionTimeInput.TabIndex = 16;
            this.motionTimeInput.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // structuresPanel
            // 
            this.structuresPanel.AutoScroll = true;
            this.structuresPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.structuresPanel.Location = new System.Drawing.Point(12, 53);
            this.structuresPanel.Name = "structuresPanel";
            this.structuresPanel.Size = new System.Drawing.Size(230, 419);
            this.structuresPanel.TabIndex = 17;
            // 
            // onButton
            // 
            this.onButton.AutoSize = true;
            this.onButton.Location = new System.Drawing.Point(249, 117);
            this.onButton.Name = "onButton";
            this.onButton.Size = new System.Drawing.Size(41, 17);
            this.onButton.TabIndex = 18;
            this.onButton.TabStop = true;
            this.onButton.Text = "ON";
            this.onButton.UseVisualStyleBackColor = true;
            this.onButton.CheckedChanged += new System.EventHandler(this.onButton_CheckedChanged);
            // 
            // offButton
            // 
            this.offButton.AutoSize = true;
            this.offButton.Location = new System.Drawing.Point(249, 141);
            this.offButton.Name = "offButton";
            this.offButton.Size = new System.Drawing.Size(45, 17);
            this.offButton.TabIndex = 19;
            this.offButton.TabStop = true;
            this.offButton.Text = "OFF";
            this.offButton.UseVisualStyleBackColor = true;
            this.offButton.CheckedChanged += new System.EventHandler(this.offButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 484);
            this.Controls.Add(this.offButton);
            this.Controls.Add(this.onButton);
            this.Controls.Add(this.structuresPanel);
            this.Controls.Add(this.motionTimeInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "MotiTemp";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.motionTimeInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown motionTimeInput;
        private System.Windows.Forms.FlowLayoutPanel structuresPanel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.RadioButton onButton;
        private System.Windows.Forms.RadioButton offButton;
    }
}

