using System;
using System.IO;
using System.Linq;
using BoxApi.V2.SDK;
using BoxApi.V2.SDK.Model;

namespace BoxApi.V2.Samples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string apiKey = "YOUR_API_KEY";
            const string authToken = "USER_AUTH_TOKEN";
            
            ExecuteAPIMethods(apiKey, authToken);
        }

        private static void ExecuteAPIMethods(string apiKey, string authToken)
        {
            // Instantiate a BoxManager with your api key and a user's auth token
            var boxManager = new BoxManager(apiKey, authToken);

            // Get all contents of the root folder
            var rootFolder = boxManager.GetFolder(Folder.Root);

            // Find a file
            var file = rootFolder.Files.Single(f => f.Name.Equals("my file.txt"));

            // Change the file's name and description
            file.Name = "the new name.txt";
            file.Description = "the new description";

            // Update the file
            // A new file object is always returned with an updated ETag.
            file = boxManager.Update(file);

            // Create a new subfolder
            var subfolder = boxManager.CreateFolder(Folder.Root, "my subfolder");

            // Move the file to the subfolder
            file = boxManager.Move(file, subfolder);

            // Write some content to the file
            file = boxManager.Write(file, new byte[] {1, 2, 3});

            // Read the contents to a stream
            using (var stream = new MemoryStream())
            {
                boxManager.Read(file, stream);
            }

            // Delete the file
            boxManager.Delete(file);
        }
    }
}