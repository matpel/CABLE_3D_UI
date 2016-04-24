using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Move_cable;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        private List<TextBox> paths;
        private List<NumericUpDown> lengths;
        private List<CheckBox> checkBoxes;
        private String rootPath;
        private int step;

        private List<MyStepper> steppers;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            paths = new List<TextBox> { path1_wid, path2_wid, path3_wid, path4_wid, path5_wid, path6_wid };
            checkBoxes = new List<CheckBox> { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6 };
            lengths = new List<NumericUpDown> { length1_wid, length2_wid, length3_wid, length4_wid, length5_wid, length6_wid };
            rootPath = root_path_wid.Text;
            step = 0;
            steppers = new List<MyStepper>();
            steppers.Add(new MyStepper());

            foreach (CheckBox check in checkBoxes)
            {
                check.Enabled = false;
            }
            checkBoxes[0].Enabled = true;
            foreach (TextBox text in paths)
            {
                text.Enabled = false;
            }
            paths[0].Enabled = true;
            foreach(NumericUpDown text in lengths)
            {
                text.Enabled = false;
            }
            lengths[0].Enabled = true;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void root_path_wid_TextChanged(object sender, EventArgs e)
        {
            rootPath = root_path_wid.Text;
            for (int i = 0; i < 6; i++)
            {
                paths[i].Text = rootPath + @"\coords" + (i + 1);
            }
            for (int i = 0; i < steppers.Count; i++)
            {
                steppers[i].Path = paths[i].Text;
            }
        }

        private void path1_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[0].Path = paths[0].Text;
        }

        private void path2_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[1].Path = paths[1].Text;
        }

        private void path3_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[2].Path = paths[2].Text;
        }

        private void path4_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[3].Path = paths[3].Text;
        }

        private void path5_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[4].Path = paths[4].Text;
        }

        private void path6_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[5].Path = paths[5].Text;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                steppers.Add(new MyStepper(path6_wid.Text, (int)length6_wid.Value));
            }
            else
            {
                steppers.RemoveAt(5);
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox6.Enabled = true;
                path6_wid.Enabled = true;
                length6_wid.Enabled = true;
                steppers.Add(new MyStepper(path5_wid.Text, (int)length5_wid.Value));
            }
            else
            {
                for (int i = 5; i < 6; i++)
                { 
                    checkBoxes[i].Enabled = false;
                    checkBoxes[i].Checked = false;
                    paths[i].Enabled = false;
                    lengths[i].Enabled = false;
                }
                steppers.RemoveRange(4, steppers.Count-4);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox5.Enabled = true;
                path5_wid.Enabled = true;
                length5_wid.Enabled = true;
                steppers.Add(new MyStepper(path4_wid.Text, (int)length4_wid.Value));
            }
            else
            {
                steppers.RemoveAt(3);
                for (int i = 4; i < 6; i++)
                {
                    checkBoxes[i].Enabled = false;
                    checkBoxes[i].Checked = false;
                    paths[i].Enabled = false;
                    lengths[i].Enabled = false;
                }
                steppers.RemoveRange(3, steppers.Count-3);
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox4.Enabled = true;
                path4_wid.Enabled = true;
                length4_wid.Enabled = true;
                steppers.Add(new MyStepper(path3_wid.Text,(int)length3_wid.Value));
            }
            else
            {
                for (int i = 3; i < 6; i++)
                {
                    checkBoxes[i].Enabled = false;
                    checkBoxes[i].Checked = false;
                    paths[i].Enabled = false;
                    lengths[i].Enabled = false;
                }
                steppers.RemoveRange(2, steppers.Count-2);
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                steppers.Add(new MyStepper(path2_wid.Text, (int)length2_wid.Value));
                checkBox3.Enabled = true;
                path3_wid.Enabled = true;
                length3_wid.Enabled = true;
            }
            else
            {
                for (int i = 2; i < 6; i++)
                {
                    checkBoxes[i].Enabled = false;
                    checkBoxes[i].Checked = false;
                    paths[i].Enabled = false;
                    lengths[i].Enabled = false;
                }
                steppers.RemoveRange(1, steppers.Count-1);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                steppers.Add(new MyStepper(path1_wid.Text, (int)length1_wid.Value));
                checkBox2.Enabled = true;
                path2_wid.Enabled = true;
                length2_wid.Enabled = true;
            }
            else
            {
                for (int i = 1; i < 6; i++)
                {
                    checkBoxes[i].Enabled = false;
                    checkBoxes[i].Checked = false;
                    paths[i].Enabled = false;
                    lengths[i].Enabled = false;
                }
                steppers.RemoveRange(0, steppers.Count);
            }
        }

        private void length1_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[0].Initial_position = (int)length1_wid.Value;
        }

        private void length2_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[1].Initial_position = (int)length2_wid.Value;
        }

        private void length3_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[2].Initial_position = (int)length3_wid.Value;
        }

        private void length4_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[3].Initial_position = (int)length4_wid.Value;
        }

        private void length5_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[4].Initial_position = (int)length5_wid.Value;
        }

        private void length6_wid_TextChanged(object sender, EventArgs e)
        {
            steppers[5].Initial_position = (int)length6_wid.Value;
        }

        private void go_button_Click(object sender, EventArgs e)
        {
            
        }

        private void length6_wid_ValueChanged(object sender, EventArgs e)
        {

        }

        private void length5_wid_ValueChanged(object sender, EventArgs e)
        {

        }

        private void length4_wid_ValueChanged(object sender, EventArgs e)
        {

        }

        private void length3_wid_ValueChanged(object sender, EventArgs e)
        {

        }

        private void length2_wid_ValueChanged(object sender, EventArgs e)
        {

        }

        private void length1_wid_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
