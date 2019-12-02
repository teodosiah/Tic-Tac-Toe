using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form2 : Form
    {
        public Form1 form1;
        public Form2(Form1 form)
        {
            InitializeComponent();
            this.form1 = form;
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            var check = sender as CheckBox;
            if(check.Name == "checkBox1")
            {
                if (check.Checked)
                {
                    checkBox2.Checked = false;
                }
                else
                {
                    checkBox2.Checked = true;
                }
            }
            else
            {
                if (check.Checked)
                {
                    checkBox1.Checked = false;
                }
                else
                {
                    checkBox1.Checked = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                form1.setPlayer('X');
            }
            else
            {
                form1.setPlayer('O');
            }
            this.Close();
            
        }
    }
}
