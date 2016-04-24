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
        Stepper_Handler stepper_handler;

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
            foreach (NumericUpDown text in lengths)
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

        }

        private void path1_wid_TextChanged(object sender, EventArgs e)
        {

        }

        private void path2_wid_TextChanged(object sender, EventArgs e)
        {

        }

        private void path3_wid_TextChanged(object sender, EventArgs e)
        {
        }

        private void path4_wid_TextChanged(object sender, EventArgs e)
        {

        }

        private void path5_wid_TextChanged(object sender, EventArgs e)
        {

        }

        private void path6_wid_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox6.Enabled = true;
                path6_wid.Enabled = true;
                length6_wid.Enabled = true;
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
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox5.Enabled = true;
                path5_wid.Enabled = true;
                length5_wid.Enabled = true;
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
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox4.Enabled = true;
                path4_wid.Enabled = true;
                length4_wid.Enabled = true;
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
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
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
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
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
            }
        }

        private void length1_wid_TextChanged(object sender, EventArgs e)
        {
        }

        private void length2_wid_TextChanged(object sender, EventArgs e)
        {
        }

        private void length3_wid_TextChanged(object sender, EventArgs e)
        {
        }

        private void length4_wid_TextChanged(object sender, EventArgs e)
        {
        }

        private void length5_wid_TextChanged(object sender, EventArgs e)
        {
        }

        private void length6_wid_TextChanged(object sender, EventArgs e)
        {
        }

        private void go_button_Click(object sender, EventArgs e)
        {
            steppers = new List<MyStepper>();
            for (int i = 0; checkBoxes[i].Checked; i++)
            {
                steppers.Add(new MyStepper(paths[i].Text, (int)lengths[i].Value));
            }
            stepper_handler = new Stepper_Handler(steppers,this);
            stepper_handler.run();
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


        public void lengths_Change(List<double> values)
        {
            for (int i = 0; i < values.Count; i++)
                lengths[i].Value =(decimal)values[i];
        }

        private void Browse_root_Click(object sender, EventArgs e)
        {
            int size = -1;
            FolderBrowserDialog fDialog = new FolderBrowserDialog();
            DialogResult result =fDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                root_path_wid.Text = fDialog.SelectedPath;
                rootPath = fDialog.SelectedPath; 
            }
        }
    }
}
