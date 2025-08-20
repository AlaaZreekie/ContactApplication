namespace ContactAppBackend.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string PhoneNumber { get; set; }
        public bool Favorite { get; set; }            
    }
}
