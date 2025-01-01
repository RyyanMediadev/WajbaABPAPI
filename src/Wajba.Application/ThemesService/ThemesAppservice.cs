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
        Theme theme = new Theme();
        if (input.FooterLogoUrl != null)
            theme.FooterLogoUrl = await _imageService.UploadAsync(input.FooterLogoUrl);
        if (input.LogoUrl != null)
            theme.LogoUrl = await _imageService.UploadAsync(input.LogoUrl);
        if (input.BrowserTabIconUrl != null)
            theme.BrowserTabIconUrl = await _imageService.UploadAsync(input.BrowserTabIconUrl);
        Theme theme1 = await _repository.InsertAsync(theme, true);
        return ObjectMapper.Map<Theme, ThemesDto>(theme1);
    }
    public async Task<ThemesDto> UpdateAsync(int id, CreateThemesDto input)
    {
        Theme theme = await _repository.GetAsync(id);
        if (theme == null)
            return null;
        theme.FooterLogoUrl = await _imageService.UploadAsync(input.FooterLogoUrl);
        theme.BrowserTabIconUrl = await _imageService.UploadAsync(input.BrowserTabIconUrl);
        theme.LogoUrl = await _imageService.UploadAsync(input.LogoUrl);
        Theme theme1 = await _repository.UpdateAsync(theme,true);
        return ObjectMapper.Map<Theme, ThemesDto>(theme1);
    }
    public async Task<ThemesDto> GetByIdAsync(int id)
    {
        Theme theme1 = await _repository.GetAsync(id);
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
    public async Task DeleteAsync(int id)
    {
        if (await _repository.FindAsync(id) == null)
            throw new Exception("Not Found");
        await _repository.DeleteAsync(id);
    }
}
