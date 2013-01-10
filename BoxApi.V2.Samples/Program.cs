using System;
using System.IO;
using System.Linq;
using System.Text;
using BoxApi.V2.Authentication.OAuth2;
using BoxApi.V2.Model;
using File = BoxApi.V2.Model.File;

namespace BoxApi.V2.Samples
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ExecuteAPIMethods("YOUR CLIENT ID", "YOUR CLIENT SECRET", "YOUR ACCESS TOKEN", "YOUR REFRESH TOKEN");
        }

        private static void ExecuteAPIMethods(string clientId, string clientSecret, string accessToken, string refreshToken)
        {
            try
            {
                // Optionally refresh the user's access token
                var authenticator = new TokenProvider(clientId, clientSecret);
                var oAuthToken = authenticator.RefreshAccessToken(refreshToken);
                accessToken = oAuthToken.AccessToken;

                // Instantiate a BoxManager with your api key and a user's auth token
                var boxManager = new BoxManager(new OAuth2RequestAuthenticator(accessToken));

                // Create a new file in the root folder
                boxManager.CreateFile(Folder.Root, "a new file.txt", Encoding.UTF8.GetBytes("hello, world!"));

                // Fetch the root folder
                Folder folder = boxManager.GetFolder(Folder.Root);

                // Find a 'mini' representation of the created file among the root folder's contents
                File file = folder.Files.Single(f => f.Name.Equals("a new file.txt"));

                // Get the file with all properties populated.
                file = boxManager.Get(file);

                // Rename the file
                file = boxManager.Rename(file, "the new name.txt");

                // Create a new subfolder
                Folder subfolder = boxManager.CreateFolder(Folder.Root, "my subfolder");

                // Move the file to the subfolder
                file = boxManager.Move(file, subfolder);

                // Write some content to the file
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("goodbye, world!")))
                {
                    file = boxManager.Write(file, stream);
                }

                // Read the contents to a stream
                using (var stream = new MemoryStream())
                {
                    boxManager.Read(file, stream);
                    using (var reader = new StreamReader(stream))
                    {
                        stream.Position = 0;
                        Console.Out.WriteLine("File content: '{0}'", reader.ReadToEnd());
                    }
                }

                // Delete the folder and its contents
                boxManager.Delete(subfolder, recursive: true);

            }
            catch (BoxException e)
            {
                Console.Out.WriteLine(e);
            }
        }
    }
}