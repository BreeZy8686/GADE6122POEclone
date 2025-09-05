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
            SuspendLayout();
            // 
            // lblDisplay
            // 
            lblDisplay.AutoSize = true;
            lblDisplay.Font = new Font("Courier New", 12F);
            lblDisplay.Location = new Point(314, 176);
            lblDisplay.Name = "lblDisplay";
            lblDisplay.Size = new Size(106, 22);
            lblDisplay.TabIndex = 0;
            lblDisplay.Text = "Display:";
            // 
            // lblWasdMovement
            // 
            lblWasdMovement.AutoSize = true;
            lblWasdMovement.Font = new Font("Segoe UI", 15F);
            lblWasdMovement.Location = new Point(49, 33);
            lblWasdMovement.Name = "lblWasdMovement";
            lblWasdMovement.Size = new Size(198, 35);
            lblWasdMovement.TabIndex = 1;
            lblWasdMovement.Text = "WASD: To move!";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(lblWasdMovement);
            Controls.Add(lblDisplay);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDisplay;
        private Label lblWasdMovement;
    }
}
