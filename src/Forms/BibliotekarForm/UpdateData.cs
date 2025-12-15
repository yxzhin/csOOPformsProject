using csOOPformsProject.Core;
using csOOPformsProject.Models;
using System;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.BibliotekarForm
{
    public static class UpdateData
    {
        // izmena celije
        // za knjige
        public static void dataGridView1_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue = Admin.DataGridView1.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim();
            string atribut = Admin.DataGridView1.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == PreEdit.OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id"
                ? (int)PreEdit.OldValue
                : (int)Admin.DataGridView1.Rows
            [e.RowIndex].Cells[0].Value;

            Knjiga knjiga = Admin.Biblioteka.Knjige.UcitajPoId(id);

            if (Admin.DataGridView1.Columns[e.ColumnIndex] is
                DataGridViewCheckBoxColumn)
            {
                bool naStanju = (bool)Admin.DataGridView1
                    .CurrentCell.Value;
                if (naStanju
                    && Admin.Biblioteka.Zaduzivanja
                    .UcitajPoId(knjiga.Id) != null)
                {
                    Admin.DataGridView1
                    .CurrentCell.Value = false;
                    Greska.Show(-19);
                    return;
                }
                knjiga.NaStanju = naStanju;
                _ = Admin.Biblioteka.Knjige.Promeni(knjiga);
                return;
            }

            switch (atribut)
            {
                case "Naziv":
                    knjiga.Naziv = newValue;
                    break;

                case "Autor":
                    Autor autor =
                        Admin.Biblioteka.Autori.UcitajPoPunomImenu
                        (newValue);
                    knjiga.Autor = autor;
                    break;

                case "Kategorija":
                    Kategorija kategorija =
                        Admin.Biblioteka.Kategorije.UcitajPoNazivu
                        (newValue);
                    knjiga.Kategorija = kategorija;
                    break;
            }

            short result = atribut == "Id"
                ? Admin.Biblioteka.Knjige.Promeni
                (knjiga, int.Parse(newValue))
                : Admin.Biblioteka.Knjige.Promeni
                (knjiga);
            bool success = result == 1;

            if (!success)
            {
                if (atribut == "Naziv")
                {
                    // otkazi promene
                    knjiga.Naziv = PreEdit.OldValue.ToString();
                }
            }

            ReadData.Read();
        }

        // za zaduzivanja
        public static void dataGridView3_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue =
                Admin.DataGridView3.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value != null
                ? Admin.DataGridView3.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim()
                : string.Empty;
            string atribut = Admin.DataGridView3.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == PreEdit.OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id" ?
                (int)PreEdit.OldValue
                : (int)Admin.DataGridView3.Rows[e.RowIndex].Cells[0]
                .Value;

            Zaduzivanje zaduzivanje =
                Admin.Biblioteka.Zaduzivanja.UcitajPoId(id);

            switch (atribut)
            {
                case "Korisnik":
                    Korisnik korisnik
                        = Admin.Biblioteka.Korisnici.UcitajPoPunomImenu
                        (newValue);

                    if (korisnik == null)
                    {
                        Greska.Show(-4, "korisnik nije pronadjen!!");
                        ReadData.Read();
                        return;
                    }

                    zaduzivanje.Korisnik = korisnik;
                    korisnik.Zaduzivanja.Add(zaduzivanje);
                    _ = Admin.Biblioteka.Korisnici.Promeni(korisnik);

                    Korisnik stariKorisnik
                        = Admin.Biblioteka.Korisnici.UcitajPoPunomImenu
                        (PreEdit.OldValue.ToString());
                    _ = stariKorisnik.Zaduzivanja.Remove(zaduzivanje);
                    _ = Admin.Biblioteka.Korisnici.Promeni(stariKorisnik);

                    break;

                case "Knjiga":
                    Knjiga knjiga
                        = Admin.Biblioteka.Knjige.UcitajPoNazivu
                        (newValue);

                    if (knjiga == null)
                    {
                        Greska.Show(-4, "knjiga nije pronadjena!!");
                        ReadData.Read();
                        return;
                    }

                    if (!knjiga.NaStanju)
                    {
                        Greska.Show(-13);
                        ReadData.Read();
                        return;
                    }

                    knjiga.NaStanju = false;
                    zaduzivanje.Knjiga = knjiga;
                    _ = Admin.Biblioteka.Knjige.Promeni(knjiga);

                    Knjiga staraKnjiga
                        = Admin.Biblioteka.Knjige.UcitajPoNazivu
                        (PreEdit.OldValue.ToString());
                    staraKnjiga.NaStanju = true;
                    _ = Admin.Biblioteka.Knjige.Promeni(staraKnjiga);

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
                Admin.Biblioteka.Zaduzivanja.Promeni(zaduzivanje,
                int.Parse(newValue))
                : Admin.Biblioteka.Zaduzivanja.Promeni(zaduzivanje);
            bool success = result == 1;

            if (!success)
            {
                Greska.Show(-2);
            }

            ReadData.Read();
        }

        // za autore
        public static void dataGridView4_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue = Admin.DataGridView4.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim();
            string atribut = Admin.DataGridView4.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == PreEdit.OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id"
                ? (int)PreEdit.OldValue
                : (int)Admin.DataGridView4.Rows
                [e.RowIndex].Cells[0].Value;

            Autor autor = Admin.Biblioteka.Autori.UcitajPoId(id);

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
                ? Admin.Biblioteka.Autori.Promeni
                (autor, int.Parse(newValue))
                : Admin.Biblioteka.Autori.Promeni
                (autor);
            bool success = result == 1;

            if (!success)
            {
                if (atribut == "PunoIme")
                {
                    // otkazi promene
                    string ime = PreEdit.OldValue.ToString()
                        .Split(' ')[0];
                    string prezime = PreEdit.OldValue.ToString()
                        .Split(' ')[1];
                    autor.Ime = ime;
                    autor.Prezime = prezime;
                }
            }

            ReadData.Read();
        }

        // za kategorije
        public static void dataGridView5_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue = Admin.DataGridView5.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim();
            string atribut = Admin.DataGridView5.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == PreEdit.OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id"
                ? (int)PreEdit.OldValue
                : (int)Admin.DataGridView5.Rows
                [e.RowIndex].Cells[0].Value;

            Kategorija kategorija = Admin.Biblioteka
                .Kategorije.UcitajPoId(id);

            switch (atribut)
            {
                case "Naziv":
                    string naziv = newValue;
                    kategorija.Naziv = naziv;
                    break;
            }

            short result = atribut == "Id"
                ? Admin.Biblioteka.Kategorije.Promeni
                (kategorija, int.Parse(newValue))
                : Admin.Biblioteka.Kategorije.Promeni
                (kategorija);
            bool success = result == 1;

            if (!success)
            {
                if (atribut == "Naziv")
                {
                    // otkazi promene
                    string naziv = PreEdit.OldValue.ToString();
                    kategorija.Naziv = naziv;
                }
            }

            ReadData.Read();
        }

        // izmena checkboxa u celiji
        // za knjige
        public static void dataGridView1_CurrentCellDirtyStateChanged
            (object sender, EventArgs e)
        {
            if (Admin.DataGridView1.IsCurrentCellDirty)
            {
                if (Admin.DataGridView1.CurrentCell is
                    DataGridViewCheckBoxCell)
                {
                    _ = Admin.DataGridView1.CommitEdit
                        (DataGridViewDataErrorContexts.Commit);
                }
            }
        }
    }
}
