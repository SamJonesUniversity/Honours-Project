using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENP1
{
    public partial class EntryForm : Form
    {
        public EntryForm()
        {
            InitializeComponent();
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            TestForm f1 = new TestForm();
            Hide();
            f1.ShowDialog();
            Show();
        }

        private void InputBtn_Click(object sender, EventArgs e)
        {
            InputForm f1 = new InputForm();
            Hide();
            f1.ShowDialog();
            Show();
        }
    }
}
