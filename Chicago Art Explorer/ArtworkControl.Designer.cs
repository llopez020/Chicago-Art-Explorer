namespace Chicago_Art_Explorer
{
    partial class ArtworkControl
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
            imageBox = new PictureBox();
            title = new Label();
            Description = new Label();
            summary = new Label();
            ((System.ComponentModel.ISupportInitialize)imageBox).BeginInit();
            SuspendLayout();
            // 
            // imageBox
            // 
            imageBox.Dock = DockStyle.Fill;
            imageBox.Location = new Point(0, 0);
            imageBox.Name = "imageBox";
            imageBox.Size = new Size(397, 553);
            imageBox.SizeMode = PictureBoxSizeMode.Zoom;
            imageBox.TabIndex = 0;
            imageBox.TabStop = false;
            // 
            // title
            // 
            title.Dock = DockStyle.Top;
            title.Location = new Point(0, 0);
            title.Name = "title";
            title.Size = new Size(397, 15);
            title.TabIndex = 1;
            title.TextAlign = ContentAlignment.TopCenter;
            // 
            // Description
            // 
            Description.AutoSize = true;
            Description.Location = new Point(3, 448);
            Description.Name = "Description";
            Description.Size = new Size(0, 15);
            Description.TabIndex = 3;
            // 
            // summary
            // 
            summary.Dock = DockStyle.Bottom;
            summary.Location = new Point(0, 538);
            summary.Name = "summary";
            summary.Size = new Size(397, 15);
            summary.TabIndex = 2;
            summary.TextAlign = ContentAlignment.TopCenter;
            // 
            // ArtworkControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Description);
            Controls.Add(summary);
            Controls.Add(title);
            Controls.Add(imageBox);
            Name = "ArtworkControl";
            Size = new Size(397, 553);
            ((System.ComponentModel.ISupportInitialize)imageBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox imageBox;
        private Label title;
        private Label Description;
        private Label summary;
    }
}
