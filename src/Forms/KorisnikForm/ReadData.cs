using csOOPformsProject.Core;
using csOOPformsProject.Serialized;
using System.Collections.Generic;

namespace csOOPformsProject.Forms.KorisnikForm
{
    public static class ReadData
    {
        public static User User { get; set; }

        public static void Read()
        {
            User.Korisnik =
                User.Biblioteka
                .Korisnici.UcitajPoId
                (Biblioteka.KorisnikId.Value);

            User.ChangeIdText
                (User.Korisnik.Id.ToString());
            User.ChangeNameText
                (User.Korisnik.PunoIme);
            User.ChangeDateText
                (User.Korisnik.DatumRodjenja
                .ToShortDateString().ToString());

            User.DataGridView1
                .ReadOnly = true;
            User.DataGridView2
                .ReadOnly = true;

            User.DataGridView1
                .DataSource = null;
            User.DataGridView2
                .DataSource = null;

            // serialized objekat ispravno radi u dataGridView

            List<SerializedZaduzivanje> serializedZaduzivanja =
                Helpers.Serialize.Zaduzivanja
                (User.Biblioteka
                .Zaduzivanja.UcitajSve());
            _ = serializedZaduzivanja.RemoveAll
                (x => x.Korisnik !=
                User.Korisnik.PunoIme
                && x.DatumVracanja != "nista");
            Sort.Dgv1Bs.DataSource = serializedZaduzivanja;

            List<SerializedKnjiga> serializedKnjige =
                Helpers.Serialize.Knjige
                (User.Biblioteka
                .Knjige.UcitajSve());
            _ = serializedKnjige.RemoveAll
                (x => !x.NaStanju);
            Sort.Dgv2Bs.DataSource = serializedKnjige;

            User.DataGridView1
                .DataSource = Sort.Dgv1Bs;
            User.DataGridView2
                .DataSource = Sort.Dgv2Bs;

            if (serializedZaduzivanja.Count == 0)
            {
                User.DataGridView1
                    .DataSource = User.Nista;
            }
            else
            {
                User.DataGridView1
                    .Columns.Remove("Korisnik");
                User.DataGridView1
                    .Columns.Remove("DatumVracanja");
            }

            if (serializedKnjige.Count == 0)
            {
                User.DataGridView2
                    .DataSource = User.Nista;
            }
            else
            {
                User.DataGridView2
                    .Columns.Remove("NaStanju");
            }
        }
    }
}
