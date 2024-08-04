using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Models
{
    public class ZdravstveniKarton
    {
        public Pacijent Pacijent { get; set; }
        public List<Termin> Termini { get; set; }
    }
}