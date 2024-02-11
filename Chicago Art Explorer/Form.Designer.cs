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
            favoritesButton = new Button();
            exitButton = new Button();
            randomButton = new Button();
            browseButton = new Button();
            panel = new Panel();
            tableLayoutPanel = new TableLayoutPanel();
            headerRight = new FlowLayoutPanel();
            headerLeft = new FlowLayoutPanel();
            footerLeft = new FlowLayoutPanel();
            footerRight = new FlowLayoutPanel();
            tableLayoutPanel.SuspendLayout();
            headerRight.SuspendLayout();
            headerLeft.SuspendLayout();
            SuspendLayout();
            // 
            // searchBox
            // 
            searchBox.Location = new Point(40, 3);
            searchBox.Margin = new Padding(3, 3, 0, 3);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(358, 23);
            searchBox.TabIndex = 1;
            searchBox.KeyDown += searchBox_KeyDown;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(398, 3);
            searchButton.Margin = new Padding(0, 3, 20, 3);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(52, 23);
            searchButton.TabIndex = 2;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // favoritesButton
            // 
            favoritesButton.Location = new Point(199, 3);
            favoritesButton.Name = "favoritesButton";
            favoritesButton.Size = new Size(75, 23);
            favoritesButton.TabIndex = 6;
            favoritesButton.Text = "Favorites";
            favoritesButton.UseVisualStyleBackColor = true;
            favoritesButton.Click += favoritesButton_Click;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(473, 3);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(75, 23);
            exitButton.TabIndex = 5;
            exitButton.Text = "Exit";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // randomButton
            // 
            randomButton.Location = new Point(101, 3);
            randomButton.Margin = new Padding(3, 3, 20, 3);
            randomButton.Name = "randomButton";
            randomButton.Size = new Size(75, 23);
            randomButton.TabIndex = 4;
            randomButton.Text = "Random";
            randomButton.UseVisualStyleBackColor = true;
            randomButton.Click += randomButton_Click;
            // 
            // browseButton
            // 
            browseButton.Dock = DockStyle.Top;
            browseButton.Location = new Point(3, 3);
            browseButton.Margin = new Padding(3, 3, 20, 3);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 3;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += homeButton_Click;
            // 
            // panel
            // 
            panel.AutoScroll = true;
            panel.BackColor = SystemColors.Control;
            tableLayoutPanel.SetColumnSpan(panel, 2);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(3, 39);
            panel.Name = "panel";
            panel.Size = new Size(869, 372);
            panel.TabIndex = 4;
            panel.Tag = "";
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.41686F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.58314F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.Controls.Add(headerRight, 1, 0);
            tableLayoutPanel.Controls.Add(panel, 0, 1);
            tableLayoutPanel.Controls.Add(headerLeft, 0, 0);
            tableLayoutPanel.Controls.Add(footerLeft, 0, 2);
            tableLayoutPanel.Controls.Add(footerRight, 1, 2);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel.Size = new Size(875, 450);
            tableLayoutPanel.TabIndex = 5;
            // 
            // headerRight
            // 
            headerRight.Controls.Add(exitButton);
            headerRight.Controls.Add(searchButton);
            headerRight.Controls.Add(searchBox);
            headerRight.Dock = DockStyle.Fill;
            headerRight.FlowDirection = FlowDirection.RightToLeft;
            headerRight.Location = new Point(321, 3);
            headerRight.Name = "headerRight";
            headerRight.Size = new Size(551, 30);
            headerRight.TabIndex = 6;
            // 
            // headerLeft
            // 
            headerLeft.Controls.Add(browseButton);
            headerLeft.Controls.Add(randomButton);
            headerLeft.Controls.Add(favoritesButton);
            headerLeft.Dock = DockStyle.Fill;
            headerLeft.Location = new Point(3, 3);
            headerLeft.Name = "headerLeft";
            headerLeft.Size = new Size(312, 30);
            headerLeft.TabIndex = 0;
            // 
            // footerLeft
            // 
            footerLeft.Dock = DockStyle.Fill;
            footerLeft.Location = new Point(3, 417);
            footerLeft.Name = "footerLeft";
            footerLeft.Size = new Size(312, 30);
            footerLeft.TabIndex = 7;
            // 
            // footerRight
            // 
            footerRight.Dock = DockStyle.Fill;
            footerRight.FlowDirection = FlowDirection.RightToLeft;
            footerRight.Location = new Point(321, 417);
            footerRight.Name = "footerRight";
            footerRight.Size = new Size(551, 30);
            footerRight.TabIndex = 8;
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 450);
            Controls.Add(tableLayoutPanel);
            Name = "Form";
            Text = "Chicago Art Explorer";
            tableLayoutPanel.ResumeLayout(false);
            headerRight.ResumeLayout(false);
            headerRight.PerformLayout();
            headerLeft.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private TextBox searchBox;
        private Button searchButton;
        private Button browseButton;
        private Button randomButton;
        private Button exitButton;
        private Button favoritesButton;
        private Panel outputPanel;
        private Panel panel1;
        private Panel panel;
        private TableLayoutPanel tableLayoutPanel;
        private FlowLayoutPanel headerLeft;
        private FlowLayoutPanel headerRight;
        private FlowLayoutPanel footerLeft;
        private FlowLayoutPanel footerRight;
    }
}
