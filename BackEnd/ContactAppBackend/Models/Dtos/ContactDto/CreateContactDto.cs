namespace ContactAppBackend.Models.Dtos.ContactDto
{
    public class CreateContactDto
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string PhoneNumber { get; set; }
        public bool Favorite { get; set; } = true;
    }
}
