using System.Windows.Forms;

namespace csOOPformsProject.Forms.BibliotekarForm
{
    public static class PreEdit
    {
        public static object OldValue { get; set; } = "";

        // sacuvaj staro znacenje pre menjanja celije
        // za knjige
        public static void dataGridView1_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = Admin.DataGridView1.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // za zaduzivanja
        public static void dataGridView3_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = Admin.DataGridView3.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // za autore
        public static void dataGridView4_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = Admin.DataGridView4.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // za kategorije
        public static void dataGridView5_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = Admin.DataGridView5.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }
    }
}
