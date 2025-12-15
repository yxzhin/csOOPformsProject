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
        private Biblioteka Biblioteka { get; }
        public static Bibliotekar Bibliotekar { get; set; } = null;

        private readonly List<Helpers.Nista> Nista
            = new List<Helpers.Nista>
            {
                new Helpers.Nista(),
            };

        private object OldValue { get; set; } = "";
        private bool SortAscending { get; set; } = true;

        private BindingSource Dgv1Bs { get; set; }
            = new BindingSource();
        private BindingSource Dgv3Bs { get; set; }
            = new BindingSource();
        private BindingSource Dgv4Bs { get; set; }
            = new BindingSource();
        private BindingSource Dgv5Bs { get; set; }
            = new BindingSource();

        public Admin(Biblioteka biblioteka, Bibliotekar bibliotekar)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Biblioteka = biblioteka;
            Bibliotekar = bibliotekar;
            Biblioteka.BibliotekarId = bibliotekar.Id;

            dataGridView1.CellBeginEdit
                += dataGridView1_CellBeginEdit;
            dataGridView3.CellBeginEdit
                += dataGridView3_CellBeginEdit;
            dataGridView4.CellBeginEdit
                += dataGridView4_CellBeginEdit;
            dataGridView5.CellBeginEdit
                += dataGridView5_CellBeginEdit;

            dataGridView1.CellValueChanged
                += dataGridView1_CellValueChanged;
            dataGridView3.CellValueChanged
                += dataGridView3_CellValueChanged;
            dataGridView4.CellValueChanged
                += dataGridView4_CellValueChanged;
            dataGridView5.CellValueChanged
                += dataGridView5_CellValueChanged;

            dataGridView1.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView1_CellValidating);
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
            dataGridView3.MultiSelect = false;
            dataGridView4.MultiSelect = false;
            dataGridView5.MultiSelect = false;

            FormClosing += Admin_FormClosing;

            dataGridView1.ColumnHeaderMouseClick
                += dataGridView1_ColumnHeaderMouseClick;
            dataGridView3.ColumnHeaderMouseClick
                += dataGridView3_ColumnHeaderMouseClick;
            dataGridView4.ColumnHeaderMouseClick
                += dataGridView4_ColumnHeaderMouseClick;
            dataGridView5.ColumnHeaderMouseClick
                += dataGridView5_ColumnHeaderMouseClick;
        }

        // sortiraj po header columnu
        // za knjige
        private void dataGridView1_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dataGridView1.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedKnjiga> list =
                (List<SerializedKnjiga>)Dgv1Bs.DataSource;

            // i am still losing my sanity
            // :sob: :skull:
            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();

            SortAscending = !SortAscending;

            Dgv1Bs.DataSource = list;
        }

        // za zaduzivanja
        private void dataGridView3_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dataGridView3.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedZaduzivanje> list =
                (List<SerializedZaduzivanje>)Dgv3Bs.DataSource;

            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();

            SortAscending = !SortAscending;

            Dgv3Bs.DataSource = list;
        }

        // za autore
        private void dataGridView4_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dataGridView4.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedAutor> list =
                (List<SerializedAutor>)Dgv4Bs.DataSource;

            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();

            SortAscending = !SortAscending;

            Dgv4Bs.DataSource = list;
        }

        // za kategorije
        private void dataGridView5_ColumnHeaderMouseClick
            (object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dataGridView5.Columns
                [e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName)
                || columnName == "nista")
            {
                return;
            }

            List<SerializedKategorija> list =
                (List<SerializedKategorija>)Dgv5Bs.DataSource;

            list = SortAscending
                ? list.OrderBy(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList()
                : list.OrderByDescending(x => x.GetType()
                .GetProperty(columnName)
                .GetValue(x)).ToList();

            SortAscending = !SortAscending;

            Dgv5Bs.DataSource = list;
        }

        private void PrikaziPodatke()
        {
            Bibliotekar =
                Biblioteka.Bibliotekari.UcitajPoId
                (Biblioteka.BibliotekarId.Value);

            label10.Text = Bibliotekar.Id.ToString();
            label6.Text = Bibliotekar.PunoIme;
            label8.Text = Bibliotekar.DatumRodjenja
                .ToShortDateString().ToString();

            dataGridView1.ReadOnly = false;
            dataGridView3.ReadOnly = false;
            dataGridView4.ReadOnly = false;
            dataGridView5.ReadOnly = false;

            dataGridView1.DataSource = null;
            dataGridView3.DataSource = null;
            dataGridView4.DataSource = null;
            dataGridView5.DataSource = null;

            // serialized objekat ispravno radi u dataGridView

            List<SerializedKnjiga> serializedKnjige =
                Helpers.Serialize.Knjige
                (Biblioteka.Knjige.UcitajSve());
            Dgv1Bs.DataSource = serializedKnjige;

            List<SerializedZaduzivanje> serializedZaduzivanja =
                Helpers.Serialize.Zaduzivanja
                (Biblioteka.Zaduzivanja.UcitajSve());
            Dgv3Bs.DataSource = serializedZaduzivanja;

            List<SerializedAutor> serializedAutori =
                Helpers.Serialize.Autori
                (Biblioteka.Autori.UcitajSve());
            Dgv4Bs.DataSource = serializedAutori;

            List<SerializedKategorija> serializedKategorije =
                Helpers.Serialize.Kategorije
                (Biblioteka.Kategorije.UcitajSve());
            Dgv5Bs.DataSource = serializedKategorije;

            dataGridView1.DataSource = Dgv1Bs;
            dataGridView3.DataSource = Dgv3Bs;
            dataGridView4.DataSource = Dgv4Bs;
            dataGridView5.DataSource = Dgv5Bs;

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
            int count1, count3, count4, count5;
            count1 = dataGridView1.SelectedRows.Count;
            count3 = dataGridView3.SelectedRows.Count;
            count4 = dataGridView4.SelectedRows.Count;
            count5 = dataGridView5.SelectedRows.Count;
            int[] nums = { count1, count3, count4, count5 };
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

        // za zaduzivanja
        private void dataGridView3_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue =
                dataGridView3.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value != null
                ? dataGridView3.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim()
                : string.Empty;
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
                    DateTime? datumVracanja = null;
                    if (!string.IsNullOrEmpty(newValue)
                        && newValue.ToLower() != "nista")
                    {
                        datumVracanja = DateTime.Parse(newValue);
                    }

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
                // dodaj knjigu
                case "dataGridView1":
                    int poslednjiId = Biblioteka.Knjige.PoslednjiId();

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
                    Knjiga knjiga = new Knjiga(poslednjiId,
                        $"novaKnjiga{poslednjiId + 1}", autor, kategorija);
                    Biblioteka.Knjige.Dodaj(knjiga);

                    break;

                // dodaj zaduzivanje
                case "dataGridView3":
                    _ = Biblioteka.Zaduzivanja.PoslednjiId();

                    int korisnikId = Biblioteka.Korisnici.PrviId();
                    if (korisnikId == 0)
                    {
                        Greska.Show(-18);
                        break;
                    }

                    int knjigaId = Biblioteka.Knjige.PrviId();
                    if (knjigaId == 0)
                    {
                        Greska.Show(-17);
                        break;
                    }

                    if (!Biblioteka.PozajmiKnjigu
                        (korisnikId, knjigaId))
                    {
                        Greska.Show(-13);
                        break;
                    }

                    break;

                // dodaj autora
                case "dataGridView4":
                    poslednjiId = Biblioteka.Autori.PoslednjiId();
                    autor = new Autor(poslednjiId,
                        "novi", $"autor{poslednjiId + 1}");
                    Biblioteka.Autori.Dodaj(autor);

                    break;

                // dodaj kategoriju
                case "dataGridView5":
                    poslednjiId = Biblioteka.Kategorije.PoslednjiId();
                    kategorija = new Kategorija(poslednjiId,
                        $"novaKategorija{poslednjiId + 1}");
                    Biblioteka.Kategorije.Dodaj(kategorija);

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

        // izloguj se
        private void Admin_FormClosing(object sender,
            FormClosingEventArgs e)
        {
            Biblioteka.BibliotekarId = null;
        }
    }
}
