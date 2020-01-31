using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhackTheMoleForm
{
    public partial class Form1 : Form
    {
        private Rectangle Hammer;
        private Rectangle Platform;
        private Rectangle Mole1;
        private Rectangle Mole2;
        private int Score = 0;
        private int Ticks = 0;
        private int choice;

        public Form1()
        {
            InitializeComponent();
            Hammer = new Rectangle(156, 90, 10, 20);
            Platform = new Rectangle(125, 150, 75, 1);
            Mole1 = new Rectangle(125, 137, 7, 10);
            Mole2 = new Rectangle(193, 137, 7, 10);
            choice = Dice.DiceRoll();
            timer1.Start();
        }

        public static class Dice
        {
            public static int DiceRoll()
            {
                Random random = new Random();
                int roll = random.Next(1, 3);
                return roll;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Refresh();

            if (!e.Handled)
            {
                switch (e.KeyData)
                {
                    case Keys.Down:
                        Hammer.Offset(0, 5);
                        break;

                    case Keys.Up:
                        Hammer.Offset(0, -5);
                        break;

                    case Keys.Left:
                        Hammer.Offset(-5, 0);
                        break;

                    case Keys.Right:
                        Hammer.Offset(5, 0);
                        break;
                }
                Invalidate();

                if (Ticks == 5)
                {
                    if (Hammer.IntersectsWith(Mole1) || Hammer.IntersectsWith(Mole2))
                    {
                        Score = 1;
                    }
                }
            }
        }

        private void drawingArea_Click(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen red = new Pen(Color.Red);
            SolidBrush redf = new SolidBrush(Color.Red);

            Pen blue = new Pen(Color.Blue, 3);

            Pen yellow = new Pen(Color.Yellow);
            SolidBrush yellowf = new SolidBrush(Color.Yellow);

            Graphics g = drawingArea.CreateGraphics();
            g.DrawRectangle(red, Hammer);
            g.FillRectangle(redf, Hammer);
            g.DrawRectangle(blue, Platform);
            if (Ticks == 5 && !Hammer.IntersectsWith(Mole1) && !Hammer.IntersectsWith(Mole2))
            {
                if (choice == 1)
                {
                    g.DrawRectangle(yellow, Mole1);
                    g.FillRectangle(yellowf, Mole1);
                }

                if (choice == 2)
                {
                    g.DrawRectangle(yellow, Mole2);
                    g.FillRectangle(yellowf, Mole2);
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Ticks++;
            Text = Ticks.ToString();

            Pen yellow = new Pen(Color.Yellow);
            SolidBrush yellowf = new SolidBrush(Color.Yellow);
            Graphics g = drawingArea.CreateGraphics();

            if (Ticks == 5 && !Hammer.IntersectsWith(Mole1) && !Hammer.IntersectsWith(Mole2))
            {
                if ( choice == 1)
                {
                    g.DrawRectangle(yellow, Mole1);
                    g.FillRectangle(yellowf, Mole1);
                }

                if (choice == 2)
                {
                    g.DrawRectangle(yellow, Mole2);
                    g.FillRectangle(yellowf, Mole2);
                }
                
            }

            if (Ticks == 10)
            {
                Text = "Game Over -- Score : " + Score.ToString();
                timer1.Stop();
            }
        }
    }
}
