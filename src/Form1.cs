using System;
using System.Windows.Forms;

namespace csOOPformsProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //@DEBUG
            //label1.Text = Helpers.DataFolder;

            Core.Biblioteka b = new Core.Biblioteka();
            b.Korisnici.Dodaj(new Models.Korisnik(b.Korisnici.PoslednjiId(),
                "foo", "bar", DateTime.Now, DateTime.Now));
            Models.Autor a = new Models.Autor(0, "test71", "test31");
            Models.Kategorija k = new Models.Kategorija(0, "test732");
            b.Knjige.Dodaj(new Models.Knjiga(b.Knjige.PoslednjiId(), "test22", a, k));
            label1.Text = string.Join(",", b.Knjige.UcitajSve());

            label2.Text = b.PozajmiKnjigu(b.Korisnici.PoslednjiId(),
                b.Knjige.PoslednjiId()).ToString();
            label2.Text += $";{b.Zaduzivanja.UcitajPoId(1)}";

        }
    }
}
