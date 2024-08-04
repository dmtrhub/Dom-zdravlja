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

        [Required]
        [RegularExpression(@"^(slobodan|zakazan)$", ErrorMessage = "Status mora biti 'slobodan' ili 'zakazan'.")]
        public string Status { get; set; } // Slobodan ili Zakazan

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DatumVreme { get; set; }

        public string OpisTerapije { get; set; }
    }
}