﻿using System;
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
         
            for (int i = 0; i < MyStepper.N_step; i++)
            {
                foreach (MyStepper stepper in steppers)
                {
                    stepper.run_step(i);
                }

                context.lengths_Change(new List<double> { steppers[0].Goal_Position, steppers[1].Goal_Position, steppers[2].Goal_Position, steppers[3].Goal_Position, steppers[4].Goal_Position, steppers[5].Goal_Position});
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