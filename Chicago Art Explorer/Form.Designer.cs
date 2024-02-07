namespace Chicago_Art_Explorer
{
    partial class Form
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
            searchBox = new TextBox();
            searchButton = new Button();
            headerPanel = new Panel();
            favoritesButton = new Button();
            exitButton = new Button();
            randomButton = new Button();
            browseButton = new Button();
            flowLayoutPanel = new FlowLayoutPanel();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // searchBox
            // 
            searchBox.Location = new Point(439, 9);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(257, 23);
            searchBox.TabIndex = 1;
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(692, 9);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(52, 23);
            searchButton.TabIndex = 2;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(favoritesButton);
            headerPanel.Controls.Add(exitButton);
            headerPanel.Controls.Add(randomButton);
            headerPanel.Controls.Add(browseButton);
            headerPanel.Controls.Add(searchBox);
            headerPanel.Controls.Add(searchButton);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(875, 43);
            headerPanel.TabIndex = 3;
            // 
            // favoritesButton
            // 
            favoritesButton.Location = new Point(193, 10);
            favoritesButton.Name = "favoritesButton";
            favoritesButton.Size = new Size(75, 23);
            favoritesButton.TabIndex = 6;
            favoritesButton.Text = "Favorites";
            favoritesButton.UseVisualStyleBackColor = true;
            favoritesButton.Click += favoritesButton_Click;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(783, 10);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(75, 23);
            exitButton.TabIndex = 5;
            exitButton.Text = "Exit";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // randomButton
            // 
            randomButton.Location = new Point(99, 10);
            randomButton.Name = "randomButton";
            randomButton.Size = new Size(75, 23);
            randomButton.TabIndex = 4;
            randomButton.Text = "Random";
            randomButton.UseVisualStyleBackColor = true;
            randomButton.Click += randomButton_Click;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(7, 10);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 3;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += homeButton_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.Location = new Point(0, 43);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new Size(875, 407);
            flowLayoutPanel.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 450);
            Controls.Add(flowLayoutPanel);
            Controls.Add(headerPanel);
            Name = "Form";
            Text = "Form";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private TextBox searchBox;
        private Button searchButton;
        private Panel headerPanel;
        private Button browseButton;
        private Button randomButton;
        private Button exitButton;
        private Button favoritesButton;
        private Panel outputPanel;
        private Panel panel1;
        private FlowLayoutPanel flowLayoutPanel;
    }
}
