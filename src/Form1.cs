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
            /*
            Core.Biblioteka b = new Core.Biblioteka();
            Models.Autor a = new Models.Autor(0, "test71", "test31");
            Models.Kategorija k = new Models.Kategorija(0, "test732");
            b.Knjige.Dodaj(new Models.Knjiga(0, "test22", a, k));
            label1.Text = string.Join(",", b.Knjige.UcitajSve());
            */



        }
    }
}
