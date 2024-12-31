global using Wajba.Models.LanguageDomain;
global using Wajba.Dtos.Languages;
using Volo.Abp.ObjectMapping;

namespace Wajba.Languages;

[RemoteService(false)]
public class LanguageAppService : ApplicationService
{
    private readonly IRepository<Language, int> _languageRepository;
    private readonly IImageService _imageUploadService; // Assume this handles image uploads

    public LanguageAppService(IRepository<Language, int> languageRepository,
        IImageService imageUploadService)
    {
        _languageRepository = languageRepository;
        _imageUploadService = imageUploadService;
    }

    public async Task<PagedResultDto<LanguageDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _languageRepository.GetQueryableAsync();
        var items = await AsyncExecuter.ToListAsync(query
            .OrderBy(input.Sorting ?? nameof(Language.Name))
            .PageBy(input.SkipCount, input.MaxResultCount));
        var totalCount = await AsyncExecuter.CountAsync(query);
        return new PagedResultDto<LanguageDto>(
            totalCount,
            ObjectMapper.Map<List<Language>, List<LanguageDto>>(items)
        );
    }

    public async Task<LanguageDto> GetAsync(int id)
    {
        var language = await _languageRepository.GetAsync(id);
        return ObjectMapper.Map<Language, LanguageDto>(language);
    }

    public async Task<LanguageDto> CreateAsync(CreateUpdateLanguageDto input)
    {
        string imageUrl = await _imageUploadService.UploadAsync(input.Image); // Upload image
        var language = ObjectMapper.Map<CreateUpdateLanguageDto, Language>(input);
        language.ImageUrl = imageUrl;

        await _languageRepository.InsertAsync(language);
        return ObjectMapper.Map<Language, LanguageDto>(language);
    }

    public async Task<LanguageDto> UpdateAsync(int id, CreateUpdateLanguageDto input)
    {
        var language = await _languageRepository.GetAsync(id);
        if (input.Image != null)
        {
            string imageUrl = await _imageUploadService.UploadAsync(input.Image);
            language.ImageUrl = imageUrl;
        }

        ObjectMapper.Map(input, language); // Map other properties

        await _languageRepository.UpdateAsync(language);
        return ObjectMapper.Map<Language, LanguageDto>(language);
    }

    public async Task DeleteAsync(int id)
    {
        await _languageRepository.DeleteAsync(id);
    }
}