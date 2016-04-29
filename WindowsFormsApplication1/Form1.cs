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
        private List<CheckBox> listEngaged;
        private int step;

        private double dt = 0.07;
        private List<MyStepper> steppers;
        private List<MyDummyStepper> dummy_steppers;
        private Stepper_Handler stepper_handler;
        private DummyStepper_Handler dummyStepper_Handler;
        private List<MyStepper> s_calibrated;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            s_calibrated = new List<MyStepper>();
            for(int i = 0; i<6; i++)
            {
                s_calibrated.Add(new MyStepper(100));
            }
            paths = new List<TextBox> { path1_wid, path2_wid, path3_wid, path4_wid, path5_wid, path6_wid };
            checkBoxes = new List<CheckBox> { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6 };
            lengths = new List<NumericUpDown> { length1_wid, length2_wid, length3_wid, length4_wid, length5_wid, length6_wid };
            Mij = new List<NumericUpDown> { M1x, M1y, M1z, M2x, M2y, M2z, M3x, M3y, M3z, M4x, M4y, M4z, M5x, M5y, M5z, M6x, M6y, M6z, P0x, P0y, P0z };
            listEngaged = new List<CheckBox> { engaged1, engaged2, engaged3, engaged4, engaged5, engaged6 };
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
        /*
        private void dt_wid_TextChanged(object sender, EventArgs e)
        {
            dt = Convert.ToDouble(dt_wid.Text);

        }
        */
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
            if(checkBox6.Checked)
            {
                engaged6.Enabled = true;
            }
            else {
                engaged6.Enabled = false;
            }

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox6.Enabled = true;
                path6_wid.Enabled = true;
                length6_wid.Enabled = true;
                engaged5.Enabled = true;
            }
            else
            {
                engaged5.Enabled = false;
                for (int i = 5; i < 6; i++)
                {
                    checkBoxes[i].Enabled = false;
                    checkBoxes[i].Checked = false;
                    paths[i].Enabled = false;
                    lengths[i].Enabled = false;
                    listEngaged[i].Enabled = false;
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
                engaged4.Enabled = true;
            }
            else
            {
                engaged4.Enabled = false;
                for (int i = 4; i < 6; i++)
                {
                    checkBoxes[i].Enabled = false;
                    checkBoxes[i].Checked = false;
                    paths[i].Enabled = false;
                    lengths[i].Enabled = false;
                    listEngaged[i].Enabled = false;
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
                engaged3.Enabled = true;
            }
            else
            {
                engaged3.Enabled = false;
                for (int i = 3; i < 6; i++)
                {
                    checkBoxes[i].Enabled = false;
                    checkBoxes[i].Checked = false;
                    paths[i].Enabled = false;
                    lengths[i].Enabled = false;
                    listEngaged[i].Enabled = false;
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
                engaged2.Enabled = true;
            }
            else
            {
                engaged2.Enabled = false;
                for (int i = 2; i < 6; i++)
                {
                    checkBoxes[i].Enabled = false;
                    checkBoxes[i].Checked = false;
                    paths[i].Enabled = false;
                    lengths[i].Enabled = false;
                    listEngaged[i].Enabled = false;
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
                engaged1.Enabled = true;
            }
            else
            {
                engaged1.Enabled = false;
                for (int i = 1; i < 6; i++)
                {
                    checkBoxes[i].Enabled = false;
                    checkBoxes[i].Checked = false;
                    paths[i].Enabled = false;
                    lengths[i].Enabled = false;
                    listEngaged[i].Enabled = false;
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
            {//si on utilise des moteurs
                steppers = new List<MyStepper>();
                //List<int> i_fail = new List<int>();
                for (int i = 0; checkBoxes[i].Checked; i++)
                {
                    steppers.Add(new MyStepper((int)lengths[i].Value, paths[i].Text, dt));
                    /*lengths[i].BackColor = Color.FromKnownColor(KnownColor.Yellow);
                    Mij[i].BackColor = Color.FromKnownColor(KnownColor.Yellow);
                    paths[i].BackColor = Color.FromKnownColor(KnownColor.Yellow);
                    int timer = 0;
                    while (!steppers[i].isAttached() && timer <= 1000)
                    {//on attend que le moteur i s'attache
                        System.Threading.Thread.Sleep(2);
                        timer++;
                    }
                    if (timer <= 1000)
                    {//si ça a marché on met les cases correspondantes en vert
                        lengths[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                        Mij[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                        paths[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                    }
                    else
                    {//sinon on mémorise l'index du moteurdans i_fail et on affiche en rouge
                        i_fail.Add(i);
                        lengths[i].BackColor = Color.FromKnownColor(KnownColor.Red);
                        Mij[i].BackColor = Color.FromKnownColor(KnownColor.Red);
                        paths[i].BackColor = Color.FromKnownColor(KnownColor.Red);
                    }*/

                }
                /*foreach (int i in i_fail)
                {//on viens checker si les moteurs en échec on finalement réussi à s'attacher
                    if (steppers[i].isAttached())
                    {//si ça a finalement marché, on affiche en vert et on enlève l'index du moteur de i_fail
                        lengths[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                        Mij[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                        paths[i].BackColor = Color.FromKnownColor(KnownColor.Green);
                        i_fail.Remove(i);
                    }
                }

                //si i_fail est vide on est bon, on peut lancer les moteurs
                
            else
            {//sinon pour l'instant on attend (TODO gerer l'erreur)
                System.Threading.Thread.Sleep(1000);
            }*/
            stepper_handler = new Stepper_Handler(steppers, this);
            stepper_handler.run();
            }
            else
            {//si on teste juste le programme sans moteurs (mode dummy)
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
        private void dt_num_ValueChanged(object sender, EventArgs e)
        {
            dt = Convert.ToDouble(dt_num.Value);
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

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged_1(object sender, EventArgs e)
        {
            if(engaged1.Checked)
            {
                s_calibrated[0].stepper.steppers[0].VelocityLimit = (int)velocity_num1.Value;
            }
            else { velocity_num1.Value = 100; }
        }

        private void step_num1_ValueChanged(object sender, EventArgs e)
        {
            if(engaged1.Checked)
            {
                s_calibrated[0].stepper.steppers[0].TargetPosition = (int)step_num1.Value;
            }
        }

        private void velocity_num2_ValueChanged(object sender, EventArgs e)
        {
            if (engaged2.Checked)
            {
                s_calibrated[1].stepper.steppers[0].VelocityLimit = (int)velocity_num2.Value;
            }
            else { velocity_num2.Value = 100; }
        }

        private void velocity_num3_ValueChanged(object sender, EventArgs e)
        {
            if (engaged3.Checked)
            {
                s_calibrated[2].stepper.steppers[0].VelocityLimit = (int)velocity_num3.Value;
            }
            else { velocity_num3.Value = 100; }
        }

        private void velocity_num4_ValueChanged(object sender, EventArgs e)
        {
            if (engaged4.Checked)
            {
                s_calibrated[3].stepper.steppers[0].VelocityLimit = (int)velocity_num4.Value;
            }
            else { velocity_num4.Value = 100; }
        }

        private void velocity_num5_ValueChanged(object sender, EventArgs e)
        {
            if (engaged5.Checked)
            {
                s_calibrated[4].stepper.steppers[0].VelocityLimit = (int)velocity_num5.Value;
            }
            else { velocity_num5.Value = 100; }
        }

        private void velocity_num6_ValueChanged(object sender, EventArgs e)
        {
            if (engaged6.Checked)
            {
                s_calibrated[5].stepper.steppers[0].VelocityLimit = (int)velocity_num6.Value;
            }
            else { velocity_num6.Value = 100; }
        }

        private void step_num2_ValueChanged(object sender, EventArgs e)
        {
            if (engaged2.Checked)
            {
                s_calibrated[1].stepper.steppers[0].TargetPosition = (int)step_num2.Value;
            }
        }

        private void step_num3_ValueChanged(object sender, EventArgs e)
        {
            if (engaged3.Checked)
            {
                s_calibrated[2].stepper.steppers[0].TargetPosition = (int)step_num3.Value;
            }
        }

        private void step_num4_ValueChanged(object sender, EventArgs e)
        {
            if (engaged4.Checked)
            {
                s_calibrated[3].stepper.steppers[0].TargetPosition = (int)step_num4.Value;
            }
        }

        private void step_num5_ValueChanged(object sender, EventArgs e)
        {
            if (engaged5.Checked)
            {
                s_calibrated[4].stepper.steppers[0].TargetPosition = (int)step_num5.Value;
            }
        }

        private void step_num6_ValueChanged(object sender, EventArgs e)
        {
            if (engaged6.Checked)
            {
                s_calibrated[5].stepper.steppers[0].TargetPosition = (int)step_num6.Value;
            }
        }

        private void engaged1_CheckedChanged_1(object sender, EventArgs e)
        {
            if(engaged1.Checked && !(s_calibrated[0].isAttached()))
            {
                unchecked1.Visible = false;
                
                if(s_calibrated[0].load())
                {
                    checked1.Visible = true;
                    step_num1.Value = s_calibrated[0].stepper.steppers[0].CurrentPosition;
                }
                else
                {
                    unchecked1.Visible = true;
                    engaged1.Checked = false;
                }
                engaged1.Checked = false;

            }
            else if (s_calibrated[0].isAttached())
            {
                s_calibrated[0].close();
                unchecked1.Visible = true;
                checked1.Visible = false;
            }
            else
            {
                unchecked1.Visible = true;
                checked1.Visible = false;
            }
        }

        private void engaged2_CheckedChanged(object sender, EventArgs e)
        {
            if (engaged2.Checked && !(s_calibrated[1].isAttached()))
            {
                unchecked2.Visible = false;

                if (s_calibrated[1].load())
                {
                    checked2.Visible = true;
                    step_num2.Value = s_calibrated[1].stepper.steppers[0].CurrentPosition;
                }
                else
                {
                    unchecked2.Visible = true;
                    engaged2.Checked = false;
                }
                engaged2.Checked = false;

            }
            else if (s_calibrated[1].isAttached())
            {
                s_calibrated[1].close();
                unchecked2.Visible = true;
                checked2.Visible = false;
            }
            else
            {
                unchecked2.Visible = true;
                checked2.Visible = false;
            }
        }

        private void engaged3_CheckedChanged_2(object sender, EventArgs e)
        {
            if (engaged3.Checked && !(s_calibrated[2].isAttached()))
            {
                unchecked3.Visible = false;

                if (s_calibrated[2].load())
                {
                    checked3.Visible = true;
                    step_num3.Value = s_calibrated[2].stepper.steppers[0].CurrentPosition;
                }
                else
                {
                    unchecked3.Visible = true;
                    engaged3.Checked = false;
                }
                engaged3.Checked = false;

            }
            else if (s_calibrated[2].isAttached())
            {
                s_calibrated[2].close();
                unchecked3.Visible = true;
                checked3.Visible = false;
            }
            else
            {
                unchecked3.Visible = true;
                checked3.Visible = false;
            }
        }

        private void engaged4_CheckedChanged(object sender, EventArgs e)
        {
            if (engaged4.Checked && !(s_calibrated[3].isAttached()))
            {
                unchecked4.Visible = false;

                if (s_calibrated[3].load())
                {
                    checked4.Visible = true;
                    step_num4.Value = s_calibrated[3].stepper.steppers[0].CurrentPosition;
                }
                else
                {
                    unchecked4.Visible = true;
                    engaged4.Checked = false;
                }
                engaged4.Checked = false;

            }
            else if (s_calibrated[3].isAttached())
            {
                s_calibrated[3].close();
                unchecked4.Visible = true;
                checked4.Visible = false;
            }
            else
            {
                unchecked4.Visible = true;
                checked4.Visible = false;
            }
        }

        private void engaged6_CheckedChanged(object sender, EventArgs e)
        {
            if (engaged6.Checked && !(s_calibrated[5].isAttached()))
            {
                unchecked6.Visible = false;

                if (s_calibrated[5].load())
                {
                    checked6.Visible = true;
                    step_num6.Value = s_calibrated[5].stepper.steppers[0].CurrentPosition;
                }
                else
                {
                    unchecked6.Visible = true;
                    engaged6.Checked = false;
                }
                engaged6.Checked = false;

            }
            else if (s_calibrated[5].isAttached())
            {
                s_calibrated[5].close();
                unchecked6.Visible = true;
                checked6.Visible = false;
            }
            else
            {
                unchecked6.Visible = true;
                checked6.Visible = false;
            }
        }

        private void engaged5_CheckedChanged(object sender, EventArgs e)
        {
            if (engaged5.Checked && !(s_calibrated[4].isAttached()))
            {
                unchecked5.Visible = false;

                if (s_calibrated[4].load())
                {
                    checked5.Visible = true;
                    step_num5.Value = s_calibrated[4].stepper.steppers[0].CurrentPosition;
                }
                else
                {
                    unchecked5.Visible = true;
                    engaged5.Checked = false;
                }
                engaged5.Checked = false;

            }
            else if (s_calibrated[4].isAttached())
            {
                s_calibrated[4].close();
                unchecked5.Visible = true;
                checked5.Visible = false;
            }
            else
            {
                unchecked5.Visible = true;
                checked5.Visible = false;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void unchecked6_Click(object sender, EventArgs e)
        {

        }
    }
}
