using csOOPformsProject.Core;
using csOOPformsProject.Forms.DirektorForm;
using csOOPformsProject.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace csOOPformsProject
{
    public partial class DirektorPanel : Form
    {
        public static Biblioteka Biblioteka { get; set; }
        public static Direktor Direktor { get; set; } = null;

        public static List<Helpers.Nista> Nista
            = new List<Helpers.Nista>
            {
                new Helpers.Nista(),
            };

        public static DataGridView DataGridView1 { get; set; }
        public static DataGridView DataGridView2 { get; set; }

        public DirektorPanel(Biblioteka biblioteka, Direktor direktor)
        {
            InitializeComponent();

            ReadData.DirektorPanel = this;
            UpdateData.DirektorPanel = this;
            DeleteData.DirektorPanel = this;

            DataGridView1 = dataGridView1;
            DataGridView2 = dataGridView2;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Biblioteka = biblioteka;
            Direktor = direktor;
            Biblioteka.DirektorId = direktor.Id;

            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button4.Click += button4_Click;
            button3.Click += button3_Click;

            dataGridView1.CellBeginEdit
                += PreEdit.dataGridView1_CellBeginEdit;
            dataGridView2.CellBeginEdit
                += PreEdit.dataGridView2_CellBeginEdit;

            dataGridView1.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (Validation.dataGridView1_CellValidating);
            dataGridView2.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (Validation.dataGridView2_CellValidating);

            dataGridView1.CellValueChanged
                += UpdateData.dataGridView1_CellValueChanged;
            dataGridView2.CellValueChanged
                += UpdateData.dataGridView2_CellValueChanged;

            dataGridView1.MultiSelect = false;
            dataGridView2.MultiSelect = false;

            FormClosing += Direktor_FormClosing;

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

        private void DirektorPanel_Load(object sender, EventArgs e)
        {
            ReadData.Read();
        }

        // prikazi podatke
        private void button1_Click(object sender, EventArgs e)
        {
            ReadData.Read();
        }

        // dodaj
        private void button2_Click(object sender, EventArgs e)
        {
            CreateData.Create();
        }

        // obrisi
        private void button4_Click(object sender, EventArgs e)
        {
            DeleteData.Delete();
        }

        // resetuj
        private void button3_Click(object sender, EventArgs e)
        {
            //Biblioteka.ResetujPodatke();
            Biblioteka.Seeder();
            ReadData.Read();
        }

        // izloguj se
        private void Direktor_FormClosing(object sender,
            FormClosingEventArgs e)
        {
            Biblioteka.DirektorId = null;
        }
    }
}
