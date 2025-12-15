using csOOPformsProject.Core;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.DirektorForm
{
    public static class DeleteData
    {
        public static DirektorPanel DirektorPanel { get; set; }

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

            if (DirektorPanel.DataGridView1
                .SelectedRows.Count == 1
                && Biblioteka.KorisnikId == id)
            {
                Greska.Show(-14);
                return;
            }

            if (DirektorPanel.DataGridView2
                .SelectedRows.Count == 1)
            {
                if (Biblioteka.BibliotekarId == id)
                {
                    Greska.Show(-14);
                    return;
                }
                else if (Biblioteka.DirektorId == id)
                {
                    Greska.Show(-15);
                    return;
                }
            }

            _ = DirektorPanel.DataGridView1
                .SelectedRows.Count == 1
                ? DirektorPanel.Biblioteka
                .ObrisiKorisnika(id)
                : DirektorPanel.Biblioteka
                .Bibliotekari.Obrisi(id);

            ReadData.Read();
        }
    }
}
