using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Chicago_Art_Explorer
{
    public partial class Form : System.Windows.Forms.Form
    {
        // Client used for connecting to API
        static HttpClient client;

        // Pagination Variables
        static RootMany paginationReference = new RootMany();
        static int TOTAL_IMAGES;
        static int MAX_PAGES = 100;

        // Variable that determines whether or not the "Previous page" or "Next page" buttons should be displayed
        static bool togglePageButtons = true;

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
            // Prepare Client with various optimizations
            var httpClientHandler = new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.All };
            httpClientHandler.Proxy = null;
            httpClientHandler.UseProxy = false;
            client = new HttpClient(httpClientHandler);

            // Initialize pagination information, as well as the components
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
            paginationReference = await GetData("https://api.artic.edu/api/v1/artworks/search?limit=1", true);

            // If null, we cannot use the program, and thus must exit
            // If not null, read in the total number of images and pages, and the limit of images per page
            if (paginationReference == null)
            {
                MessageBox.Show("Could not connect to The Art Institute of Chicago's API. The application will now close.", "Application Closing");
                this.Close();
            }
            else
                TOTAL_IMAGES = paginationReference.pagination.total;

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

            drawSearchComponent("https://api.artic.edu/api/v1/artworks?fields=api_link");
        }

        /// <summary> 
        /// Excuted on 'Random' button click.
        /// </summary>
        async void randomButton_Click(object sender, EventArgs e)
        {
            drawLabel("Loading...", panel);

            // Gets a random page value
            int randPage = random.Next(1, TOTAL_IMAGES + 1);

            // Determines if the artwork given is valid. If not, find another random artwork to draw
            bool validArt = await drawArtworkComponent("https://api.artic.edu/api/v1/artworks?fields=api_link&limit=1&page=" + randPage, true);
            if (!validArt)
                randomButton_Click(this, new EventArgs());

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

            drawSearchComponent("https://api.artic.edu/api/v1/artworks/search?fields=api_link&q=" + searchBox.Text + "&page=1");
        }

        /// <summary> 
        /// Executed on 'Previous/Next page' button click.
        /// </summary>
        void pageButton_Click(object sender, EventArgs e)
        {
            Button self = sender as Button;

            drawLabel("Loading...", panel);

            drawSearchComponent(self.Tag.ToString());
        }



        /// 
        /// DRAWING METHODS
        /// 



        /// <summary> 
        /// Draws an ArtworkControl Component to the main panel.
        /// </summary>
        /// <param name="url">URL to obtain JSON data from</param>         
        /// <param name="many">The data id for the artwork. -1 for single data returns</param> 
        /// <returns>True = Artwork is valid and will be drawn. False = Artwork is invalid and will not be drawn.</returns>
        public async Task<bool> drawArtworkComponent(string url, bool many)
        {
            // Prevent user from sending too many requests
            DisableButtons();

            // Prevent page buttons from being drawn
            togglePageButtons = false;

            // Get artwork data
            dynamic jsonObject = await GetData(url, many);

            // Return if no artwork data is found, return false as artwork is invalid
            if (jsonObject == null) 
                return false;

            // Clear main panel
            panel.Controls.Clear();

            // Set artwork based off of the data format
            Artwork artwork = many ? jsonObject.data[0] : jsonObject.data;

            // Create new component, summary, title, and image have to be filled out
            ArtworkControl component = new ArtworkControl();

            // If it is a multiple artwork container, get the data inside
            if (many)
            {
                RootOne artworkRoot = await GetData(artwork.api_link, false);
                artwork = artworkRoot.data;
            }

            // Component title
            component.SetTitle = artwork.title;

            // Component summary
            if (artwork.description != null)
                component.SetSummary = artwork.description;

            // Get image information from Artwork
            Image image = await GetImage(artwork.image_id);

            // Component image
            component.SetImage = image;

            // Center component
            component.Dock = DockStyle.Fill;

            // If image exists, add component to main panel, else return false (artwork is invalid)
            if (image != null)
                panel.Controls.Add(component);
            else 
                return false;

            // Allow user to send requests
            EnableButtons();

            // Artwork is valid, so return true
            return true;
        }

        /// <summary> 
        /// Draws a SearchControl Component to the main panel.
        /// </summary>
        /// <param name="url">URL to obtain JSON data from</param>         
        public async void drawSearchComponent(string url)
        {
            // Prevent user from sending too many requests
            DisableButtons();

            // Get JSON data from search query
            RootMany jsonObject = await GetData(url, true);

            // Artwork does not exist, so return
            if (jsonObject == null) return;

            // Clear main panel
            panel.Controls.Clear();

            // Initialize a new FlowLayoutPanel, so that all components are presented dynamically
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.AutoScroll = true;

            // Add FlowLayoutPanel to main panel
            panel.Controls.Add(flowLayoutPanel);

            // Unpack all of given data from JSON, and present it to user
            foreach (var artwork in jsonObject.data)
            {
                // Create new component, summary, title, and image have to be filled out
                SearchControl component = new SearchControl();

                RootOne artworkJson = await GetData(artwork.api_link + "?fields=image_id,id,title,description", false);
                artwork.title = artworkJson.data.title;
                artwork.image = await GetImage(artworkJson.data.image_id);
                artwork.description = artworkJson.data.description;

                // Component title
                component.SetTitle = artwork.title;

                // Component summary
                if (artwork.description != null)
                    component.SetSummary = artwork.description;

                // Component image
                component.SetImage = artwork.image;

                // Component API link
                component.SetAPILink = artwork.api_link;

                // Component parent panel
                component.SetPanel = panel;

                // Component parent form
                component.SetForm = this;

                // If image exists, add component to main panel
                if (artwork.image != null)
                    flowLayoutPanel.Controls.Add(component);
            }

            // Set url strings
            string next_url = jsonObject.pagination.next_url;
            string prev_url = jsonObject.pagination.prev_url;

            // If previous page exists, draw button to indicate and allow access
            if (togglePageButtons && (prev_url != null 
                || jsonObject.pagination.current_page > 1))
            {
                // Add page button to footer
                footerLeft.Controls.Add(createPageButton("Previous Page", url, prev_url, jsonObject.pagination.current_page - 1));
            }

            // If next page exists, draw button to indicate and allow access
            if (togglePageButtons && (next_url != null 
                || (jsonObject.pagination.current_page < jsonObject.pagination.total_pages - 1 
                && jsonObject.pagination.current_page <= MAX_PAGES)))
            {
                // Add page button to footer
                footerRight.Controls.Add(createPageButton("Next Page", url, next_url, jsonObject.pagination.current_page + 1));
            }

            // Allow user to send requests
            EnableButtons();
        }

        /// <summary> 
        /// Creates a label in the center of the given Control.
        /// </summary>
        /// <param name="text">The text to write out</param>         
        /// <param name="control">The Control to place label in</param>
        public void drawLabel(string text, Control control)
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
            footerLeft.Controls.Clear();
            footerRight.Controls.Clear();

            randomButton.Enabled = false;
            browseButton.Enabled = false;
            searchButton.Enabled = false;
            favoritesButton.Enabled = false;
            searchBox.Enabled = false;

            togglePageButtons = true;
        }

        /// <summary>
        /// Creates a new page traversal button and returns it.
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="initial_url">Initial page URL</param>
        /// <param name="url">Initial button URL</param>
        /// <param name="page_number">Page number to be reached</param>
        /// <returns>Page traversal button</returns>
        Button createPageButton(string text, string initial_url, string url, int page_number)
        {
            // Create button
            Button button = new Button();
            button.Text = text;
            button.AutoSize = true;

            // If url exists, simply use tag. If not, we have to create our own tag, using the page number
            if (url != null)
                button.Tag = url;
            else
            {
                // Remove any "&page=" in our link, as we want to set a new one
                button.Tag = initial_url.Substring(0, initial_url.LastIndexOf('&'));

                // Create url and set as tag
                button.Tag = button.Tag.ToString() + "&page=" + (page_number);
            }

            // Add page button method
            button.Click += pageButton_Click;

            // Return the created button
            return button;
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
        public string next_url { get; set; }
        public string prev_url { get; set; }
        public int current_page {  get; set; }

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
