namespace Wajba.Dtos.Languages;

public class LanguageDto : EntityDto<int>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Image { get; set; }
    public Status Status { get; set; }
}
