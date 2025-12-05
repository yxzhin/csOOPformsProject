using System;

namespace csOOPformsProject.Models
{
    public sealed class Bibliotekar : Osoba
    {
        public string SifraRadnika { get; set; }
        public Bibliotekar(int id, string ime, string prezime, DateTime datumRodjenja,
            string sifraRadnika)
            : base(id, ime, prezime, datumRodjenja)
        {
            SifraRadnika = sifraRadnika;
        }
    }
}
