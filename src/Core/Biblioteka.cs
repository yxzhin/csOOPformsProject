using csOOPformsProject.Models;
using csOOPformsProject.Repositories;
using System;

namespace csOOPformsProject.Core
{
    public sealed class Biblioteka
    {
        public KnjigaRepozitorium Knjige { get; }
        public KorisnikRepozitorium Korisnici { get; }
        public BibliotekarRepozitorium Bibliotekari { get; }
        public ZaduzivanjeRepozitorium Zaduzivanja { get; }

        public Biblioteka()
        {
            Knjige = new KnjigaRepozitorium(Helpers.DataFolder
                + "\\knjige.json");
            Korisnici = new KorisnikRepozitorium(Helpers.DataFolder
                + "\\korisnici.json");
            Bibliotekari = new BibliotekarRepozitorium(Helpers.DataFolder
                + "\\bibliotekari.json");
            Zaduzivanja = new ZaduzivanjeRepozitorium(Helpers.DataFolder
                + "\\zaduzivanja.json");
        }

        public bool PozajmiKnjigu(int korisnikId, int knjigaId)
        {
            Korisnik korisnik = Korisnici.UcitajPoId(korisnikId);
            Knjiga knjiga = Knjige.UcitajPoId(knjigaId);

            if (korisnik == null || knjiga == null || !knjiga.NaStanju)
            {
                return false;
            }

            knjiga.NaStanju = false;
            Knjige.Promeni(knjiga);

            int poslednjeZaduzivanjeId = Zaduzivanja.PoslednjiId();
            Zaduzivanje zaduzivanje = new Zaduzivanje(poslednjeZaduzivanjeId,
                korisnik, knjiga);
            Zaduzivanja.Dodaj(zaduzivanje);

            korisnik.Zaduzivanja.Add(zaduzivanje);
            Korisnici.Promeni(korisnik);

            return true;
        }
        public void VratiKnjigu(int zaduzivanjeId)
        {
            Zaduzivanje zaduzivanje = Zaduzivanja.UcitajPoId(zaduzivanjeId);
            if (zaduzivanje == null)
            {
                return;
            }

            zaduzivanje.DatumVracanja = DateTime.Now;
            zaduzivanje.Knjiga.NaStanju = true;

            Zaduzivanja.Promeni(zaduzivanje);
            Knjige.Promeni(zaduzivanje.Knjiga);
        }

    }
}
