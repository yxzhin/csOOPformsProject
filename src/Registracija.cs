using csOOPformsProject.Core;
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

        }

        private void Registracija_Load(object sender, EventArgs e)
        {

        }
    }
}
