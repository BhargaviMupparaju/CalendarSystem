using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Globalization;

namespace CalendarSystem
{
    public partial class Form1 : Form
    {
        
        Event selectedEvent;
        int flag = 0;
        int employeeid;
        ArrayList eventList = new ArrayList();
        string role;
        int addeventcode = -1;
        private DateTime prevStartDate;
        private DateTime prevEndDate;
        private bool busyStartDate = false;
        private bool busyEndDate = false;
        Button managerButton;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
           employeeid = int.Parse(textBox1.Text);
            String pass = maskedTextBox1.Text;
           
            if (textBox1.Text == "" || maskedTextBox1.Text == "")
            {
                MessageBox.Show("Please provide UserName and PIN");
                return;
            }
            
            try
            {

                Employee user = new Employee();
                bool result = user.login(employeeid, pass);
                Console.WriteLine("result: " + result);
                role = user.getRole();

                if (result)
                {
                    Employee thisEmployee = new Employee(employeeid, pass);
                    eventList = Event.retrieveEvents(employeeid);
                    if (role == "manager")
                    {
                        RowStyle temp = tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowCount - 1];
                        //increase panel rows count by one
                        tableLayoutPanel1.RowCount++;
                        //add a new RowStyle as a copy of the previous one
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                        managerButton = new Button();
                        managerButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
                        managerButton.Font = new Font("Microsoft Sans Serif", 36, FontStyle.Bold);
                        managerButton.ForeColor = Color.Firebrick;
                        managerButton.TextAlign = ContentAlignment.MiddleCenter;
                        managerButton.BackColor = Color.PaleTurquoise;
                        managerButton.Text = "Schedule Meeting";
                        managerButton.Name = "managerButton";
                        managerButton.Click += new EventHandler(this.managerButton_Click);
                        tableLayoutPanel1.Controls.Add(managerButton);
                    }
                    panel1.Visible = false;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    panel4.Visible = false;
                    panel6.Visible = false;
                    panel8.Visible = false;
                    panel9.Visible = false;
                    tableLayoutPanel1.Visible = true;
                }
                else
                {

                    MessageBox.Show("Invalid username and password");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" " + ex.Message);
                MessageBox.Show("Invalid format of username and password");
            }
            
        }

        private void managerButton_Click(object sender, EventArgs e)
        {
          
            tableLayoutPanel1.Visible = false;
            panel1.Visible = false;
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            panel6.Visible = false;
            panel8.Visible = false;
            panel9.Visible = false;      

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = false;
            panel1.Visible = true;
            textBox1.Clear();
            maskedTextBox1.Clear();
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Event tempEvent;
            Console.WriteLine("number of Events: " + eventList.Count);
            for (int i = 0; i < eventList.Count; i++)
            {
                tempEvent = (Event)eventList[i];
                listBox1.Items.Add("                            " + tempEvent.getEventTitle());

            }
            tableLayoutPanel1.Visible = false;
            panel4.Visible = true;
            groupBox4.Text = "You are about to edit an event";
            label3.Text = "Events you can edit are ";
            flag = 1;
            
        }

        






        private void button4_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();
            Event tempEvent;
            Console.WriteLine("number of Events: " + eventList.Count);
            for (int i = 0; i < eventList.Count; i++)
            {
                tempEvent = (Event)eventList[i];
                listBox1.Items.Add("                            " + tempEvent.getEventTitle());

            }
            
            tableLayoutPanel1.Visible = false;
            panel4.Visible = true;
            groupBox4.Text = "You are about to delete an event";
            label3.Text = "Events you can delete are ";
            flag = 2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Event tempEvent;
            Console.WriteLine("number of Events: " + eventList.Count);
            for (int i = 0; i < eventList.Count; i++)
            {
                tempEvent = (Event)eventList[i];
                listBox1.Items.Add("                            " + tempEvent.getEventTitle());

            }
            panel4.Visible = true;
            tableLayoutPanel1.Visible = false;
            groupBox4.Text = "You are about to view an event";
            label3.Text = "List of events in your calendar ";
            flag = 3;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MonthlyCalendarForm monthlyCalendar = new MonthlyCalendarForm();
            monthlyCalendar.setEventList(eventList);
            monthlyCalendar.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;

            if (DateTime.Now.Minute > 0 && DateTime.Now.Minute < 30)
            {
                int diff = 30 - dateTimePicker3.Value.Minute;
                dateTimePicker3.Value = dateTimePicker3.Value.AddMinutes(diff);
                dateTimePicker4.Value = dateTimePicker4.Value.AddMinutes(diff);
            }
            else if (DateTime.Now.Minute > 30 && DateTime.Now.Minute < 60)
            {
               
                int diff = 60 - dateTimePicker3.Value.Minute;

                dateTimePicker3.Value = dateTimePicker3.Value.AddMinutes(diff);
                dateTimePicker4.Value = dateTimePicker4.Value.AddMinutes(diff);

            }

            prevStartDate = dateTimePicker3.Value;
            prevEndDate = dateTimePicker4.Value;
            dateTimePicker3.ValueChanged += new EventHandler(DateTimePicker3_ValueChanged);
            dateTimePicker4.ValueChanged += new EventHandler(DateTimePicker4_ValueChanged);
            richTextBox3.Clear();
            panel8.Visible = true;
            tableLayoutPanel1.Visible = false;
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            panel6.Visible = false;

            panel2.Visible = false;
            panel9.Visible = false;
        }
        private void DateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            if (!busyEndDate)
            {
                busyEndDate = true;


                DateTime dt = dateTimePicker4.Value;

                if ((dt.Minute * 60) % 3600 != 0)
                {
                    if (prevEndDate > dt)
                    {
                        dateTimePicker4.Value = prevEndDate.AddMinutes(-30);
                    }
                    else dateTimePicker4.Value = prevEndDate.AddMinutes(30);
                }
                busyEndDate = false;
                prevEndDate = dateTimePicker4.Value;

            }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BringToFront();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            eventList = Event.retrieveEvents(employeeid);
            tableLayoutPanel1.Visible = true;
            panel3.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DateTime dateOfMeeting = dateTimePicker1.Value;
            int duration = int.Parse(textBox4.Text);
            string[] attendees = richTextBox2.Text.Split(',');
            Event eventObject = new Event();
            IEnumerable< DateTime> possibleTimeSlots = eventObject.searchAvailability(dateOfMeeting, attendees,duration);
            listBox4.Items.Clear();
            DateTime timeSlot;
            Console.WriteLine("number of Possible TimeSlots: " + possibleTimeSlots.Count());
            for (int i = 0; i < possibleTimeSlots.Count(); i++)
            {
                timeSlot = (DateTime)possibleTimeSlots.ElementAt(i);
                listBox4.Items.Add("                            " + timeSlot.TimeOfDay+" to "+timeSlot.AddMinutes(60).TimeOfDay);

            }
            panel9.Visible = true;
            panel2.Visible = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Enabled = true;
            selectedEvent = (Event)eventList[listBox1.SelectedIndex];
            DateTime startTime = DateTime.Parse("April 27 2022 9:00");
            DateTime endTime = DateTime.Parse("April 27 2022 10:00");
            String eventDescription = null;
            String eventTitle = null;
            int eventID;
            Console.WriteLine("flag value" + flag);
            if (flag==1)
            {
                eventTitle = selectedEvent.getEventTitle();
                eventDescription = selectedEvent.getEventDescription();
                endTime = selectedEvent.getEventEndDateAndTime();
                startTime = selectedEvent.getEventStartDateAndTime();
                eventID = selectedEvent.getEventID();
                Form2 editForm = new Form2(startTime, endTime, eventDescription, eventTitle, eventID);
                editForm.LabelContents();
                editForm.FormClosing += new FormClosingEventHandler(this.EditForm_FormClosing);
                editForm.ShowDialog();
            }

            if (flag == 2)
            {
                
                int eventId = selectedEvent.getEventID();
                selectedEvent.deleteEvent(eventId);
                panel4.Visible = false;
                panel6.Visible = true;
            }

            if (flag == 3)
            {
                
                int eventId = selectedEvent.getEventID();
                eventTitle = selectedEvent.getEventTitle();
                eventDescription = selectedEvent.getEventDescription();
                String endDateAndTime = selectedEvent.getEventEndDateAndTime().ToString();
                String startDateAndTime = selectedEvent.getEventStartDateAndTime().ToString();
                eventID = selectedEvent.getEventID();
                Form3 viewForm = new Form3(eventTitle, startDateAndTime, endDateAndTime, eventDescription);
                viewForm.LabelContents();
                viewForm.Visible = true;
                viewForm.FormClosing += new FormClosingEventHandler(this.ViewForm_FormClosing);
               
            }
        }
        private void DateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

            if (!busyStartDate)
            {
                busyStartDate = true;


                DateTime dt = dateTimePicker3.Value;

                if ((dt.Minute * 60) % 3600 != 0)
                {
                    if (prevStartDate > dt)
                    {
                        dateTimePicker3.Value = prevStartDate.AddMinutes(-30);
                    }
                    else dateTimePicker3.Value = prevStartDate.AddMinutes(30);
                }
                busyStartDate = false;
                prevStartDate = dateTimePicker3.Value;

            }

        }
        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            eventList = Event.retrieveEvents(employeeid);
            listBox1.Items.Clear();
            Event tempEvent;
            Console.WriteLine("number of Events: " + eventList.Count);
            for (int i = 0; i < eventList.Count; i++)
            {
                tempEvent = (Event)eventList[i];
                listBox1.Items.Add("                            " + tempEvent.getEventTitle());
            }
        }

        private void ViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            eventList = Event.retrieveEvents(employeeid);
            listBox1.Items.Clear();
            Event tempEvent;
            Console.WriteLine("number of Events: " + eventList.Count);
            for (int i = 0; i < eventList.Count; i++)
            {
                tempEvent = (Event)eventList[i];
                listBox1.Items.Add("                            " + tempEvent.getEventTitle());
            }
        }

       

        private void button10_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            eventList = Event.retrieveEvents(employeeid);
            tableLayoutPanel1.Visible = true;

        }

      
        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            eventList = Event.retrieveEvents(employeeid);
            tableLayoutPanel1.Visible = true;
        }

       

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
                String title = textBox3.Text;
                DateTime startDateAndTime = dateTimePicker3.Value;
                DateTime endDateAndTime = dateTimePicker4.Value;
                string description = richTextBox3.Text;
                int employeeid = int.Parse(textBox1.Text);
                if (startDateAndTime.CompareTo(DateTime.Now) < 0)
                {
                    panel8.Visible = false;
                    panel3.Visible = true;
                    tableLayoutPanel1.Visible = false;
                    panel1.Visible = false;
                    panel4.Visible = false;

                    panel6.Visible = false;

                    panel2.Visible = false;
                    panel9.Visible = false;
                    label8.Text = "Choose a time later than current time";

                }
                else if (endDateAndTime.CompareTo(startDateAndTime) <= 0)
                {
                    panel8.Visible = false;
                    panel3.Visible = true;
                    tableLayoutPanel1.Visible = false;
                    panel1.Visible = false;
                    panel4.Visible = false;

                    panel6.Visible = false;

                    panel2.Visible = false;
                    panel9.Visible = false;
                    label8.Text = "The endTime is less than or equal to the startTime";
                }
                else
                {
                    Event newEventToBeAdded = new Event();
                    addeventcode = newEventToBeAdded.addEvent(title, startDateAndTime, endDateAndTime, description, employeeid);
                    Console.WriteLine("addeventcode" + addeventcode);

                    if (addeventcode == 1)
                    {
                        label8.Text = "An event is already present in given timeslot";
                    }
                    else
                    {
                        label8.Text = "An event has been successfully added in your calendar";
                    }
                    panel8.Visible = false;
                    panel3.Visible = true;
                    tableLayoutPanel1.Visible = false;
                    panel1.Visible = false;
                    panel4.Visible = false;

                    panel6.Visible = false;

                    panel2.Visible = false;
                    panel9.Visible = false;
                }


            }

            private void button19_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            eventList = Event.retrieveEvents(employeeid);
            panel9.Visible = false;
            tableLayoutPanel1.Visible = true;
        }
    }
}
