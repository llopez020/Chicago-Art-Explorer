﻿using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chicago_Art_Explorer
{
    public partial class ArtworkControl : UserControl
    {

        /// <summary> 
        /// Initialize Component.
        /// </summary>
        public ArtworkControl()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Set or get component title.
        /// </summary>
        public string SetTitle
        {
            get { return title.Text; }
            set { title.Text = value; }
        }

        /// <summary> 
        /// Set or get component summary.
        /// </summary>
        public string SetSummary
        {
            get { return summary.Text; }
            set { summary.Text = Regex.Replace(value, @"<[^>]*>", ""); }
        }

        /// <summary> 
        /// Set or get component image.
        /// </summary>
        public Image SetImage
        {
            get { return imageBox.Image; }
            set { imageBox.Image = value; }
        }
    }
}
