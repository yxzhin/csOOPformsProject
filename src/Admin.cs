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
        private readonly List<Helpers.Nista> Nista
            = new List<Helpers.Nista>
            {
                new Helpers.Nista(),
            };
        private object OldValue { get; set; }
        public Admin(Biblioteka biblioteka, Bibliotekar bibliotekar)
        {
            InitializeComponent();

            Biblioteka = biblioteka;
            Bibliotekar = bibliotekar;

            dataGridView1.CellBeginEdit
                += dataGridView1_CellBeginEdit;
            dataGridView2.CellBeginEdit
                += dataGridView2_CellBeginEdit;
            dataGridView3.CellBeginEdit
                += dataGridView3_CellBeginEdit;

            dataGridView1.CellValueChanged
                += dataGridView1_CellValueChanged;
            /*
            dataGridView2.CellValueChanged
                += dataGridView2_CellValueChanged;
            dataGridView3.CellValueChanged
                += dataGridView3_CellValueChanged;
            */

            dataGridView1.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView1_CellValidating);
            /*
            dataGridView2.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView2_CellValidating);
            dataGridView3.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView3_CellValidating);
            */

            //dataGridView1.DataError += dataGridView1_DataError;

            dataGridView1.CurrentCellDirtyStateChanged
                += dataGridView1_CurrentCellDirtyStateChanged;
            /*
            dataGridView2.CurrentCellDirtyStateChanged
                += dataGridView2_CurrentCellDirtyStateChanged;
            dataGridView3.CurrentCellDirtyStateChanged
                += dataGridView3_CurrentCellDirtyStateChanged;
            */

            dataGridView1.MultiSelect = false;
            dataGridView2.MultiSelect = false;
            dataGridView3.MultiSelect = false;
        }

        private void PrikaziPodatke()
        {

            label10.Text = Bibliotekar.Id.ToString();
            label6.Text = Bibliotekar.PunoIme;
            label8.Text = Bibliotekar.DatumRodjenja
                .ToShortDateString().ToString();

            dataGridView1.ReadOnly = false;
            dataGridView2.ReadOnly = false;
            dataGridView3.ReadOnly = false;

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;

            // serialized objekat ispravno radi u dataGridView

            List<SerializedKnjiga> serializedKnjige =
                Helpers.Serialize.Knjige
                (Biblioteka.Knjige.UcitajSve());

            List<SerializedKorisnik> serializedKorisnici =
                Helpers.Serialize.Korisnici
                (Biblioteka.Korisnici.UcitajSve());

            List<SerializedZaduzivanje> serializedZaduzivanja =
                Helpers.Serialize.Zaduzivanja
                (Biblioteka.Zaduzivanja.UcitajSve());

            dataGridView1.DataSource = serializedKnjige;
            dataGridView2.DataSource = serializedKorisnici;
            dataGridView3.DataSource = serializedZaduzivanja;

            if (serializedKnjige.Count == 0)
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = Nista;
            }

            if (serializedKorisnici.Count == 0)
            {
                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = Nista;
            }

            if (serializedZaduzivanja.Count == 0)
            {
                dataGridView3.ReadOnly = true;
                dataGridView3.DataSource = Nista;
            }

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

        // sacuvaj staro znacenje pre menjanja celije
        private void dataGridView1_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = dataGridView1.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }
        private void dataGridView2_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = dataGridView2.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }
        private void dataGridView3_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = dataGridView3.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // validacija novog znacenja u celiji
        private void dataGridView1_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString();
            string atribut = dataGridView1.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();
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
        private void dataGridView1_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int id;
                object newValue = dataGridView1.Rows[e.RowIndex]
                    .Cells[e.ColumnIndex].Value;
                string atribut = dataGridView1.Columns[e.ColumnIndex]
                    .HeaderCell.Value.ToString();
                id = atribut == "Id" ?
                    (int)OldValue
                    : (int)dataGridView1.Rows[e.RowIndex].Cells[0]
                    .Value;
                Knjiga knjiga = Biblioteka.Knjige.UcitajPoId(id);
                if (dataGridView1.Columns[e.ColumnIndex] is
                    DataGridViewCheckBoxColumn)
                {
                    knjiga.NaStanju = (bool)dataGridView1
                        .CurrentCell.Value;
                    _ = Biblioteka.Knjige.Promeni(knjiga);
                    return;
                }
                switch (atribut)
                {
                    /*
                    case "Id":
                        knjiga.Id = (int)newValue; break;
                    */
                    case "Naziv":
                        knjiga.Naziv = newValue.ToString();
                        break;
                    case "Autor":
                        string ime = newValue.ToString()
                            .Split(' ')[0];
                        string prezime = newValue.ToString()
                            .Split(' ')[1];
                        knjiga.Autor.Ime = ime;
                        knjiga.Autor.Prezime = prezime;
                        break;
                    case "Kategorija":
                        knjiga.Kategorija.Naziv = newValue.ToString();
                        break;
                }

                bool success = atribut == "Id" ?
                    Biblioteka.Knjige.Promeni(knjiga, (int)newValue)
                    : Biblioteka.Knjige.Promeni(knjiga);

                if (!success)
                {
                    Greska.Show(-2);
                    PrikaziPodatke();
                    return;
                }
            }
        }

        // izmena checkboxa u celiji
        private void dataGridView1_CurrentCellDirtyStateChanged
            (object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                if (dataGridView1.CurrentCell is
                    DataGridViewCheckBoxCell)
                {
                    _ = dataGridView1.CommitEdit
                        (DataGridViewDataErrorContexts.Commit);
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

            if (dataGridView1.SelectedRows.Count == 0
                && dataGridView2.SelectedRows.Count == 0
                && dataGridView3.SelectedRows.Count == 0)
            {
                Greska.Show(-5);
                return;
            }

            if (IzabranoViseOdJednogRedaOdjednom())
            {
                Greska.Show(-6);
                return;
            }

            DataGridViewRow izabraniRed =
                dataGridView1.SelectedRows.Count == 1
                ? dataGridView1.SelectedRows[0]
                : dataGridView2.SelectedRows.Count == 1
                ? dataGridView2.SelectedRows[0]
                : dataGridView3.SelectedRows[0];
            /*
           izabraniRed = dataGridView1.SelectedRows[0];
           izabraniRed = dataGridView2.SelectedRows.Count == 1 ?
               dataGridView2.SelectedRows[0]
               : dataGridView3.SelectedRows[0];
           */

            int id = (int)izabraniRed.Cells[0].Value;

            _ = dataGridView1.SelectedRows.Count == 1
                ? Biblioteka.Knjige.Obrisi(id)
                : dataGridView2.SelectedRows.Count == 1
                ? Biblioteka.Korisnici.Obrisi(id)
                : Biblioteka.Zaduzivanja.Obrisi(id);

            PrikaziPodatke();

        }

        // resetuj
        private void button3_Click(object sender, EventArgs e)
        {
            Biblioteka.ResetujPodatke();
            PrikaziPodatke();
        }
    }
}
