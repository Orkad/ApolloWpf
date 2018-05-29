namespace ApolloWpf.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public UserGroup Group { get; set; }
        public int GroupId { get; set; }
    }
}
