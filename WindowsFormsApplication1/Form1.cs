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
        private String rootPath = @"D:\Projets\Departement\Trajectoire";
        private List<NumericUpDown> lengths;
        private List<CheckBox> checkBoxes;
        private List<NumericUpDown> Mij;
        private int step;


        private List<MyStepper> steppers;
        private List<MyDummyStepper> dummy_steppers;
        private Stepper_Handler stepper_handler;
        private DummyStepper_Handler dummyStepper_Handler;

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
            Mij = new List<NumericUpDown> { M1x, M1y, M1z, M2x, M2y, M2z, M3x, M3y, M3z, M4x, M4y, M4z, M5x, M5y, M5z, M6x, M6y, M6z, P0x, P0y, P0z };

            step = 0;

            root_path_wid.Text = rootPath;
            List<String> lines;
            if (!System.IO.File.Exists(rootPath + @"\input.txt"))
                lines = new List<String> { "0,0,0", "0,0,0", "0,0,0", "0,0,0", "0,0,0", "0,0,0", "0,0,0" };
            else
                lines = new List<String>(System.IO.File.ReadAllLines(rootPath + @"\input.txt"));
            for (int i = 0; i < Mij.Count; i++)
            {
                Mij[i].Maximum = 100000000000;
                Mij[i].Minimum = -100000000000;
            }
            for (int i = 0; i < Mij.Count; i += 3)
            {
                String[] coords = lines[i / 3].Split(',');
                Mij[i].Value = (decimal)Int32.Parse(coords[0]);
                Mij[i + 1].Value = (decimal)Int32.Parse(coords[1]);
                Mij[i + 2].Value = (decimal)Int32.Parse(coords[2]);
            }

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
                paths[i].Text = rootPath + @"\coords" + (i + 1) + ".txt";
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
            if (!Dummy_check.Checked)
            {
                steppers = new List<MyStepper>();
                List<int> i_fail = new List<int>();
                for (int i = 0; checkBoxes[i].Checked; i++)
                {
                    steppers.Add(new MyStepper((int)lengths[i].Value, paths[i].Text));
                    lengths[i].BackColor = Color.FromKnownColor(KnownColor.Yellow);
                    Mij[i].BackColor = Color.FromKnownColor(KnownColor.Yellow);
                    paths[i].BackColor = Color.FromKnownColor(KnownColor.Yellow);
                    int timer = 0;
                    while (!steppers[i].isAttached() && timer <= 1000)
                    {
                        System.Threading.Thread.Sleep(2);
                        timer++;
                    }
                    if (timer <= 1000)
                    {
                        lengths[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                        Mij[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                        paths[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                    }
                    else
                    {
                        i_fail.Add(i);
                        lengths[i].BackColor = Color.FromKnownColor(KnownColor.Red);
                        Mij[i].BackColor = Color.FromKnownColor(KnownColor.Red);
                        paths[i].BackColor = Color.FromKnownColor(KnownColor.Red);
                    }

                }
                foreach (int i in i_fail)
                {
                    if (steppers[i].isAttached())
                    {
                        lengths[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                        Mij[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                        paths[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                        i_fail.Remove(i);
                    }
                }

                if (i_fail.Count <= 0)
                {
                    stepper_handler = new Stepper_Handler(steppers, this);
                    stepper_handler.run();
                }
                else { System.Threading.Thread.Sleep(1000); }
            }
            else
            {

                dummy_steppers = new List<MyDummyStepper>();
                for (int i = 0; checkBoxes[i].Checked; i++)
                {
                    dummy_steppers.Add(new MyDummyStepper((int)lengths[i].Value, paths[i].Text));
                }
                dummyStepper_Handler = new DummyStepper_Handler(dummy_steppers, this);
                dummyStepper_Handler.run();
            }
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
            {
                lengths[i].Value = (decimal)values[i];
                lengths[i].Update();
            }
        }

        public void set_progress(int state)
        {
            progressBar1.Value = state;
        }

        public void set_nstep(int N)
        {
            progressBar1.Maximum = N;
        }

        private void Browse_root_Click(object sender, EventArgs e)
        {
            int size = -1;
            FolderBrowserDialog fDialog = new FolderBrowserDialog();
            DialogResult result = fDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                root_path_wid.Text = fDialog.SelectedPath;
                rootPath = fDialog.SelectedPath;
            }
        }

        private void numericUpDown18_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown16_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M6y_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M5z_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M5y_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M5x_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M4z_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M4y_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M4x_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M3z_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M3y_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M3x_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M2z_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M2y_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M2x_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M1z_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M1y_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M1x_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M2z_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M6y_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void m6x_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M5z_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M5y_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M5x_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M4z_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M4y_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M4x_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M3z_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M3y_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M3x_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M6z_ValueChanged(object sender, EventArgs e)
        {

        }

        private void M2y_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M2x_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M1z_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M1y_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void M1x_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> lines = new List<String>();
            for (int i = 0; i < Mij.Count; i += 3)
                lines.Add(Mij[i].Value + "," + Mij[i + 1].Value + "," + Mij[i + 2].Value);
            System.IO.File.WriteAllLines(rootPath + @"\input.txt", lines);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void P0z_ValueChanged(object sender, EventArgs e)
        {

        }

        private void P0x_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Dummy_check_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
