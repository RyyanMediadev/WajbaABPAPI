namespace Wajba.Models.CompanyDomain;

public class Company:FullAuditedEntity<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string WebsiteURL { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string CountryCode { get; set; }
    public string ZipCode { get; set; }
    public string Address { get; set; }
  
   // public ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public Company()
    {
        
    }
}
