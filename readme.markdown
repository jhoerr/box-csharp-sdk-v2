# box-csharp-sdk-v2

A C# client for the [Box v2 REST API](http://developers.box.com/docs/).

```csharp
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
