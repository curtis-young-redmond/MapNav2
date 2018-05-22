using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MapNav
{
    public partial class Form1 : Form
    {
        #region Local Variables
        enum direction
        {
            posy,
            negy,
            posx,
            negx
        }
        Coord mycoord;
        direction currentstate = direction.posy; //due north initial state
        #endregion
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mycoord = new Coord();
                char[] splitchar = { ',', ' ', ';', ':' };
                var mywalkinstruction = textBox1.Text.Split(splitchar, StringSplitOptions.RemoveEmptyEntries);
                var arrsize = mywalkinstruction.Count();
                System.Drawing.Point[] myptarry = new Point[arrsize + 1];
                myptarry[0] = new Point(0, 0);
                int i = 1;
                foreach (var step in mywalkinstruction)
                {
                    var vector = step[0];
                    var steps = step.Substring(1).ToString();
                    var numOfSteps = Convert.ToInt32(steps.ToString());
                    StateTransition(vector, numOfSteps);
                    myptarry[i++] = mycoord.mypoint;
                }
                textBox3.Text = mycoord.blocksFromDrop().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        void StateTransition(Char c, int numsteps)
        {
            switch (currentstate)
            {
                case direction.posy:
                    {
                        if (char.ToUpper(c) == 'R')
                        {
                            currentstate = direction.posx;
                            mycoord.updateXposition(numsteps);
                        }
                        if (char.ToUpper(c) == 'L')
                        {
                            currentstate = direction.negx;
                            mycoord.updateXposition(-numsteps);
                        }
                    }
                    break;
                case direction.posx:
                    {
                        if (char.ToUpper(c) == 'R')
                        {
                            currentstate = direction.negy;
                            mycoord.updateYposition(-numsteps);
                        }
                        if (char.ToUpper(c) == 'L')
                        {
                            currentstate = direction.posy;
                            mycoord.updateYposition(numsteps);
                        }
                    }
                    break;
                case direction.negx:
                    {
                        if (char.ToUpper(c) == 'R')
                        {
                            currentstate = direction.posy;
                            mycoord.updateYposition(numsteps);
                        }
                        if (char.ToUpper(c) == 'L')
                        {
                            currentstate = direction.negy;
                            mycoord.updateYposition(-numsteps);
                        }
                    }
                    break;
                case direction.negy:
                    {
                        if (char.ToUpper(c) == 'R')
                        {
                            currentstate = direction.negx;
                            mycoord.updateXposition(-numsteps);
                        }
                        if (char.ToUpper(c) == 'L')
                        {
                            currentstate = direction.posx;
                            mycoord.updateXposition(numsteps);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #region TEST DATA
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "L3, R2, L5, R1, L1, L2";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "L3, R2, L5, R1, L1, L2, L2, R1, R5, R1, L1, L2, R2, R4, L4, L3, L3, R5, L1, R3, L5, L2, R4, L5, R4, R2, L2, L1, R1, L3, L3, R2, R1, L4, L1, L1, R4, R5, R1, L2, L1, R188, R4, L3, R54, L4, R4, R74, R2, L4, R185, R1, R3, R5, L2, L3, R1, L1, L3, R3, R2, L3, L4, R1, L3, L5, L2, R2, L1, R2, R1, L4, R5, R4, L5, L5, L4, R5, R4, L5, L3, R4, R1, L5, L4, L3, R5, L5, L2, L4, R4, R4, R2, L1, L3, L2, R5, R4, L5, R1, R2, R5, L2, R4, R5, L2, L3, R3, L4, R3, L2, R1, R4, L5, R1, L5, L3, R4, L2, L2, L5, L5, R5, R2, L5, R1, L3, L2, L2, R3, L3, L4, R2, R3, L1, R2, L5, L3, R4, L4, R4, R3, L3, R1, L3, R5, L5, R1, R5, R3, L1";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
        #endregion

    }

    #region Supporting Class
    public class Coord
    {
        public Point mypoint;
        public int maxblocks;
        public Coord()
        {
            mypoint = new Point();
            maxblocks = 0;
        }
        public void updateXposition(int x)
        {
            mypoint.X += x;
        }
        public void updateYposition(int y)
        {
            mypoint.Y += y;
        }
        public int blocksFromDrop()
        {
            return Math.Abs(mypoint.X) + Math.Abs(mypoint.Y);
        }
    }
    #endregion
}
