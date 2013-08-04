using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TkLight2
{
    public partial class Form1 : Form
    {
        public Bitmap foreGroundPicture;
        public Color currentColor;
        public Brush currentBrush;
        public List<LightForm> lightFormList = new List<LightForm>();

        Random r = new Random();

        public bool initialized = false;

        public double fadePosition = 0;

        public Form1()
        {
            InitializeComponent();
        }


        public void AppendSelectedColor(object sender, MouseEventArgs e)
        {
            // TODO: Höhe und Breite abfangen
            if (e.Button == MouseButtons.Left/* && e.X >= pictureBox1.Location.X && e.Y >= pictureBox1.Location.Y*/)
            {
                AppendColor(e.X, e.Y);
            }
        }

        public void AppendColor(int x, int y)
        {
            currentColor = foreGroundPicture.GetPixel(x, y);
            currentBrush = new SolidBrush(currentColor);

            foreach (LightForm currentLightForm in lightFormList)
            {
                // TODO: Transparente Farbe abfangen
                try
                {
                    currentLightForm.BackColor = currentColor;
                }
                catch (Exception ex)
                {

                }
            }

            if (checkBox2.Checked)
            {

                StreamWriter sw = new StreamWriter(textBox1.Text, false);
                sw.Write("<html><head><script type=\"text/javascript\">function bla() { window.location.href = 'http://192.168.2.100/tklight/' + Math.floor(Math.random()*11020220) + '.php'; }</script></head><body style=\"height: 2000px; overflow: hidden; background: rgb(" + currentColor.R.ToString() + "," + currentColor.G.ToString() + "," + currentColor.B.ToString() + ");\"><script type=\"text/javascript\">window.setTimeout('bla();', 200);</script></body></html>");
                sw.Close();
                sw.Dispose();
            
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            foreGroundPicture = (Bitmap)pictureBox1.Image;
            Int16 screenCounter = 0;
            LightForm currentLightForm;
            foreach (Screen currentScreen in Screen.AllScreens)
            {
                ++screenCounter;
                checkedListBox1.Items.Add("Bildschirm " + screenCounter.ToString() + " (" + currentScreen.Bounds.Left + "px/" + currentScreen.Bounds.Top + "/px" + currentScreen.Bounds.X + "/px" + currentScreen.Bounds.Y + "px)", true);
                currentLightForm = new LightForm(currentScreen, this);
                currentLightForm.Show();
                lightFormList.Add(currentLightForm);
            
            }
            TopMost = true;
            BringToFront();

            initialized = true;
        }





        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (initialized)
            {
                if (e.CurrentValue == CheckState.Checked)
                {
                    lightFormList[e.Index].Hide();
                }
                else
                {
                    lightFormList[e.Index].Show();
                }
                BringToFront();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double r = 120;

            int addx = 145;
            int addy = 145;

            int x = Math.Abs(Convert.ToInt32(Math.Sin((double)fadePosition) * r) + addx);
            int y = Math.Abs(Convert.ToInt32(Math.Cos((double)fadePosition) * r) + addy);

            AppendColor(x,y);

            fadePosition += (double)trackBar2.Value / (double)100000;
            if (fadePosition > 360)
            {
                fadePosition = 0;
            }

            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
        }



    }
}
