using System;
using System.Collections.Generic;
using System.Text;
using Phidgets; //For the Stepper class and the exceptions class
using Phidgets.Events; //For the event handling classes
using System.Diagnostics;
namespace Move_cable
{

    public class MyDummyStepper
    {
        const double pas = 2.5; //Pas de la vis sans fin en mm
        const int R = 10; // Rayon de la vis sans fin en mm
        public static double dt = 0.07; // Pas de temps donne par GrassHoper en sec.
        public static int N_step;
        public Boolean stopped;
        public List<double> liste;
        public int Initial_position;
        public double Length;
        double a0 = 2 * Math.PI * Math.Sqrt(R * R + (pas / (2 * Math.PI)) * (pas / (2 * Math.PI)));
        public double derive;
        public double Goal_Position;
        public Stepper stepper;
        public double delta_L;
        public String Path;
        static bool tourne;
        public Boolean Error_timestamp = false;

        public MyDummyStepper(double Length, String Path = "", int Initial_position = 0)
        {
            this.Path = Path;
            this.Initial_position = Initial_position;
            this.Length = Length;
            stepper = new Stepper();
            liste = new List<double>();
            tourne = true;
            this.Length = Length;
        }
        public void load()
        {
            try
            {
                // Read File corresponding to Motor ID
                load_trajectory();

            }
            catch (PhidgetException ex)
            {
                Console.WriteLine(ex.Description);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void run_step(int i)
        {

            double l0 = liste[i];
            double l1 = liste[i + 1];
            double dx = 16 * (l0 - l1) * 200 / a0;
            double speed = Math.Abs(dx) / dt;

            derive += Goal_Position - (int)Goal_Position;
            if (derive >= 1)
            {
                dx += 1;
                derive = 1 - (int)derive;
            }
            else if (derive <= -1)
            {
                dx += -1;
                derive = (int)derive + 1;
            }
            Length = l1;
            i++;
            stopped = false;
            System.Threading.Thread.Sleep((int)(1000*dt));
            stopped = true;
        }
        public bool isStopped()
        {
            if (stopped)
                return true;
            else return false;
        }
        public void close()
        {
            //stepper.steppers[0].Engaged = false;
            //close the Stepper object
            stepper.close();
            //set the object to null to get it out of memory
            stepper = null;
        }
        public void load_trajectory()
        {
            string[] lines = System.IO.File.ReadAllLines(Path);
            foreach (string line in lines)
            {
                liste.Add(Convert.ToDouble(line.Replace(',', '.')));
            }
            N_step = liste.Count;

        }
        public void stepper_Attach(object sender, AttachEventArgs e)
        {
            Console.WriteLine("{0} {1} attached!", e.Device.Name,
                                        e.Device.SerialNumber.ToString());
        }

        //Detach event handler....Display the name and serial number of the detached 
        //Stepper device
        public void stepper_Detach(object sender, DetachEventArgs e)
        {
            Console.WriteLine("{0} {1} detached!", e.Device.Name,
                                        e.Device.SerialNumber.ToString());
        }

        //Error event handler....Display the description of the error
        public void stepper_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Description);
        }

        //Current Change event handler....Display the motor index and new value of 
        //the current
        public void stepper_CurrentChange(object sender, CurrentChangeEventArgs e)
        {
            Console.WriteLine("Stepper Motor {0} current changed! New Value: {1}",
                                        e.Index.ToString(), e.Current.ToString());
        }

        //Input Change event handler....Display the Input index and the new input value
        public void stepper_InputChange(object sender, InputChangeEventArgs e)
        {
            Console.WriteLine("Input {0} changed! New Value: {1}", e.Index.ToString(),
                                        e.Value.ToString());
        }

        //Position Change event handler. Display the motor index and the position value
        public void stepper_PositionChange(object sender,
                                                StepperPositionChangeEventArgs e)
        {
            Console.WriteLine("Stepper Motor {0} position: {1}", e.Index,
                                        e.Position.ToString());

        }

        //Velocity Change event handler. Display the motor index and the velocity value
        public void stepper_VelocityChange(object sender, VelocityChangeEventArgs e)
        {
            Console.WriteLine("Stepper Motor {0} velocity: {1}", e.Index,
                                        e.Velocity.ToString());
        }
        public bool isAttached()
        {
            return stepper.Attached;
        }

        public void correct(double L_ensg, int t)
        {
            if (t <= liste.Count)
                delta_L += liste[t] - L_ensg;
            else Error_timestamp = true;
        }
    }
}

