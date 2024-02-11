namespace Chicago_Art_Explorer
{
    partial class SearchControl
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
            label1 = new Label();
            summary = new TextBox();
            title = new TextBox();
            ((System.ComponentModel.ISupportInitialize)imageBox).BeginInit();
            SuspendLayout();
            // 
            // imageBox
            // 
            imageBox.Location = new Point(20, 17);
            imageBox.Name = "imageBox";
            imageBox.Size = new Size(123, 114);
            imageBox.SizeMode = PictureBoxSizeMode.Zoom;
            imageBox.TabIndex = 0;
            imageBox.TabStop = false;
            imageBox.Click += searchControl_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(149, 17);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 1;
            label1.Click += searchControl_Click;
            // 
            // summary
            // 
            summary.Location = new Point(155, 17);
            summary.Multiline = true;
            summary.Name = "summary";
            summary.ReadOnly = true;
            summary.Size = new Size(172, 114);
            summary.TabIndex = 2;
            summary.Click += searchControl_Click;
            // 
            // title
            // 
            title.Location = new Point(20, 149);
            title.Multiline = true;
            title.Name = "title";
            title.ReadOnly = true;
            title.Size = new Size(307, 44);
            title.TabIndex = 3;
            title.Click += searchControl_Click;
            //
            // SearchControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(title);
            Controls.Add(summary);
            Controls.Add(label1);
            Controls.Add(imageBox);
            this.Click += searchControl_Click;
            this.BorderStyle = BorderStyle.Fixed3D;
            Name = "SearchControl";
            Size = new Size(344, 209);
            
            ((System.ComponentModel.ISupportInitialize)imageBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox imageBox;
        private Label label1;
        private TextBox summary;
        private TextBox title;
    }
}
