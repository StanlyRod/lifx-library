using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Drawing;
using System.Reflection.Emit;
using System.Xml.Linq;
using System;

namespace LifxLibrary
{
   
    
        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        public class Capabilities
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

        public class Color
        {
            public int hue { get; set; }
            public int saturation { get; set; }
            public int kelvin { get; set; }
        }

        public class Group
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Location
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Product
        {
            public string name { get; set; }
            public string identifier { get; set; }
            public string company { get; set; }
            public int vendor_id { get; set; }
            public int product_id { get; set; }
            public Capabilities capabilities { get; set; }
        }

        public class Root
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
            public DateTime last_seen { get; set; }
            public int seconds_since_seen { get; set; }
        }

        
        public class BulbState
        {
           public string id { get; set; }
           public string uuid { get; set; }
           public string label { get; set; }
           public bool connected { get; set; }
           public string power { get; set; }
           public int hue { get; set; }
           public int saturation { get; set; }
           public double brightness { get; set; }
           
        }


    
    public class LightsSearcher
    {

        private string TokenKey { get; set; }
       

        public LightsSearcher(string tokenKey)
        {
            TokenKey = tokenKey;
        }


        //this method returns an integer of all connected devices
        public async Task<List<string>> CountDevices()
        {
            string endPoint = "https://api.lifx.com/v1/lights/all";
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenKey);
            
            HttpResponseMessage response = await client.GetAsync(endPoint);

            //string totalDevices = "";
            List<string> namesco = new List<string>();

            if (response.IsSuccessStatusCode)
            {
                var responsebody = await response.Content.ReadAsStreamAsync();

                List<Root> obj = await JsonSerializer.DeserializeAsync<List<Root>>(responsebody);

                
                foreach(var value in obj[0].label)
                {
                    if(value.Equals("label"))
                    {
                        namesco.Add(value.ToString());
                    }
                }
                
              
            }
            else if(!response.IsSuccessStatusCode)
            {
                throw new Exception($"Something went wrong {response.StatusCode}");
            }

            return namesco;
        }







        //this method returns the lights label name
        public async Task<List<string>> GetLightsNames()
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






        public async Task<BulbState> ShowLightState(string labelName)
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

                lightState.id = obj[0].id;
                lightState.uuid = obj[0].uuid;
                lightState.label = obj[0].label;
                lightState.connected = obj[0].connected;
                lightState.power = obj[0].power;
                lightState.hue = obj[0].color.hue;
                lightState.saturation = obj[0].color.saturation;
                lightState.brightness = obj[0].brightness;
                    
            }
            else if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Something went wrong {response.StatusCode}");
            }

            return lightState;
        }








        /*public async Task<int> CountDevices()
        {
            string endPoint = "https://api.lifx.com/v1/lights/all";
            RestRequest req = new RestRequest(endPoint);
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse response = await req.SendAsync();

            

            Root obj = await JsonSerializer.DeserializeAsync<Root>(await response.);
            int label = obj.label.Count();
            return label;
        }*/
    }

}