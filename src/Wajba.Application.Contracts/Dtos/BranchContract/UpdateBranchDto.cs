namespace Wajba.Dtos.BranchContract
{
    public class UpdateBranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
    }
}
