﻿using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels;

public class ForgetPasswordViewModel
{
    [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
    [Display(Name = "Email :")]
    public string Email { get; set; }
}
