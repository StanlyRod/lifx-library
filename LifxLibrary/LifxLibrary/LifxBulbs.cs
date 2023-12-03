using System;
using System.Net.Http;
using System.Threading.Tasks;
using RestWrapper;
using System.Text.Json;
//LifxLibrary a .NET library to control lifx smart led bulbs over the cloud
//author: Ramon Stanly Rodriguez

namespace LifxLibrary
{
    public class LifxBulbs
    {
        private string TokenKey { get; set; }

        private string LightLabel { get; set; }


        public LifxBulbs(string tokenKey) 
        {
            TokenKey = tokenKey;
        }

        public LifxBulbs(string tokenKey, string lightLabel)
        {
            TokenKey = tokenKey;
            LightLabel = lightLabel;
        }


        //throw exceptions related with specific http status code
        private void ExceptionsThrower(RestResponse resp)
        {
            if (resp.StatusCode == 401)
            {
                throw new Exception("The token key is not valid.");
            }
            else if (resp.StatusCode == 404)
            {
                throw new Exception("The label name did not match the light.");
            }
            else if (resp.StatusCode == 400)
            {
                throw new Exception("Request was invalid.");
            }
            else if (resp.StatusCode == 500 || resp.StatusCode == 502 || resp.StatusCode == 503 || resp.StatusCode == 523)
            {
                throw new Exception("Server error Something went wrong on LIFX's end.");
            }
            else if (resp.StatusCode == 422)
            {
                throw new Exception("Missing or malformed value.");
            }
            else if (resp.StatusCode == 429)
            {
                throw new Exception("Error Too Many Requests.");
            }
            else if (resp.StatusCode == 403)
            {
                throw new Exception("Bad OAuth scope.");
            }
            else
            {
                throw new Exception($"Error - Unhandled status code: {resp.StatusCode}");
            }
        }



        #region toggle bulb methods
        //synchronous method to toggle the light bulb
        public void LightToggle(double duration = 0)
        {
            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }

            var payload = new
            {
                duration = duration
            };

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            RestRequest req = new RestRequest($"https://api.lifx.com/v1/lights/label:{LightLabel}/toggle", HttpMethod.Post);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = req.Send(csharpToJson);

            ExceptionsThrower(resp);

        }



        //asynchronous method to toggle the light bulb
        public async Task LightToggleAsync(double duration = 0)
        {
            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }

            var payload = new
            {
                duration = duration
            };

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            RestRequest req = new RestRequest($"https://api.lifx.com/v1/lights/label:{LightLabel}/toggle", HttpMethod.Post);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync(csharpToJson);

            ExceptionsThrower(resp);
        }



        //this method will perform a general synchronous toggle across the connected devices
        public void SweepToggle(double duration = 0)
        {
            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }

            var payload = new
            {
                duration = duration
            };

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            RestRequest req = new RestRequest("https://api.lifx.com/v1/lights/all/toggle", HttpMethod.Post);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = req.Send(csharpToJson);

            ExceptionsThrower(resp);
        }



        //this method will perform a general Asynchronous toggle across the connected devices
        public async Task SweepToggleAsync(double duration = 0)
        {
            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }

            var payload = new
            {
                duration = duration
            };

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            RestRequest req = new RestRequest("https://api.lifx.com/v1/lights/all/toggle", HttpMethod.Post);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync(csharpToJson);

            ExceptionsThrower(resp);
        }
        #endregion


        #region set brightness methods
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
            var csharpToJson = JsonSerializer.Serialize(payload);

            //send the http request
            RestRequest req = new RestRequest($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = req.Send(csharpToJson);//send data to the api

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
            RestRequest req = new RestRequest($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync(csharpToJson);//send data to the api

            ExceptionsThrower(resp);

        }
        #endregion


        #region set color methods
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
            RestRequest req = new RestRequest($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
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
            RestRequest req = new RestRequest($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync(csharpToJson);//send data to the api

            ExceptionsThrower(resp);
        }
        #endregion


        #region multiuse methods
        //synchronous multi use method to change the state of the light bulb 
        public void MultiUse(string power = null, string color = null, double brightness = 100, double duration = 0, bool fast = false)
        {
            if (brightness < 0 || brightness > 100)
            {
                throw new ArgumentOutOfRangeException("Error the brightness level have to be set between 0 and 100");
            }

            double brightnessLevel = (double)brightness / 100.0;

            if(duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }
            

            //double durationTime = (double)duration / 100.0;

            //anonymous object type
            var payload = new
            {
                power = power,
                color = color,
                brightness = brightnessLevel,
                duration = duration,
                fast = fast
            };

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            //send http request
            RestRequest req = new RestRequest($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = req.Send(csharpToJson);//send data to the api

            ExceptionsThrower(resp);
        }


        //Asynchronous multi use method to change the state of the light bulb 
        public async Task MultiUseAsync(string power = null, string color = null, double brightness = 100, double duration = 0, bool fast = false)
        {
            if (brightness < 0 || brightness > 100)
            {
                throw new ArgumentOutOfRangeException("Error the brightness level have to be set between 0 and 100");
            }

            double brightnessLevel = (double)brightness / 100.0;

            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }


            //double durationTime = (double)duration / 100.0;

            //anonymous object type
            var payload = new
            {
                power = power,
                color = color,
                brightness = brightnessLevel,
                duration = duration,
                fast = fast
            };

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            //send http request
            RestRequest req = new RestRequest($"https://api.lifx.com/v1/lights/label:{LightLabel}/state", HttpMethod.Put);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync(csharpToJson);//send data to the api

            ExceptionsThrower(resp);
        }
        #endregion


    }
}
