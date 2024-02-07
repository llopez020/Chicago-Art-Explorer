using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Chicago_Art_Explorer
{
    public partial class Form : System.Windows.Forms.Form
    {

        static readonly HttpClient client = new HttpClient();

        /// <summary> 
        /// Initialize Component.
        /// </summary>
        public Form()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Excuted on Form initialization.
        /// </summary>
        void Form_Load(object sender, EventArgs e) { }

        /// <summary> 
        /// Excuted on 'Home' button click.
        /// </summary>
        void homeButton_Click(object sender, EventArgs e) { }

        /// <summary> 
        /// Excuted on 'Random' button click.
        /// </summary>
        void randomButton_Click(object sender, EventArgs e) { }

        /// <summary> 
        /// Excuted on 'Favorites' button click.
        /// </summary>
        void favoritesButton_Click(object sender, EventArgs e) { }

        /// <summary> 
        /// Excuted on Search Box text input.
        /// </summary>
        void searchBox_TextChanged(object sender, EventArgs e) { }

        /// <summary> 
        /// Excuted on 'Exit' button click.
        /// </summary>
        void exitButton_Click(object sender, EventArgs e) { }


        /// <summary> 
        /// Excuted on 'Search' button click.
        /// </summary>
        async void searchButton_Click(object sender, EventArgs e)
        {

            // Clear main panel
            flowLayoutPanel.Controls.Clear();

            // Get JSON data from search query
            RootMany jsonObject = await GetData("https://api.artic.edu/api/v1/artworks/search?q=" + searchBox.Text, true);
            
            if (jsonObject == null)
                return;

            // Unpack all of given data from JSON, and present it to user
            foreach (var artworks in jsonObject.data)
            {
                // Create new component, summary, title, and image have to be filled out
                SearchControl component = new SearchControl();

                // Component title
                component.SetTitle = artworks.title;

                // Get more information from the Artwork, including description and image
                RootOne artwork = await GetData(artworks.api_link, false);

                // Component summary
                if (artwork.data.description!=null)
                    component.SetSummary = artwork.data.description;

                // Get image information from Artwork
                Image image = await GetImage(artwork.data.image_id);

                // Component image
                component.SetImage = image;

                // If image exists, add component to main panel
                if (image != null)
                    flowLayoutPanel.Controls.Add(component);
            }
        }



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
                    jsonObject = JsonConvert.DeserializeObject<RootMany>(responseBody);
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
    }

    /// <summary> 
    /// JSON container for the 'Root' data.
    /// Creates a list of Artworks that can be pulled from.
    /// </summary>
    public class RootMany()
    {
        public List<Artwork> data { get; set; }
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
