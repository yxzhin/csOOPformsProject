using System;

namespace csOOPformsProject.Models
{
    public sealed class Korisnik : Osoba
    {
        public DateTime DatumClanarine { get; set; }
        public Korisnik(int id, string ime, string prezime, DateTime datumRodjenja,
            DateTime datumClanarine)
            : base(id, ime, prezime, datumRodjenja)
        {
            DatumClanarine = datumClanarine;
        }
    }
}
