using System;
using System.Net.Http;
using System.Threading.Tasks;
using RestWrapper;
using System.Text.Json;
//LifxLibrary a .NET library to control lifx led bulbs over the cloud
//author: Ramon Stanly Rodriguez

namespace LifxLibrary
{
    public class LifxBulbs
    {
        private string TokenKey { get; set; }

        private string SelectorLabel { get; set; }


        public LifxBulbs(string tokenKey)
        {
            TokenKey = tokenKey;
        }

        public LifxBulbs(string tokenKey, string selectorlabel)
        {
            TokenKey = tokenKey;
            SelectorLabel = selectorlabel;
        }


        //throw exceptions related with a specific http status code
        private void ExceptionsThrower(RestResponse resp)
        {
            switch (resp.StatusCode)
            {
                case 401:
                    throw new HttpRequestException("The token key is required or is invalid");
                case 404:
                    throw new HttpRequestException("The label name is missing or do not match the bulb name or group name.");
                case 400:
                    throw new HttpRequestException("Request was invalid.");
                case 500:
                case 502:
                case 503:
                case 523:
                    throw new HttpRequestException("Server error Something went wrong on LIFX's end.");
                case 422:
                    throw new HttpRequestException("The arguments are missing or malformed value.");
                case 429:
                    throw new HttpRequestException("Error Too Many Requests.");
                case 403:
                    throw new HttpRequestException("Bad OAuth scope.");
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



        #region toggle bulb methods

        //asynchronous method to toggle the light bulb
        public async Task LightToggleAsync(double duration = 0)
        {
            //Input validation: ensure duration is set between 0 and 100 seconds
            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }

            //anonymous object type
            var payload = new
            {
                duration = duration
            };

            //API endpoint
            string url = $"https://api.lifx.com/v1/lights/{SelectorLabel}/toggle";

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Post);

            using RestResponse resp = await req.SendAsync(csharpToJson);

            ExceptionsThrower(resp);
        }


        //This method will perform a general Asynchronous toggle across the connected devices
        public async Task SweepToggleAsync(double duration = 0)
        {
            //Validation input: ensure duration is set between 0 and 100 seconds
            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }

            //anonymous object type
            var payload = new
            {
                duration = duration
            };

            // API endpoint
            string url = "https://api.lifx.com/v1/lights/all/toggle";

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Post);

            using RestResponse resp = await req.SendAsync(csharpToJson);

            ExceptionsThrower(resp);
        }
        #endregion


        #region power bulb method


        //asynchronous method to change power state of the light bulb
        public async Task PutPowerAsync(string power, double duration = 0)
        {
            // Input validation: Ensure duration is between 0 and 100 seconds
            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }

            // anonymous object type
            var payload = new
            {
                power = power,
                duration = duration
            };

            // API endpoint
            string url = $"https://api.lifx.com/v1/lights/{SelectorLabel}/state";

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Put);

            using RestResponse resp = await req.SendAsync(csharpToJson);

            ExceptionsThrower(resp);
        }

        #endregion


        #region set brightness method


        //Asynchronous method to change the light bulb brightness
        public async Task PutBrightnessAsync(int intensity)
        {
            // Input validation: Ensure intensity is between 0% and 100%
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

            // API endpoint
            string url = $"https://api.lifx.com/v1/lights/{SelectorLabel}/state";

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Put);

            using RestResponse resp = await req.SendAsync(csharpToJson); //send data to the api

            ExceptionsThrower(resp);

        }
        #endregion


        #region set color method

        //Asynchronous method to change the light bulb color
        public async Task PutColorAsync(string colorValue)
        {
            //anonymous object type
            var payload = new
            {
                color = colorValue
            };

            // API endpoint
            string url = $"https://api.lifx.com/v1/lights/{SelectorLabel}/state";

            // convert the csharp objects to json objects
            var csharpToJson = JsonSerializer.Serialize(payload);

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
            // Validate brightness parameter is within acceptable range (0-100%)
            if (brightness < 0 || brightness > 100)
            {
                throw new ArgumentOutOfRangeException("Error the brightness level have to be set between 0 and 100");
            }

            // Convert brightness from percentage (0-100) to decimal format (0.0-1.0) for LIFX API
            double brightnessLevel = (double)brightness / 100.0;

            // Input validation: Ensure brightness is between 0 and 100 seconds
            if (duration < 0 || duration > 100)
            {
                throw new ArgumentOutOfRangeException("Error the duration time have to be set between 0 and 100 seconds");
            }

            // API endpoint
            string url = $"https://api.lifx.com/v1/lights/{SelectorLabel}/state";

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

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Put);

            using RestResponse resp = await req.SendAsync(csharpToJson);//send data to the api

            ExceptionsThrower(resp);
        }
        #endregion


        #region effects methods

        // Asynchronous method that applies a breathing effect to LIFX lights.
        public async Task BreatheEffectAsync(
            string color,
            string from_color = null,
            double period = 1,
            int cycles = 1,
            bool persist = false,
            bool powerOn = true,
            double peak = 0.5)
        {
            //Validate required parameters
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color is required.", nameof(color));
            if (peak < 0 || peak > 1)
                throw new ArgumentOutOfRangeException(nameof(peak), "Peak must be between 0 and 1.");

            // Lifx API endpoint for breathe effect
            string url = $"https://api.lifx.com/v1/lights/{SelectorLabel}/effects/breathe";

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
            string color,
            string from_color = null,
            double period = 1,
            double cycles = 1,
            bool persist = false,
            bool power_on = true)
        {
            //Validate required parameters
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color is required.", nameof(color));
            if (period <= 0)
                throw new ArgumentOutOfRangeException(nameof(period), "Period must be > 0.");
            if (cycles < 0)
                throw new ArgumentOutOfRangeException(nameof(cycles), "Cycles must be >= 0.");

            // Lifx API endpoint for pulse effect
            var url = $"https://api.lifx.com/v1/lights/{SelectorLabel}/effects/pulse";

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


        //Turns off any running effects on the device. 
        public async Task EffectsOffAsync(string selector, bool powerOff = false)
        {
            //validate token
            if (string.IsNullOrWhiteSpace(TokenKey))
                throw new ArgumentException("Token is required.", nameof(TokenKey));
            //validate selector
            if (string.IsNullOrWhiteSpace(selector))
                throw new ArgumentException("Selector is required.", nameof(selector));

            //API endpoint
            string url = $"https://api.lifx.com/v1/lights/{selector}/effects/off";

            // Request body payload
            var payload = new
            {
                power_off = powerOff
            };

            // Build the http request with headers
            string json = JsonSerializer.Serialize(payload);

            // Build the http request with headers
            using var req = BuildRequest(url, HttpMethod.Post);

            using RestResponse resp = await req.SendAsync(json);

            ExceptionsThrower(resp);

        }
        #endregion
    }
}