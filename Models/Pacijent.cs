using Dom_zdravlja.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Models
{
    public class Pacijent : Korisnik
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "JMBG mora imati tačno 13 numeričkih karaktera.")]
        public string JMBG { get; set; }

        public List<Termin> ZakazaniTermini { get; set; }

        public List<Terapija> Terapije { get; set; } 

        public Pacijent()
        {
            Terapije = new List<Terapija>();
            ZakazaniTermini = new List<Termin>();
        }
    }
}