global using Wajba.CartService;
global using Wajba.WajbaUsersService;
global using Wajba.Dtos.CartContract;
global using Wajba.Models.WajbaUserDomain;

namespace Wajba.Controllers;

public class CartController : WajbaController
{
    private readonly CartAppService _CartAppService;
    private readonly WajbaUsersAppservice _wajbaUsers;

    public CartController(CartAppService CartAppService,
   WajbaUsersAppservice wajbaUsers
        )
    {
        _CartAppService = CartAppService;

        _wajbaUsers = wajbaUsers;
    }

    [HttpPost("add-item-to-cart")]
    public async Task<IActionResult> AddCartItem(List<CartItemDto> cartItemDto)
    {
        if (!ModelState.IsValid)
        {
            return Ok(new { success = false, message = ModelState });
        }

        string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
        {
            return Ok(new { success = false, message = "Token is required" });
        }
        WajbaUser customer = await _wajbaUsers.Decodetoken(token);
        if (customer == null)
        {
            return Ok(new { success = false, message = "Invalid token or customer not found" });
        }
        try
        {
            await _CartAppService.CreateAsync(customer.Id, cartItemDto);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Cart created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating cart: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpGet("GetCarforcustomer")]
    public async Task<IActionResult> GetCart()
    {
        string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
        {
            return Ok(new { success = false, message = "Token is required" });
        }
        WajbaUser customer = await _wajbaUsers.Decodetoken(token);
        if (customer == null)
        {
            return Ok(new { success = false, message = "Invalid token or customer not found" });
        }



        return null;
    }
    //    [HttpPost("Ordernow")]
    //[Authorize]
    //public async Task<ActionResult> Ordernow(int id)
    //{
    //    string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    ApplicationUser customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }
    //    Order order = await _CartAppService.GetOrderByIdAsync(id);
    //    if (order == null || order.CustomerId != customerId)
    //    {
    //        return Ok(new { success = false, message = "Order not found." });
    //    }
    //    Cart existingCart = await _CartAppService.GetCartByCustomeridAsync(customerId);
    //    if (existingCart == null)
    //    {
    //        existingCart = new Cart()
    //        {
    //            CustomerId = customerId,
    //            CartItems = new List<CartItem>(),
    //            Note = ""
    //        };
    //        await _CartAppService.CreateAsync(existingCart);
    //        await _unitOfWork.CompleteAsync();
    //    }
    //    foreach (var i in order.OrderItems)
    //    {
    //        CartItem existingItem = existingCart.CartItems.FirstOrDefault(p => p.ItemId == i.ItemId);
    //        ItemDto item = await _ItemAppServices.GetByIdAsync(i.ItemId);
    //        PopularItem popularItem = await _PopularItemAppservice.GetpopItemByitemId(item.Id);
    //        if (existingItem != null)
    //        {
    //            existingItem.Quantity += i.Quantity;
    //            existingItem.Notes = i.Notes;
    //            existingItem.price = item.Price;
    //        }
    //        else
    //        {
    //            existingItem = new CartItem()
    //            {
    //                CartId = existingCart.Id,
    //                ItemName = i.Item.Name,
    //                price = i.Item.Price,
    //                ImgUrl = item.imageUrl,
    //                Notes = i.Notes,
    //            };
    //        }
    //        existingItem.SelectedVariations.Clear();
    //        if (i.SelectedVariations != null)
    //        {
    //            foreach (var variationDto in i.SelectedVariations)
    //            {
    //                var variation = new CartItemVariation()
    //                {
    //                };
    //                existingCart.SubTotal += variation.AdditionalPrice;
    //                existingItem.SelectedVariations.Add(variation);
    //            }
    //        }
    //        existingItem.SelectedAddons.Clear();
    //        if (i.SelectedAddons != null)
    //        {
    //            foreach (var addonDto in i.SelectedAddons)
    //            {
    //                var addon = new CartItemAddon()
    //                {
    //                };
    //                existingCart.SubTotal += addon.AdditionalPrice;
    //                existingItem.SelectedAddons.Add(addon);
    //            }
    //        }

    //        existingItem.SelectedExtras.Clear();
    //        if (i.SelectedExtras != null)
    //        {
    //            foreach (var extraDto in i.SelectedExtras)
    //            {
    //                var extra = new CartItemExtra()
    //                {
    //                };
    //                existingCart.SubTotal += extra.AdditionalPrice;
    //                existingItem.SelectedExtras.Add(extra);
    //            }
    //        }
    //        existingCart.SubTotal += existingCart.CartItems.Sum(i => i.Quantity * i.price);
    //        existingCart.TotalAmount = existingCart.SubTotal + existingCart.DeliveryFee +
    //      existingCart.ServiceFee + existingCart.DiscountAmount;
    //        existingCart.CartItems.Add(existingItem);
    //        await _unitOfWork.CompleteAsync();
    //    }
    //    await _unitOfWork.CompleteAsync();

    //    return Ok(new { success = true, message = "order added to cart successfully", data = "" });
    //}
    //[HttpPost("update-cartitem-quantity")]
    //public async Task<IActionResult> UpdateCartItemQuantity([FromBody] UpdateCartItemQuantityDTO dto)
    //{
    //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    var customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }
    //    var cart = await _CartAppService.GetCartByCustomerIdAsync(customerId);
    //    if (cart == null)
    //    {
    //        return Ok(new { success = false, message = "Cart not found for this customer." });
    //    }
    //    var cartItem = await _CartAppService.GetCartItemByCustomerAndItemIdAsync(customerId, dto.cartItemId);
    //    if (cartItem == null)
    //    {
    //        return Ok(new { success = false, message = "Cart item not found in your cart." });
    //    }
    //    var newQuantity = dto.quantityChange;
    //    if (newQuantity < 0)
    //    {
    //        return Ok(new { success = false, message = "Quantity cannot be reduced below zero." });
    //    }
    //    cartItem.Quantity = newQuantity;
    //    decimal newTotalAmount = 0;
    //    var cartItems = await _CartAppService.GetCartItemsByCustomerIdAsync(customerId);
    //    foreach (var item in cartItems)
    //    {
    //        decimal itemTotalPrice = 0;
    //        if (item.SelectedVariations != null)
    //        {
    //            foreach (var variation in item.SelectedVariations)
    //            {
    //                itemTotalPrice += variation.AdditionalPrice;
    //            }
    //        }
    //        if (item.SelectedExtras != null)
    //        {
    //            foreach (var extra in item.SelectedExtras)
    //            {
    //                itemTotalPrice += extra.AdditionalPrice;
    //            }
    //        }
    //        if (item.SelectedAddons != null)
    //        {
    //            foreach (var addon in item.SelectedAddons)
    //            {
    //                itemTotalPrice += addon.AdditionalPrice;
    //            }
    //        }
    //        newTotalAmount += item.Quantity * itemTotalPrice;
    //    }
    //    cart.SubTotal = newTotalAmount;
    //    cart.TotalAmount = cart.SubTotal + cart.ServiceFee + cart.DeliveryFee;



    //    await _unitOfWork.cartItems.UpdateAsync(cartItem);
    //    await _unitOfWork.carts.UpdateAsync(cart);
    //    await _unitOfWork.CompleteAsync();
    //    return Ok(new
    //    {
    //        success = true,
    //        message = "Cart item quantity updated",
    //        Data = new
    //        {
    //            cartItem.Id,
    //            cartItem.Quantity,
    //            cartItem.price,
    //            totalCartAmount = newTotalAmount
    //        }
    //    });
    //}

    //[HttpPost("apply-voucher-code")]
    //public async Task<IActionResult> ApplyVoucherCode([FromBody] ApplyVoucherDto applyVoucherDto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return Ok(new { success = false, message = "Invalid data", errors = ModelState });
    //    }
    //    string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    ApplicationUser customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }
    //    var voucherUsage = await _context.CustomerVoucherUsages
    //        .FirstOrDefaultAsync(vu => vu.CustomerId == customerId && vu.VoucherCode == applyVoucherDto.Code);
    //    if (voucherUsage != null)
    //    {
    //        return Ok(new { success = false, message = "You have already applied this voucher code." });
    //    }
    //    Cart existingCart = await _CartAppService.GetCartByCustomerIdAsync(customerId);
    //    if (existingCart == null)
    //    {
    //        return Ok(new { success = false, message = "Cart not found for this customer" });
    //    }
    //    var coupon = (await _couponAppService.GetListAsync())
    //       .FirstOrDefault(c => c.code == applyVoucherDto.Code);
    //    if (coupon == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid coupon code" });
    //    }
    //    if (coupon.StartDate.HasValue && coupon.StartDate.Value > DateTime.UtcNow)
    //    {
    //        return Ok(new { success = false, message = "Coupon is not yet valid" });
    //    }
    //    if (coupon.EndDate.HasValue && coupon.EndDate.Value < DateTime.UtcNow)
    //    {
    //        return Ok(new { success = false, message = "Coupon has expired" });
    //    }
    //    var totalForCart = existingCart.SubTotal;
    //    // Check if the minimum order amount is met
    //    if (totalForCart < coupon.MinimumOrderAmount)
    //    {
    //        return Ok(new { success = false, message = $"Minimum order amount for the coupon is {coupon.MinimumOrderAmount:C}" });
    //    }

    //    Calculate discount
    //        decimal discountAmount = 0;
    //    if (coupon.discountType == DiscountType.Percentage)
    //    {
    //        discountAmount = (existingCart.TotalAmount.Value * coupon.Discount) / 100;
    //    }
    //    else if (coupon.discountType == DiscountType.Fixed)
    //    {
    //        discountAmount = coupon.Discount;
    //    }
    //    if (discountAmount > coupon.MaximumDiscount)
    //    {
    //        discountAmount = coupon.MaximumDiscount;
    //    }
    //    existingCart.TotalAmount -= discountAmount;
    //    existingCart.voucherCode = coupon.code;
    //    existingCart.DiscountAmount = discountAmount;
    //    var customerVoucherUsage = new CustomerVoucherUsage()
    //    {
    //        CustomerId = customerId,
    //        VoucherCode = coupon.code,
    //        DiscountAmount = discountAmount,
    //        AppliedDate = DateTime.UtcNow
    //    };
    //    await _context.CustomerVoucherUsages.AddAsync(customerVoucherUsage);
    //    existingCart.voucherCode = discountAmount;
    //    await _CartAppService.UpdateCartAsync(existingCart);
    //    await _unitOfWork.CompleteAsync();
    //    var totalAmount = 0m;
    //    var cartDto = new ReturnCartDto()
    //    {
    //        Items = existingCart.CartItems.Select(item =>
    //        {
    //            decimal itemTotalPrice = 0;
    //            if (item.SelectedVariations != null)
    //                itemTotalPrice += item.SelectedVariations.Sum(v => v.AdditionalPrice);
    //            if (item.SelectedAddons != null)
    //                itemTotalPrice += item.SelectedAddons.Sum(a => a.AdditionalPrice);
    //            if (item.SelectedExtras != null)
    //                itemTotalPrice += item.SelectedExtras.Sum(e => e.AdditionalPrice);
    //            decimal itemFinalPrice = itemTotalPrice * item.Quantity;
    //            totalAmount += itemFinalPrice;
    //            return new ReturnCartItemDto()
    //            {
    //                price = itemFinalPrice,/* item.price+ itemFinalPrice,*/
    //                Variations = item.SelectedVariations?.Select(v => new ReturnCartItemVariationDto()
    //                {

    //                }).ToList(),
    //                Addons = item.SelectedAddons?.Select(a => new ReturnCartItemAddonDto()
    //                {

    //                }).ToList(),
    //                Extras = item.SelectedExtras?.Select(e => new ReturnExtraDto()
    //                {

    //                }).ToList()
    //            };
    //        }
    //       ).ToList(),
    //    };
    //    cartDto.SubTotal = totalAmount;
    //    cartDto.TotalAmount = cartDto.SubTotal + cartDto.ServiceFee + cartDto.DeliveryFee - cartDto.DiscountAmount;
    //    cartDto.DiscountAmount = coupon.Discount;
    //    cartDto.TotalAmount = totalAmount + existingCart.ServiceFee + existingCart.DeliveryFee - existingCart.DiscountAmount;
    //    return Ok(new { success = true, message = "Voucher code applied successfully", data = cartDto });
    //}
    //[HttpPost("checkout")]
    //public async Task<IActionResult> Checkout(CheckoutRequestDTO dto)
    //{
    //    extract user data from token
    //     Get token from headers
    //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    var customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }
    //    var cart = await _CartAppService.GetCartByCustomerIdAsync(customerId);
    //    if (cart == null)
    //        return Ok(new { success = false, message = "Cart not found", data = cart });
    //    decimal totalAmount = 0;
    //    var cartDto = new ReturnCartDto()
    //    {
    //        Note = dto.Note,
    //        Items = cart.CartItems.Select(item =>
    //        {
    //            decimal itemTotalPrice = 0;
    //            if (item.SelectedVariations != null)
    //            {
    //                itemTotalPrice += item.SelectedVariations.Sum(v => v.AdditionalPrice);
    //            }
    //            if (item.SelectedAddons != null)
    //            {
    //                itemTotalPrice += item.SelectedAddons.Sum(a => a.AdditionalPrice);
    //            }
    //            if (item.SelectedExtras != null)
    //            {
    //                itemTotalPrice += item.SelectedExtras.Sum(e => e.AdditionalPrice);
    //            }
    //            decimal itemFinalPrice = itemTotalPrice * item.Quantity;
    //            totalAmount += itemFinalPrice;
    //            return new ReturnCartItemDto()
    //            {
    //                price = itemFinalPrice,
    //                Variations = item.SelectedVariations?.Select(v => new ReturnCartItemVariationDto()
    //                {
    //                }).ToList(),
    //                Addons = item.SelectedAddons?.Select(a => new ReturnCartItemAddonDto()
    //                {
    //                }).ToList(),
    //                Extras = item.SelectedExtras?.Select(e => new ReturnExtraDto()
    //                {
    //                }).ToList()
    //            };
    //        }).ToList()
    //    };
    //    cartDto.DiscountAmount = 0m;
    //    var voucherUsage = await _context.CustomerVoucherUsages
    //        .FirstOrDefaultAsync(vu => vu.CustomerId == customerId);
    //    if (voucherUsage != null)
    //    {
    //        cartDto.DiscountAmount = voucherUsage.DiscountAmount;
    //    }
    //    cartDto.SubTotal = totalAmount;
    //    cartDto.TotalAmount = cartDto.SubTotal + cartDto.ServiceFee - cartDto.DiscountAmount + cartDto.DeliveryFee;
    //    return Ok(new { success = true, message = "Checkout complete", cartDto });
    //}
    //[Authorize]
    //[HttpGet("get-cart")]
    //public async Task<IActionResult> GetCartByCustomer()
    //{
    //    Get token from headers
    //   var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    var customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }
    //    var cart = await _CartAppService.GetCartByCustomerIdAsync(customerId);
    //    if (cart == null)
    //        return Ok(new { success = false, message = "Cart not found", data = cart });
    //    decimal totalAmount = 0;
    //    var cartDto = new ReturnCartDto()
    //    {
    //        Items = cart.CartItems.Select(item =>
    //        {
    //            decimal itemTotalPrice = 0;
    //            if (item.SelectedVariations != null)
    //            {
    //                itemTotalPrice += item.SelectedVariations.Sum(v => v.AdditionalPrice);
    //            }
    //            if (item.SelectedAddons != null)
    //            {
    //                itemTotalPrice += item.SelectedAddons.Sum(a => a.AdditionalPrice);
    //            }
    //            if (item.SelectedExtras != null)
    //            {
    //                itemTotalPrice += item.SelectedExtras.Sum(e => e.AdditionalPrice);
    //            }
    //            decimal itemFinalPrice = itemTotalPrice * item.Quantity;
    //            totalAmount += itemFinalPrice;
    //            return new ReturnCartItemDto()
    //            {
    //                price = itemFinalPrice,/* item.price+ itemFinalPrice,*/
    //                Variations = item.SelectedVariations?.Select(v => new ReturnCartItemVariationDto()
    //                {
    //                }).ToList(),
    //                Addons = item.SelectedAddons?.Select(a => new ReturnCartItemAddonDto()
    //                {

    //                }).ToList(),
    //                Extras = item.SelectedExtras?.Select(e => new ReturnExtraDto()
    //                {
    //                }).ToList()
    //            };
    //        }
    //    ).ToList(),
    //    };
    //    cartDto.SubTotal = totalAmount;
    //    var voucherUsage = await CustomerVoucherUsages
    //        .FirstOrDefaultAsync(vu => vu.CustomerId == customerId);
    //    cartDto.DiscountAmount = voucherUsage?.DiscountAmount ?? 0m;
    //    cartDto.TotalAmount = cartDto.SubTotal + cartDto.ServiceFee + cartDto.DeliveryFee - cartDto.DiscountAmount;
    //    return Ok(new { success = true, data = cartDto });
    //}

    //[HttpPut("update-item/{cartItemId}")]
    //public async Task<IActionResult> UpdateCartItem(int cartItemId, [FromBody] UpdateCartItemDto cartItemDto)
    //{
    //    if (!ModelState.IsValid)
    //        return Ok(ModelState);
    //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    //var customer = await ValidateTokenAndGetUser(token);
    //    //if (customer == null)
    //    //{
    //    //    return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    //}
    //    // Get the cart associated with the customer
    //    var cart = await _CartAppService.GetCartByCustomerIdAsync(customerId);
    //    if (cart == null)
    //    {
    //        return Ok(new { success = false, message = "Cart not found", data = cart });
    //    }

    //    // Find the cart item to update
    //    var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
    //    if (cartItem == null)
    //    {
    //        return Ok(new { success = false, message = "Cart item not found in the cart" });
    //    }

    //    // Update the cart item properties
    //    cartItem.ItemId = cartItemDto.ItemId;
    //    cartItem.ItemName = cartItemDto.ItemName;
    //    cartItem.ImgUrl = cartItemDto.ImgUrl;
    //    cartItem.Quantity = cartItemDto.Quantity;
    //    cartItem.Notes = cartItemDto.Notes;
    //    cartItem.price = cartItemDto.price;

    //    // Update variations
    //    cartItem.SelectedVariations.Clear();
    //    if (cartItemDto.Variations != null)
    //    {
    //        foreach (var variationDto in cartItemDto.Variations)
    //        {
    //            var variation = new CartItemVariation()
    //            {
    //            };
    //            cartItem.SelectedVariations.Add(variation);
    //        }
    //    }
    //    // Update addons
    //    cartItem.SelectedAddons.Clear();
    //    if (cartItemDto.Addons != null)
    //    {
    //        foreach (var addonDto in cartItemDto.Addons)
    //        {
    //            var addon = new CartItemAddon()
    //            {
    //            };
    //            cartItem.SelectedAddons.Add(addon);
    //        }
    //    }
    //    // Update extras
    //    cartItem.SelectedExtras.Clear();
    //    if (cartItemDto.Extras != null)
    //    {
    //        foreach (var extraDto in cartItemDto.Extras)
    //        {
    //            var extra = new CartItemExtra()
    //            {
    //            };
    //            cartItem.SelectedExtras.Add(extra);
    //        }
    //    }
    //    //  await _unitOfWork.CompleteAsync();
    //    var updatedCartItemDto = new ReturnCartItemDto()
    //    {
    //        Variations = cartItem.SelectedVariations.Select(v => new ReturnCartItemVariationDto()
    //        {
    //        }).ToList(),
    //        Addons = cartItem.SelectedAddons.Select(a => new ReturnCartItemAddonDto()
    //        {
    //        }).ToList(),
    //        Extras = cartItem.SelectedExtras.Select(e => new ReturnExtraDto()
    //        {
    //        }).ToList()
    //    };
    //    return Ok(new { success = true, message = "Cart item updated successfully", data = updatedCartItemDto });
    //}

    //[HttpDelete("delete-item-from-cart/{cartItemId}")]
    //public async Task<IActionResult> DeleteCartItem(int cartItemId)
    //{
    //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    //var customer = await ValidateTokenAndGetUser(token);
    //    //if (customer == null)
    //    //{
    //    //    return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    //}
    //    var cartItem = await _CartAppService.GetCartItemByCustomerAndItemIdAsync(customerId, cartItemId);
    //    if (cartItem == null)
    //    {
    //        return Ok(new { success = false, message = "Cart item not found in your cart." });
    //    }
    //    var amountOfDeletedItem = cartItem.Quantity * cartItem.price;
    //    await _CartAppService.RemoveAsync(cartItem);
    //    var cart = await _CartAppService.GetCartByCustomerIdAsync(customerId);
    //    if (cart == null)
    //    {
    //        return Ok(new { success = false, message = "Cart not found for this customer.", data = cart });
    //    }
    //    cart.SubTotal -= amountOfDeletedItem;
    //    cart.TotalAmount -= amountOfDeletedItem;
    //    await _CartAppService.UpdateCartAsync(cart);
    //    //await _unitOfWork.CompleteAsync();
    //    return Ok(new { success = true, message = "Item removed from cart successfully" });
    //}

    //[HttpDelete("clear-cart")]
    //public async Task<IActionResult> ClearCart()
    //{
    //    Get token from headers
    //   var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    var customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }
    //    var cart = await _CartAppService.GetCartByCustomerIdAsync(customerId);
    //    if (cart == null)
    //        return Ok(new { success = false, message = "Cart not found", data = cart });
    //    cart.CartItems.Clear();
    //    await _CartAppService.RemoveAsync(cart);
    //    await _unitOfWork.CompleteAsync();
    //    return Ok(new { success = true, message = "Cart cleared", cart });
    //}
    //private async Task<ApplicationUser> ValidateTokenAndGetUser(string token)
    //{
    //    var principal = GetPrincipalFromToken(token);
    //    if (principal == null)
    //    {
    //        return null;
    //    }
    //    var userIdClaim = principal.FindFirst("Id");
    //    if (userIdClaim == null || principal.FindFirst("UserType").Value != "Customer")
    //    {
    //        return null;
    //    }
    //    var userId = userIdClaim.Value;
    //    return await _usermanager.FindByIdAsync(userId);
    //}

    //private ClaimsPrincipal GetPrincipalFromToken(string token)
    //{
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
    //    try
    //    {
    //        return tokenHandler.ValidateToken(token, new TokenValidationParameters
    //        {
    //            ValidateIssuer = true,
    //            ValidateAudience = true,
    //            ValidateLifetime = true,
    //            ValidateIssuerSigningKey = true,
    //            ValidIssuer = _configuration["Jwt:Issuer"],
    //            ValidAudience = _configuration["Jwt:Audience"],
    //            IssuerSigningKey = new SymmetricSecurityKey(key)
    //        }, out SecurityToken validatedToken);
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}

    //}
}