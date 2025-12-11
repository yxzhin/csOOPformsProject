using csOOPformsProject.Models;
using csOOPformsProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject.Core
{
    public sealed class Biblioteka
    {
        public AutorRepozitorium Autori { get; }
        public KategorijaRepozitorium Kategorije { get; }
        public KnjigaRepozitorium Knjige { get; }
        public KorisnikRepozitorium Korisnici { get; }
        public BibliotekarRepozitorium Bibliotekari { get; }
        public ZaduzivanjeRepozitorium Zaduzivanja { get; }

        public Biblioteka()
        {
            Autori = new AutorRepozitorium(
                Helpers.DataFolder
                + "\\autori.json");
            Kategorije = new KategorijaRepozitorium(
                Helpers.DataFolder
                + "\\kategorije.json");
            Knjige = new KnjigaRepozitorium(
                Helpers.DataFolder
                + "\\knjige.json");
            Korisnici = new KorisnikRepozitorium(
                Helpers.DataFolder
                + "\\korisnici.json");
            Bibliotekari = new BibliotekarRepozitorium(
                Helpers.DataFolder
                + "\\bibliotekari.json");
            Zaduzivanja = new ZaduzivanjeRepozitorium(
                Helpers.DataFolder
                + "\\zaduzivanja.json");
        }

        public void ResetujPodatke()
        {
            _ = Autori.ObrisiSve();
            _ = Kategorije.ObrisiSve();
            _ = Knjige.ObrisiSve();
            _ = Korisnici.ObrisiSve();
            _ = Bibliotekari.ObrisiSve();
            _ = Zaduzivanja.ObrisiSve();
        }

        public void Seeder()
        {

            ResetujPodatke();

            Autor autor1 = new Autor(0, "default", "autor1");
            Autor autor2 = new Autor(1, "default", "autor2");
            Autor autor3 = new Autor(2, "default", "autor3");

            Autori.Dodaj(autor1);
            Autori.Dodaj(autor2);
            Autori.Dodaj(autor3);

            Kategorija kategorija1 = new Kategorija(0,
                "defaultKategorija1");
            Kategorija kategorija2 = new Kategorija(1,
                "defaultKategorija2");
            Kategorija kategorija3 = new Kategorija(2,
                "defaultKategorija3");

            Kategorije.Dodaj(kategorija1);
            Kategorije.Dodaj(kategorija2);
            Kategorije.Dodaj(kategorija3);

            Knjiga knjiga1 = new Knjiga(0, "defaultKnjiga1",
                autor1, kategorija1);
            Knjiga knjiga2 = new Knjiga(1, "defaultKnjiga2",
                autor2, kategorija2);
            Knjiga knjiga3 = new Knjiga(2, "defaultKnjiga3",
                autor3, kategorija3);

            Knjige.Dodaj(knjiga1);
            Knjige.Dodaj(knjiga2);
            Knjige.Dodaj(knjiga3);

            Korisnik korisnik1 = new Korisnik(0, "default",
                "korisnik1", new DateTime(2007, 7, 3), "sifra1",
                new DateTime(2003, 3, 7));
            Korisnik korisnik2 = new Korisnik(1, "default",
                "korisnik2", new DateTime(2073, 3, 7), "sifra2",
                new DateTime(2073, 7, 3));
            Korisnik korisnik3 = new Korisnik(2, "default",
                "korisnik3", new DateTime(2037, 3, 3), "sifra3",
                new DateTime(2037, 7, 7));

            Korisnici.Dodaj(korisnik1);
            Korisnici.Dodaj(korisnik2);
            Korisnici.Dodaj(korisnik3);

            Bibliotekar bibliotekar1 = new Bibliotekar(0,
                "default", "bibliotekar1", new DateTime(2007, 7, 3),
                "sifra1", "sifraRadnika1");
            Bibliotekar bibliotekar2 = new Bibliotekar(1,
                "default", "bibliotekar2", new DateTime(2073, 3, 7),
                "sifra2", "sifraRadnika2");
            Bibliotekar bibliotekar3 = new Bibliotekar(2,
                "default", "bibliotekar3", new DateTime(2037, 7, 3),
                "sifra3", "sifraRadnika3");

            Bibliotekari.Dodaj(bibliotekar1);
            Bibliotekari.Dodaj(bibliotekar2);
            Bibliotekari.Dodaj(bibliotekar3);

            //Zaduzivanje zaduzivanje1 = new Zaduzivanje(0, korisnik1, knjiga1);
            _ = PozajmiKnjigu(1, 1);

            //Zaduzivanja.Dodaj(zaduzivanje1);

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
            _ = Knjige.Promeni(knjiga);

            int poslednjeZaduzivanjeId = Zaduzivanja.PoslednjiId();
            Zaduzivanje zaduzivanje = new Zaduzivanje(
                poslednjeZaduzivanjeId, korisnik, knjiga);
            Zaduzivanja.Dodaj(zaduzivanje);

            korisnik.Zaduzivanja.Add(zaduzivanje);
            _ = Korisnici.Promeni(korisnik);

            return true;
        }

        public bool VratiKnjigu(int zaduzivanjeId,
            bool obrisiZaduzivanje = false)
        {
            Zaduzivanje zaduzivanje = Zaduzivanja
                .UcitajPoId(zaduzivanjeId);

            if (zaduzivanje == null)
            {
                return false;
            }

            zaduzivanje.DatumVracanja = DateTime.Now;
            zaduzivanje.Knjiga.NaStanju = true;

            if (obrisiZaduzivanje)
            {
                _ = Zaduzivanja.Obrisi(zaduzivanjeId);
            }
            else
            {
                _ = Zaduzivanja.Promeni(zaduzivanje);
            }
            /*
            _ = obrisiZaduzivanje
                ? Zaduzivanja.Obrisi(zaduzivanjeId)
                : Zaduzivanja.Promeni(zaduzivanje);
            */

            _ = Knjige.Promeni(zaduzivanje.Knjiga);

            return true;
        }

        public Form UlogujSe(string ime, string prezime, string sifra)
        {
            Osoba osoba;
            osoba = Korisnici.UcitajPoPodacima(ime, prezime, sifra);
            if (osoba != null)
            {
                Korisnik korisnik = osoba as Korisnik;
                User user = new User(this, korisnik);
                return user;
            }
            osoba = Bibliotekari.UcitajPoPodacima(ime, prezime, sifra);
            if (osoba != null)
            {
                Bibliotekar bibliotekar = osoba as Bibliotekar;
                Admin admin = new Admin(this, bibliotekar);
                return admin;
            }
            return null;
        }

        public bool ObrisiKnjigu(int id)
        {
            List<Zaduzivanje> zaduzivanja = Zaduzivanja.UcitajSve();

            if (zaduzivanja.FirstOrDefault
                (x => x.Knjiga.Id == id) != null)
            {
                Greska.Show(-11);
                return false;
            }

            _ = Knjige.Obrisi(id);

            return true;
        }

        public bool ObrisiAutora(int id)
        {
            List<Knjiga> knjige = Knjige.UcitajSve();

            if (knjige.FirstOrDefault
                (x => x.Autor.Id == id) != null)
            {
                Greska.Show(-7);
                return false;
            }

            _ = Autori.Obrisi(id);

            return true;
        }

        public bool ObrisiKategoriju(int id)
        {
            List<Knjiga> knjige = Knjige.UcitajSve();

            if (knjige.FirstOrDefault
                (x => x.Kategorija.Id == id) != null)
            {
                Greska.Show(-8);
                return false;
            }

            _ = Kategorije.Obrisi(id);

            return true;
        }
    }
}
