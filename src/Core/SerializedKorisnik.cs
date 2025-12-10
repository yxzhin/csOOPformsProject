using csOOPformsProject.Interfaces;
using csOOPformsProject.Models;
using System;
using System.Collections.Generic;

namespace csOOPformsProject.Core
{
    public sealed class SerializedKorisnik : IImenovan
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string PunoIme => $"{Ime} {Prezime}";
        public DateTime DatumRodjenja { get; set; }
        public DateTime DatumClanarine { get; set; }
        public List<string> Zaduzivanja { get; set; }

        public SerializedKorisnik(int id, string ime, string prezime,
            DateTime datumRodjenja, DateTime datumClanarine,
            List<Zaduzivanje> zaduzivanja)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            DatumClanarine = datumClanarine;
            Zaduzivanja = new List<string>();
            foreach (Zaduzivanje zaduzivanje in zaduzivanja)
            {
                Zaduzivanja.Add($"{zaduzivanje.Knjiga.Naziv} | " +
                    $"{zaduzivanje.Knjiga.Autor.PunoIme} | " +
                    $"{zaduzivanje.DatumZaduzivanja.ToUniversalTime()} | " +
                    $"{zaduzivanje.RokZaduzivanja.ToUniversalTime()}");
            }
        }

    }
}
