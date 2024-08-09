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
        [EmailAddress]
        public string Email { get; set; }

        public List<Termin> ZakazaniTermini { get; set; }

        public List<Terapija> Terapije { get; set; } // Terapije koje su prepisane pacijentu

        public Pacijent()
        {
            Terapije = new List<Terapija>();
        }
    }
}