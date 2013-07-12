# Notice of Supersedence

Box has released an [official .Net SDK](https://github.com/box/box-windows-sdk-v2).  As such I (jhoerr) will no longer be actively developing this SDK.  Please consider migrating your application as appropriate.

# Box C# SDK (API v2)

This is a C# client implementation of the [Box v2 REST API](http://developers.box.com/docs/).  Please feel free to open an issue if you find a bug or would like to request a feature.

## License

[Creative Commons Attribution 3.0 Unported License](http://creativecommons.org/licenses/by/3.0/)

## Nuget

This client is [available on Nuget](http://nuget.org/packages/Box.v2.SDK).  There is also an [MVC-based example](https://github.com/jhoerr/box-csharp-sdk-v2.sample.oauth) of the Box OAuth2 authentication flow available.

## Release Notes

###2.0 (21 Mar 2013)
 + [On-Behalf-Of](http://developers.blog.box.com/2013/03/08/announcing-on-behalf-of-the-simplest-most-powerful-admin-api-youll-ever-use/)
 + Pagination of folder item collections
 + Improved error handling, including optional one-time retry of failed (500) requests.
 + Ability to disable shared links
 + Ability to convert enterprise users to standalone users
 + Folder sync state
 + File uploads are now directed to https://upload.box.com
 + [Bug](https://github.com/jhoerr/box-csharp-sdk-v2/issues/35): Misbehavior when creating shared link with default values. (h/t to everettevola)
 + *Breaking Change*: All `fields` have been reimplemented as enumerations specific to their type.  That is, folder operations take `FolderField`s.  This was done to more accurately specify which properties could be returned for the operation.

###1.3 (30 Jan 2013)
 + Search
 + File thumbnails
 + File/Folder path collections now provided
 + Bug: Strings are now trimmed, which prevents request problems in certain circumstances
 + Bug: File/Folder size would fail to deserialize when very large
 + Bug: Boolean values in query strings were improperly serialized
 + *Breaking Change*: File/Folder Path and PathId have been removed from the API
 + *Breaking Change*: File/Folder Size is now a double (was an int)
 
###1.2 (15 Jan 2013)
 + Now honoring the HTTP 202 <em>Retry-After</em> header when attempting to download file contents.
 + Faster performance when uploading files.

###1.1 (11 Jan 2013)
 + Bug: BoxException information is now properly saved.
 + *Breaking Change*: The BoxManager constructor signatures have changed to resolve ambigious method call errors in Visual Studio.
 + *Breaking Change*: The enterprise-level User methods have all changed to resolve an issue that could potentially lead to data loss.

###1.0 (25 Dec 2012)
 + Initial release 

## Known Issues

+ Support for several features is planned but not yet available, including Trash.
+ Long-polling of events is not currently supported due to limitations in the underlying request model.

## Usage Example

```csharp
// Instantiate a BoxManager client.
var boxManager = new BoxManager("AccessToken");

// Optionally refresh the access token (they are only good for an hour!)
// You may want to persist these new values for later use.
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
```

As an enterprise administrator you can create a client and perform Box operations [on behalf of](http://developers.blog.box.com/2013/03/08/announcing-on-behalf-of-the-simplest-most-powerful-admin-api-youll-ever-use/) another user.

```csharp
// Instantiate a BoxManager client.
var boxManager = new BoxManager("AccessToken", onBehalfOf: "user@domain.com");

// ... do stuff as that user
// ... use your power only for awesome!
```

## Copyright

(c) 2012-2013 John Hoerr, The Trustees of Indiana University
