using csOOPformsProject.Core;
using csOOPformsProject.Models;
using csOOPformsProject.Serialized;
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

        private object OldValue { get; set; } = "";

        public Admin(Biblioteka biblioteka, Bibliotekar bibliotekar)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Biblioteka = biblioteka;
            Bibliotekar = bibliotekar;

            dataGridView1.CellBeginEdit
                += dataGridView1_CellBeginEdit;
            dataGridView2.CellBeginEdit
                += dataGridView2_CellBeginEdit;
            dataGridView3.CellBeginEdit
                += dataGridView3_CellBeginEdit;
            dataGridView4.CellBeginEdit
                += dataGridView4_CellBeginEdit;
            dataGridView5.CellBeginEdit
                += dataGridView5_CellBeginEdit;

            dataGridView1.CellValueChanged
                += dataGridView1_CellValueChanged;
            dataGridView2.CellValueChanged
                += dataGridView2_CellValueChanged;
            dataGridView3.CellValueChanged
                += dataGridView3_CellValueChanged;
            dataGridView4.CellValueChanged
                += dataGridView4_CellValueChanged;
            dataGridView5.CellValueChanged
                += dataGridView5_CellValueChanged;

            dataGridView1.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView1_CellValidating);
            dataGridView2.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView2_CellValidating);
            dataGridView3.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView3_CellValidating);
            dataGridView4.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView4_CellValidating);
            dataGridView5.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView5_CellValidating);

            //dataGridView1.DataError += dataGridView1_DataError;

            dataGridView1.CurrentCellDirtyStateChanged
                += dataGridView1_CurrentCellDirtyStateChanged;
            /*
            dataGridView3.CurrentCellDirtyStateChanged
                += dataGridView3_CurrentCellDirtyStateChanged;
            */

            dataGridView1.MultiSelect = false;
            dataGridView2.MultiSelect = false;
            dataGridView3.MultiSelect = false;
            dataGridView4.MultiSelect = false;
            dataGridView5.MultiSelect = false;
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
            dataGridView4.ReadOnly = false;
            dataGridView5.ReadOnly = false;

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            dataGridView4.DataSource = null;
            dataGridView5.DataSource = null;

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

            List<SerializedAutor> serializedAutori =
                Helpers.Serialize.Autori
                (Biblioteka.Autori.UcitajSve());

            List<SerializedKategorija> serializedKategorije =
                Helpers.Serialize.Kategorije
                (Biblioteka.Kategorije.UcitajSve());

            dataGridView1.DataSource = serializedKnjige;
            dataGridView2.DataSource = serializedKorisnici;
            dataGridView3.DataSource = serializedZaduzivanja;
            dataGridView4.DataSource = serializedAutori;
            dataGridView5.DataSource = serializedKategorije;

            if (serializedKnjige.Count == 0)
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = Nista;
            }
            else
            {
                // napravi comboBox za izbor autora knjige
                DataGridViewComboBoxColumn knjigeIzborAutora
                    = new DataGridViewComboBoxColumn
                    {
                        Name = "Autor",
                        DataPropertyName = "Autor",
                        HeaderText = "Autor",
                        DataSource =
                        Biblioteka.Autori.UcitajSve()
                        .Select(x => x.ToString()).ToList()
                    };

                dataGridView1.Columns.Remove("Autor");
                _ = dataGridView1.Columns.Add(knjigeIzborAutora);

                // napravi comboxBox za izbor kategorije knjige
                DataGridViewComboBoxColumn knjigeIzborKategorije
                    = new DataGridViewComboBoxColumn
                    {
                        Name = "Kategorija",
                        DataPropertyName = "Kategorija",
                        HeaderText = "Kategorija",
                        DataSource =
                        Biblioteka.Kategorije.UcitajSve()
                        .Select(x => x.ToString()).ToList()
                    };

                dataGridView1.Columns.Remove("Kategorija");
                _ = dataGridView1.Columns.Add(knjigeIzborKategorije);
            }

            if (serializedKorisnici.Count == 0)
            {
                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = Nista;
            }
            else
            {
                // zaduzivanja korisnika se ne smeju rucno menjati
                dataGridView2.Columns[4].ReadOnly = true;
            }

            if (serializedZaduzivanja.Count == 0)
            {
                dataGridView3.ReadOnly = true;
                dataGridView3.DataSource = Nista;
            }
            else
            {
                // istekli rok zaduzivanja se ne sme rucno menjati
                dataGridView3.Columns[6].ReadOnly = true;
            }

            if (serializedAutori.Count == 0)
            {
                dataGridView4.ReadOnly = true;
                dataGridView4.DataSource = Nista;
            }

            if (serializedKategorije.Count == 0)
            {
                dataGridView5.ReadOnly = true;
                dataGridView5.DataSource = Nista;
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            PrikaziPodatke();
        }

        // vraca array sa kolicinama izabranih redova za svaki dataGridView
        private int[] KolicineIzabranihRedova()
        {
            int count1, count2, count3, count4, count5;
            count1 = dataGridView1.SelectedRows.Count;
            count2 = dataGridView2.SelectedRows.Count;
            count3 = dataGridView3.SelectedRows.Count;
            count4 = dataGridView4.SelectedRows.Count;
            count5 = dataGridView5.SelectedRows.Count;
            int[] nums = { count1, count2, count3, count4, count5 };
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

            // brate sta je ovo :sob: :skull:
            return dataGridView1.SelectedRows.Count == 1
                ? dataGridView1.SelectedRows[0]
                : dataGridView2.SelectedRows.Count == 1
                ? dataGridView2.SelectedRows[0]
                : dataGridView3.SelectedRows.Count == 1
                ? dataGridView3.SelectedRows[0]
                : dataGridView4.SelectedRows.Count == 1
                ? dataGridView4.SelectedRows[0]
                : dataGridView5.SelectedRows[0];
        }

        // prikazi podatke
        private void button1_Click(object sender, EventArgs e)
        {
            PrikaziPodatke();
        }

        // sacuvaj staro znacenje pre menjanja celije
        // za knjige
        private void dataGridView1_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = dataGridView1.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // za korisnike
        private void dataGridView2_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = dataGridView2.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // za zaduzivanja
        private void dataGridView3_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = dataGridView3.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // za autore
        private void dataGridView4_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = dataGridView4.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // za kategorije
        private void dataGridView5_CellBeginEdit
            (object sender, DataGridViewCellCancelEventArgs e)
        {
            OldValue = dataGridView5.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value;
        }

        // validacija novog znacenja u celiji
        // za knjige
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

        // za korisnike
        private void dataGridView2_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = dataGridView2.Columns[e.ColumnIndex]
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

        // za zaduzivanja
        private void dataGridView3_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = dataGridView3.Columns[e.ColumnIndex]
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
                || atribut == "DatumVracanja")
                && !DateTime.TryParse(newValue, out _))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }

        }

        // za autore
        private void dataGridView4_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = dataGridView4.Columns[e.ColumnIndex]
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
        }

        // za kategorije
        private void dataGridView5_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            _ = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newValue = e.FormattedValue.ToString().Trim();
            string atribut = dataGridView5.Columns[e.ColumnIndex]
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

            if (atribut == "Naziv"
                && string.IsNullOrEmpty(newValue))
            {
                e.Cancel = true;
                Greska.Show(-1);
                return;
            }
        }

        // izmena celije
        // za knjige
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
                    knjiga.Naziv = newValue;
                    break;

                case "Autor":
                    Autor autor =
                        Biblioteka.Autori.UcitajPoPunomImenu
                        (newValue);
                    knjiga.Autor = autor;
                    /*
                    string ime = newValue.ToString()
                        .Split(' ')[0];
                    string prezime = newValue.ToString()
                        .Split(' ')[1];
                    knjiga.Autor.Ime = ime;
                    knjiga.Autor.Prezime = prezime;
                    */
                    break;

                case "Kategorija":
                    Kategorija kategorija =
                        Biblioteka.Kategorije.UcitajPoNazivu
                        (newValue);
                    knjiga.Kategorija = kategorija;
                    //knjiga.Kategorija.Naziv = newValue.ToString();
                    break;
            }

            short result = atribut == "Id"
                ? Biblioteka.Knjige.Promeni
                (knjiga, int.Parse(newValue))
                : Biblioteka.Knjige.Promeni
                (knjiga);
            bool success = result == 1;

            if (!success)
            {
                if (atribut == "Naziv")
                {
                    // otkazi promene
                    knjiga.Naziv = OldValue.ToString();
                }
            }

            PrikaziPodatke();

        }

        // za korisnike
        private void dataGridView2_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue = dataGridView2.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim();
            string atribut = dataGridView2.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id"
                ? (int)OldValue
                : (int)dataGridView2.Rows
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

        // za zaduzivanja
        private void dataGridView3_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue = dataGridView3.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim();
            string atribut = dataGridView3.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id" ?
                (int)OldValue
                : (int)dataGridView3.Rows[e.RowIndex].Cells[0]
                .Value;

            Zaduzivanje zaduzivanje =
                Biblioteka.Zaduzivanja.UcitajPoId(id);

            switch (atribut)
            {
                case "Korisnik":
                    Korisnik korisnik
                        = Biblioteka.Korisnici.UcitajPoPunomImenu
                        (newValue);

                    if (korisnik == null)
                    {
                        Greska.Show(-4, "korisnik nije pronadjen!!");
                        PrikaziPodatke();
                        return;
                    }

                    zaduzivanje.Korisnik = korisnik;
                    korisnik.Zaduzivanja.Add(zaduzivanje);
                    _ = Biblioteka.Korisnici.Promeni(korisnik);

                    Korisnik stariKorisnik
                        = Biblioteka.Korisnici.UcitajPoPunomImenu
                        (OldValue.ToString());
                    _ = stariKorisnik.Zaduzivanja.Remove(zaduzivanje);
                    _ = Biblioteka.Korisnici.Promeni(stariKorisnik);

                    break;

                case "Knjiga":
                    Knjiga knjiga
                        = Biblioteka.Knjige.UcitajPoNazivu
                        (newValue);

                    if (knjiga == null)
                    {
                        Greska.Show(-4, "knjiga nije pronadjena!!");
                        PrikaziPodatke();
                        return;
                    }

                    if (!knjiga.NaStanju)
                    {
                        Greska.Show(-13);
                        PrikaziPodatke();
                        return;
                    }

                    knjiga.NaStanju = false;
                    zaduzivanje.Knjiga = knjiga;
                    _ = Biblioteka.Knjige.Promeni(knjiga);

                    Knjiga staraKnjiga
                        = Biblioteka.Knjige.UcitajPoNazivu
                        (OldValue.ToString());
                    staraKnjiga.NaStanju = true;
                    _ = Biblioteka.Knjige.Promeni(staraKnjiga);

                    break;

                case "DatumZaduzivanja":
                    DateTime datumZaduzivanja
                        = DateTime.Parse(newValue);
                    zaduzivanje.DatumZaduzivanja = datumZaduzivanja;
                    break;

                case "RokZaduzivanja":
                    DateTime rokZaduzivanja
                        = DateTime.Parse(newValue);
                    zaduzivanje.RokZaduzivanja = rokZaduzivanja;
                    break;

                case "DatumVracanja":
                    DateTime datumVracanja
                        = DateTime.Parse(newValue);
                    zaduzivanje.DatumVracanja = datumVracanja;
                    break;
            }

            short result = atribut == "Id" ?
                Biblioteka.Zaduzivanja.Promeni(zaduzivanje,
                int.Parse(newValue))
                : Biblioteka.Zaduzivanja.Promeni(zaduzivanje);
            bool success = result == 1;

            if (!success)
            {
                Greska.Show(-2);
            }

            PrikaziPodatke();

        }

        // za autore
        private void dataGridView4_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue = dataGridView4.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim();
            string atribut = dataGridView4.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id"
                ? (int)OldValue
                : (int)dataGridView4.Rows
                [e.RowIndex].Cells[0].Value;

            Autor autor = Biblioteka.Autori.UcitajPoId(id);

            switch (atribut)
            {
                case "PunoIme":
                    string ime = newValue.Split(' ')[0];
                    string prezime = newValue.Split(' ')[1];
                    autor.Ime = ime;
                    autor.Prezime = prezime;
                    break;
            }

            short result = atribut == "Id"
                ? Biblioteka.Autori.Promeni
                (autor, int.Parse(newValue))
                : Biblioteka.Autori.Promeni
                (autor);
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
                    autor.Ime = ime;
                    autor.Prezime = prezime;
                }
            }

            PrikaziPodatke();

        }

        // za kategorije
        private void dataGridView5_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue = dataGridView5.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim();
            string atribut = dataGridView5.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id"
                ? (int)OldValue
                : (int)dataGridView5.Rows
                [e.RowIndex].Cells[0].Value;

            Kategorija kategorija = Biblioteka.Kategorije.UcitajPoId(id);

            switch (atribut)
            {
                case "Naziv":
                    string naziv = newValue;
                    kategorija.Naziv = naziv;
                    break;
            }

            short result = atribut == "Id"
                ? Biblioteka.Kategorije.Promeni
                (kategorija, int.Parse(newValue))
                : Biblioteka.Kategorije.Promeni
                (kategorija);
            bool success = result == 1;

            if (!success)
            {
                if (atribut == "Naziv")
                {
                    // otkazi promene
                    string naziv = OldValue.ToString();
                    kategorija.Naziv = naziv;
                }
            }

            PrikaziPodatke();

        }

        // izmena checkboxa u celiji
        // za knjige
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
                case "dataGridView1":
                    int id =
                        Biblioteka.Knjige.PoslednjiId();

                    int autorId = Biblioteka.Autori.PrviId();
                    if (autorId == 0)
                    {
                        Greska.Show(-9);
                        break;
                    }

                    int kategorijaId = Biblioteka.Kategorije.PrviId();
                    if (kategorijaId == 0)
                    {
                        Greska.Show(-10);
                        break;
                    }

                    Autor autor =
                        Biblioteka.Autori.UcitajPoId(autorId);
                    Kategorija kategorija =
                        Biblioteka.Kategorije.UcitajPoId(kategorijaId);
                    Knjiga knjiga = new Knjiga(id, "novaKnjiga",
                        autor, kategorija);
                    Biblioteka.Knjige.Dodaj(knjiga);

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
                ? Biblioteka.ObrisiKnjigu(id)
                //Biblioteka.Knjige.Obrisi(id)
                : dataGridView2.SelectedRows.Count == 1
                ? Biblioteka.ObrisiKorisnika(id)
                : dataGridView3.SelectedRows.Count == 1
                ? Biblioteka.VratiKnjigu(id, true)
                : dataGridView4.SelectedRows.Count == 1
                ? Biblioteka.ObrisiAutora(id)
                : Biblioteka.ObrisiKategoriju(id);
            //? Biblioteka.Korisnici.Obrisi(id)
            //? Biblioteka.Autori.Obrisi(id)
            //: Biblioteka.Kategorije.Obrisi(id);
            //Biblioteka.Zaduzivanja.Obrisi(id);

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
