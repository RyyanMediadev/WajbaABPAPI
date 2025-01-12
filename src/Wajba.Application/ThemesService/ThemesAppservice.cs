global using Wajba.Dtos.ThemesContract;
global using Wajba.Models.ThemesDomain;

namespace Wajba.ThemesService;

[RemoteService(false)]
public class ThemesAppservice : ApplicationService
{
	private readonly IRepository<Theme, int> _repository;
	private readonly IImageService _imageService;

	public ThemesAppservice(IRepository<Theme, int> repository, IImageService imageService)
	{
		_repository = repository;
		_imageService = imageService;
	}

	public async Task<ThemesDto> CreateAsync(CreateThemesDto themesDto)
	{
		Theme theme = await _repository.FirstOrDefaultAsync();
		if (theme != null)
			throw new Exception("Theme already exists");
		theme = new Theme();
		if (themesDto.FooterLogoUrl == null || themesDto.LogoUrl == null || themesDto.BrowserTabIconUrl == null)
			throw new Exception("Please provide all the required fields");
		var imagebytes = Convert.FromBase64String(themesDto.FooterLogoUrl.Base64Content);
		using var ms = new MemoryStream(imagebytes);
		theme.FooterLogoUrl = await _imageService.UploadAsync(ms, themesDto.FooterLogoUrl.FileName);
		imagebytes = Convert.FromBase64String(themesDto.LogoUrl.Base64Content);
		using var ms1 = new MemoryStream(imagebytes);
		theme.LogoUrl = await _imageService.UploadAsync(ms1, themesDto.LogoUrl.FileName);
		imagebytes = Convert.FromBase64String(themesDto.BrowserTabIconUrl.Base64Content);
		using var ms2 = new MemoryStream(imagebytes);
		theme.BrowserTabIconUrl = await _imageService.UploadAsync(ms2, themesDto.BrowserTabIconUrl.FileName);
		theme.LastModificationTime = DateTime.Now;
		Theme theme1 = await _repository.InsertAsync(theme, true);
		return ObjectMapper.Map<Theme, ThemesDto>(theme1);
	}
	public async Task<ThemesDto> UpdateAsync(CreateThemesDto themesDto)
	{
		Theme theme = await _repository.FirstOrDefaultAsync();
		if (theme == null)
			throw new Exception("Not Found");
		if (themesDto.FooterLogoUrl == null || themesDto.LogoUrl == null || themesDto.BrowserTabIconUrl == null)
			throw new Exception("Please provide all the required fields");
		var FooterLogoUrl = Convert.FromBase64String(themesDto.FooterLogoUrl.Base64Content);
		var BrowserTabIconUrl = Convert.FromBase64String(themesDto.BrowserTabIconUrl.Base64Content);
		var LogoUrl = Convert.FromBase64String(themesDto.LogoUrl.Base64Content);
		using var ms = new MemoryStream(FooterLogoUrl);
		using var ms1 = new MemoryStream(BrowserTabIconUrl);
		using var ms2 = new MemoryStream(LogoUrl);
		theme.FooterLogoUrl = await _imageService.UploadAsync(ms, themesDto.FooterLogoUrl.FileName);
		theme.BrowserTabIconUrl = await _imageService.UploadAsync(ms1, themesDto.BrowserTabIconUrl.FileName);
		theme.LogoUrl = await _imageService.UploadAsync(ms2, themesDto.LogoUrl.FileName);
		theme.LastModificationTime = DateTime.Now;
		Theme theme1 = await _repository.UpdateAsync(theme, true);
		return ObjectMapper.Map<Theme, ThemesDto>(theme1);
	}
	public async Task<ThemesDto> GetByIdAsync()
	{
		Theme theme1 = await _repository.FirstOrDefaultAsync();
		if (theme1 == null)
			throw new Exception("Not Found");
		return ObjectMapper.Map<Theme, ThemesDto>(theme1);
	}
	public async Task<PagedResultDto<ThemesDto>> GetListAsync(GetThemeInput input)
	{
		var queryable = await _repository.GetQueryableAsync();
		var totalCount = await AsyncExecuter.CountAsync(queryable);
		var themes = await AsyncExecuter.ToListAsync(queryable
		   //.OrderBy(input.Sorting ?? nameof(Category.Name))
		   .PageBy(input.SkipCount, input.MaxResultCount));
		return new PagedResultDto<ThemesDto>(
	  totalCount,
	  ObjectMapper.Map<List<Theme>, List<ThemesDto>>(themes)
  );
	}
	public async Task DeleteAsync()
	{
		Theme theme = await _repository.FirstOrDefaultAsync();
		if (theme == null)
			throw new Exception("Not Found");
		await _repository.DeleteAsync(theme, true);
	}
}