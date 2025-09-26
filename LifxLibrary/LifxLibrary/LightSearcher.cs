using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text;

namespace LifxLibrary
{

    class Capabilities
    {
        public bool has_color { get; set; }
        public bool has_variable_color_temp { get; set; }
        public bool has_ir { get; set; }
        public bool has_hev { get; set; }
        public bool has_chain { get; set; }
        public bool has_matrix { get; set; }
        public bool has_multizone { get; set; }
        public int min_kelvin { get; set; }
        public int max_kelvin { get; set; }
    }

    class Color
    {
        public double hue { get; set; }
        public double saturation { get; set; }
        public int kelvin { get; set; }
    }

    class Group
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    class Location
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    class Product
    {
        public string name { get; set; }
        public string identifier { get; set; }
        public string company { get; set; }
        public int vendor_id { get; set; }
        public int product_id { get; set; }
        public Capabilities capabilities { get; set; }
    }

    class Root
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string label { get; set; }
        public bool connected { get; set; }
        public string power { get; set; }
        public Color color { get; set; }
        public double brightness { get; set; }
        public Group group { get; set; }
        public Location location { get; set; }
        public Product product { get; set; }

        [JsonIgnore]
        public DateTime last_seen { get; set; }
        public int seconds_since_seen { get; set; }
    }


    public class BulbState
    {

        public string Id { get; set;}
        public string UUID { get; set; }
        public string Label { get; set; }
        public bool Connected { get; set; }
        public string Power { get; set; }
        public double Hue { get; set; }
        public double Saturation { get; set; }
        public double Brightness { get; set; }

    }

    public static class LightSearcher
    {
        
        private static string TokenKey { get; set; }

        public static void SetTokenKey(string token)
        {
            // Validate token api key
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Error Token api key is required.", nameof(token));

            TokenKey = token;
        }



        //Retrieves a list of labels for all connected LIFX devices.
        public static async Task<List<string>> ShowConnectedDevicesAsync()
        {
            // API endpoint to get all lights
            string endPoint = "https://api.lifx.com/v1/lights/all";

            // Create an HttpClient and set the Authorization header with the API token
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenKey);

            // Send the GET request to the LIFX API
            HttpResponseMessage response = await client.GetAsync(endPoint);

            // List to store labels of connected devices
            List<string> connectedDevices = new List<string>();

            if (response.IsSuccessStatusCode)
            {
                // Read the response content stream
                var responsebody = await response.Content.ReadAsStreamAsync();

                // Deserialize JSON response into a list of Root objects (bulb info)
                List<Root> obj = await JsonSerializer.DeserializeAsync<List<Root>>(responsebody);

                // Loop through all bulbs and add labels for those that are connected
                foreach (var bulb in obj)
                {
                    if (bulb.connected.Equals(true))
                    {
                        connectedDevices.Add(bulb.label);
                    }
                }
            }
            else if (!response.IsSuccessStatusCode)
            {
                // Throw an exception if the request failed
                throw new Exception($"Something went wrong {response.StatusCode}");
            }

            // Return the list of connected device labels
            return connectedDevices;
        }



        //This method returns the bulb label names
        public static async Task<List<string>> GetNamesAsync()
        {
            // API endpoint to get all lights
            string endPoint = "https://api.lifx.com/v1/lights/all";

            // Create an HttpClient and set the Authorization header with the API token
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenKey);

            // Send the GET request to the LIFX API
            HttpResponseMessage response = await client.GetAsync(endPoint);

            // List to store the labels of each device
            List<string> lightsNames = new List<string>();

            if (response.IsSuccessStatusCode)
            {
                // Read the response content stream
                var responsebody = await response.Content.ReadAsStreamAsync();

                // Deserialize JSON response into a list of Root objects
                List<Root> obj = await JsonSerializer.DeserializeAsync<List<Root>>(responsebody);

                foreach (var root in obj)
                {
                    //adds the lights label names into a List of string
                    lightsNames.Add(root.label);
                }

            }
            else if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Something went wrong {response.StatusCode}");
            }

            //return the list of labels
            return lightsNames;
        }



        // Retrieves the current state of a specific LIFX bulb by it's label name.
        public static async Task<BulbState> ShowStateAsync(string labelName)
        {
            // Validate label name
            if (string.IsNullOrWhiteSpace(labelName))
                throw new ArgumentException("Label name is required.", nameof(labelName));

            // API endpoint to get the state of a specific bulb by its label
            string endPoint = $"https://api.lifx.com/v1/lights/label:{labelName}";

            // Create HttpClient and attach the Authorization header with the API token
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenKey);

            // Send the GET request to the LIFX API
            HttpResponseMessage response = await client.GetAsync(endPoint);

            // Initialize an empty BulbState object to hold the result
            BulbState lightState = new BulbState();

            if (response.IsSuccessStatusCode)
            {
                // Read the response content stream
                var responsebody = await response.Content.ReadAsStreamAsync();

                // Deserialize JSON into a list of Root objects (the API returns an array)
                List<Root> obj = await JsonSerializer.DeserializeAsync<List<Root>>(responsebody);

                var bulb = obj[0];

                // Populate the BulbState object 
                lightState.Id = bulb.id;
                lightState.UUID = bulb.uuid;
                lightState.Label = bulb.label;
                lightState.Connected = bulb.connected;
                lightState.Power = bulb.power;
                lightState.Hue = bulb.color.hue;
                lightState.Saturation = bulb.color.saturation;
                lightState.Brightness = bulb.brightness;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // throw exception if status code is 404
                throw new HttpRequestException($"Error could not find light with label name: {labelName}");
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // throw exception if status code is 401
                throw new HttpRequestException("Error 401 Invalid API token key");
            }
            else
            {
                // throw exception
                throw new Exception($"Something went wrong {response.StatusCode}");
            }

            // Return the populated BulbState object
            return lightState;
        }

    }
}
