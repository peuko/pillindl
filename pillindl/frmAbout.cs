using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace pillindl {
    public partial class frmAbout : Form {
        string urlcontact = "http://wenupix.cl/wp/contacto";

        public frmAbout() {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void frmAbout_Load(object sender, EventArgs e) {
            lblWebpage.Links.Add(0, 0, urlcontact);
        }

        private void lblWebpage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(urlcontact);
        }
    }
}
