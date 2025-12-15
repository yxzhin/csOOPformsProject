using csOOPformsProject.Core;
using csOOPformsProject.Serialized;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.BibliotekarForm
{
    public static class ReadData
    {
        public static Admin Admin { get; set; }

        public static void Read()
        {
            Admin.Bibliotekar =
                Admin.Biblioteka.Bibliotekari.UcitajPoId
                (Biblioteka.BibliotekarId.Value);

            Admin.ChangeIdText(Admin.Bibliotekar.Id.ToString());
            Admin.ChangeNameText(Admin.Bibliotekar.PunoIme);
            Admin.ChangeDateText(Admin.Bibliotekar.DatumRodjenja
            .ToShortDateString().ToString());

            Admin.DataGridView1.ReadOnly = false;
            Admin.DataGridView3.ReadOnly = false;
            Admin.DataGridView4.ReadOnly = false;
            Admin.DataGridView5.ReadOnly = false;

            Admin.DataGridView1.DataSource = null;
            Admin.DataGridView3.DataSource = null;
            Admin.DataGridView4.DataSource = null;
            Admin.DataGridView5.DataSource = null;

            // serialized objekat ispravno radi u dataGridView

            List<SerializedKnjiga> serializedKnjige =
                Helpers.Serialize.Knjige
                (Admin.Biblioteka.Knjige.UcitajSve());
            Sort.Dgv1Bs.DataSource = serializedKnjige;

            List<SerializedZaduzivanje> serializedZaduzivanja =
                Helpers.Serialize.Zaduzivanja
                (Admin.Biblioteka.Zaduzivanja.UcitajSve());
            Sort.Dgv3Bs.DataSource = serializedZaduzivanja;

            List<SerializedAutor> serializedAutori =
                Helpers.Serialize.Autori
                (Admin.Biblioteka.Autori.UcitajSve());
            Sort.Dgv4Bs.DataSource = serializedAutori;

            List<SerializedKategorija> serializedKategorije =
                Helpers.Serialize.Kategorije
                (Admin.Biblioteka.Kategorije.UcitajSve());
            Sort.Dgv5Bs.DataSource = serializedKategorije;

            Admin.DataGridView1.DataSource = Sort.Dgv1Bs;
            Admin.DataGridView3.DataSource = Sort.Dgv3Bs;
            Admin.DataGridView4.DataSource = Sort.Dgv4Bs;
            Admin.DataGridView5.DataSource = Sort.Dgv5Bs;

            if (serializedKnjige.Count == 0)
            {
                Admin.DataGridView1.ReadOnly = true;
                Admin.DataGridView1.DataSource = Admin.Nista;
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
                        Admin.Biblioteka.Autori.UcitajSve()
                        .Select(x => x.ToString()).ToList()
                    };

                Admin.DataGridView1.Columns.Remove("Autor");
                _ = Admin.DataGridView1.Columns.Add(knjigeIzborAutora);

                // napravi comboxBox za izbor kategorije knjige
                DataGridViewComboBoxColumn knjigeIzborKategorije
                    = new DataGridViewComboBoxColumn
                    {
                        Name = "Kategorija",
                        DataPropertyName = "Kategorija",
                        HeaderText = "Kategorija",
                        DataSource =
                        Admin.Biblioteka.Kategorije.UcitajSve()
                        .Select(x => x.ToString()).ToList()
                    };

                Admin.DataGridView1.Columns.Remove("Kategorija");
                _ = Admin.DataGridView1.Columns.Add(knjigeIzborKategorije);
            }

            if (serializedZaduzivanja.Count == 0)
            {
                Admin.DataGridView3.ReadOnly = true;
                Admin.DataGridView3.DataSource = Admin.Nista;
            }
            else
            {
                // istekli rok zaduzivanja se ne sme rucno menjati
                Admin.DataGridView3.Columns[6].ReadOnly = true;
            }

            if (serializedAutori.Count == 0)
            {
                Admin.DataGridView4.ReadOnly = true;
                Admin.DataGridView4.DataSource = Admin.Nista;
            }

            if (serializedKategorije.Count == 0)
            {
                Admin.DataGridView5.ReadOnly = true;
                Admin.DataGridView5.DataSource = Admin.Nista;
            }
        }
    }
}
