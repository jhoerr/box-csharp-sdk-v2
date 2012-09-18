using System;
using BoxApi.V2.SDK;
using BoxApi.V2.SDK.Model;

namespace BoxApi.V2.Samples
{
    internal class ExerciseV2API
    {
        //
        // NOTE - The api_key and auth_token need to be changed from the ones below
        //
        private static void Main(string[] args)
        {
            var boxManager = new BoxManager("shh8qvbfbv53vsbmtmvkzdydbf9vvcu1", null);
            var authToken = boxManager.GetAppAuthTokenForUser("carsongross@gmail.com");
            Console.WriteLine("Auth token : " + authToken);
        }

        private static void ExecuteAPIMethods(User user)
        {
            # region Files API

            /*
            // GET /files/2026759912
            _boxAuthLayer._manager.GetFileInfo(2026759912, 0);

            // GET /files/2026759912?version=1
            _boxAuthLayer._manager.GetFileInfo(2026759912, 1);

            // GET /files/2026759912?version=2
            _boxAuthLayer._manager.GetFileInfo(2026759912, 2);

            // POST /files/2026759912/copy
            // SERVER-NOT-WORKING _boxAuthLayer._manager.CopyFile(2026759912, 0);

            // Rename file: PUT /files/2027059362
            // SERVER-NOT-WORKING _boxAuthLayer._manager.RenameFile(2027059362, "newFileName");

            // Update description: PUT /files/2027059362
            // SERVER-NOT-WORKING _boxAuthLayer._manager.UpdateDescription(2027059362, "newDescription");

            // DELETE /files/2027059362
            // SERVER-NOT-WORKING _boxAuthLayer._manager.DeleteFile(2027059362, 0);

            // DELETE /files/2027059362/versions/1
            // SERVER-NOT-WORKING _boxAuthLayer._manager.DeleteFile(2027059362, 1);

            // Download a file's data - GET /files/123/data
            _boxAuthLayer._manager.GetFileData(2027059362, 0);

            // Download a file's data - GET /files/123/versions/1
            _boxAuthLayer._manager.GetFileData(2027059362, 1);
             
            // Upload a new file - POST /files/data
            string fileData = "test line 1";
            // SERVER-NOT-WORKING _boxAuthLayer._manager.CreateFile(fileData);

            // Upload a new version of a file - POST /files/1234/data
            // SERVER-NOT-WORKING _boxAuthLayer._manager.UploadFile(2026759912, fileData);
            */

            #endregion

            #region Comments API
            /*
            // Post a comment - POST /files/2026759912/comments
            _boxAuthLayer._manager.PostComment(2026759912, "absolutely new comment");
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
            */
            #endregion
        }
    }
}