using System.Windows.Forms;

namespace csOOPformsProject.Forms.DirektorForm
{
    public static class PreEdit
    {
        public static object OldValue { get; set; } = "";

        // sacuvaj staro znacenje pre menjanja celije
        // za korisnike
        public static void dataGridView1_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            PreEdit.OldValue = DirektorPanel
                .DataGridView1.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // za bibliotekare
        public static void dataGridView2_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            PreEdit.OldValue = DirektorPanel
                .DataGridView2.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }
    }
}
