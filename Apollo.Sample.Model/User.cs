namespace Apollo.Sample.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserGroup Group { get; set; }
        public int GroupId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
