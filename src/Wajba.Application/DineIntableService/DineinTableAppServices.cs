global using Wajba.Dtos.DineInTableContract;
global using Wajba.Services.QrCodeServices;

namespace Wajba.DineIntableService;

[RemoteService(false)]
public class DineinTableAppServices : ApplicationService
{
    private readonly IRepository<DineInTable, int> _repository;
    private readonly IRepository<Branch, int> _branchrepo;

    public DineinTableAppServices(IRepository<DineInTable, int> repository,
        IRepository<Branch, int> branchrepo)
    {
        _repository = repository;
        _branchrepo = branchrepo;
    }
    public async Task<DiniINDto> CreateAsync(CreateDineIntable input)
    {
        if (await _branchrepo.FindAsync(input.BranchId) == null)
            throw new Exception("NotFound branch");
        QrcodeServices qrcodeServices = new QrcodeServices();
        string qrCodeUrl = qrcodeServices.GenerateQrCodeUrl(input.BranchId, input.Name);
        string qrCodeImage = qrcodeServices.GenerateQrCodeImage(qrCodeUrl);
        DineInTable dineInTable = new DineInTable()
        {
            BranchId = input.BranchId,
            IsDeleted = false,
            Name = input.Name,
            Size = input.Size,
            Status =(Status) input.status,
            QrCode = qrCodeImage
        };
        DineInTable dineInTable1 = await _repository.InsertAsync(dineInTable, true);
        return ObjectMapper.Map<DineInTable, DiniINDto>(dineInTable1);
    }
    public async Task<DiniINDto> UpdateAsync(int id, CreateDineIntable dineIntable)
    {
        if (await _branchrepo.FindAsync(dineIntable.BranchId) == null)
            throw new Exception("NotFound branch");
        DineInTable dineInTable1 = await _repository.FindAsync(id);
        if (dineInTable1 == null)
            throw new Exception("NotFound DineTable");
        QrcodeServices qrcodeServices = new QrcodeServices();
        dineInTable1.BranchId = dineIntable.BranchId;
        dineInTable1.Name = dineIntable.Name;
        dineInTable1.Status = (Status)dineIntable.status;
        string qrCodeUrl = qrcodeServices.GenerateQrCodeUrl(dineIntable.BranchId, dineIntable.Name);
        string qrCodeImage = qrcodeServices.GenerateQrCodeImage(qrCodeUrl);
        dineInTable1.QrCode = qrCodeImage;
        dineInTable1.IsDeleted = false;
        dineInTable1.Size = dineIntable.Size;
        dineInTable1.LastModificationTime = DateTime.UtcNow;
        DineInTable dineInTable3 = await _repository.UpdateAsync(dineInTable1, true);
        return ObjectMapper.Map<DineInTable, DiniINDto>(dineInTable3);
    }
    public async Task<PagedResultDto<DiniINDto>> GetListAsync(GetDiniTableInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        queryable = queryable
            .WhereIf(!string.IsNullOrEmpty(input.Name),
            p => p.Name.ToLower() == input.Name.ToLower())
            .WhereIf(input.Size != null, p => p.Size == input.Size)
            .WhereIf(!string.IsNullOrEmpty(input.Status)
            , p => p.Status.ToString() == input.Status)
            .WhereIf(input.BranchId.HasValue, p => p.BranchId == input.BranchId.Value);
    
        var totalCount = await AsyncExecuter.CountAsync(queryable);
        var dineInTables = await AsyncExecuter.ToListAsync(queryable
            .OrderBy(p=>p.Name)
              .PageBy(input.SkipCount, input.MaxResultCount));
        return new PagedResultDto<DiniINDto>(
      totalCount,
      ObjectMapper.Map<List<DineInTable>, List<DiniINDto>>(dineInTables)
  );
    }
    public async Task<DiniINDto> GetByIdAsync(int id)
    {
        DineInTable dine = await _repository.GetAsync(id);
        return ObjectMapper.Map<DineInTable, DiniINDto>(dine);
    }
    public async Task DeleteAsync(int id)
    {
        DineInTable dine = await _repository.GetAsync(id);
        if (dine == null) throw new Exception("NotFound DineTable");
        await _repository.DeleteAsync(id);
    }
}