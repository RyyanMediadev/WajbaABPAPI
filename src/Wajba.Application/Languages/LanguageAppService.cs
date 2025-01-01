global using Wajba.Dtos.Languages;
global using Wajba.Models.LanguageDomain;

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

    public async Task<PagedResultDto<LanguageDto>> GetListAsync(GetLanguageInput input)
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
        Language language = await _languageRepository.GetAsync(id);
        if (language == null)
            throw new Exception("Not found");
        return ObjectMapper.Map<Language, LanguageDto>(language);
    }

    public async Task<LanguageDto> CreateAsync(CreateUpdateLanguageDto input)
    {
        if(input.Image == null)
            throw new Exception("Image is required");
        Language language = new Language()
        {
            Code = input.Code,
            Name = input.Name,
            Status = input.Status
        };
       language.ImageUrl = await _imageUploadService.UploadAsync(input.Image); // Upload image
        await _languageRepository.InsertAsync(language, true);
        return ObjectMapper.Map<Language, LanguageDto>(language);
    }

    public async Task<LanguageDto> UpdateAsync(int id, UpdateLanguagedto input)
    {
        Language language = await _languageRepository.GetAsync(id);
        if (language == null)
            throw new Exception("Not found");
        if (input.Image == null)
            throw new Exception("Image is required");
        language.ImageUrl = await _imageUploadService.UploadAsync(input.Image);
        language.Status = input.Status;
        language.Code = input.Code;
        language.Name = input.Name;
        language.LastModificationTime = DateTime.UtcNow;
        await _languageRepository.UpdateAsync(language, true);
        return ObjectMapper.Map<Language, LanguageDto>(language);
    }

    public async Task DeleteAsync(int id)
    {
        Language language = await _languageRepository.GetAsync(id);
        if (language == null)
            throw new Exception("Not found");
        await _languageRepository.DeleteAsync(id);
    }
}