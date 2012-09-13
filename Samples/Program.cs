using System;
using BoxApi.V2;
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
           # region Folder API
            
            // GET /folders/0
            _boxAuthLayer._manager.GetFolder("0");

            // POST /folders/0
            int new_folder_id = _boxAuthLayer._manager.CreateFolder(0, "MyNewFolder");

            // PUT /folders/1234
            _boxAuthLayer._manager.UpdateFolder(new_folder_id, "MyUpdatedFolder");

            // DELETE /folders/1234
            _boxAuthLayer._manager.DeleteFolder(new_folder_id);
                         
            #endregion

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
            
            // Get all file comments - GET /files/2026759912/comments
            _boxAuthLayer._manager.GetFileComments(2026759912);
             
            // Get a specific comment - GET /comments/8536883
            _boxAuthLayer._manager.GetComment(8536883);

            // Delete a comment - DELETE /comments/8536883
            _boxAuthLayer._manager.DeleteComment(8536883);
            
            // Update a comment - PUT /comments/8537019
            _boxAuthLayer._manager.UpdateComment(8537019, "changed the comment!");
            */

            #endregion

            #region Discussions API
            /*
            // Get Discussions for a folder - GET /folders/259009302/discussions
            _boxAuthLayer._manager.GetDiscussions(259009302);

            // Delete discussion - DELETE /discussions/402617
            _boxAuthLayer._manager.DeleteDiscussion(402617);

            // Get all comments in discussion - GET /discussions/402603/comments
            _boxAuthLayer._manager.GetDiscussionComments(402603);

            // Post a comment in discussion - POST /discussions/402603/comments
            _boxAuthLayer._manager.PostCommentInDiscussion(402603, "great comment!");

            // Update discussion name - PUT /discussions/402603
            _boxAuthLayer._manager.UpdateDiscussionName(402603, "new discussion name");
            */

            #endregion

            #region Events API
            /*
            // GET /events
            _boxAuthLayer._manager.GetEvents();
             */
 
            #endregion

        }
	}
}
