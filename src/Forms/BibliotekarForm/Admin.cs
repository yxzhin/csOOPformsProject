using csOOPformsProject.Core;
using csOOPformsProject.Forms.BibliotekarForm;
using csOOPformsProject.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace csOOPformsProject
{
    public partial class Admin : Form
    {
        public static Biblioteka Biblioteka { get; set; }
        public static Bibliotekar Bibliotekar { get; set; } = null;

        public static List<Helpers.Nista> Nista
            = new List<Helpers.Nista>
            {
                new Helpers.Nista(),
            };

        public static DataGridView DataGridView1 { get; set; }
        public static DataGridView DataGridView3 { get; set; }
        public static DataGridView DataGridView4 { get; set; }
        public static DataGridView DataGridView5 { get; set; }

        public Admin(Biblioteka biblioteka, Bibliotekar bibliotekar)
        {
            InitializeComponent();

            ReadData.Admin = this;

            DataGridView1 = dataGridView1;
            DataGridView3 = dataGridView3;
            DataGridView4 = dataGridView4;
            DataGridView5 = dataGridView5;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Biblioteka = biblioteka;
            Bibliotekar = bibliotekar;
            Biblioteka.BibliotekarId = bibliotekar.Id;

            dataGridView1.CellBeginEdit
                += PreEdit.dataGridView1_CellBeginEdit;
            dataGridView3.CellBeginEdit
                += PreEdit.dataGridView3_CellBeginEdit;
            dataGridView4.CellBeginEdit
                += PreEdit.dataGridView4_CellBeginEdit;
            dataGridView5.CellBeginEdit
                += PreEdit.dataGridView5_CellBeginEdit;

            dataGridView1.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (Validation.dataGridView1_CellValidating);
            dataGridView3.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (Validation.dataGridView3_CellValidating);
            dataGridView4.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (Validation.dataGridView4_CellValidating);
            dataGridView5.CellValidating
                += new DataGridViewCellValidatingEventHandler
                (Validation.dataGridView5_CellValidating);

            dataGridView1.CellValueChanged
                += UpdateData.dataGridView1_CellValueChanged;
            dataGridView3.CellValueChanged
                += UpdateData.dataGridView3_CellValueChanged;
            dataGridView4.CellValueChanged
                += UpdateData.dataGridView4_CellValueChanged;
            dataGridView5.CellValueChanged
                += UpdateData.dataGridView5_CellValueChanged;

            //dataGridView1.DataError += dataGridView1_DataError;

            dataGridView1.CurrentCellDirtyStateChanged
                += UpdateData.dataGridView1_CurrentCellDirtyStateChanged;
            /*
            dataGridView3.CurrentCellDirtyStateChanged
                += dataGridView3_CurrentCellDirtyStateChanged;
            */

            dataGridView1.MultiSelect = false;
            dataGridView3.MultiSelect = false;
            dataGridView4.MultiSelect = false;
            dataGridView5.MultiSelect = false;

            FormClosing += Admin_FormClosing;

            dataGridView1.ColumnHeaderMouseClick
                += Sort.dataGridView1_ColumnHeaderMouseClick;
            dataGridView3.ColumnHeaderMouseClick
                += Sort.dataGridView3_ColumnHeaderMouseClick;
            dataGridView4.ColumnHeaderMouseClick
                += Sort.dataGridView4_ColumnHeaderMouseClick;
            dataGridView5.ColumnHeaderMouseClick
                += Sort.dataGridView5_ColumnHeaderMouseClick;
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

        private void Admin_Load(object sender, EventArgs e)
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

        // izloguj se
        private void Admin_FormClosing(object sender,
            FormClosingEventArgs e)
        {
            Biblioteka.BibliotekarId = null;
        }
    }
}
