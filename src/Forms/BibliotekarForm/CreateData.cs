using csOOPformsProject.Core;
using csOOPformsProject.Models;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.BibliotekarForm
{
    public static class CreateData
    {
        public static Admin Admin { get; set; }

        public static void Create()
        {
            if (Redovi.NistaNijeIzabrano())
            {
                Greska.Show(-5);
                return;
            }

            if (Redovi.IzabranoViseOdJednogRedaOdjednom())
            {
                Greska.Show(-6);
                return;
            }

            DataGridViewRow izabraniRed = Redovi.UcitajIzabraniRed();

            /*
            if (izabraniRed.Cells[0].Value.ToString()
                == "nema nista da se prikaze!!")
            {
                Greska.Show(-5);
                return;
            }
            */

            switch (izabraniRed.DataGridView.Name)
            {
                // dodaj knjigu
                case "dataGridView1":
                    int poslednjiId = Admin.Biblioteka.Knjige.PoslednjiId();

                    int autorId = Admin.Biblioteka.Autori.PrviId();
                    if (autorId == 0)
                    {
                        Greska.Show(-9);
                        break;
                    }

                    int kategorijaId = Admin.Biblioteka.Kategorije.PrviId();
                    if (kategorijaId == 0)
                    {
                        Greska.Show(-10);
                        break;
                    }

                    Autor autor =
                        Admin.Biblioteka.Autori.UcitajPoId(autorId);
                    Kategorija kategorija =
                        Admin.Biblioteka.Kategorije.UcitajPoId(kategorijaId);
                    Knjiga knjiga = new Knjiga(poslednjiId,
                        $"novaKnjiga{poslednjiId + 1}", autor, kategorija);
                    Admin.Biblioteka.Knjige.Dodaj(knjiga);

                    break;

                // dodaj zaduzivanje
                case "dataGridView3":
                    _ = Admin.Biblioteka.Zaduzivanja.PoslednjiId();

                    int korisnikId = Admin.Biblioteka.Korisnici.PrviId();
                    if (korisnikId == 0)
                    {
                        Greska.Show(-18);
                        break;
                    }

                    int knjigaId = Admin.Biblioteka.Knjige.PrviId();
                    if (knjigaId == 0)
                    {
                        Greska.Show(-17);
                        break;
                    }

                    if (!Admin.Biblioteka.PozajmiKnjigu
                        (korisnikId, knjigaId))
                    {
                        Greska.Show(-13);
                        break;
                    }

                    break;

                // dodaj autora
                case "dataGridView4":
                    poslednjiId = Admin.Biblioteka.Autori.PoslednjiId();
                    autor = new Autor(poslednjiId,
                        "novi", $"autor{poslednjiId + 1}");
                    Admin.Biblioteka.Autori.Dodaj(autor);

                    break;

                // dodaj kategoriju
                case "dataGridView5":
                    poslednjiId = Admin.Biblioteka.Kategorije.PoslednjiId();
                    kategorija = new Kategorija(poslednjiId,
                        $"novaKategorija{poslednjiId + 1}");
                    Admin.Biblioteka.Kategorije.Dodaj(kategorija);

                    break;

            }

            ReadData.Read();
        }
    }
}
