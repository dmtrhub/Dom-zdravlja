using Dom_zdravlja.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Models
{
    public class Korisnik
    {
        [Required]
        [Display(Name = "Korisničko ime")]
        [JsonProperty(Order = 1)]
        public string KorisnickoIme { get; set; }

        [Required]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        [JsonProperty(Order = 2)]
        public string Sifra { get; set; }

        [Required]
        [JsonProperty(Order = 3)]
        public string Ime { get; set; }

        [Required]
        [JsonProperty(Order = 4)]
        public string Prezime { get; set; }

        [Required]
        [Display(Name = "Datum rodjenja")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [JsonConverter(typeof(DateConverter))]
        [JsonProperty(Order = 5)]
        public DateTime DatumRodjenja { get; set; }
    }
}