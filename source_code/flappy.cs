using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy
{
    public partial class flappy : Form
    {
        int pipeSpeed = 4;
        int gravity = 5;
        int star1Up = 3;
        int score = 0;           
        Random randy = new Random();

        public flappy()
        {          
            InitializeComponent();            
        }

        // Stars appear every other point the player scores
        // Stars will have a random y-coordinate position when appearing
        private void Star()
        {
            star1.Top += star1Up;
            if (star1.Top < 100 || star1.Top > 300)
            {
                star1Up = -star1Up;
            }

            if (star1.Left < -60)
            {
                star1.Left = 700;
                star1.Visible = true;
            }
               
            // If unicorn touches a star, they player earns a point
            if (bird.Bounds.IntersectsWith(star1.Bounds) && star1.Visible == true)
            {
                star1.Visible = false;
                score++;
            }       
        }

        private bool EndCondition()
        {
            // Conditions that would end the game
            if (pipeBottom2.Visible == true && pipeTop2.Visible == true)
            {
                if (bird.Bounds.IntersectsWith(pipeBottom2.Bounds) ||
                bird.Bounds.IntersectsWith(pipeTop2.Bounds))
                {
                    return true;
                }
            }
            if (bird.Bounds.IntersectsWith(ground.Bounds) || bird.Bounds.IntersectsWith(pipeBottom.Bounds) || 
                bird.Bounds.IntersectsWith(pipeTop.Bounds) || bird.Top <= 0)
            {
                return true;
            }
            return false;
        }    

        private void gameTimerEvent(object sender, EventArgs e)
        {           
            labelScore.Text = "Score: " + score.ToString();
            bird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            pipeBottom2.Left -= pipeSpeed;
            pipeTop2.Left -= pipeSpeed;
            star1.Left -= pipeSpeed;

            // Timer checks whether or not stars should appear
            Star();         

            // Increases difficulty every 3 points by increasing pipespeed by 1 unit
            if (score != 0 && score % 3 == 0)
            {
                pipeBottom2.Visible = true;
                pipeTop2.Visible = true;
                pipeSpeed = NextLevel();
            // After the player scores 3 points, the second set of pipes
            } 
            else if (score >= 3)
            {
                pipeBottom2.Visible = true;
                pipeTop2.Visible = true;
            }
            else
            {
                pipeBottom2.Visible = false;
                pipeTop2.Visible = false;
            }

            if (pipeBottom.Left < -60 && pipeTop.Left < -60)
            {
                score ++;
                pipeBottom.Left = randy.Next(725, 750);
                pipeTop.Left = randy.Next(725, 750);
                pipeTop.Height = randy.Next(150,200);
                pipeBottom.Height = randy.Next(210, 350);
            }

            if (pipeBottom2.Left < -60 && pipeTop2.Left < -60)
            {
                if (pipeBottom2.Visible == true && pipeTop2.Visible == true)
                {
                    score ++;
                }
                pipeBottom2.Left = randy.Next(700, 732);
                pipeTop2.Left = randy.Next(700, 732);
                pipeTop2.Height = randy.Next(60, 80);
                pipeBottom2.Height = randy.Next(250, 320);
            }

            if (EndCondition()) 
            {
                EndGame();                
            }          
        }

        private int NextLevel()
        {
            // For every multiple of 3, the function returns an integer that is 1 unit greater than the last
            // The returned integer will be used to define new pipespeed as game progresses            
            if (score != 0 && score % 3 == 0)
            {
                if (pipeSpeed < 10)
                {
                    return (score / 3) + 4;
                }
            }
            return pipeSpeed;
        }

        private void gameKeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameTimer.Enabled == false && labelEnd.Visible == false)
            {
                if (e.KeyCode == Keys.Space)
                {
                    labelStart.Visible = false;
                    gameTimer.Enabled = true;
                }
            }

            if (e.KeyCode == Keys.Space)
            {                                   
                gravity = -10;                                             
            }
        }

        private void gamKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 5;
            }
        }

        private void EndGame()
        {
            gameTimer.Stop();
            labelScore.Text = "Score: " + score;
            labelEnd.Visible = true;
        }

        private void flappy_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (EndCondition())
                {
                    // We must reset all components to original positions
                    labelEnd.Visible = false;
                    pipeSpeed = 4;

                    star1.Left = 200;
                    star1.Visible = false;

                    pipeBottom.Left = 650;
                    pipeBottom.Height = 190;
                    pipeTop.Left = 650;
                    pipeTop.Height = 162;

                    pipeBottom2.Visible = false;
                    pipeTop2.Visible = false;
                    pipeBottom2.Left = 280;
                    pipeBottom2.Height = 330;
                    pipeTop2.Left = 280;
                    pipeTop2.Height = 90;

                    bird.SetBounds(43, 155, 47, 43);
                    score = 0;
                    labelScore.Text = "Score: 0";
                    gameTimer.Start();
                }
            }
        }

        private void labelEnd_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (EndCondition())
                {
                    // We must reset all components to original positions
                    labelEnd.Visible = false;
                    pipeSpeed = 4;

                    star1.Left = 200;
                    star1.Visible = false;

                    pipeBottom.Left = 650;
                    pipeBottom.Height = 190;
                    pipeTop.Left = 650;
                    pipeTop.Height = 162;

                    pipeBottom2.Visible = false;
                    pipeTop2.Visible = false;
                    pipeBottom2.Left = 280;
                    pipeBottom2.Height = 330;
                    pipeTop2.Left = 280;
                    pipeTop2.Height = 90;

                    bird.SetBounds(43, 155, 47, 43);
                    score = 0;
                    labelScore.Text = "Score: 0";
                    gameTimer.Start();
                }
            }
        }
    }
}