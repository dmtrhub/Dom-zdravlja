using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dom_zdravlja.Models
{
    public class Administrator
    {
        string korisnickoIme;
        int sifra;
        string ime;
        string prezime;
        DateTime datumRodjenja;

        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }

        public int Sifra { get => sifra; set => sifra = value; }

        public string Ime { get => ime; set => ime = value; }

        public string Prezime { get => prezime; set => prezime = value; }

        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
    }
}