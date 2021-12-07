using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poprigun
{
    public partial class Form2 : Form
    {
        public Agent agnt { get; set; } = null;
        public Model1 db { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (agnt == null)
            {
                agentBindingSource.AddNew();
            }
            else
            {
                agentBindingSource.Add(agnt);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (titleTextBox.Text == "" || addressTextBox.Text == "" || iNNTextBox.Text == "" || kPPTextBox.Text == "" || directorNameTextBox.Text == "" || phoneTextBox.Text == "" || emailTextBox.Text == "" ||
                logoTextBox.Text == "" || priorityTextBox.Text == "" || agentTypeIDTextBox.Text == "")
            {
                MessageBox.Show("Заполните пустые поля!");
            }
            else if (agnt == null)
            {
                agnt = (Agent)agentBindingSource.Current;
                db.Agent.Add(agnt);
            }
            db.SaveChanges();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
