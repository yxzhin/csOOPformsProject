using csOOPformsProject.Core;
using csOOPformsProject.Models;
using System;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.DirektorForm
{
    public static class UpdateData
    {
        public static DirektorPanel DirektorPanel { get; set; }

        // izmena celije
        // za korisnike
        public static void dataGridView1_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue = DirektorPanel
                .DataGridView1.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim();
            string atribut = DirektorPanel
                .DataGridView1.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == PreEdit
                .OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id"
                ? (int)PreEdit.OldValue
                : (int)DirektorPanel.DataGridView1.Rows
                [e.RowIndex].Cells[0].Value;

            Korisnik korisnik = DirektorPanel
                .Biblioteka.Korisnici.UcitajPoId(id);

            switch (atribut)
            {
                case "PunoIme":
                    string ime = newValue.Split(' ')[0];
                    string prezime = newValue.Split(' ')[1];
                    korisnik.Ime = ime;
                    korisnik.Prezime = prezime;
                    break;

                case "Sifra":
                    string sifra = newValue;
                    korisnik.Sifra = sifra;
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

            if (atribut == "Id"
                && int.Parse(PreEdit
                .OldValue.ToString())
                == Biblioteka.KorisnikId)
            {
                Biblioteka.KorisnikId = int.Parse(newValue);
            }

            short result = atribut == "Id"
                ? DirektorPanel.Biblioteka
                .Korisnici.Promeni
                (korisnik, int.Parse(newValue))
                : DirektorPanel.Biblioteka
                .Korisnici.Promeni
                (korisnik);
            bool success = result == 1;

            if (!success)
            {
                if (atribut == "PunoIme")
                {
                    // otkazi promene
                    string ime = PreEdit
                        .OldValue.ToString()
                        .Split(' ')[0];
                    string prezime = PreEdit
                        .OldValue.ToString()
                        .Split(' ')[1];
                    korisnik.Ime = ime;
                    korisnik.Prezime = prezime;
                }
                else if (atribut == "Sifra")
                {
                    string sifra = PreEdit
                        .OldValue.ToString();
                    korisnik.Sifra = sifra;
                }
            }

            ReadData.Read();
        }

        // za bibliotekare
        public static void dataGridView2_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int id;
            string newValue = DirektorPanel
                .DataGridView2.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Value.ToString().Trim();
            string atribut = DirektorPanel
                .DataGridView2.Columns[e.ColumnIndex]
                .HeaderCell.Value.ToString();

            if (newValue == PreEdit
                .OldValue.ToString())
            {
                return;
            }

            id = atribut == "Id"
                ? (int)PreEdit.OldValue
                : (int)DirektorPanel.DataGridView2.Rows
                [e.RowIndex].Cells[0].Value;

            Bibliotekar bibliotekar
                = DirektorPanel.Biblioteka
                .Bibliotekari.UcitajPoId(id);

            switch (atribut)
            {
                case "PunoIme":
                    string ime = newValue.Split(' ')[0];
                    string prezime = newValue.Split(' ')[1];
                    bibliotekar.Ime = ime;
                    bibliotekar.Prezime = prezime;
                    break;

                case "DatumRodjenja":
                    DateTime datumRodjenja =
                        DateTime.Parse(newValue);
                    bibliotekar.DatumRodjenja = datumRodjenja;
                    break;

                case "Sifra":
                    string sifra = newValue;
                    bibliotekar.Sifra = sifra;
                    break;

                case "SifraRadnika":
                    string sifraRadnika = newValue;
                    bibliotekar.SifraRadnika = sifraRadnika;
                    break;
            }

            if (atribut == "Id")
            {
                if (int.Parse(PreEdit
                    .OldValue.ToString())
                == Biblioteka.BibliotekarId)
                {
                    Biblioteka.BibliotekarId = int.Parse(newValue);
                }
                else if (int.Parse(PreEdit
                    .OldValue.ToString())
                == Biblioteka.DirektorId)
                {
                    Biblioteka.DirektorId = int.Parse(newValue);
                }
            }

            short result = atribut == "Id"
                ? DirektorPanel.Biblioteka
                .Bibliotekari.Promeni
                (bibliotekar, int.Parse(newValue))
                : DirektorPanel.Biblioteka
                .Bibliotekari.Promeni
                (bibliotekar);
            bool success = result == 1;

            if (!success)
            {
                if (atribut == "PunoIme")
                {
                    // otkazi promene
                    string ime = PreEdit
                        .OldValue.ToString()
                        .Split(' ')[0];
                    string prezime = PreEdit
                        .OldValue.ToString()
                        .Split(' ')[1];
                    bibliotekar.Ime = ime;
                    bibliotekar.Prezime = prezime;
                }
                else if (atribut == "Sifra")
                {
                    string sifra = PreEdit
                        .OldValue.ToString();
                    bibliotekar.Sifra = sifra;
                }
                else if (atribut == "SifraRadnika")
                {
                    string sifraRadnika = PreEdit
                        .OldValue.ToString();
                    bibliotekar.SifraRadnika = sifraRadnika;
                }
            }

            ReadData.Read();
        }
    }
}
