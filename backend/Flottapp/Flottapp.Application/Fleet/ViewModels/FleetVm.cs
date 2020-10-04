using System.Collections.Generic;

namespace Flottapp.Infrastucture
{
    public class FleetVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Users { get; set; }
    }
}