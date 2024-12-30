global using System.ComponentModel.DataAnnotations;

namespace Wajba.Dtos.CompanyContact;

public class CreateUpdateComanyDto
{
    [Required]
    public string Name { get; set; }
    [Required,EmailAddress]
    public string Email { get; set; }
    [Required,Phone]
    public string Phone { get; set; }
    [Required,Url]
    public string WebsiteURL { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string CountryCode { get; set; }
    [Required]
    public string ZipCode { get; set; }
    [Required]
    public string Address { get; set; }
    
   
    public CreateUpdateComanyDto()
    {
        
    }
}
