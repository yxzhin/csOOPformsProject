using System.Windows.Forms;

namespace csOOPformsProject.Core
{
    public static class Greska
    {
        public static void Show(int type)
        {
            switch (type)
            {
                case -1:
                    _ = MessageBox.Show("unesite validne vrednosti!!",
                    "greska!!", MessageBoxButtons.OK, MessageBoxIcon.Error); break;
                case -2:
                    _ = MessageBox.Show("id je vec zauzet!!",
                    "greska!!", MessageBoxButtons.OK, MessageBoxIcon.Error); break;
            }

        }
    }
}
