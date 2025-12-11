using System.Windows.Forms;

namespace csOOPformsProject.Core
{
    public static class Greska
    {
        public static void Show(int type, string customMessage = null)
        {
            switch (type)
            {
                case -1:
                    _ = MessageBox.Show("unesite validne vrednosti!!",
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
                case -2:
                    _ = MessageBox.Show("id je vec zauzet!!",
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
                case -3:
                    _ = MessageBox.Show("popunite sva polja!!",
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
                case -4:
                    string msg = customMessage ?? "nalog nije " +
                        "pronadjen!!";
                    _ = MessageBox.Show(msg,
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
                case -5:
                    _ = MessageBox.Show("izaberite red!!",
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
                case -6:
                    _ = MessageBox.Show("izaberite samo jedan " +
                        "red odjednom!!",
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
                case -7:
                    _ = MessageBox.Show("pre nego sto obrisete " +
                        "autora, morate obrisati " +
                        "sve knjige od tog autora ili izabrati " +
                        "drugacijeg za njih!!",
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
                case -8:
                    _ = MessageBox.Show("pre nego sto obrisete " +
                        "kategoriju, morate obrisati " +
                        "sve knjige te kategorije ili izabrati " +
                        "drugaciju za njih!!",
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
                case -9:
                    _ = MessageBox.Show("nemate nijednog autora!! " +
                        "morate dodati bar jednog!!",
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
                case -10:
                    _ = MessageBox.Show("nemate nijednu kategoriju!! " +
                        "morate dodati bar jednu!!",
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
                case -11:
                    _ = MessageBox.Show("pre nego sto obrisete " +
                        "knjigu, morate obrisati " +
                        "sva zaduzivanja te knjige!!",
                    "greska!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
