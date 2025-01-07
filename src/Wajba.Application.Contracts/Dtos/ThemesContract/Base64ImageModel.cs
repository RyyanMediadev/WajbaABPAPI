namespace Wajba.Dtos.ThemesContract;

public class Base64ImageModel : EntityDto<int>
{
 //   public int Id { get; set; }
	public string FileName { get; set; }
	public string Base64Content { get; set; }

}
