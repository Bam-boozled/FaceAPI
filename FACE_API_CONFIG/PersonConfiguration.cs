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

        /*
         * AddPersonToGroup(string personGroupId, string personName, string personInfo)
         * Create a person and add it to the group. 
         
            parameters:

            personGroupId : Specifying the target person group to create the person.
            personName: 	Display name of the target person. The maximum length is 128.
            personInfo: 	Field for user-provided data attached to a person. Size limit is 16KB.

            returns:

            A successful call returns a new personId created. 

                 
        */
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


        /*  
          DeletePerson(string personGroupId, string personId)
          Deletes person from group

          parameters:

          personGroupId : Specifying the person group containing the person.
          personId:       The target personId to delete.

          returns:

          A successful call returns an empty response body

      */

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
