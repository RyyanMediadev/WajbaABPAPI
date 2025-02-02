﻿global using System.Collections.Generic;
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
        if (input.Model == null)
            throw new Exception("Image is required");
        var category = new Category
        {
            Name = input.name,
            Description = input.Description,
            Status = (Status)input.status
        };
        var imagebytes = Convert.FromBase64String(input.Model.Base64Content);
        using var ms = new System.IO.MemoryStream(imagebytes);
        category.ImageUrl = await _imageService.UploadAsync(ms, input.Model.FileName);
        var insertedCategory = await _categoryRepository.InsertAsync(category, true);
        return ObjectMapper.Map<Category, CategoryDto>(insertedCategory);
    }

    public async Task<CategoryDto> UpdateAsync(int id, UpdateCategory input)
    {
        Category category = await _categoryRepository.GetAsync(id);
        if (category == null)
            throw new Exception("Not found");
        if (input.Model == null)
            throw new Exception("Image is required");
        var imagebytes = Convert.FromBase64String(input.Model.Base64Content);
        using var ms = new MemoryStream(imagebytes);
        category.ImageUrl = await _imageService.UploadAsync(ms, input.Model.FileName);
        category.Name = input.name;
        category.Description = input.Description;
        category.Status = (Status)input.status;
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

    public async Task<PagedResultDto<CategoryItemsDto>> GetListAsync(GetCategoryInput input)
    {
        var queryable = await _categoryRepository.WithDetailsAsync(p => p.Items);
        if (queryable == null)
            throw new EntityNotFoundException(typeof(Category));
        if (input.Name != null)
            queryable = queryable.Where(p => p.Name.ToLower() == input.Name.ToLower());
        List<CategoryItemsDto> categoryItemsDtos = new List<CategoryItemsDto>();
        int countitems = 0;
        foreach(var category in queryable)
        {
            var categoryItemsDto = new CategoryItemsDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Status = (int)category.Status,
                ImageUrl = category.ImageUrl,
                IsFilled = false
            };
            if (input.BranchId == null)
            {
                categoryItemsDto.IsFilled = category.Items.Count > 0;
                categoryItemsDto.TotalItems = category.Items.Count;
                categoryItemsDtos.Add(categoryItemsDto);
                continue;
            }
            var items = category.Items.ToList();
            foreach (var i in items)
            {
                Item item =  _itemrepo.WithDetailsAsync(p => p.ItemBranches).Result.FirstOrDefault(p => p.Id == i.Id);
                var itemBranches = item.ItemBranches.ToList();
                if (itemBranches.Any(p => p.BranchId == input.BranchId))
                {
                    categoryItemsDto.IsFilled = true;
                    countitems++;
                }
            }
            categoryItemsDto.TotalItems = countitems;
            countitems= 0;
            categoryItemsDtos.Add(categoryItemsDto);
        }
        //queryable = queryable.WhereIf(
        //    !string.IsNullOrWhiteSpace(input.Name),
        //    c => c.Name.ToLower() == input.Name.ToLower()
        //);
        int totalCount = queryable.Count();
        categoryItemsDtos = categoryItemsDtos.OrderBy(p => p.Name).
            Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToList();
        return new PagedResultDto<CategoryItemsDto>(totalCount, categoryItemsDtos);
        //.OrderBy(input.Sorting ?? nameof(Category.Name))
        //.Skip(input.SkipCount)
        //.Take(input.MaxResultCount)
        //.ToList();
        //List <Category> categories = await AsyncExecuter.ToListAsync(queryable
        //    .OrderBy(input.Sorting ?? nameof(Category.Name))
        //    .PageBy(input.SkipCount, input.MaxResultCount));
        //int totalCount = await AsyncExecuter.CountAsync(queryable);
        //List<CategoryDto> categoryItemsDtos = ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);
        //return new PagedResultDto<CategoryDto>(
        //    totalCount,
        //    categoryItemsDtos);
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
                Status = (int)category.Status,
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
    public async Task<CategoryDto> UpdateItemImage(int id, Base64ImageModel model)
    {
        Category category = await _categoryRepository.FindAsync(id);
        if (category == null)
            throw new EntityNotFoundException(typeof(Category), id);
        if (model == null)
            throw new Exception("Image is required");
        var imagebytes = Convert.FromBase64String(model.Base64Content);
        using var ms = new MemoryStream(imagebytes);
        category.ImageUrl = await _imageService.UploadAsync(ms, model.FileName);
        category.LastModificationTime = DateTime.UtcNow;
        await _categoryRepository.UpdateAsync(category, true);
        return ObjectMapper.Map<Category, CategoryDto>(category);
    }
    public async Task DeleteAsync(int id)
    {
        Category category = await _categoryRepository.GetAsync(id);
        if (category == null)
            throw new EntityNotFoundException(typeof(Category), id);
        await _categoryRepository.DeleteAsync(id, true);
    }
}