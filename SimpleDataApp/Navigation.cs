using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleDataApp {
    public partial class Navigation : Form {
        public Navigation() {
            InitializeComponent();
        }

        private void Navigation_Load(object sender, EventArgs e) {

        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void btnGoToAdd_Click(object sender, EventArgs e) {
            Form frm = new NewCustomer();
            frm.Show();
        }

        private void btnGoToFillOrCancel_Click(object sender, EventArgs e) {
            Form frm = new FillOrCancel();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
