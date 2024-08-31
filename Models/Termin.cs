using Dom_zdravlja.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Models
{
    public class Termin
    {
        [Required]
        public Lekar Lekar { get; set; }

        public string KorisnickoImePacijenta { get; set; }

        [Required]
        [RegularExpression(@"^(slobodan|zakazan)$", ErrorMessage = "Status mora biti 'slobodan' ili 'zakazan'.")]
        public string Status { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DatumVreme { get; set; }

        public string OpisTerapije { get; set; }

        public Pacijent Pacijent { get; set; }
    }
}