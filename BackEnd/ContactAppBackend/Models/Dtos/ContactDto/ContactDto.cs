namespace ContactAppBackend.Models.Dtos.ContactDto
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Favorite { get; set; }
    }
}
