using System.Linq;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Fields;

namespace BoxApi.V2.Extensions
{
    public static class BoxManagerFolderExtensions
    {
        /// <summary>
        /// Returns the total number of items in this folder and all folders contained therein.  A folder is considered as a distinct item.  If folder 'A' contains one file and one subfolder, it contains two items.
        /// </summary>
        /// <param name="boxManager">The BoxManager with which to perform the count.</param>
        /// <param name="folderId">The ID of the folder to count.</param>
        /// <returns>The total number of items in this folder and all folders contained therein</returns>
        public static long TotalItemsInFolder(this BoxManager boxManager, string folderId)
        {
            Folder folder = boxManager.GetFolder(folderId, new[] {FolderField.ItemCollection});
            return folder.ItemCollection.TotalCount + folder.Folders.Sum(f => TotalItemsInFolder(boxManager, f.Id));
        }
    }
}