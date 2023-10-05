using RestWrapper;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace LifxLibrary
{
    public class LifxBulbs
    {
        private string TokenKey { get; set; }

        private string LightLabel { get; set; }

        public LifxBulbs(string tokenKey, string lightLabel) 
        {
            TokenKey = tokenKey;
            LightLabel = lightLabel;
        }

        
        #region toggle bulb methods
        //synchronous method to toggle the light bulb
        public void LightToggle()
        {
             RestRequest req = new($"https://api.lifx.com/v1/lights/label:{LightLabel}/toggle", HttpMethod.Post);
             req.Headers.Add("Authorization", $"Bearer {TokenKey}");
             RestResponse resp = req.Send();
             if (resp.StatusCode == 401)
             {
                throw new Exception($"HttpRequestError the token key is not valid code {resp.StatusCode}");
             }
             else if (resp.StatusCode == 404)
             {
                throw new Exception($"HttpRequestError the label name is not valid code {resp.StatusCode}");
             }
        }


        

        //asynchronous method to toggle the light bulb
        public async Task AsyncLightToggle()
        {
            RestRequest req = new($"https://api.lifx.com/v1/lights/label:{LightLabel}/toggle", HttpMethod.Post);
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync();
            if (resp.StatusCode == 401)
            {
               throw new Exception($"HttpRequestError the token key is not valid code {resp.StatusCode}");
            }
            else if (resp.StatusCode == 404)
            {
               throw new Exception($"HttpRequestError the label name is not valid code {resp.StatusCode}");
            }
        }
        #endregion


        //synchronous method to change the light bulb brightness
        public void PutBrightness(int intensity)
        {
            if (intensity < 0 || intensity > 100)
            {
                throw new ArgumentOutOfRangeException("Error the brightness level have to be set between 0 and 100");
            }

            double brightnessLevel = (double)intensity / 100.0;

            //anonymous object type
            var payload = new
            {
                brightness = brightnessLevel
            };

            // convert the csharp objects to json objects
            var sharpToJson = JsonSerializer.Serialize(payload);

            //send the http request
            RestRequest req = new($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = req.Send(sharpToJson);//send data to the api
        }


        //Asynchronous method to change the light bulb brightness
        public async Task AsyncPutBrightness(int intensity)
        {
            if (intensity < 0 || intensity > 100)
            {
                throw new ArgumentOutOfRangeException("Error the brightness level have to be set between 0 and 100");
            }

            double brightnessLevel = (double)intensity / 100.0;

            //anonymous object type
            var payload = new
            {
                brightness = brightnessLevel
            };

            // convert the csharp objects to json objects
            var sharpToJson = JsonSerializer.Serialize(payload);

            //send http request
            RestRequest req = new($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync(sharpToJson);//send data to the api
        }
    }
}