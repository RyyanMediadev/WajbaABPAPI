﻿global using Wajba.Dtos.ItemsDtos;
global using Wajba.Models.Items;
using AutoMapper.Internal.Mappers;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Wajba.Models.CategoriesDomain;
using Wajba.Services.ImageService;

namespace Wajba.ItemServices;

public class ItemAppServices<T> : ApplicationService  
{
    private readonly IRepository<Item, int> _repository;
    private readonly IRepository<Category, int> _repository1;
    private readonly IImageService _imageService;

    public ItemAppServices(IRepository<Item, int> repository,IRepository<Category ,int> repository1, IImageService imageService)
    {
        _repository = repository;
        _repository1 = repository1;
        _imageService = imageService;
    }
    public async Task<ItemDto> CreateAsync(CreateItemDto input)
    {
        string? imageUrl = null;
        if (input.ImageUrl != null)
            imageUrl = await _imageService.UploadAsync(input.ImageUrl);
        Category category = await _repository1.FindAsync(input.CategoryId);
        if(category==null)
            return null;

        Item item = new Item()
        {
            Name = input.Name,
            Description = input.Description,
            ImageUrl = imageUrl,
            CategoryId = input.CategoryId,
            IsFeatured = input.IsFeatured,
            TaxValue = input.TaxValue,
            Price = input.Price,
            ItemType = (Enums.ItemType)input.ItemType,
            Note = input.Note,
            Status = (Enums.Status)input.status,
            IsDeleted = false,
        };
        await _repository.InsertAsync(item);
        return ObjectMapper.Map<Item, ItemDto>(item);
    }
}