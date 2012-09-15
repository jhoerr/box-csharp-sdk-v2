using System;

namespace BoxApi.V2.SDK.Model
{
    public enum Access
    {
        Open,
        Company, 
        Collaborators,
    }

    public static class EnumExtensions
    {
        public static string Name(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);

        }
    }
}