

namespace Wajba.Dtos.UserDTO
{
    public class UserBackofficeDTO
    {


        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SocondName { get; set; }
        [Required]
        public string ThirdName { get; set; }
        [Required]
        public string FamilyName { get; set; }
        //Uniqe Properties
        [Required]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public int? MedicalOrganizationId { get; set; }
        public int? NationalityId { get; set; }

        public int? CountryId { get; set; }

        public bool IsActive { get; set; }
        public int AccountTypeId { get; set; }
    }
}
