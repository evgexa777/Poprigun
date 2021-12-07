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
    public partial class Form1 : Form
    {
        Model1 db = new Model1();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            agentBindingSource.DataSource = db.Agent.ToList();
            agentTypeBindingSource.DataSource = db.AgentType.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                agentBindingSource.DataSource = null;
                agentBindingSource.DataSource = db.Agent.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Agent agnt = (Agent)agentBindingSource.Current;

            Form2 frm = new Form2();
            frm.agnt = agnt;

            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                agentBindingSource.DataSource = db.Agent.ToList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Agent agnt = (Agent)agentBindingSource.Current;
            DialogResult dr = MessageBox.Show("Удалить агента" + agnt.AgentType + "?", "Удалиение агента", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                db.Agent.Remove(agnt);
                try
                {
                    db.SaveChanges();
                    agentBindingSource.DataSource = db.Agent.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.InnerException.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void agentDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            Agent agnt = (Agent)agentBindingSource.Current;
            try
            {
                if (agnt == null) return;
                if (agnt.Logo != "")
                {
                    string str = agnt.Logo.Substring(1);
                    pictureBox1.Image = Image.FromFile(str);
                }
                else
                {
                    pictureBox1.Image = Image.FromFile("agents\\picture.png");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null) return;
            int n = (int)comboBox1.SelectedValue;
            agentBindingSource.DataSource = db.Agent.Where(x => x.AgentTypeID == n).ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            agentBindingSource.DataSource = db.Agent.Where(p => p.Title.Contains(textBox1.Text)
            || p.Email.Contains(textBox1.Text)).ToList();
        }
    }
}
