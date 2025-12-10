using csOOPformsProject.Interfaces;
using System;

namespace csOOPformsProject.Models
{
    public abstract class Osoba : IEntitet, IImenovan
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string PunoIme => $"{Ime} {Prezime}";
        public DateTime DatumRodjenja { get; set; }
        public string Sifra { get; protected set; }

        public Osoba(int id, string ime, string prezime, DateTime datumRodjenja,
            string sifra)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            Sifra = sifra;
        }
    }
}
