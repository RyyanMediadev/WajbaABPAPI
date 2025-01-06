global using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Wajba.Enums;

public enum Status
{
    [Display(Description = "InActive")]
    InActive=1 ,
    [Display(Description = "Active")]
    Active = 2 
}
public enum ItemType
{
    [Description("NonVeg")]
    NonVeg = 0,
    [Description("Veg")]
    Veg = 1
}