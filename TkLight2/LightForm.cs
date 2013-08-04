using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TkLight2
{
    public partial class LightForm : Form
    {
        public Screen targetScreen;
        public Form1 mainForm;

        public LightForm(Screen targetScreen, Form1 mainForm)
        {
            InitializeComponent();
            this.targetScreen = targetScreen;
            this.mainForm = mainForm;
        }

        private void LightForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(targetScreen.Bounds.Left, targetScreen.Bounds.Top);
            Size = new Size(targetScreen.Bounds.Width, targetScreen.Bounds.Height);
            TopMost = true;
            ShowInTaskbar = false;
        }

        private void LightForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (mainForm.WindowState != FormWindowState.Normal)
            {
                mainForm.WindowState = FormWindowState.Normal;
            }
            mainForm.BringToFront();

        }

        
    }
}
