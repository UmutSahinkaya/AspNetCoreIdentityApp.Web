﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspNetCoreIdentityApp.Web.Extensions;

public static class ModelStateExtensions
{
    public static void AddModelErrorList(this ModelStateDictionary modelState,List<string> errors)
    {
        errors.ForEach(x =>
        {
            modelState.AddModelError(string.Empty, x);
        });
    }
}
