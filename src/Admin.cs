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
            /*
            dataGridView2.CellValueChanged
                += dataGridView2_CellValueChanged;
            dataGridView3.CellValueChanged
                += dataGridView3_CellValueChanged;
            dataGridView4.CellValueChanged
                += dataGridView4_CellValueChanged;
            dataGridView5.CellValueChanged
                += dataGridView5_CellValueChanged;
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
            dataGridView4.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView4_CellValidating);
            dataGridView5.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (dataGridView5_CellValidating);
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

            List<Autor> autori =
                Biblioteka.Autori.UcitajSve();

            List<Kategorija> kategorije =
                Biblioteka.Kategorije.UcitajSve();

            dataGridView1.DataSource = serializedKnjige;
            dataGridView2.DataSource = serializedKorisnici;
            dataGridView3.DataSource = serializedZaduzivanja;
            dataGridView4.DataSource = autori;
            dataGridView5.DataSource = kategorije;

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
                        autori.Select(x => x.ToString()).ToList()
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
                        kategorije.Select(x => x.ToString()).ToList()
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
                // zaduzivanja se ne smeju rucno menjati
                dataGridView2.Columns[4].ReadOnly = true;
            }

            if (serializedZaduzivanja.Count == 0)
            {
                dataGridView3.ReadOnly = true;
                dataGridView3.DataSource = Nista;
            }

            if (autori.Count == 0)
            {
                dataGridView4.ReadOnly = true;
                dataGridView4.DataSource = Nista;
            }

            if (kategorije.Count == 0)
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
        // za knjige
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
                        Autor autor =
                            Biblioteka.Autori.UcitajPoPunomImenu
                            (newValue.ToString());
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
                            (newValue.ToString());
                        knjiga.Kategorija = kategorija;
                        //knjiga.Kategorija.Naziv = newValue.ToString();
                        break;
                }

                short result = atribut == "Id" ?
                    Biblioteka.Knjige.Promeni(knjiga, (int)newValue)
                    : Biblioteka.Knjige.Promeni(knjiga);
                bool success = result == 1;

                if (!success)
                {
                    Greska.Show(-2);
                }

                PrikaziPodatke();
            }
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

        // za zaduzivanja

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
