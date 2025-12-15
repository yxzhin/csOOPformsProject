using csOOPformsProject.Core;
using csOOPformsProject.Models;
using System;
using System.Windows.Forms;

namespace csOOPformsProject.Forms.DirektorForm
{
    public static class CreateData
    {
        public static void Create()
        {
            if (Redovi.NistaNijeIzabrano())
            {
                Greska.Show(-5);
                return;
            }

            if (Redovi.IzabranoViseOdJednogRedaOdjednom())
            {
                Greska.Show(-6);
                return;
            }

            DataGridViewRow izabraniRed = Redovi.UcitajIzabraniRed();

            switch (izabraniRed.DataGridView.Name)
            {
                // dodaj korisnika
                case "dataGridView1":
                    int poslednjiId = DirektorPanel.Biblioteka
                        .Korisnici.PoslednjiId();
                    Korisnik korisnik = new Korisnik(poslednjiId,
                        "novi", $"korisnik{poslednjiId + 1}",
                        new DateTime(2073, 7, 3),
                        $"sifra{poslednjiId + 1}",
                        new DateTime(2037, 3, 7));
                    DirektorPanel.Biblioteka
                        .Korisnici.Dodaj(korisnik);

                    break;

                // dodaj bibliotekara
                case "dataGridView2":
                    poslednjiId = DirektorPanel.Biblioteka
                        .Bibliotekari.PoslednjiId();
                    Bibliotekar bibliotekar
                        = new Bibliotekar(poslednjiId,
                        "novi", $"bibliotekar{poslednjiId + 1}",
                        new DateTime(2073, 7, 3),
                        $"sifra{poslednjiId + 1}",
                        $"sifraRadnika{poslednjiId + 1}");
                    DirektorPanel.Biblioteka
                        .Bibliotekari.Dodaj(bibliotekar);

                    break;
            }

            ReadData.Read();
        }
    }
}
