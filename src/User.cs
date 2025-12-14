using csOOPformsProject.Models;
using System;
using System.Windows.Forms;

namespace csOOPformsProject.Core
{
    public partial class User : Form
    {
        private Biblioteka Biblioteka { get; }
        public static Korisnik Korisnik { get; set; } = null;
        public User(Biblioteka biblioteka, Korisnik korisnik)
        {
            Biblioteka = biblioteka;
            Korisnik = korisnik;

            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            FormClosing += User_FormClosing;
        }

        private void User_Load(object sender, EventArgs e)
        {

        }

        // izloguj se
        private void User_FormClosing(object sender,
            FormClosingEventArgs e)
        {
            Biblioteka.KorisnikId = null;
        }
    }
}
