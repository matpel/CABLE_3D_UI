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

        public Stepper_Handler(List<MyStepper> steppers)
        {
            this.steppers = steppers;
        }

        public Boolean run()
        {
            foreach (MyStepper stepper in steppers)
                stepper.load();
         
            for (int i = 0; i < MyStepper.N_step; i++)
            {
                foreach (MyStepper stepper in steppers)
                {
                    stepper.run_step(i);
                }

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
