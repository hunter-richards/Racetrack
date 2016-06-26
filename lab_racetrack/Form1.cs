using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_racetrack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Guy CurrentBettor;
            //SetBettor(0);

            Random MyRandomizer = new Random();

            GreyhoundArray[0] = new Greyhound()
            {
                MyPictureBox = pictureBox1,
                StartingPosition = pictureBox1.Left,
                RaceTrackLength = racetrackPictureBox.Width - pictureBox1.Width,
                Randomizer = MyRandomizer
            };

            GreyhoundArray[1] = new Greyhound()
            {
                MyPictureBox = pictureBox2,
                StartingPosition = pictureBox2.Left,
                RaceTrackLength = racetrackPictureBox.Width - pictureBox2.Width,
                Randomizer = MyRandomizer
            };

            GreyhoundArray[2] = new Greyhound()
            {
                MyPictureBox = pictureBox3,
                StartingPosition = pictureBox3.Left,
                RaceTrackLength = racetrackPictureBox.Width - pictureBox3.Width,
                Randomizer = MyRandomizer
            };

            GreyhoundArray[3] = new Greyhound()
            {
                MyPictureBox = pictureBox4,
                StartingPosition = pictureBox4.Left,
                RaceTrackLength = racetrackPictureBox.Width - pictureBox4.Width,
                Randomizer = MyRandomizer
            };

            GuyArray[0] = new Guy()
            {
                Name = "Joe",
                Cash = 50,
                MyRadioButton = radioButton1,
                MyLabel = label4,
                MyBet = null
            };

            GuyArray[0].UpdateLabels();

            GuyArray[1] = new Guy()
            {
                Name = "Bob",
                Cash = 75,
                MyRadioButton = radioButton2,
                MyLabel = label5,
                MyBet = null
            };

            GuyArray[1].UpdateLabels();

            GuyArray[2] = new Guy()
            {
                Name = "Al",
                Cash = 45,
                MyRadioButton = radioButton3,
                MyLabel = label6,
                MyBet = null
            };

            GuyArray[2].UpdateLabels();

            for (int p = 0; p < GuyArray.Length; p++)
            {
                GuyArray[p].ClearBet();
                GuyArray[p].UpdateLabels();
            }

            CurrentBettor = GuyArray[0];



        }

        Greyhound[] GreyhoundArray = new Greyhound[4];

        Guy[] GuyArray = new Guy[3];

        Guy CurrentBettor;

        private void SetBettor(int index)
        {
            CurrentBettor = GuyArray[index];
            bettorLabel.Text = CurrentBettor.Name;

            if (CurrentBettor.MyBet != null)//take the bet for the current bettor
            {
                numericUpDown1.Value = CurrentBettor.MyBet.Amount;
                numericUpDown2.Value = CurrentBettor.MyBet.Dog;
            }
            else// reset the updown controls
            {
                numericUpDown1.Value = numericUpDown1.Minimum;
                numericUpDown2.Value = 1;
            }
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            for (int i = 0; i < GreyhoundArray.Length; i++)
            {
                if (GreyhoundArray[i].Run()){
                    int Victor = i + 1; // this is the number of the winning dog
                    timer1.Stop();
                    MessageBox.Show("Dog #" + Victor + " is the winner!");
                    for (int j = 0; j < GuyArray.Length; j++)
                    {
                        if (GuyArray[j].MyBet != null)
                        {
                            GuyArray[j].Collect(Victor);
                        }
                        
                    }
                    for (int p = 0; p < GreyhoundArray.Length; p++)
                    {
                        GreyhoundArray[p].TakeStartingPosition();
                    }
                    groupBox1.Enabled = true;
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = true;
                    radioButton3.Enabled = true;
                    numericUpDown1.Enabled = true;
                    numericUpDown2.Enabled = true;
                    button1.Enabled = true;
                    button2.Enabled = true;
                }


            }

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CurrentBettor.PlaceBet((int)numericUpDown1.Value, (int)numericUpDown2.Value))
            {
                CurrentBettor.PlaceBet((int)numericUpDown1.Value, (int)numericUpDown2.Value);
                CurrentBettor.UpdateLabels();
            }
            else
            {
                MessageBox.Show(CurrentBettor.Name + " doesn't have enough cash!");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetBettor(0);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SetBettor(1);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SetBettor(2);
        }

    
  
    }
}
