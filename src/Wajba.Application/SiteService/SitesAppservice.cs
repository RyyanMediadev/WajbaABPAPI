﻿global using Wajba.Models.SiteDomain;
global using Wajba.Dtos.SitesContact;

namespace Wajba.SiteService;
[RemoteService(false)]
public class SitesAppservice : ApplicationService
{
    private readonly IRepository<Site, int> _repository;
    private readonly IRepository<Branch, int> _repository1;
    private readonly IRepository<Currencies, int> _repository2;
    private readonly IRepository<Language, int> _repository3;

    public SitesAppservice(IRepository<Site, int> repository,
        IRepository<Branch, int> repository1,
        IRepository<Currencies, int> repository2,
        IRepository<Language, int> repository3)
    {
        _repository = repository;
        _repository1 = repository1;
        _repository2 = repository2;
        _repository3 = repository3;
    }
    public async Task<SiteDto> CreateAsync(CreateSiteDto input)
    {
        Branch branch = await _repository1.FindAsync(input.BranchId);
        if (branch == null)
            throw new Exception("Branch Not Found");
        if (await _repository2.FindAsync(input.CurrencyId) == null)
            throw new Exception("Currency Not Found");
        if (await _repository3.FindAsync(input.LanguageId) == null)
            throw new Exception("Language Not Found");
        Site site = new Site()
        {
            AndroidAPPLink = input.AndroidAPPLink,
            BranchId = input.BranchId,
            CurrencyId = input.CurrencyId,
            currencyPosition = input.CurrencyPosition,
            Email = input.Email,
            GoogleMapKey = input.GoogleMapKey,
            Copyrights = input.Copyrights,
            Quantity = input.Quantity,
            Name = input.Name,
            LanguageId = input.LanguageId,
            languageSwitch = input.LanguageSwitch,
            IOSAPPLink = input.IOSAPPLink,
            IsDeleted = false,
        };
        Site site1 = await _repository.InsertAsync(site, true);
        return ObjectMapper.Map<Site, SiteDto>(site1);
    }
    public async Task<PagedResultDto<SiteDto>> GetListAsync(GetSiteInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var totalCount = await AsyncExecuter.CountAsync(queryable);
        var sites = await AsyncExecuter.ToListAsync(queryable
                       .PageBy(input.SkipCount, input.MaxResultCount));
        return new PagedResultDto<SiteDto>(
  totalCount,
  ObjectMapper.Map<List<Site>, List<SiteDto>>(sites)
);
    }
    public async Task<SiteDto> UpdateAsync(int id, CreateSiteDto input)
    {
        Site site = await _repository.GetAsync(id);
        if (site == null)
            throw new Exception("Site Not Found");
        Branch branch = await _repository1.FindAsync(input.BranchId);
        if (branch == null)
            throw new Exception("Branch Not Found");
        if (await _repository2.FindAsync(input.CurrencyId) == null)
            throw new Exception("Currency Not Found");
        if (await _repository3.FindAsync(input.LanguageId) == null)
            throw new Exception("Language Not Found");
        site.Name = input.Name;
        site.Email = input.Email;
        site.IOSAPPLink = input.IOSAPPLink;
        site.AndroidAPPLink = input.AndroidAPPLink;
        site.Copyrights = input.Copyrights;
        site.GoogleMapKey = input.GoogleMapKey;
        site.Quantity = input.Quantity;
        site.currencyPosition = input.CurrencyPosition;
        site.languageSwitch = input.LanguageSwitch;
        site.BranchId = input.BranchId;
        site.CurrencyId = input.CurrencyId;
        site.LanguageId = input.LanguageId;
        site.LastModificationTime = System.DateTime.UtcNow;
        Site site1 = await _repository.UpdateAsync(site, true);
        return ObjectMapper.Map<Site, SiteDto>(site1);
    }
    public async Task<SiteDto> GetByIdAsync(int id)
    {
        Site site = await _repository.GetAsync(id);
        return ObjectMapper.Map<Site, SiteDto>(site);
    }
    public async Task DeleteAsync(int id)
    {
        if (await _repository.FindAsync(id) == null)
            throw new Exception("Not Found");
        await _repository.DeleteAsync(id);

    }
}