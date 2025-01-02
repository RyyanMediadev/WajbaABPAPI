global using System.Collections.Generic;
global using System.Linq;
global using System.Linq.Dynamic.Core;
global using System.Threading.Tasks;
global using Volo.Abp;
global using Volo.Abp.Application.Dtos;
global using Volo.Abp.Application.Services;
global using Volo.Abp.Domain.Repositories;
global using Wajba.Dtos.Categories;
global using Wajba.Models.CategoriesDomain;
global using Wajba.Services.ImageService;

namespace Wajba.Categories;

[RemoteService(false)]
public class CategoryAppService : ApplicationService
{
    private readonly IRepository<Category, int> _categoryRepository;
    private readonly IImageService _imageService;
    private readonly IRepository<Item, int> _itemrepo;

    public CategoryAppService(IRepository<Category, int> categoryRepository, IImageService imageService,
        IRepository<Item, int> itemrepo)
    {
        _categoryRepository = categoryRepository;
        _imageService = imageService;
        _itemrepo = itemrepo;
    }

    public async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
    {
        if (input.Image == null)
            throw new Exception("Image is required");
        var category = new Category
        {
            Name = input.name,
            Description = input.Description,
            Status = input.status
        };
        category.ImageUrl = await _imageService.UploadAsync(input.Image);
        var insertedCategory = await _categoryRepository.InsertAsync(category, true);
        return ObjectMapper.Map<Category, CategoryDto>(insertedCategory);
    }

    public async Task<CategoryDto> UpdateAsync(int id, UpdateCategory input)
    {
        Category category = await _categoryRepository.GetAsync(id);
        if (category == null)
            throw new Exception("Not found");
        if (input.Image == null)
            throw new Exception("Image is required");
        category.ImageUrl = await _imageService.UploadAsync(input.Image);
        category.Name = input.name;
        category.Description = input.Description;
        category.Status = input.status;
        category.LastModificationTime = DateTime.UtcNow;
        Category updatedcategory = await _categoryRepository.UpdateAsync(category, true);
        return ObjectMapper.Map<Category, CategoryDto>(updatedcategory);
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        Category category = await _categoryRepository.GetAsync(id);
        if (category == null)
            throw new Exception("Not found");
        return ObjectMapper.Map<Category, CategoryDto>(category);
    }

    public async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryInput input)
    {
        IQueryable<Category> queryable = await _categoryRepository.GetQueryableAsync();
        queryable = queryable.WhereIf(
            !string.IsNullOrWhiteSpace(input.Name),
            c => c.Name.ToLower() == input.Name.ToLower()
        );
        List<Category> categories = await AsyncExecuter.ToListAsync(queryable
            .OrderBy(input.Sorting ?? nameof(Category.Name))
            .PageBy(input.SkipCount, input.MaxResultCount));
        int totalCount = await AsyncExecuter.CountAsync(queryable);
        List<CategoryDto> categoryItemsDtos = ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);
        return new PagedResultDto<CategoryDto>(
            totalCount,
            categoryItemsDtos);
    }
   public async Task<PagedResultDto<CategoryItemsDto>> GetCategoryItemsDtosAsync(int branchid)
    {
        var queryable = await _categoryRepository.WithDetailsAsync(x => x.Items);
        List<CategoryItemsDto> categoryItemsDtos = new List<CategoryItemsDto>();
        foreach (var category in queryable)
        {
            var categoryItemsDto = new CategoryItemsDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Status = category.Status,
                IsFilled = false,
                ImageUrl = category.ImageUrl,
            };
            var items = category.Items.ToList();
            foreach (var i in items)
            {
                Item item = _itemrepo.WithDetailsAsync(p => p.ItemBranches).Result.FirstOrDefault(p => p.Id == i.Id);
                var itemBranches = item.ItemBranches.ToList();
                foreach (var l in itemBranches)
                {
                    if (l.BranchId == branchid)
                        categoryItemsDto.IsFilled = true;
                }
            }
            categoryItemsDtos.Add(categoryItemsDto);
        }
        int totalCount = await AsyncExecuter.CountAsync(queryable);
        return new PagedResultDto<CategoryItemsDto>(
            totalCount,
            (IReadOnlyList<CategoryItemsDto>)categoryItemsDtos
        );
    }
    public async Task<CategoryDto> UpdateItemImage(int id, IFormFile image)
    {
        Category category = await _categoryRepository.FindAsync(id);
        if (category == null)
            throw new Exception("Not Found");
        category.ImageUrl = await _imageService.UploadAsync(image);
        category.LastModificationTime = DateTime.UtcNow;
        _categoryRepository.UpdateAsync(category, true);
        return ObjectMapper.Map<Category, CategoryDto>(category);
    }
    public async Task DeleteAsync(int id)
    {
        Category category = await _categoryRepository.GetAsync(id);
        if (category == null)
            throw new Exception("Not found");
        await _categoryRepository.DeleteAsync(id,true);
    }
}