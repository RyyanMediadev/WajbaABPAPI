﻿global using Volo.Abp.Identity;
using AutoMapper;
using System.Diagnostics.Metrics;

namespace Wajba.Models.UsersDomain;

public class User /*: IdentityUser*/: FullAuditedEntity<int>
{
   

    [Required]
    public string FirstName { get; set; }
    [Required]
    public string SecoundName { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Address { get; set; }
    public int? ProfileId { get; set; }
    public Profile Profile { get; set; }

    //public int? ProfileId { get; set; }
    //public Profile Profile { get; set; }
    //[Required]
    //public int CountryId { get; set; }
    //public Country Country { get; set; }



    public int BranchId { get; set; }
    public Branch Branch { get; set; }





    // }

}
