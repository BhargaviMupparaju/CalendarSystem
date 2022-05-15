using System;
using System.Collections;
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
    public partial class Form2 : Form
    {
        DateTime startEventTime;
        DateTime endEventTime;
        String eventDescription;
        String eventTitle;
        int eventID;

        public Form2(DateTime startTime, DateTime endTime, String eventDescript, String eventName, int eventNum)
        {
            InitializeComponent();
            startEventTime = startTime;
            endEventTime = endTime;
            eventDescription = eventDescript;
            eventTitle = eventName;
            eventID = eventNum;
        }
       

        internal void LabelContents()
        {
            
            dateTimePicker1.Value = startEventTime;
            dateTimePicker2.Value = endEventTime;
            richTextBox1.Text = eventDescription;
            textBox2.Text = eventTitle;

        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //label3.Text = richTextBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //label8.Text = textBox2.Text;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
          // label2.Text = dateTimePicker1.Text;

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
           // label9.Text = dateTimePicker2.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            startEventTime = dateTimePicker1.Value;
            endEventTime = dateTimePicker2.Value;
            eventDescription = richTextBox1.Text;
            eventTitle = textBox2.Text;
            Event edittedEvent = new Event();
            edittedEvent.editEvent(eventTitle, startEventTime, endEventTime, eventDescription, eventID);
            this.Close();
           
            
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }
    }
}
