namespace GADE6122
{
    partial class Form1
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
            lblDisplay = new Label();
            lblWasdMovement = new Label();
            lblHeroStats = new Label();
            SuspendLayout();
            // 
            // lblDisplay
            // 
            lblDisplay.AutoSize = true;
            lblDisplay.Font = new Font("Courier New", 12F);
            lblDisplay.Location = new Point(467, 227);
            lblDisplay.Name = "lblDisplay";
            lblDisplay.Size = new Size(88, 18);
            lblDisplay.TabIndex = 0;
            lblDisplay.Text = "Display:";
            // 
            // lblWasdMovement
            // 
            lblWasdMovement.AutoSize = true;
            lblWasdMovement.Font = new Font("Segoe UI", 15F);
            lblWasdMovement.Location = new Point(456, 114);
            lblWasdMovement.Name = "lblWasdMovement";
            lblWasdMovement.Size = new Size(157, 28);
            lblWasdMovement.TabIndex = 1;
            lblWasdMovement.Text = "WASD: To move!";
            lblWasdMovement.Click += lblWasdMovement_Click;
            // 
            // lblHeroStats
            // 
            lblHeroStats.AutoSize = true;
            lblHeroStats.Font = new Font("Courier New", 12F);
            lblHeroStats.Location = new Point(847, 148);
            lblHeroStats.Name = "lblHeroStats";
            lblHeroStats.Size = new Size(108, 18);
            lblHeroStats.TabIndex = 2;
            lblHeroStats.Text = "Hero's HP:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1154, 961);
            Controls.Add(lblHeroStats);
            Controls.Add(lblWasdMovement);
            Controls.Add(lblDisplay);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDisplay;
        private Label lblWasdMovement;
        private Label lblHeroStats;
    }
}
