# Box C# SDK (API v2)

## About

This is a C# client implementation of the [Box v2 REST API](http://developers.box.com/docs/).  Please feel free to open an issue if you find a bug or would like to request a feature.

## License

[Creative Commons Attribution 3.0 Unported License](http://creativecommons.org/licenses/by/3.0/)

## Nuget

This client is [available on Nuget](http://nuget.org/packages/Box.v2.SDK).  There is also an [MVC-based example](http://nuget.org/packages/Box.v2.SDK.Sample.Oauth2) of the Box OAuth2 authentication flow available.

## Known Issues

Due to limitations in the underlying request model, long-polling of events is not currently supported.

## Usage Example

```csharp
// Instantiate a BoxManager with your api key and a user's auth token
var boxManager = new BoxManager("ClientId", "ClientSecret", "AccessToken", "RefreshToken");

// Optionally refresh the access token (they are only good for an hour!)
// You can persist these new values for later use.
var refreshedAccessToken = boxManager.RefreshAccessToken();

// Create a new file in the root folder
boxManager.CreateFile(Folder.Root, "a new file.txt", Encoding.UTF8.GetBytes("hello, world!"));
            
// Fetch the root folder
var folder = boxManager.GetFolder(Folder.Root);

// Find a 'mini' representation of the created file among the root folder's contents
var file = folder.Files.Single(f => f.Name.Equals("a new file.txt"));

// Get the file with all properties populated.
file = boxManager.Get(file);

// Rename the file
file = boxManager.Rename(file, "the new name.txt");

// Create a new subfolder
var subfolder = boxManager.CreateFolder(Folder.Root, "my subfolder");

// Move the file to the subfolder
file = boxManager.Move(file, subfolder);

// Write some content to the file
using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("goodbye, world!")))
{
    file = boxManager.Write(file, stream);
}

// Read the contents to a stream and write them to the console
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
