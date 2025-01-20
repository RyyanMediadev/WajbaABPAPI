global using Wajba.Dtos.DineInTableContract;
global using Wajba.Services.QrCodeServices;
using Wajba.Models.OfferDomain;

namespace Wajba.DineIntableService;

[RemoteService(false)]
public class DineinTableAppServices : ApplicationService
{
    private readonly IRepository<DineInTable, int> _repository;
    private readonly IRepository<Branch, int> _branchrepo;
    private readonly IImageService _imageService;

    public DineinTableAppServices(IRepository<DineInTable, int> repository,
        IRepository<Branch, int> branchrepo,
                IImageService imageService)
    {
        _repository = repository;
        _branchrepo = branchrepo;
     _imageService = imageService;
    }
    public async Task<DiniINDto> CreateAsync(CreateDineIntable input)
    {
        if (await _branchrepo.FindAsync(input.BranchId) == null)
            throw new Exception("NotFound branch");
        QrcodeServices qrcodeServices = new QrcodeServices();
        string qrCodeUrl = qrcodeServices.GenerateQrCodeUrl(input.BranchId, input.Name);
        string qrCodeImage = qrcodeServices.GenerateQrCodeImage(qrCodeUrl);
        var writer = new BarcodeWriterSvg
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
                Width = 300,  // Width of the QR code
                Height = 300, // Height of the QR code
                Margin = 1    // Margin around the QR code
            }
        };
        var svgImage = writer.Write(qrCodeUrl);
        var svgContent = svgImage.Content;
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(svgContent);
        using var stream = new MemoryStream(bytes);
        string url = await _imageService.UploadAsync(stream, qrCodeUrl);
        DineInTable dineInTable = new DineInTable()
        {
            BranchId = input.BranchId,
            IsDeleted = false,
            Name = input.Name,
            Size = input.Size,
            Status = (Status)input.status,
            QrCode = qrCodeImage,
            
        };
        Branch branch =await _branchrepo.FindAsync(input.BranchId);
        DineInTable dineInTable1 = await _repository.InsertAsync(dineInTable, true);
        DiniINDto diniINDto = new DiniINDto()
        {
            BranchId = dineInTable1.BranchId,
            Id = dineInTable1.Id,
            Status = (int)dineInTable1.Status,
            Name = dineInTable1.Name,
            Size = (byte)dineInTable1.Size,
            BranchName = branch.Name,
            Phone = branch.Phone,
            Address = branch.Address,
            QrCode = dineInTable1.QrCode,
            url = url
        };
        return diniINDto;
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
        Branch branch = await _branchrepo.FindAsync(dineIntable.BranchId);
        DiniINDto diniINDto = new DiniINDto()
        {
            BranchId = dineInTable3.BranchId,
            Id = dineInTable3.Id,
            Status = (int)dineInTable3.Status,
            Name = dineInTable3.Name,
            Size = (byte)dineInTable3.Size,
            QrCode = dineInTable3.QrCode,
            Address = branch.Address,
            Phone = branch.Phone,
            BranchName = branch.Name,
        };
        return diniINDto;
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
        if (dine == null)
            throw new Exception("Not Found");
        Branch branch = await _branchrepo.GetAsync(id);
        QrcodeServices qrcodeServices = new QrcodeServices();
        string qrCodeUrl = qrcodeServices.GenerateQrCodeUrl(branch.Id, dine.Name);
        string qrCodeImage = qrcodeServices.GenerateQrCodeImage(qrCodeUrl);
        var writer = new BarcodeWriterSvg
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
                Width = 300,  // Width of the QR code
                Height = 300, // Height of the QR code
                Margin = 1    // Margin around the QR code
            }
        };
        var svgImage = writer.Write(qrCodeUrl);
        var svgContent = svgImage.Content;
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(svgContent);
        using var stream = new MemoryStream(bytes);
        string url = await _imageService.UploadAsync(stream, qrCodeUrl);

        DiniINDto diniINDto = new DiniINDto()
        {
            Name = dine.Name,
            QrCode = dine.QrCode,
            Status = (int)dine.Status,
            BranchId = dine.BranchId,
            Id = dine.Id,
            Size = (byte)dine.Size,
            BranchName = branch.Name,
            Phone = branch.Phone,
            Address = branch.Address,
            url=url
        };
        return diniINDto;
    }
    public async Task DeleteAsync(int id)
    {
        DineInTable dine = await _repository.GetAsync(id);
        if (dine == null) throw new Exception("NotFound DineTable");
        await _repository.DeleteAsync(id, true);
    }
}
