using System;

namespace csOOPformsProject.Models
{
    public class Bibliotekar : Osoba
    {
        public string SifraRadnika { get; set; }
        public bool ImaSvaPrava { get; protected set; } = false;
        public Bibliotekar(int id, string ime, string prezime,
            DateTime datumRodjenja, string sifra, string sifraRadnika)
            : base(id, ime, prezime, datumRodjenja, sifra)
        {
            SifraRadnika = sifraRadnika;
        }

        public override string ToString()
        {
            return PunoIme;
        }
    }
}
