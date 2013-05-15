namespace BoxApi.V2
{
    /// <summary>
    /// Methods available to Box standalone users
    /// </summary>
    public interface IUserManager : IFile, IFolder, IStandaloneUser, ICollaboration, ISharedItems, IComment, IDiscussion
    {
    }
}