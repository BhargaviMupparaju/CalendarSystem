using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalendarSystem
{
    public partial class Form3 : Form
    {

        String startEventTime;
        String endEventTime;
        String eventDescription;
        String eventTitle;
      
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(String eventName, String startDateAndTime, String endDateAndTime, String eventdescription)
        {
            
            InitializeComponent();
            eventTitle = eventName;
            startEventTime = startDateAndTime;
            endEventTime = endDateAndTime;
            eventDescription = eventdescription;
          
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
        }


        internal void LabelContents()
        {

        
            label3.Text = startEventTime;
            label7.Text = endEventTime;
            label9.Text = eventDescription;
            label2.Text = eventTitle;
           
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

    }
}
