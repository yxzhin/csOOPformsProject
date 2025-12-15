using csOOPformsProject.Serialized;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.BibliotekarForm
{
    public static class Sort
    {
        private static bool SortAscending { get; set; } = true;

        public static BindingSource Dgv1Bs { get; set; }
            = new BindingSource();
        public static BindingSource Dgv3Bs { get; set; }
            = new BindingSource();
        public static BindingSource Dgv4Bs { get; set; }
            = new BindingSource();
        public static BindingSource Dgv5Bs { get; set; }
            = new BindingSource();

        // sortiraj po header columnu
        // za knjige
        public static void dataGridView1_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = Admin.DataGridView1.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedKnjiga> list =
                (List<SerializedKnjiga>)Dgv1Bs.DataSource;

            // i am still losing my sanity
            // :sob: :skull:
            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();

            SortAscending = !SortAscending;

            Dgv1Bs.DataSource = list;

            Admin.DataGridView1.Columns
                [e.ColumnIndex].HeaderCell
                .SortGlyphDirection =
                SortAscending
                ? SortOrder.Descending
                : SortOrder.Ascending;
        }

        // za zaduzivanja
        public static void dataGridView3_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = Admin.DataGridView3.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedZaduzivanje> list =
                (List<SerializedZaduzivanje>)Dgv3Bs.DataSource;

            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();

            SortAscending = !SortAscending;

            Dgv3Bs.DataSource = list;

            Admin.DataGridView3.Columns
                [e.ColumnIndex].HeaderCell
                .SortGlyphDirection =
                SortAscending
                ? SortOrder.Descending
                : SortOrder.Ascending;
        }

        // za autore
        public static void dataGridView4_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = Admin.DataGridView4.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedAutor> list =
                (List<SerializedAutor>)Dgv4Bs.DataSource;

            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();

            SortAscending = !SortAscending;

            Dgv4Bs.DataSource = list;

            Admin.DataGridView4.Columns
                [e.ColumnIndex].HeaderCell
                .SortGlyphDirection =
                SortAscending
                ? SortOrder.Descending
                : SortOrder.Ascending;
        }

        // za kategorije
        public static void dataGridView5_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = Admin.DataGridView5.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedKategorija> list =
                (List<SerializedKategorija>)Dgv5Bs.DataSource;

            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();

            SortAscending = !SortAscending;

            Dgv5Bs.DataSource = list;

            Admin.DataGridView5.Columns
                [e.ColumnIndex].HeaderCell
                .SortGlyphDirection =
                SortAscending
                ? SortOrder.Descending
                : SortOrder.Ascending;
        }
    }
}
