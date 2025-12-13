using csOOPformsProject.Models;
using System;
using System.Collections.Generic;

namespace csOOPformsProject.Serialized
{
    public sealed class SerializedKorisnik
    {
        public int Id { get; set; }
        public string PunoIme { get; set; }
        public string DatumRodjenja { get; set; }
        public string DatumClanarine { get; set; }
        public string Zaduzivanja { get; set; }

        public SerializedKorisnik(int id, string ime, string prezime,
            DateTime datumRodjenja, DateTime datumClanarine,
            List<Zaduzivanje> zaduzivanja)
        {
            Id = id;
            PunoIme = $"{ime} {prezime}";
            DatumRodjenja = datumRodjenja.ToShortDateString();
            DatumClanarine = datumClanarine.ToShortDateString();
            List<string> _zaduzivanja = new List<string>();
            foreach (Zaduzivanje zaduzivanje in zaduzivanja)
            {
                _zaduzivanja.Add(zaduzivanje.ToString());
            }
            Zaduzivanja = string.Join("\n", _zaduzivanja);
        }
    }
}
