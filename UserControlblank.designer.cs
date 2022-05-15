
namespace CalendarSystem
{
    partial class UserControlblank
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Tempory = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Tempory
            // 
            this.Tempory.AutoSize = true;
            this.Tempory.Location = new System.Drawing.Point(93, 69);
            this.Tempory.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.Tempory.Name = "Tempory";
            this.Tempory.Size = new System.Drawing.Size(0, 32);
            this.Tempory.TabIndex = 0;
            // 
            // UserControlblank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Tempory);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "UserControlblank";
            this.Size = new System.Drawing.Size(512, 288);
            this.Load += new System.EventHandler(this.UserControlblank_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Tempory;
    }
}
