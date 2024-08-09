using Dom_zdravlja.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Models
{
    public class Terapija
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Datum { get; set; }
        public Lekar Lekar { get; set; }
        public Pacijent Pacijent { get; set; }
    }
}