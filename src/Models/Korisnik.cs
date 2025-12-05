using System;
using System.Collections.Generic;

namespace csOOPformsProject.Models
{
    public sealed class Korisnik : Osoba
    {
        public DateTime DatumClanarine { get; set; }
        public List<Zaduzivanje> Zaduzivanja { get; set; }
            = new List<Zaduzivanje>();
        public Korisnik(int id, string ime, string prezime, DateTime datumRodjenja,
            DateTime datumClanarine)
            : base(id, ime, prezime, datumRodjenja)
        {
            DatumClanarine = datumClanarine;
        }

        public override string ToString()
        {
            return $"Korisnik.Id: {Id}; Korisnik.PunoIme: {PunoIme}";
        }

    }
}
