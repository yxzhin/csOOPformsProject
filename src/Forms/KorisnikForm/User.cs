using csOOPformsProject.Core;
using csOOPformsProject.Forms.KorisnikForm;
using csOOPformsProject.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace csOOPformsProject
{
    public partial class User : Form
    {
        public static Biblioteka Biblioteka { get; set; }
        public static Korisnik Korisnik { get; set; } = null;

        public static List<Helpers.Nista> Nista
            = new List<Helpers.Nista>
            {
                new Helpers.Nista(),
            };

        public static DataGridView DataGridView1 { get; set; }
        public static DataGridView DataGridView2 { get; set; }

        public User(Biblioteka biblioteka, Korisnik korisnik)
        {
            InitializeComponent();

            CreateData.User = this;
            ReadData.User = this;
            DeleteData.User = this;

            DataGridView1 = dataGridView1;
            DataGridView2 = dataGridView2;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Biblioteka = biblioteka;
            Korisnik = korisnik;
            Biblioteka.KorisnikId = korisnik.Id;

            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;

            dataGridView1.MultiSelect = false;
            dataGridView2.MultiSelect = false;

            FormClosing += User_FormClosing;

            dataGridView1.ColumnHeaderMouseClick
                += Sort.dataGridView1_ColumnHeaderMouseClick;
            dataGridView2.ColumnHeaderMouseClick
                += Sort.dataGridView2_ColumnHeaderMouseClick;
        }

        public void ChangeIdText(string text)
        {
            label10.Text = text;
        }

        public void ChangeNameText(string text)
        {
            label6.Text = text;
        }

        public void ChangeDateText(string text)
        {
            label8.Text = text;
        }

        private void User_Load(object sender, EventArgs e)
        {
            ReadData.Read();
        }

        // prikazi podatke
        private void button1_Click(object sender, EventArgs e)
        {
            ReadData.Read();
        }

        // pozajmi knjigu
        private void button2_Click(object sender, EventArgs e)
        {
            CreateData.Create();
        }

        // vrati knjigu
        private void button3_Click(object sender, EventArgs e)
        {
            DeleteData.Delete();
        }

        // izloguj se
        private void User_FormClosing(object sender,
            FormClosingEventArgs e)
        {
            Biblioteka.KorisnikId = null;
        }
    }
}
