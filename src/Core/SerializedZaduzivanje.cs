using csOOPformsProject.Models;
using System;

namespace csOOPformsProject.Core
{
    public class SerializedZaduzivanje
    {
        public int Id { get; set; }
        public string Korisnik { get; set; }
        public string Knjiga { get; set; }
        public string DatumZaduzivanja { get; set; }
        public string RokZaduzivanja { get; set; }
        public string DatumVracanja { get; set; }
        public bool IstekliRok { get; set; }
        public SerializedZaduzivanje(int id, Korisnik korisnik, Knjiga knjiga,
            DateTime datumZaduzivanja, DateTime rokZaduzivanja,
            DateTime? datumVracanja, bool istekliRok)
        {
            Id = id;
            Korisnik = korisnik.ToString();
            Knjiga = knjiga.ToString();
            DatumZaduzivanja = datumZaduzivanja.ToShortDateString();
            RokZaduzivanja = rokZaduzivanja.ToShortDateString();
            DatumVracanja = datumVracanja is DateTime
                ? datumVracanja.Value.ToShortDateString()
                : "nista";
            IstekliRok = istekliRok;
        }
    }
}
