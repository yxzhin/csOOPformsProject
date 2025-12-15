using csOOPformsProject.Core;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.BibliotekarForm
{

    public static class DeleteData
    {
        public static Admin Admin { get; set; }

        public static void Delete()
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

            if (izabraniRed.Cells[0].Value.ToString()
                == "nema nista da se prikaze!!")
            {
                Greska.Show(-5);
                return;
            }

            int id = (int)izabraniRed.Cells[0].Value;

            _ = Admin.DataGridView1.SelectedRows.Count == 1
                ? Admin.Biblioteka.ObrisiKnjigu(id)
            //Biblioteka.Knjige.Obrisi(id)
                : Admin.DataGridView3.SelectedRows.Count == 1
            ? Admin.Biblioteka.VratiKnjigu(id, true)
                : Admin.DataGridView4.SelectedRows.Count == 1
                ? Admin.Biblioteka.ObrisiAutora(id)
                : Admin.Biblioteka.ObrisiKategoriju(id);
            //? Biblioteka.Korisnici.Obrisi(id)
            //? Biblioteka.Autori.Obrisi(id)
            //: Biblioteka.Kategorije.Obrisi(id);
            //Biblioteka.Zaduzivanja.Obrisi(id);

            ReadData.Read();
        }
    }
}
