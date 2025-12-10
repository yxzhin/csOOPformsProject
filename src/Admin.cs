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
        private Bibliotekar Bibliotekar { get; set; }
        private readonly List<Helpers.Nista> Nista = new List<Helpers.Nista>();
        public Admin(Biblioteka biblioteka, Bibliotekar bibliotekar)
        {
            InitializeComponent();
            Biblioteka = biblioteka;
            Bibliotekar = bibliotekar;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            //dataGridView1.DataError += dataGridView1_DataError;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
            dataGridView1.MultiSelect = false;
            dataGridView2.MultiSelect = false;
            dataGridView3.MultiSelect = false;
        }

        private void PrikaziPodatke()
        {

            label10.Text = Bibliotekar.Id.ToString();
            label6.Text = Bibliotekar.PunoIme;
            label8.Text = Bibliotekar.DatumRodjenja.ToShortDateString().ToString();

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;

            // serialized objekat ispravno radi u dataGridView

            List<SerializedKnjiga> serializedKnjige =
                Helpers.Serialize.Knjige(Biblioteka.Knjige.UcitajSve());

            List<SerializedKorisnik> serializedKorisnici =
                Helpers.Serialize.Korisnici(Biblioteka.Korisnici.UcitajSve());

            /*
            List<SerializedZaduzivanje> serializedZaduzivanja =
                Helpers.Serialize.Zaduzivanja(Biblioteka.Zaduzivanja.UcitajSve());
            */

            dataGridView1.DataSource = serializedKnjige;
            dataGridView2.DataSource = serializedKorisnici;
            //dataGridView3.DataSource = serializedZaduzivanja;

            if (serializedKnjige.Count == 0)
            {
                dataGridView1.DataSource = Nista;
            }

            if (serializedKorisnici.Count == 0)
            {
                dataGridView2.DataSource = Nista;
            }

            /*
            if (serializedZaduzivanja.Count == 0)
            {
                dataGridView3.DataSource = Nista;
            }
            */

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            PrikaziPodatke();
        }

        private bool IzabranoViseOdJednogRedaOdjednom()
        {
            return (dataGridView1.SelectedRows.Count > 0
                && dataGridView2.SelectedRows.Count > 0
                && dataGridView3.SelectedRows.Count > 0)
                || (dataGridView1.SelectedRows.Count > 0
                && dataGridView2.SelectedRows.Count > 0)
                || (dataGridView2.SelectedRows.Count > 0
                && dataGridView3.SelectedRows.Count > 0)
                || (dataGridView1.SelectedRows.Count > 0
                && dataGridView3.SelectedRows.Count > 0);
        }

        // prikazi podatke
        private void button1_Click(object sender, EventArgs e)
        {
            PrikaziPodatke();
        }

        // validacija
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

        // izmena celije
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

        // izmena checkboxa u celiji
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

        // dodaj
        private void button2_Click(object sender, EventArgs e)
        {

        }

        // obrisi
        private void button4_Click(object sender, EventArgs e)
        {
            if (IzabranoViseOdJednogRedaOdjednom())
            {
                Greska.Show(-5);
                return;
            }

        }
    }
}
