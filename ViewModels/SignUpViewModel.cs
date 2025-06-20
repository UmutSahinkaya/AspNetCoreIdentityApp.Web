﻿using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels;

public class SignUpViewModel
{
    [Required(ErrorMessage ="Kullanıcı Ad alanı boş bırakılamaz.")]
    [Display(Name ="Kullanıcı Adı :")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
    [Display(Name = "Email :")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Telefon alanı boş bırakılamaz.")]
    [Display(Name = "Telefon :")]
    public string Phone { get; set; }
    [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
    [Display(Name = "Şifre :")]
    public string Password { get; set; }

    [Compare(nameof(Password),ErrorMessage ="Şifreler uyuşmuyor.")]
    [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz")]
    [Display(Name = "Şifre Tekrar :")]
    public string PasswordConfirm { get; set; }
}
