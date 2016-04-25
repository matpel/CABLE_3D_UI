using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Move_cable;

namespace WindowsFormsApplication1
{

    class Stepper_Handler
    {
        private List<MyStepper> steppers;
        public String ErrorMessage = "";
        public Form1 context;

        public Stepper_Handler(List<MyStepper> steppers,Form1 context)
        {
            this.steppers = steppers;
            this.context = context;
        }

        public Boolean run()
        {
            foreach (MyStepper stepper in steppers)
                stepper.load();
            context.set_nstep(MyStepper.N_step);


            for (int i = 0; i < MyStepper.N_step - 1; i++)
            {
                List<double> lengths = new List<double>();
                foreach (MyStepper stepper in steppers)
                {
                    stepper.run_step(i);
                    lengths.Add(stepper.Length);
                }

                context.lengths_Change(lengths);
                context.set_progress(i);
                Boolean running;
                do
                {
                    running = false;
                    foreach (MyStepper stepper in steppers)
                    {
                        running = running || !stepper.isStopped();
                    }

                }while(running);
            }
            foreach(MyStepper stepper in steppers)
                stepper.close();
            return true;
        }
    }
}
