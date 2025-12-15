using csOOPformsProject.Serialized;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.KorisnikForm
{
    public static class Sort
    {
        private static bool SortAscending { get; set; } = true;

        public static BindingSource Dgv1Bs { get; set; }
            = new BindingSource();
        public static BindingSource Dgv2Bs { get; set; }
            = new BindingSource();

        // sortiraj po header columnu
        // za zaduzivanja korisnika
        public static void dataGridView1_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = User.DataGridView1.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedZaduzivanje> list =
                (List<SerializedZaduzivanje>)Dgv1Bs.DataSource;

            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();
            _ = list.RemoveAll
                (x => x.Korisnik !=
                User.Korisnik.PunoIme
                && x.DatumVracanja != "nista");

            SortAscending = !SortAscending;

            Dgv1Bs.DataSource = list;
            User.DataGridView1
                .Columns.Remove("Korisnik");
            User.DataGridView1
                .Columns.Remove("DatumVracanja");

            User.DataGridView1.Columns
                [e.ColumnIndex].HeaderCell
                .SortGlyphDirection =
                SortAscending
                ? SortOrder.Descending
                : SortOrder.Ascending;
        }

        // za knjige na stanju
        public static void dataGridView2_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = User.DataGridView2.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedKnjiga> list =
                (List<SerializedKnjiga>)Dgv2Bs.DataSource;

            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();
            _ = list.RemoveAll
                (x => !x.NaStanju);

            SortAscending = !SortAscending;

            Dgv2Bs.DataSource = list;
            User.DataGridView2
                .Columns.Remove("NaStanju");

            User.DataGridView2.Columns
                [e.ColumnIndex].HeaderCell
                .SortGlyphDirection =
                SortAscending
                ? SortOrder.Descending
                : SortOrder.Ascending;
        }
    }
}
