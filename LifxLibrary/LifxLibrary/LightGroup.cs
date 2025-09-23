using RestWrapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LifxLibrary
{
    public class LightGroup
    {
        private string TokenKey { get; set; }

        private string GroupName { get; set; }


        public LightGroup(string tokenKey, string groupName) 
        {
            TokenKey = tokenKey;
            GroupName = groupName;
        }


        //throw exceptions related with a specific http status code
        private void ExceptionsThrower(RestResponse resp)
        {
            switch (resp.StatusCode)
            {
                case 401:
                    throw new Exception("The token key is required or is invalid");
                case 404:
                    throw new Exception("The label name is missing or do not match the bulb name.");
                case 400:
                    throw new Exception("Request was invalid.");
                case 500:
                case 502:
                case 503:
                case 523:
                    throw new Exception("Server error Something went wrong on LIFX's end.");
                case 422:
                    throw new Exception("The arguments are missing or malformed value.");
                case 429:
                    throw new Exception("Error Too Many Requests.");
                case 403:
                    throw new Exception("Bad OAuth scope.");
            }
        }

        // Build json http request
        private RestRequest BuildRequest(string endpoint, HttpMethod method)
        {
            // Create the request with target URL and HTTP verb
            RestRequest req = new RestRequest(endpoint, method);

            // Body (if any) will be JSON
            req.ContentType = "application/json";

            // Bearer token authentication for the LIFX Cloud API
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");

            // Ask the server to return JSON
            req.Headers.Add("Accept", "application/json");

            return req;
        }



        #region toggle method

        //this method will perform a general Asynchronous toggle across the connected devices in a specific group
        public async Task SweepToggleAsync(double duration = 0)
        {
            //Input validation: Ensure duration is between 0 and 100 seconds
            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }

            //anonymous object type
            var payload = new
            {
                duration = duration
            };

            string url = $"https://api.lifx.com/v1/lights/group:{GroupName}/toggle";

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Post);

            using RestResponse resp = await req.SendAsync(csharpToJson);

            ExceptionsThrower(resp);
        }
        #endregion


        #region birghtness method

        //Asynchronous method to change the light bulb brightness
        public async Task PutBrightnessAsync(int intensity)
        {
            //Input validation: Ensure intensity level is between 0% and 100%
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

            string url = $"https://api.lifx.com/v1/lights/group:{GroupName}/state";

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Put);

            using RestResponse resp = await req.SendAsync(csharpToJson); //send data to the api

            ExceptionsThrower(resp);

        }

        #endregion


        #region color method

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

            string url = $"https://api.lifx.com/v1/lights/group:{GroupName}/state";

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Put);

            using RestResponse resp = await req.SendAsync(csharpToJson); //send data to the api

            ExceptionsThrower(resp);
        }

        #endregion


        #region multiuse method

        //Asynchronous multi use method to change the state of the light bulb 
        public async Task MultiUseAsync(string power = null, string color = null, double brightness = 100, double duration = 0, bool fast = false)
        {
            //Input validation: Ensure brightness is set between 0% and 100%
            if (brightness < 0 || brightness > 100)
            {
                throw new ArgumentOutOfRangeException("Error the brightness level have to be set between 0 and 100");
            }

            double brightnessLevel = (double)brightness / 100.0;

            //Input validation: Ensure duration is set between 0 and 100 seconds
            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }

            //anonymous object type
            var payload = new
            {
                power = power,
                color = color,
                brightness = brightnessLevel,
                duration = duration,
                fast = fast
            };

            //API endpoint
            string url = $"https://api.lifx.com/v1/lights/group:{GroupName}/state";

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Put);

            using RestResponse resp = await req.SendAsync(csharpToJson);//send data to the api

            ExceptionsThrower(resp);
        }

        #endregion


        #region effects methods

        // Asynchronous method that applies a breathing effect to LIFX lights.
        public async Task BreatheEffectAsync(
            string selector,
            string color,
            string from_color = null,
            double period = 1,
            int cycles = 1,
            bool persist = false,
            bool powerOn = true,
            double peak = 0.5)
        {
            //Validate required parameters
            if (string.IsNullOrWhiteSpace(selector))
                throw new ArgumentException("Selector is required.", nameof(selector));
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color is required.", nameof(color));
            if (peak < 0 || peak > 1)
                throw new ArgumentOutOfRangeException(nameof(peak), "Peak must be between 0 and 1.");

            // Lifx API endpoint for breathe effect
            string url = $"https://api.lifx.com/v1/lights/{selector}/effects/breathe";

            // Request body payload
            var payload = new
            {
                color = color,
                from_color = from_color,  // omit if null -> uses current bulb color
                period = period,
                cycles = cycles,
                persist = persist,
                power_on = powerOn,
                peak = peak
            };

            // Convert csharp object to JSON object
            var json = JsonSerializer.Serialize(payload);

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Post);

            // Send the asynchronous HTTP request with the JSON payload
            using var resp = await req.SendAsync(json);

            ExceptionsThrower(resp);
        }


        // Triggers the LIFX pulse effect, flashing between colors.
        public async Task PulseEffectAsync(
            string selector,
            string color,
            string from_color = null,
            double period = 1,
            double cycles = 1,
            bool persist = false,
            bool power_on = true)
        {
            //Validate required parameters
            if (string.IsNullOrWhiteSpace(selector))
                throw new ArgumentException("Selector is required.", nameof(selector));
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color is required.", nameof(color));
            if (period <= 0)
                throw new ArgumentOutOfRangeException(nameof(period), "Period must be > 0.");
            if (cycles < 0)
                throw new ArgumentOutOfRangeException(nameof(cycles), "Cycles must be >= 0.");

            // Lifx API endpoint for pulse effect
            var url = $"https://api.lifx.com/v1/lights/{selector}/effects/pulse";

            // Request body payload
            var payload = new
            {
                color = color,                  // required
                from_color = from_color,       // optional; null -> current bulb color
                period = period,              // seconds per cycle
                cycles = cycles,             // times to repeat
                persist = persist,          // keep last color if true
                power_on = power_on        // power on before effect
            };

            // Convert csharp object to JSON object
            var json = JsonSerializer.Serialize(payload);

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Post);

            // Send the asynchronous HTTP request with the JSON payload
            using var resp = await req.SendAsync(json);

            ExceptionsThrower(resp);
        }




        #endregion


    }
}
