using BoxApi.V2.Model.Enum;

namespace BoxApi.V2.Model
{
    public class UserEntity : Entity
    {
        public UserEntity()
        {
            Type = ResourceType.User;
        }

        /// <summary>
        ///     The login/email address of the user
        /// </summary>
        public string Login { get; set; }
    }
}