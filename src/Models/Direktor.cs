using System;

namespace csOOPformsProject.Models
{
    public sealed class Direktor : Bibliotekar
    {
        public Direktor(int id, string ime, string prezime,
            DateTime datumRodjenja, string sifra,
            string sifraRadnika)
            : base(id, ime, prezime, datumRodjenja,
                  sifra, sifraRadnika)
        {
            ImaSvaPrava = true;
            return;
        }
    }
}
