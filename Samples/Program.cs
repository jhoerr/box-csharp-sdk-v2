using System;
using System.IO;
using System.Linq;
using BoxApi.V2;
using BoxApi.V2.SDK;
using BoxApi.V2.SDK.Model;

namespace BoxApi.V2.Samples
{
	class ExerciseV2API
	{
        //
        // NOTE - The api_key and auth_token need to be changed from the ones below
        //
        private readonly static BoxAuthLayer _boxAuthLayer = new BoxAuthLayer("tb1fyy8l9g8gy2nyirri00rdsyxr5ae2", null);

		static void Main(string[] args)
		{
             // Uncomment these lines to go through the regular auth workflow
             // i.e. this application will throw up a browser for you to authenticate with box.com with
             // user name and password, and obtain an auth token.
            
            _boxAuthLayer.StartAuthentication();

            Console.WriteLine(@"Type ""OK"" and hit ENTER after successful logging onto Box...");
            while (Console.ReadLine().ToLower() != "ok")
            { }
            
            _boxAuthLayer.FinishAuthentication(ExecuteAPIMethods);

			Console.ReadLine();
		}

		private static void ExecuteAPIMethods(User user)
		{
// Instantiate a BoxManager with your api key and a user's auth token
var boxManager = new BoxManager("api_key", "auth_token");

// Get all contents of the root folder
Folder rootFolder = boxManager.GetFolder(Folder.Root);

// Find a file
var file = rootFolder.Files.Single(f => f.Name.Equals("my file.txt"));

// Change the file's name and description
file.Name = "the new name.txt";
file.Description = "the new description";

// Update the file
// A new file object is always returned with an updated ETag.
file = boxManager.Update(file);

// Create a new subfolder
Folder subfolder = boxManager.CreateFolder(Folder.Root, "my subfolder");

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
