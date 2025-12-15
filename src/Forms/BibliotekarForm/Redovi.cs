using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.BibliotekarForm
{
    public static class Redovi
    {

        // vraca array sa kolicinama izabranih redova za svaki dataGridView
        public static int[] KolicineIzabranihRedova()
        {
            int count1, count3, count4, count5;
            count1 = Admin.DataGridView1.SelectedRows.Count;
            count3 = Admin.DataGridView3.SelectedRows.Count;
            count4 = Admin.DataGridView4.SelectedRows.Count;
            count5 = Admin.DataGridView5.SelectedRows.Count;
            int[] nums = { count1, count3, count4, count5 };
            return nums;
        }

        public static int IzabranoRedova()
        {
            return KolicineIzabranihRedova().Count(x => x > 0);
        }

        public static bool NistaNijeIzabrano()
        {
            return IzabranoRedova() == 0;
        }

        public static bool IzabranoViseOdJednogRedaOdjednom()
        {
            return IzabranoRedova() > 1;
        }

        public static DataGridViewRow UcitajIzabraniRed()
        {

            // brate sta je ovo :sob: :skull:
            return Admin.DataGridView1.SelectedRows.Count == 1
            ? Admin.DataGridView1.SelectedRows[0]
            : Admin.DataGridView3.SelectedRows.Count == 1
            ? Admin.DataGridView3.SelectedRows[0]
            : Admin.DataGridView4.SelectedRows.Count == 1
            ? Admin.DataGridView4.SelectedRows[0]
            : Admin.DataGridView5.SelectedRows[0];
        }
    }
}
