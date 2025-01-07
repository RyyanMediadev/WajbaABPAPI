global using Wajba.Dtos.FaqsContract;
global using Wajba.Models.FaqsDomain;

namespace Wajba.FaqService;

[RemoteService(false)]
public class FaqAppService : ApplicationService
{
    private readonly IRepository<FAQs, int> _repository;

    public FaqAppService(IRepository<FAQs, int> repository)
    {
        _repository = repository;
    }
    public async Task<FaqDto> CreateAsync(CreateFaqs input)
    {
        FAQs fAQs = new FAQs()
        {
            Answer = input.Answer,
            Question = input.Question
        };
        FAQs fAQs1 = await _repository.InsertAsync(fAQs, true);
        return ObjectMapper.Map<FAQs, FaqDto>(fAQs1);
    }
    public async Task<FaqDto> UpdateAsync(int id, UpadtefaqDto input)
    {
        FAQs fAQs = await _repository.FindAsync(id);
        if (fAQs == null)
            throw new Exception("Not Found");
        fAQs.Answer = input.Answer;
        fAQs.Question = input.Question;
        fAQs.LastModificationTime = DateTime.UtcNow;
        FAQs fAQs1 = await _repository.UpdateAsync(fAQs, true);
        return ObjectMapper.Map<FAQs, FaqDto>(fAQs1);
    }
    public async Task<FaqDto> GetByIdAsync(int id)
    {
        FAQs fAQs = await _repository.GetAsync(id);
        if (fAQs == null)
            throw new EntityNotFoundException(typeof(FAQs), id);
        return ObjectMapper.Map<FAQs, FaqDto>(fAQs);
    }
    public async Task<PagedResultDto<FaqDto>> GetListAsync(GetFaqInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var totalCount = await AsyncExecuter.CountAsync(queryable);
        var fAQs = await AsyncExecuter.ToListAsync(queryable
           //.OrderBy(input.Sorting ?? nameof(Category.Name))
           .PageBy(input.SkipCount, input.MaxResultCount));
        return new PagedResultDto<FaqDto>(
      totalCount,
      ObjectMapper.Map<List<FAQs>, List<FaqDto>>(fAQs)
  );
    }

    public async Task DeleteAsync(int id)
    {
        FAQs fAQs = await _repository.FindAsync(id);
        if (fAQs == null)
            throw new EntityNotFoundException(typeof(FAQs), id);
        await _repository.DeleteAsync(id, true);
    }
}