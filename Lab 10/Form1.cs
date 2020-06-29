using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double current_time;
        int current_state;
        List<double> times = new List<double>();
        List<int> states = new List<int>();
        double[,] Q = new double[3, 3] { { -0.4, 0.3, 0.1 }, { 0.4, -0.8, 0.4 }, { 0.1, 0.4, -0.5 } };
        Bitmap[] weatherStates;
        PictureBox[] boxes;

        private void simulateBtn_Click(object sender, EventArgs e)
        {
            boxes = new PictureBox[7] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7 };
            Random rnd = new Random();
            weatherStates = new Bitmap[3] { Properties.Resources.Clear, Properties.Resources.Cloudy, Properties.Resources.Overcast };

            // generation
            for (int i = 0; i < 7; i++)
            {
                current_time = 0.0;
                current_state = rnd.Next(0, 3);
                times.Add(current_time);
                states.Add(current_state);
                double decay_rate = -Q[current_state, current_state];
                double life_time = 1;
                times.Add(times.Last() + life_time);
                //The probability to decay to the state new_state is proportional to transition rate Q[current_state][new_state]
                //generate an uniformly distributed random variable [0, decay_rate] (decay_rate - sum of transition rates of all possible new states) and map it on the states:
                double target_value = rnd.NextDouble() * decay_rate;
                double sum = 0.0;
                for (int new_state = 0; new_state < 3; new_state++)
                {
                    if (new_state == current_state)
                        continue;
                    sum += Q[current_state, new_state];
                    if (sum > target_value)
                    { //found next state
                        current_state = new_state;
                        boxes[i].BackgroundImage = weatherStates[current_state];
                        states.Add(current_state);
                        break;
                    }
                }
            }
        }
    }
}
