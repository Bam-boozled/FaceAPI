using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FACE_API_CONFIG
{
    class PersonConfiguration
    {
        public static string APIkey = ConfigurationManager.AppSettings["APIkey"];

    
        /// <summary>
        /// Create a person and add it to the group
        /// </summary>
        /// <param name="personGroupId">Specifying the target person group to create the person.</param>
        /// <param name="personName">Display name of the target person. The maximum length is 128.</param>
        /// <param name="personInfo">Field for user-provided data attached to a person. Size limit is 16KB.</param>
        public static async void AddPersonToGroup(string personGroupId, string personName, string personInfo)
        {

            var client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIkey);

            // Assemble the URI for the REST API Call.
            string uri = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/" + personGroupId + "/persons";

            // Assemble the json body for the request body
            string json = "{\"name\":\"" + personName + "\", \"userData\":\"" + personInfo + "\"}";
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Execute the REST API call.
            HttpResponseMessage response = await client.PostAsync(uri, content);

            // Display the Response status code. 
            Console.WriteLine("Response status: " + response.StatusCode);

        }


        /// <summary>
        /// Deletes person from group
        /// </summary>
        /// <param name="personGroupId">Specifying the person group containing the person.</param>
        /// <param name="personId">The target personId to delete.</param>
        public static async void DeletePerson(string personGroupId, string personId)
        {

            var client = new HttpClient();
            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIkey);
            // Assemble the URI for the REST API Call.
            string uri = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/" + personGroupId + "/persons/" + personId;
            //Execute the Rest API call. 
            HttpResponseMessage response = await client.DeleteAsync(uri);
            // Display the Response status code.
            Console.WriteLine("Response status: " + response.StatusCode);


        }
    }
}
