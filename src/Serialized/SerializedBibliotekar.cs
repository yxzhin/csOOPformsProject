using System;

namespace csOOPformsProject.Serialized
{
    public sealed class SerializedBibliotekar
    {
        public int Id { get; set; }
        public string PunoIme { get; set; }
        public string DatumRodjenja { get; set; }
        public string Sifra { get; set; }
        public string SifraRadnika { get; set; }

        public SerializedBibliotekar(int id, string ime, string prezime,
            DateTime datumRodjenja, string sifra, string sifraRadnika)
        {
            Id = id;
            PunoIme = $"{ime} {prezime}";
            DatumRodjenja = datumRodjenja.ToShortDateString();
            Sifra = sifra;
            SifraRadnika = sifraRadnika;
        }
    }
}
