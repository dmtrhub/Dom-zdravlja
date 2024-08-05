using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Models.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obavezno.")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Šifra je obavezna.")]
        [DataType(DataType.Password)]
        [Display(Name = "Šifra")]
        public string Sifra { get; set; }
    }
}