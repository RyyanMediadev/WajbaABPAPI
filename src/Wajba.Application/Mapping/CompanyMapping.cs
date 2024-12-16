﻿using Wajba.Dtos.CompanyContact;

namespace Wajba.Mapping;

public class CompanyMapping:Profile
{
    public CompanyMapping()
    {
        CreateMap<Company, CompanyDto>()
            .ReverseMap();
    }
}
