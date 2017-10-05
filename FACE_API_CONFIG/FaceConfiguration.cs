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
    class FaceConfiguration
    {

        public static string APIkey = ConfigurationManager.AppSettings["APIkey"];


   
        /// <summary>
        ///  Add a face to the target perosn. 
        /// </summary>
        /// <param name="personGroupId">Specifying the target person group to create the person.</param>
        /// <param name="personId">Target person that the face is added to.</param>
        /// <param name="imageFilePath">Face image location.</param>
        public static async void AddFaceToPerson(string personGroupId, string personId, string imageFilePath)
        {

            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIkey);

            // Assemble the URI for the REST API Call.
            string uri = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/" + personGroupId + "/persons/" + personId + "/persistedFaces?";

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream". 
                // If you want to use a picture stored on the internet you should use application/json
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the Response status. 200 is ok
                Console.WriteLine("Response status: " + response.StatusCode);

            }

        }

    
        /// <summary>
        /// Deletes face from person in group
        /// </summary>
        /// <param name="personGroupId">Specifying the person group containing the person.</param>
        /// <param name="personId">Specifying the person that the target persisted face belong to.</param>
        /// <param name="persistedFaceId">The persisted face to remove. </param>
        public static async void DeleteFaceFromPerson(string personGroupId, string personId, string persistedFaceId)
        {

            var client = new HttpClient();
            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIkey);
            // Assemble the URI for the REST API Call.
            string uri = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/" + personGroupId + "/persons/" + personId + "/persistedFaces/" + persistedFaceId;
            // Execute the REST API call.
            HttpResponseMessage response = await client.DeleteAsync(uri);
            // Display the Response status code.
            Console.WriteLine("Response status: " + response.StatusCode);


        }


        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">location of .jpg file</param>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
}
