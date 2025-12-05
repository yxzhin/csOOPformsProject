using csOOPformsProject.Interfaces;
using System;

namespace csOOPformsProject.Models
{
    public sealed class Zaduzivanje : IEntitet
    {
        public int Id { get; set; }
        public Korisnik Korisnik { get; set; }
        public Knjiga Knjiga { get; set; }
        public DateTime DatumZaduzivanja { get; set; } = DateTime.Now;
        public DateTime RokZaduzivanja { get; set; } = DateTime.Now.AddDays(73);
        public DateTime? DatumVracanja { get; set; } = null;
        public bool IstekliRok =>
            DatumVracanja == null && DateTime.Now > DatumZaduzivanja;
        public Zaduzivanje(int id, Korisnik korisnik, Knjiga knjiga,
            DateTime? datumVracanja = null)
        {
            Id = id;
            Korisnik = korisnik;
            Knjiga = knjiga;
            DatumVracanja = datumVracanja;
        }

        public override string ToString()
        {
            return $"Zaduzivanje.Id: {Id};" +
                $"Zaduzivanje.Korisnik.Id: {Korisnik.Id};" +
                $"Zaduzivanje.Knjiga.Id: {Knjiga.Id}";
        }
    }
}
