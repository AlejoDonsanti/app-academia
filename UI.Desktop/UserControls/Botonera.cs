using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class Botonera : UserControl
    {
        public string Module { get; set; }
        public Botonera()
        {
            InitializeComponent();
        }

        public virtual void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        public virtual void tsbEditar_Click(object sender, EventArgs e)
        {

        }

        public virtual void tsbEliminar_Click(object sender, EventArgs e)
        {

        }

        public virtual void tsbReporte_Click(object sender, EventArgs e)
        {

        }
    }
}
