using csOOPformsProject.Core;
using csOOPformsProject.Models;
using csOOPformsProject.Serialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject
{
    public partial class DirektorPanel : Form
    {
        private Biblioteka Biblioteka { get; set; }
        private Direktor Direktor { get; set; }

        private readonly List<Helpers.Nista> Nista
            = new List<Helpers.Nista>
            {
                new Helpers.Nista(),
            };

        private object OldValue { get; set; } = "";

        public DirektorPanel(Biblioteka biblioteka, Direktor direktor)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Biblioteka = biblioteka;
            Direktor = direktor;

            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button4.Click += button4_Click;
            button3.Click += button3_Click;

            dataGridView1.CellBeginEdit
                += dataGridView1_CellBeginEdit;
            dataGridView2.CellBeginEdit
                += dataGridView2_CellBeginEdit;

            dataGridView1.CellValueChanged
                += dataGridView1_CellValueChanged;
            /*
            dataGridView2.CellValueChanged
                += dataGridView2_CellValueChanged;
            */

            dataGridView1.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView1_CellValidating);
            /*
            dataGridView2.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView2_CellValidating);
            */

            dataGridView1.MultiSelect = false;
        }

        private void PrikaziPodatke()
        {
            label10.Text = Direktor.Id.ToString();
            label6.Text = Direktor.PunoIme;
            label8.Text = Direktor.DatumRodjenja
                .ToShortDateString().ToString();

            dataGridView1.ReadOnly = false;
            dataGridView2.ReadOnly = false;

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            // serialized objekat ispravno radi u dataGridView

            List<SerializedKorisnik> serializedKorisnici =
                Helpers.Serialize.Korisnici
                (Biblioteka.Korisnici.UcitajSve());
            /*
            List<SerializedBibliotekar> serializedBibliotekari =
                Helpers.Serialize.Bibliotekari
                (Biblioteka.Bibliotekari.UcitajSve());
            */

            dataGridView1.DataSource = serializedKorisnici;
            //dataGridView2.DataSource = serializedBibliotekari;

            if (serializedKorisnici.Count == 0)
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = Nista;
            }
            else
            {
                // zaduzivanja korisnika se ne smeju rucno menjati
                dataGridView1.Columns[5].ReadOnly = true;
            }

            /*
            if (serializedBibliotekari.Count == 0)
            {
                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = Nista;
            }
            */
        }

        private void DirektorPanel_Load(object sender, EventArgs e)
        {
            PrikaziPodatke();
        }

        // vraca array sa kolicinama izabranih redova za svaki dataGridView
        private int[] KolicineIzabranihRedova()
        {
            int count1, count2;
            count1 = dataGridView1.SelectedRows.Count;
            count2 = dataGridView2.SelectedRows.Count;
            int[] nums = { count1, count2 };
            return nums;
        }
        private int IzabranoRedova()
        {
            return KolicineIzabranihRedova().Count(x => x > 0);
        }

        private bool NistaNijeIzabrano()
        {
            return IzabranoRedova() == 0;
        }

        private bool IzabranoViseOdJednogRedaOdjednom()
        {
            return IzabranoRedova() > 1;
        }

        private DataGridViewRow UcitajIzabraniRed()
        {
            return dataGridView1.SelectedRows.Count == 1
                ? dataGridView1.SelectedRows[0]
                : dataGridView2.SelectedRows[0];
        }

        // prikazi podatke
        private void button1_Click(object sender, EventArgs e)
        {
            PrikaziPodatke();
        }

        // sacuvaj staro znacenje pre menjanja celije
        // za korisnike
        private void dataGridView1_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = dataGridView1.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // za bibliotekare
        private void dataGridView2_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = dataGridView2.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // validacija novog znacenja u celiji
        // za korisnike
        private void dataGridView1_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = dataGridView1.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == OldValue.ToString())
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

            if ((atribut == "DatumRodjenja"
                || atribut == "DatumClanarine")
                && !DateTime.TryParse(newValue, out _))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }
        }

        // izmena celije
        // za korisnike
        private void dataGridView1_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue = dataGridView1.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim();
            string atribut = dataGridView1.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id"
                ? (int)OldValue
                : (int)dataGridView1.Rows
                [e.RowIndex].Cells[0].Value;

            Korisnik korisnik = Biblioteka.Korisnici.UcitajPoId(id);

            switch (atribut)
            {
                case "PunoIme":
                    string ime = newValue.Split(' ')[0];
                    string prezime = newValue.Split(' ')[1];
                    korisnik.Ime = ime;
                    korisnik.Prezime = prezime;
                    break;

                case "DatumRodjenja":
                    DateTime datumRodjenja =
                        DateTime.Parse(newValue);
                    korisnik.DatumRodjenja = datumRodjenja;
                    break;

                case "DatumClanarine":
                    DateTime datumClanarine =
                        DateTime.Parse(newValue);
                    korisnik.DatumClanarine = datumClanarine;
                    break;
            }

            short result = atribut == "Id"
                ? Biblioteka.Korisnici.Promeni
                (korisnik, int.Parse(newValue))
                : Biblioteka.Korisnici.Promeni
                (korisnik);
            bool success = result == 1;

            if (!success)
            {
                if (atribut == "PunoIme")
                {
                    // otkazi promene
                    string ime = OldValue.ToString()
                        .Split(' ')[0];
                    string prezime = OldValue.ToString()
                        .Split(' ')[1];
                    korisnik.Ime = ime;
                    korisnik.Prezime = prezime;
                }
            }

            PrikaziPodatke();
        }

        // dodaj
        private void button2_Click(object sender, EventArgs e)
        {
            if (NistaNijeIzabrano())
            {
                Greska.Show(-5);
                return;
            }

            if (IzabranoViseOdJednogRedaOdjednom())
            {
                Greska.Show(-6);
                return;
            }

            DataGridViewRow izabraniRed = UcitajIzabraniRed();

            /*
            if (izabraniRed.Cells[0].Value.ToString()
                == "nema nista da se prikaze!!")
            {
                Greska.Show(-5);
                return;
            }
            */

            switch (izabraniRed.DataGridView.Name)
            {
                // dodaj korisnika
                case "dataGridView1":
                    int poslednjiId = Biblioteka.Korisnici.PoslednjiId();
                    Korisnik korisnik = new Korisnik(poslednjiId,
                        "novi", $"korisnik{poslednjiId}",
                        new DateTime(2073, 7, 3),
                        $"sifra{poslednjiId}",
                        new DateTime(2037, 3, 7));
                    Biblioteka.Korisnici.Dodaj(korisnik);

                    break;
            }

            PrikaziPodatke();
        }

        // obrisi
        private void button4_Click(object sender, EventArgs e)
        {

            if (NistaNijeIzabrano())
            {
                Greska.Show(-5);
                return;
            }

            if (IzabranoViseOdJednogRedaOdjednom())
            {
                Greska.Show(-6);
                return;
            }

            DataGridViewRow izabraniRed = UcitajIzabraniRed();

            if (izabraniRed.Cells[0].Value.ToString()
                == "nema nista da se prikaze!!")
            {
                Greska.Show(-5);
                return;
            }

            int id = (int)izabraniRed.Cells[0].Value;

            _ = dataGridView1.SelectedRows.Count == 1
                ? Biblioteka.ObrisiKorisnika(id)
                : Biblioteka.Bibliotekari.Obrisi(id);

            PrikaziPodatke();
        }

        // resetuj
        private void button3_Click(object sender, EventArgs e)
        {
            //Biblioteka.ResetujPodatke();
            Biblioteka.Seeder();
            PrikaziPodatke();
        }
    }
}
