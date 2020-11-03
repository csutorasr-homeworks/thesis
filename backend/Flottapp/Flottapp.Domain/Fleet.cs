using System.Collections.Generic;
using Flottapp.Model;

namespace Flottapp.Domain
{
    public class Fleet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Car> Cars { get; private set; } = new List<Car>();
        public ICollection<AuthorizationData> Users { get; private set; } = new List<AuthorizationData>();
    }
}
