using csOOPformsProject.Core;
using System;
using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.DirektorForm
{
    public static class Validation
    {
        // validacija novog znacenja u celiji
        // za korisnike
        public static void dataGridView1_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = DirektorPanel.DataGridView1
                .Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == DirektorPanel
                .OldValue.ToString())
            {
                return;
            }

            if (atribut == "Id"
                && !int.TryParse(newValue, out _))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

            if (atribut == "Id"
                && int.Parse(newValue) < 0)
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

            if ((atribut == "PunoIme"
                || atribut == "Sifra")
                && string.IsNullOrEmpty(newValue))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

            if (atribut == "PunoIme"
                && newValue.Split(' ').Count() != 2)
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

            if ((atribut == "DatumRodjenja"
                || atribut == "DatumClanarine")
                && !DateTime.TryParse(newValue, out _))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }
        }

        // za bibliotekare
        public static void dataGridView2_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = DirektorPanel.DataGridView2
                .Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == DirektorPanel
                .OldValue.ToString())
            {
                return;
            }

            if (atribut == "Id"
                && !int.TryParse(newValue, out _))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

            if (atribut == "Id"
                && int.Parse(newValue) < 0)
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

            if ((atribut == "PunoIme"
                || atribut == "Sifra"
                || atribut == "SifraRadnika")
                && string.IsNullOrEmpty(newValue))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

            if (atribut == "PunoIme"
                && newValue.Split(' ').Count() != 2)
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

            if (atribut == "DatumRodjenja"
                && !DateTime.TryParse(newValue, out _))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }
        }
    }
}
