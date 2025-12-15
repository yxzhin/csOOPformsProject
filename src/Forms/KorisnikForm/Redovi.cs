using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.KorisnikForm
{
    public static class Redovi
    {
        // vraca array sa kolicinama izabranih redova za svaki dataGridView
        public static int[] KolicineIzabranihRedova()
        {
            int count1, count2;
            count1 = User.DataGridView1
                .SelectedRows.Count;
            count2 = User.DataGridView2
                .SelectedRows.Count;
            int[] nums = { count1, count2 };
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
            return User.DataGridView1
                .SelectedRows.Count == 1
                ? User.DataGridView1
                .SelectedRows[0]
                : User.DataGridView2
                .SelectedRows[0];
        }
    }
}
