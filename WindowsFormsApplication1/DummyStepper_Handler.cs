using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Move_cable;

namespace WindowsFormsApplication1
{

    class DummyStepper_Handler
    {
        private List<MyDummyStepper> steppers;
        public String ErrorMessage = "";
        public Form1 context;

        public DummyStepper_Handler(List<MyDummyStepper> steppers, Form1 context)
        {
            this.steppers = steppers;
            this.context = context;
        }

        public Boolean run()
        {
            foreach (MyDummyStepper stepper in steppers)
                stepper.load();
            context.set_nstep(MyDummyStepper.N_step);


            for (int i = 0; i < MyDummyStepper.N_step - 1; i++)
            {
                List<double> lengths = new List<double>();
                foreach (MyDummyStepper stepper in steppers)
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
                    foreach (MyDummyStepper stepper in steppers)
                    {
                        running = running || !stepper.isStopped();
                    }

                } while (running);
            }
            foreach (MyDummyStepper stepper in steppers)
                stepper.close();
            return true;
        }
    }
}
