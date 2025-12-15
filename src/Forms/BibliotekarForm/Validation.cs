using csOOPformsProject.Core;
using System;
using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.BibliotekarForm
{
    public static class Validation
    {

        // validacija novog znacenja u celiji
        // za knjige
        public static void dataGridView1_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = Admin.DataGridView1.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == PreEdit.OldValue.ToString())
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

            if ((atribut == "Naziv"
                || atribut == "Autor"
                || atribut == "Kategorija")
                && string.IsNullOrEmpty(newValue))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

            if (atribut == "Autor"
                && newValue.Split(' ').Count() != 2)
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }
        }

        // za zaduzivanja
        public static void dataGridView3_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = Admin.DataGridView3.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == PreEdit.OldValue.ToString())
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

            if ((atribut == "Korisnik"
                || atribut == "Knjiga")
                && string.IsNullOrEmpty(newValue))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

            if ((atribut == "DatumZaduzivanja"
                || atribut == "RokZaduzivanja"
                || (atribut == "DatumVracanja"
                && !string.IsNullOrEmpty(newValue)
                && newValue.ToLower() != "nista"))
                && !DateTime.TryParse(newValue, out _))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }
        }

        // za autore
        public static void dataGridView4_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = Admin.DataGridView4.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == PreEdit.OldValue.ToString())
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

            if (atribut == "PunoIme"
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
        }

        // za kategorije
        public static void dataGridView5_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = Admin.DataGridView5.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == PreEdit.OldValue.ToString())
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

            if (atribut == "Naziv"
                && string.IsNullOrEmpty(newValue))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }
        }
    }
}
