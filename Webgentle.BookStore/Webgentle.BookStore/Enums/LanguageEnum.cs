using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webgentle.BookStore.Enums
{
    public enum LanguageEnum
    {
        [Display(Name = "Arabic language")]
        Arabic = 10,
        English = 11,
        Germany = 12,
        Hindi = 13,
        Italy = 14,
        France = 15
    }
}
