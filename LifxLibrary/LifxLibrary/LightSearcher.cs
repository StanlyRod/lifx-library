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
        public int hue { get; set; }
        public int saturation { get; set; }
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
        public int Hue { get; set; }
        public int Saturation { get; set; }
        public double Brightness { get; set; }

    }




    public static class LightSearcher
    {
<<<<<<< HEAD
        
        private static string TokenKey { get; set; }

        public static void SetTokenKey(string token)
        {
            TokenKey = token;
        }
=======
        
        private static string TokenKey { get; set; }

        public static void SetTokenKey(string token)
        {
            TokenKey = token;
        }

        
       /*public LightSearcher(string tokenKey)
        {
            TokenKey = tokenKey;
        }*/
>>>>>>> a1589ce4e0ef4815b347548c668b637c199521ed


        //this method returns a list of string with the label names of all connected devices
        public static async Task<List<string>> ShowConnectedDevicesAsync()
        {
            string endPoint = "https://api.lifx.com/v1/lights/all";
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenKey);

            HttpResponseMessage response = await client.GetAsync(endPoint);


            List<string> connectedDevices = new List<string>();


            if (response.IsSuccessStatusCode)
            {
                var responsebody = await response.Content.ReadAsStreamAsync();

                List<Root> obj = await JsonSerializer.DeserializeAsync<List<Root>>(responsebody);

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
                throw new Exception($"Something went wrong {response.StatusCode}");
            }

            return connectedDevices;
        }







<<<<<<< HEAD
        //this method returns the led label names
=======
        //this method returns the lights label names
>>>>>>> a1589ce4e0ef4815b347548c668b637c199521ed
        public static async Task<List<string>> GetNamesAsync()
        {
            string endPoint = "https://api.lifx.com/v1/lights/all";
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenKey);

            HttpResponseMessage response = await client.GetAsync(endPoint);//send http get request

            List<string> lightsNames = new List<string>();

            if (response.IsSuccessStatusCode)
            {
                var responsebody = await response.Content.ReadAsStreamAsync();

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

            return lightsNames;
        }





<<<<<<< HEAD
        //this method returns the current states of the led bulbs
        public static async Task<BulbState> ShowStateAsync(string labelName)
=======
        //this method returns the current states of the led bulb
        public static async Task<BulbState> ShowLightStateAsync(string labelName)
>>>>>>> a1589ce4e0ef4815b347548c668b637c199521ed
        {
            string endPoint = $"https://api.lifx.com/v1/lights/label:{labelName}";
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenKey);

            HttpResponseMessage response = await client.GetAsync(endPoint);//send http get request


            BulbState lightState = new BulbState();


            if (response.IsSuccessStatusCode)
            {
                var responsebody = await response.Content.ReadAsStreamAsync();

                List<Root> obj = await JsonSerializer.DeserializeAsync<List<Root>>(responsebody);

                lightState.Id = obj[0].id;
                lightState.UUID = obj[0].uuid;
                lightState.Label = obj[0].label;
                lightState.Connected = obj[0].connected;
                lightState.Power = obj[0].power;
                lightState.Hue = obj[0].color.hue;
                lightState.Saturation = obj[0].color.saturation;
                lightState.Brightness = obj[0].brightness;

            }
            else if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Something went wrong {response.StatusCode}");
            }

            return lightState;
        }


    }
}
