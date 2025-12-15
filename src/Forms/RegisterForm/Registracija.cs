using csOOPformsProject.Core;
using csOOPformsProject.Models;
using System;
using System.Windows.Forms;

namespace csOOPformsProject
{
    public partial class Registracija : Form
    {
        private Biblioteka Biblioteka { get; }

        public Registracija(Biblioteka biblioteka)
        {
            InitializeComponent();

            Biblioteka = biblioteka;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            textBox3.PasswordChar = '*';
        }

        private void Registracija_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ime = textBox1.Text.Trim();
            string prezime = textBox2.Text.Trim();
            string sifra = textBox3.Text.Trim();
            string datumRodjenja = textBox4.Text.Trim();

            if (string.IsNullOrEmpty(ime)
                || string.IsNullOrEmpty(prezime)
                || string.IsNullOrEmpty(sifra)
                || string.IsNullOrEmpty(datumRodjenja)
                || !DateTime.TryParse(datumRodjenja, out _))
            {
                Greska.Show(-1);
                return;
            }

            int poslednjiId = Biblioteka.Korisnici.PoslednjiId();
            Korisnik korisnik = new Korisnik(poslednjiId,
                ime, prezime, DateTime.Parse(datumRodjenja),
                sifra, DateTime.Now);
            Biblioteka.Korisnici.Dodaj(korisnik);
            OcistiInpute();
            _ = MessageBox.Show("uspesno ste se registrovali!!",
                "uspeh!!", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void OcistiInpute()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}
