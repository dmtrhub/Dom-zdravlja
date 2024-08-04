using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Models
{
    public class Pacijent
    {
        [Required]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG mora imati tačno 13 karaktera.")]
        public string JMBG { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Sifra { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<Termin> ZakazaniTermini { get; set; } = new List<Termin>();
    }
}