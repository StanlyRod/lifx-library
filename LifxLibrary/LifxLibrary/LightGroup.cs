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
                    throw new Exception("The token key is not valid.");
                case 404:
                    throw new Exception("The group name is not valid.");
                case 400:
                    throw new Exception("Request was invalid.");
                case 500:
                case 502:
                case 503:
                case 523:
                    throw new Exception("Server error Something went wrong on LIFX's end.");
                case 422:
                    throw new Exception("Missing or malformed value.");
                case 429:
                    throw new Exception("Error Too Many Requests.");
                case 403:
                    throw new Exception("Bad OAuth scope.");
            }
        }



        #region toggle methods
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

            RestRequest req = new RestRequest($"https://api.lifx.com/v1/lights/group:{GroupName}/toggle", HttpMethod.Post);
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

            RestRequest req = new RestRequest($"https://api.lifx.com/v1/lights/group:{GroupName}/toggle", HttpMethod.Post);
            req.ContentType = "application/json";
            req.Headers.Add("Authorization", $"Bearer {TokenKey}");
            RestResponse resp = await req.SendAsync(csharpToJson);

            ExceptionsThrower(resp);
        }
        #endregion

    }
}
