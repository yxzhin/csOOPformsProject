using csOOPformsProject.Core;
using csOOPformsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject
{
    public partial class Admin : Form
    {
        private Biblioteka Biblioteka { get; set; }
        public Admin(Biblioteka biblioteka)
        {
            InitializeComponent();
            Biblioteka = biblioteka;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            //dataGridView1.DataError += dataGridView1_DataError;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
        }

        private void PrikaziPodatke()
        {
            dataGridView1.DataSource = null;
            // serialized knjiga ispravno radi u dataGridView
            List<SerializedKnjiga> serializedKnjige =
                Helpers.SerializeKnjige(Biblioteka.Knjige.UcitajSve());
            dataGridView1.DataSource = serializedKnjige;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            PrikaziPodatke();
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString();
            string atribut = dataGridView1.Columns[e.ColumnIndex].HeaderCell.Value.ToString();
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                object newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string atribut = dataGridView1.Columns[e.ColumnIndex].HeaderCell.Value.ToString();
                Knjiga knjiga = Biblioteka.Knjige.UcitajPoId(id);
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    knjiga.NaStanju = (bool)dataGridView1.CurrentCell.Value;
                    _ = Biblioteka.Knjige.Promeni(knjiga);
                    return;
                }
                switch (atribut)
                {
                    case "Id":
                        knjiga.Id = (int)newValue; break;
                    case "Naziv":
                        knjiga.Naziv = newValue.ToString(); break;
                    case "Autor":
                        string ime = newValue.ToString().Split(' ')[0];
                        string prezime = newValue.ToString().Split(' ')[1];
                        knjiga.Autor.Ime = ime;
                        knjiga.Autor.Prezime = prezime;
                        break;
                    case "Kategorija":
                        knjiga.Kategorija.Naziv = newValue.ToString(); break;
                }
                if (!Biblioteka.Knjige.Promeni(knjiga))
                {
                    Greska.Show(-2);
                    return;
                }
            }
        }
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                if (dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
                {
                    _ = dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }
    }
}
