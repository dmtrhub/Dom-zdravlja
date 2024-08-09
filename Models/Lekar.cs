using Dom_zdravlja.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Models
{
    public class Lekar : Korisnik
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public List<Termin> Termini { get; set; }

        public List<Terapija> Terapije { get; set; }

        public List<Pacijent> Pacijenti { get; set; } 

        public Lekar()
        {
            Termini = new List<Termin>();
            Terapije = new List<Terapija>();
            Pacijenti = new List<Pacijent>();
        }
    }
}