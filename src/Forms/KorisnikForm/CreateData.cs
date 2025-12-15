using csOOPformsProject.Core;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.KorisnikForm
{
    public static class CreateData
    {
        public static void Create()
        {
            if (Redovi.NistaNijeIzabrano()
                // smeju se izabrati samo knjige (dataGridView2)
                || Redovi.KolicineIzabranihRedova()[1] != 1)
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
            _ = User.Biblioteka.PozajmiKnjigu
                (User.Korisnik.Id, id);

            ReadData.Read();
        }
    }
}
