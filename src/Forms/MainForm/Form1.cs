using csOOPformsProject.Core;
using System;
using System.Windows.Forms;

namespace csOOPformsProject
{
    public partial class Form1 : Form
    {
        private Biblioteka Biblioteka { get; }
        public Form1()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Biblioteka = new Biblioteka();
            Biblioteka.Seeder();

            textBox3.PasswordChar = '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void OcistiInpute()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        // uloguj se
        private void button1_Click(object sender, EventArgs e)
        {
            string ime = textBox1.Text.Trim();
            string prezime = textBox2.Text.Trim();
            string sifra = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(ime)
                || string.IsNullOrEmpty(prezime)
                || string.IsNullOrEmpty(sifra))
            {
                Greska.Show(-3);
                return;
            }

            Form form = Biblioteka.UlogujSe(ime, prezime, sifra);
            if (form == null)
            {
                if (Biblioteka.FailedLoginUserType == "korisnik"
                    && Biblioteka.KorisnikId != null)
                {
                    Greska.Show(-16, "Korisnik");
                    return;
                }

                if (Biblioteka.FailedLoginUserType == "bibliotekar"
                    && Biblioteka.BibliotekarId != null)
                {
                    Greska.Show(-16, "Bibliotekar");
                    return;
                }

                if (Biblioteka.FailedLoginUserType == "direktor"
                    && Biblioteka.DirektorId != null)
                {
                    Greska.Show(-16, "Direktor");
                    return;
                }
                Greska.Show(-4);
                return;
            }
            OcistiInpute();
            form.Show();
        }

        // registruj se
        private void button2_Click(object sender, EventArgs e)
        {
            Registracija registracia
                = new Registracija(Biblioteka);
            registracia.Show();
        }
    }
}
