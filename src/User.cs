using csOOPformsProject.Models;
using System;
using System.Windows.Forms;

namespace csOOPformsProject.Core
{
    public partial class User : Form
    {
        private Biblioteka Biblioteka { get; set; }
        private Korisnik Korisnik { get; set; }
        public User(Biblioteka biblioteka, Korisnik korisnik)
        {
            Biblioteka = biblioteka;
            Korisnik = korisnik;
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {

        }
    }
}
