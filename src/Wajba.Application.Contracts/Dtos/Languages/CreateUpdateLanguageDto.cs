﻿namespace Wajba.Dtos.Languages;

public class CreateUpdateLanguageDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public IFormFile Image { get; set; } // File input for the image
    [Required]
    public Status Status { get; set; }
}
