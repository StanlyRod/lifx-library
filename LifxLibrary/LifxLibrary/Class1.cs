﻿using RestWrapper;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;

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


        private void ExceptionsThrower(RestResponse resp)
        {
            if (resp.StatusCode == 401)
            {
                throw new Exception("The token key is not valid code");
            }
            else if (resp.StatusCode == 404)
            {
                throw new Exception("The label name did not match the light");
            }
            else if(resp.StatusCode == 400)
            {
                throw new Exception("Request was invalid");
            }
            else if(resp.StatusCode == 500 || resp.StatusCode == 502 || resp.StatusCode == 503 || resp.StatusCode == 523)
            {
                throw new Exception("Server error Something went wrong on LIFX's end");
            }
            else if(resp.StatusCode == 422)
            {
                throw new Exception("Missing or malformed value");
            }
            else if (resp.StatusCode == 429)
            {
                throw new Exception("Error Too Many Requests.");
            }
        }

        
        #region toggle bulb methods
        //synchronous method to toggle the light bulb
        public void LightToggle()
        {
             RestRequest req = new($"https://api.lifx.com/v1/lights/label:{LightLabel}/toggle", HttpMethod.Post);
             req.Headers.Add("Authorization", $"Bearer {TokenKey}");
             RestResponse resp = req.Send();

             ExceptionsThrower(resp);
        }


        

        //asynchronous method to toggle the light bulb
        public async Task LightToggleAsync()
        {
            RestRequest req = new($"https://api.lifx.com/v1/lights/label:{LightLabel}/toggle", HttpMethod.Post);
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync();
            
            ExceptionsThrower(resp);
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

            ExceptionsThrower(resp);

        }


        //Asynchronous method to change the light bulb brightness
        public async Task PutBrightnessAsync(int intensity)
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
            var csharpToJson = JsonSerializer.Serialize(payload);

            //send http request
            RestRequest req = new($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync(csharpToJson);//send data to the api

            ExceptionsThrower(resp);

        }



        //synchronous method to change the light bulb color
        public void PutColor(string colorValue)
        {
            //anonymous object type
            var payload = new
            {
                color = colorValue
            };

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            //send http request
            RestRequest req = new($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = req.Send(csharpToJson);//send data to the api

            ExceptionsThrower(resp);
        }


        //Asynchronous method to change the light bulb color
        public async Task PutColorAsync(string colorValue)
        {
            //anonymous object type
            var payload = new
            {
                color = colorValue
            };

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            //send http request
            RestRequest req = new($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync(csharpToJson);//send data to the api

            ExceptionsThrower(resp);
        }
    }
}