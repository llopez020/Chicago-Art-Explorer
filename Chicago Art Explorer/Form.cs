using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Chicago_Art_Explorer
{
    public partial class Form : System.Windows.Forms.Form
    {

        static readonly HttpClient client = new HttpClient();

        // Pagination Variables
        static RootMany paginationReference = new RootMany();
        static int total_pages;
        static int limit;
        static int total_images;

        // Randomizer
        static readonly Random random = new Random();



        ///
        /// FORMS METHODS
        /// 
        


        /// <summary> 
        /// Initialize The form.
        /// </summary>
        public Form()
        {
            InitializePagination();
        }

        /// <summary> 
        /// Initialize Pagination data, allowing us to know important information about the gallery.
        /// </summary>
        async void InitializePagination()
        {
            // Prevent user from modifying window size
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Let user know that the program is initializing
            drawLabel("Initializing...", this);

            // Get pagination data from the API
            paginationReference = await GetData("https://api.artic.edu/api/v1/artworks", true);

            // If null, we cannot use the program, and thus must exit
            // If not null, read in the total number of images and pages, and the limit of images per page
            if (paginationReference == null)
            {
                MessageBox.Show("Could not connect to The Art Institute of Chicago's API. The application will now close.", "Application Closing");
                this.Close();
            }
            else
            {
                total_images = paginationReference.pagination.total;
                total_pages = paginationReference.pagination.total_pages;
                limit = paginationReference.pagination.limit;
            }

            // Clear screen
            this.Controls.Clear();

            // Allow user to modify window size
            this.MaximizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Finally, initialize all components
            InitializeComponent();
        }

        /// <summary> 
        /// Excuted on 'Home' button click.
        /// </summary>
        void homeButton_Click(object sender, EventArgs e)
        {
            drawLabel("Loading...", panel);

            drawSearchComponent("https://api.artic.edu/api/v1/artworks");
        }

        /// <summary> 
        /// Excuted on 'Random' button click.
        /// </summary>
        void randomButton_Click(object sender, EventArgs e)
        {
            drawLabel("Loading...", panel);

            int artworkLimit = limit;

            // Gets a random page value
            int randPage = random.Next(1, total_pages + 1);

            // Determines how many art pieces are available on the page based on the limit
            // Only important in the case the user rolled the last available page
            if (randPage == total_pages)
                artworkLimit = total_images % artworkLimit;

            // Gets random art value
            int randArt = random.Next(0, artworkLimit);

            drawArtworkComponent("https://api.artic.edu/api/v1/artworks?page=" + randPage, randArt);
        }

        /// <summary> 
        /// Excuted on 'Favorites' button click.
        /// </summary>
        void favoritesButton_Click(object sender, EventArgs e) { }

        /// <summary> 
        /// Excuted on Search Box text input.
        /// </summary>
        void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            // If enter is pressed while searchbox is highlighted, search
            if (e.KeyCode == Keys.Enter)
                searchButton_Click(this, new EventArgs());
        }

        /// <summary> 
        /// Excuted on 'Exit' button click.
        /// </summary>
        void exitButton_Click(object sender, EventArgs e)
        {
            // Close the program upon clicking exit
            this.Close();
        }


        /// <summary> 
        /// Excuted on 'Search' button click.
        /// </summary>
        void searchButton_Click(object sender, EventArgs e)
        {
            drawLabel("Searching...", panel);

            drawSearchComponent("https://api.artic.edu/api/v1/artworks/search?q=" + searchBox.Text);
        }



        /// 
        /// DRAWING METHODS
        /// 



        /// <summary> 
        /// Draws an ArtworkControl Component to the main panel.
        /// </summary>
        /// <param name="url">URL to obtain JSON data from</param>         
        /// <param name="id">The data id for the artwork</param> 
        async void drawArtworkComponent(string url, int id)
        {
            // Prevent user from sending too many requests
            DisableButtons();

            RootMany jsonObject = await GetData(url, true);

            if (jsonObject == null) return;

            // Clear main panel
            panel.Controls.Clear();

            // Create new component, summary, title, and image have to be filled out
            ArtworkControl component = new ArtworkControl();

            // Component title
            component.SetTitle = jsonObject.data[id].title;

            // Get more information from the Artwork, including description and image
            RootOne artwork = await GetData(jsonObject.data[id].api_link, false);

            // Component summary
            if (artwork.data.description != null)
                component.SetSummary = artwork.data.description;

            // Get image information from Artwork
            Image image = await GetImage(artwork.data.image_id);

            // Component image
            component.SetImage = image;

            // Center component
            component.Dock = DockStyle.Fill;

            // If image exists, add component to main panel
            if (image != null)
                panel.Controls.Add(component);

            // Allow user to send requests
            EnableButtons();
        }

        /// <summary> 
        /// Draws a SearchControl Component to the main panel.
        /// </summary>
        /// <param name="url">URL to obtain JSON data from</param>         
        async void drawSearchComponent(string url)
        {
            // Prevent user from sending too many requests
            DisableButtons();

            // Get JSON data from search query
            RootMany jsonObject = await GetData(url, true);

            if (jsonObject == null) return;

            // Clear main panel
            panel.Controls.Clear();

            // Initialize a new FlowLayoutPanel, so that all components are presented dynamically
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.AutoScroll = true;

            // Unpack all of given data from JSON, and present it to user
            foreach (var artwork in jsonObject.data)
            {
                // Create new component, summary, title, and image have to be filled out
                SearchControl component = new SearchControl();

                // Component title
                component.SetTitle = artwork.title;

                // Component summary
                if (artwork.description != null)
                    component.SetSummary = artwork.description;

                // Component image
                component.SetImage = artwork.image;

                // If image exists, add component to main panel
                if (artwork.image != null)
                    flowLayoutPanel.Controls.Add(component);
            }

            // Add FlowLayoutPanel to main panel
            panel.Controls.Add(flowLayoutPanel);

            // Allow user to send requests
            EnableButtons();
        }

        /// <summary> 
        /// Creates a label in the center of the given Control.
        /// </summary>
        /// <param name="text">The text to write out</param>         
        /// <param name="control">The Control to place label in</param>
        void drawLabel(string text, Control control)
        {
            // Clear given control
            control.Controls.Clear();

            // Initialize Label and Label settings
            Label label = new Label();
            label.Text = text;
            label.Font = new Font(label.Font.FontFamily, 20, label.Font.Style);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Dock = DockStyle.Fill;

            // Add label to control
            control.Controls.Add(label);
        }



        //
        // MISC METHODS
        //


        /// <summary> 
        /// Enables all buttons in the program.
        /// </summary>
        void EnableButtons()
        {
            randomButton.Enabled = true;
            browseButton.Enabled = true;
            searchButton.Enabled = true;
            favoritesButton.Enabled = true;
            searchBox.Enabled = true;
        }

        /// <summary> 
        /// Disables all buttons in the program.
        /// </summary>
        void DisableButtons()
        {
            randomButton.Enabled = false;
            browseButton.Enabled = false;
            searchButton.Enabled = false;
            favoritesButton.Enabled = false;
            searchBox.Enabled = false;
        }



        ///
        /// DATA RETRIEVAL METHODS
        ///



        /// <summary> 
        /// Returns data from JSON object. Can be used for many Artworks, or just one.
        /// </summary>
        /// <param name="url">URL to JSON object</param>         
        /// <param name="many">True = many Artworks, False = one Artwork</param>
        /// <returns>Deserialized JSON Object</returns>
        async static Task<dynamic> GetData(string url, bool many)
        {
            try
            {
                // Get response from url, and set up the deserialized JSON object
                string responseBody = await client.GetStringAsync(url);
                dynamic jsonObject;

                // Fill deserialized JSON object based on request size
                if (many)
                {
                    jsonObject = JsonConvert.DeserializeObject<RootMany>(responseBody);

                    // Fill out important information for each artwork
                    foreach (var artworks in jsonObject.data)
                    {
                        RootOne artwork = await GetData(artworks.api_link, false);
                        artworks.image = await GetImage(artwork.data.image_id);
                        artworks.description = artwork.data.description;
                    }
                }
                else
                    jsonObject = JsonConvert.DeserializeObject<RootOne>(responseBody);



                return jsonObject!;
            }
            catch
            {
                return null;
            }
        }

        /// <summary> 
        /// Returns Image data from JSON object.
        /// </summary>
        /// <param name="image_id">ID of image</param>
        /// <returns>Image</returns>
        async static Task<Image> GetImage(string image_id)
        {
            try
            {
                // Set up url , and get response
                string url = "https://www.artic.edu/iiif/2/" + image_id + "/full/843,/0/default.jpg";
                byte[] responseImage = await client.GetByteArrayAsync(url);

                // Convert byte array to Image 
                var imageBuffer = new MemoryStream(responseImage);
                Image image = Image.FromStream(imageBuffer);

                return image;
            }
            catch
            {
                return null;
            }
        }
    }



    ///
    /// JSON CONTAINERS
    ///



    /// <summary> 
    /// JSON container for the 'Pagination' data.
    /// </summary>
    public class Pagination()
    {
        public int total { get; set; }
        public int limit { get; set; }
        public int total_pages { get; set; }
    }

    /// <summary> 
    /// JSON container for the 'Artwork' data.
    /// </summary>
    public class Artwork()
    {
        public string api_link { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string image_id { get; set; }
        public string description { get; set; }
        public Image image;
    }

    /// <summary> 
    /// JSON container for the 'Root' data.
    /// Creates a list of Artworks that can be pulled from.
    /// </summary>
    public class RootMany()
    {
        public List<Artwork> data { get; set; }
        public Pagination pagination { get; set; }
    }

    /// <summary> 
    /// JSON container for the 'Root' data.
    /// Holds single artwork data.
    /// </summary>
    public class RootOne()
    {
        public Artwork data { get; set; }
    }

}
