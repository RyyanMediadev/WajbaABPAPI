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
    public async Task<ThemesDto> CreateAsync(CreateThemesDto input)
    {
        Theme theme2=await _repository.FirstOrDefaultAsync();
        if (theme2 != null)
            throw new Exception("Theme already exists");
        Theme theme = new Theme();
        if (input.FooterLogoUrl == null || input.LogoUrl == null || input.BrowserTabIconUrl == null)
            throw new Exception("Please provide all the required fields");
        theme.FooterLogoUrl = await _imageService.UploadAsync(input.FooterLogoUrl);
        theme.LogoUrl = await _imageService.UploadAsync(input.LogoUrl);
        theme.BrowserTabIconUrl = await _imageService.UploadAsync(input.BrowserTabIconUrl);
        Theme theme1 = await _repository.InsertAsync(theme, true);
        return ObjectMapper.Map<Theme, ThemesDto>(theme1);
    }
    public async Task<ThemesDto> UpdateAsync( CreateThemesDto input)
    {
        Theme theme = await _repository.FirstOrDefaultAsync();
        if (theme == null)
            throw new Exception("Not Found");
        if (input.FooterLogoUrl == null || input.LogoUrl == null || input.BrowserTabIconUrl == null)
            throw new Exception("Please provide all the required fields");
        theme.FooterLogoUrl = await _imageService.UploadAsync(input.FooterLogoUrl);
        theme.BrowserTabIconUrl = await _imageService.UploadAsync(input.BrowserTabIconUrl);
        theme.LogoUrl = await _imageService.UploadAsync(input.LogoUrl);
        theme.LastModificationTime = DateTime.Now;
        Theme theme1 = await _repository.UpdateAsync(theme,true);
        return ObjectMapper.Map<Theme, ThemesDto>(theme1);
    }
    public async Task<ThemesDto> UpdateBrowserTabIconUrl(IFormFile formFile)
    {
        Theme theme = await _repository.FirstOrDefaultAsync();
        if (theme == null) throw new Exception("Not Found");
        theme.BrowserTabIconUrl = await _imageService.UploadAsync(formFile);
        theme.LastModificationTime = DateTime.UtcNow;
        Theme theme1 = await _repository.UpdateAsync(theme, true);
        return ObjectMapper.Map<Theme, ThemesDto>(theme1);
    }
    public async Task<ThemesDto> UpdateLogoUrl(IFormFile formFile)
    {
        Theme theme = await _repository.FirstOrDefaultAsync();
        if (theme == null) throw new Exception("Not Found");
        theme.LogoUrl = await _imageService.UploadAsync(formFile);
        theme.LastModificationTime = DateTime.UtcNow;
        Theme theme1 = await _repository.UpdateAsync(theme, true);
        return ObjectMapper.Map<Theme, ThemesDto>(theme1);
    }
    public async Task<ThemesDto> UpdateFooterLogoUrl(IFormFile formFile)
    {
        Theme theme = await _repository.FirstOrDefaultAsync();
        if (theme == null) throw new Exception("Not Found");
        theme.FooterLogoUrl = await _imageService.UploadAsync(formFile);
        theme.LastModificationTime = DateTime.UtcNow;
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
