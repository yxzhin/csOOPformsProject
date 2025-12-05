using csOOPformsProject.Repositories;

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

    }
}
