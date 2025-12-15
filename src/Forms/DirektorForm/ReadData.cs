using csOOPformsProject.Core;
using csOOPformsProject.Models;
using csOOPformsProject.Serialized;
using System.Collections.Generic;

namespace csOOPformsProject.Forms.DirektorForm
{
    public static class ReadData
    {
        public static DirektorPanel DirektorPanel { get; set; }

        public static void Read()
        {
            DirektorPanel.Direktor =
                DirektorPanel.Biblioteka
                .Bibliotekari.UcitajPoId
                (Biblioteka.DirektorId.Value)
                as Direktor;

            DirektorPanel.ChangeIdText
                (DirektorPanel.Direktor.Id.ToString());
            DirektorPanel.ChangeNameText
                (DirektorPanel.Direktor.PunoIme);
            DirektorPanel.ChangeDateText
                (DirektorPanel.Direktor.DatumRodjenja
                .ToShortDateString().ToString());

            DirektorPanel.DataGridView1
                .ReadOnly = false;
            DirektorPanel.DataGridView2
                .ReadOnly = false;

            DirektorPanel.DataGridView1
                .DataSource = null;
            DirektorPanel.DataGridView2
                .DataSource = null;

            // serialized objekat ispravno radi u dataGridView

            List<SerializedKorisnik> serializedKorisnici =
                Helpers.Serialize.Korisnici
                (DirektorPanel.Biblioteka
                .Korisnici.UcitajSve());
            Sort.Dgv1Bs.DataSource = serializedKorisnici;

            List<SerializedBibliotekar> serializedBibliotekari =
                Helpers.Serialize.Bibliotekari
                (DirektorPanel.Biblioteka
                .Bibliotekari.UcitajSve());
            Sort.Dgv2Bs.DataSource = serializedBibliotekari;

            DirektorPanel.DataGridView1
                .DataSource = Sort.Dgv1Bs;
            DirektorPanel.DataGridView2
                .DataSource = Sort.Dgv2Bs;

            if (serializedKorisnici.Count == 0)
            {
                DirektorPanel.DataGridView1
                    .ReadOnly = true;
                DirektorPanel.DataGridView1
                    .DataSource = DirektorPanel.Nista;
            }
            else
            {
                // zaduzivanja korisnika se ne smeju rucno menjati
                DirektorPanel.DataGridView1
                    .Columns[5].ReadOnly = true;
            }

            if (serializedBibliotekari.Count == 0)
            {
                DirektorPanel.DataGridView2
                    .ReadOnly = true;
                DirektorPanel.DataGridView2
                    .DataSource = DirektorPanel.Nista;
            }
        }
    }
}
