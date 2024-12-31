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

    public CategoryAppService(IRepository<Category, int> categoryRepository, IImageService imageService)
    {
        _categoryRepository = categoryRepository;
        _imageService = imageService;
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

    public async Task<CategoryDto> UpdateAsync(int id, CreateUpdateCategoryDto input)
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
        int totalCount = await AsyncExecuter.CountAsync(queryable);
        List<Category> items = await AsyncExecuter.ToListAsync(queryable
            .OrderBy(input.Sorting ?? nameof(Category.Name))
            .PageBy(input.SkipCount, input.MaxResultCount));
        return new PagedResultDto<CategoryDto>(
            totalCount,
            ObjectMapper.Map<List<Category>, List<CategoryDto>>(items)
        );
    }
    public async Task DeleteAsync(int id)
    {
        Category category = await _categoryRepository.GetAsync(id);
        if (category == null)
            throw new Exception("Not found");
        await _categoryRepository.DeleteAsync(id,true);
    }
}