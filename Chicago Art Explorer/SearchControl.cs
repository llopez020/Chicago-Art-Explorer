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
    public partial class SearchControl : UserControl
    {
        Form form;
        Panel panel;
        string api_link;

        /// <summary> 
        /// Initialize Component.
        /// </summary>
        public SearchControl()
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

        /// <summary> 
        /// Set or get parent form.
        /// </summary>
        public Form SetForm
        {
            get { return form; }
            set { form = value; }
        }

        /// <summary> 
        /// Set or get parent panel.
        /// </summary>
        public Panel SetPanel
        {
            get { return panel; }
            set { panel = value; }
        }

        /// <summary> 
        /// Set or get API link.
        /// </summary>
        public string SetAPILink
        {
            get { return api_link; }
            set { api_link = value; }
        }

        /// <summary> 
        /// Excuted on 'Search Control' click.
        /// </summary>
        async void searchControl_Click(object sender, EventArgs e)
        {
            form.drawLabel("Loading...", panel);

            await form.drawArtworkComponent(api_link, false);
        }
    }
}
