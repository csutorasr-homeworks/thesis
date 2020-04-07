using System;
using System.Collections.Generic;

namespace Flottapp.Domain
{
    public class Fleet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Car> Cars { get; private set; } = new List<Car>();
        public ICollection<User> Users { get; set; }
    }
}
