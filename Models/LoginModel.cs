using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Korisničko ime je obavezno.")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Šifra je obavezna.")]
        [Display(Name = "Šifra")]
        [DataType(DataType.Password)]
        public string Sifra { get; set; }
    }
}