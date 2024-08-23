using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Methode_ROA
{
    public partial class Trace : Form
    {
        public Trace(string chaine)
        {
            InitializeComponent();
            webBrowser1.DocumentText = chaine;
        }

        private void Trace_Load(object sender, EventArgs e)
        {

        }
    }
}
