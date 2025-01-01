global using Volo.Abp.Domain.Entities.Auditing;

namespace Wajba.Models.LanguageDomain;

public class Language : FullAuditedEntity<int>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string ImageUrl { get; set; } // Path or URL to the uploaded image
    public Status Status { get; set; }


    public Language()
    {

    }
}
