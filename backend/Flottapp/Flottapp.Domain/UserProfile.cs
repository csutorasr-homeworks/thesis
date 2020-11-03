using Flottapp.Model;

namespace Flottapp.Domain
{
    public class UserProfile
    {
        public string Id { get; set; }
        public AuthorizationData AuthorizationData { get; set; }
        public string Name { get; set; }
    }
}