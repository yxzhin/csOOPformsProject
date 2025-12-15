using csOOPformsProject.Serialized;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.DirektorForm
{
    public static class Sort
    {

        private static bool SortAscending { get; set; } = true;

        public static BindingSource Dgv1Bs { get; set; }
            = new BindingSource();
        public static BindingSource Dgv2Bs { get; set; }
            = new BindingSource();

        // sortiraj po header columnu
        // za korisnike
        public static void dataGridView1_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = DirektorPanel.DataGridView1.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedKorisnik> list =
                (List<SerializedKorisnik>)Dgv1Bs.DataSource;

            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();

            SortAscending = !SortAscending;

            Dgv1Bs.DataSource = list;

            DirektorPanel.DataGridView1.Columns
                [e.ColumnIndex].HeaderCell
                .SortGlyphDirection =
                SortAscending
                ? SortOrder.Descending
                : SortOrder.Ascending;
        }

        // za bibliotekare
        public static void dataGridView2_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = DirektorPanel.DataGridView2.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedBibliotekar> list =
                (List<SerializedBibliotekar>)Dgv2Bs.DataSource;

            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();

            SortAscending = !SortAscending;

            Dgv2Bs.DataSource = list;

            DirektorPanel.DataGridView2.Columns
                [e.ColumnIndex].HeaderCell
                .SortGlyphDirection =
                SortAscending
                ? SortOrder.Descending
                : SortOrder.Ascending;
        }
    }
}
