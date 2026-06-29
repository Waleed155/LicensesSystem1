namespace Licenses.Dto.ClientsDto
{
    public class ClientReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string FirstPhoneNumber { get; set; }
        public string? SecondPhoneNumber { get; set; }
    }
}
