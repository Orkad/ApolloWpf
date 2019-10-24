using System.Collections.Generic;

namespace Apollo.Sample.Model
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
