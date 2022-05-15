using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;

namespace CalendarSystem
{
    class Event
    {
        String title;
        DateTime startDateAndTime;
        DateTime endDateAndTime;
        String description;
        bool checkIfConflict;
        int employeeid;
        int numberOfConflictEvents;
        List<List<List<DateTime>>> listOfUsedTimeSlots;
        List<DateTime> employeeEventStartTimeAndEndtime;
        List<List<DateTime>> employeeEvents;
        List<List<DateTime>> availableTimeSlotsForAllEmployees;
        List<DateTime> availableTimeSlotsForEachEmployee;
        IEnumerable<DateTime> intersectionList;
        DateTime start = DateTime.Today.AddHours(9);
        DateTime end = DateTime.Today.AddHours(17);
        int eventid = 0;


        public int addEvent(String title, DateTime startDateAndTime, DateTime endDateAndTime, String description, int employeeid)
        {
            string connStr = "server=157.89.28.29;user=student;database=csc340_db;port = 3306; password = Maroon@21?";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT MAX(eventid) FROM mupparajuevent;";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    eventid = Int32.Parse(myReader[0].ToString());
                    Console.WriteLine("newEventId" + eventid);
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            if (eventid == -1)
            {
                Console.WriteLine("Error:  Cannot find and assign a new event id.");
            }
            else
            {

                connStr = "server=157.89.28.29;user=student;database=csc340_db;port = 3306; password = Maroon@21?";
                conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
                bool result = checkConflict(employeeid, startDateAndTime, endDateAndTime);
                Console.WriteLine("result conflict " + result);

                if (result)
                {
                    return 1;
                }
                else
                {
                    try
                    {
                        eventid = eventid + 1;
                        Console.WriteLine("Connecting to MySQL...");
                        conn.Open();
                        string sql = "INSERT INTO mupparajuevent (eventid,title, startDateAndTime,endDateAndTime,description,employeeid,startDate,endDate) VALUES (@eventid,@title, @startDateAndTime,@endDateAndTime,@description,@id,@startDate,@endDate)";

                        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                        Console.WriteLine("sql value" + sql);


                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@startDateAndTime", startDateAndTime);
                        cmd.Parameters.AddWithValue("@startDate", startDateAndTime.Date);
                        cmd.Parameters.AddWithValue("@endDate", endDateAndTime.Date);
                        cmd.Parameters.AddWithValue("@endDateAndTime", endDateAndTime);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@id", employeeid);
                        cmd.Parameters.AddWithValue("@eventid", eventid);

                        cmd.ExecuteNonQuery();


                    }



                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    conn.Close();
                }

            }
            return 0;
        }

        private bool checkConflict(int employeeid, DateTime startDateAndTime, DateTime endDateAndTime)
        {
            string connStr = "server=157.89.28.29;user=student;database=csc340_db;port = 3306; password = Maroon@21?";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                String sql = "SELECT count(*) from mupparajuevent WHERE (employeeid=@id) AND ((startDateAndTime < @startDateAndTime AND endDateAndTime > @startDateAndTime) OR (endDateAndTime < @startDateAndTime AND endDateAndTime > @endDateAndTime))";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                Console.WriteLine("sql value" + sql);
                cmd.Parameters.AddWithValue("@id", employeeid);
                cmd.Parameters.AddWithValue("@startDateAndTime", startDateAndTime);
                cmd.Parameters.AddWithValue("@endDateAndTime", endDateAndTime);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    numberOfConflictEvents = Int32.Parse(myReader[0].ToString());
                    Console.WriteLine("numberOfConflictEvents " + numberOfConflictEvents);
                }
                myReader.Close();
                if (numberOfConflictEvents > 0)
                    checkIfConflict = true;
                else
                    checkIfConflict = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("startdateandtime" + startDateAndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine("enddateandtime" + endDateAndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return checkIfConflict;
        }

        public static ArrayList retrieveEvents(int employeeid)
        {

            Console.WriteLine("userid in accounts: " + employeeid);
            ArrayList eventList = new ArrayList();

            DataTable myTable = new DataTable();
            string connStr = "server=157.89.28.29;user=student;database=csc340_db;port = 3306; password = Maroon@21?";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM mupparajuevent WHERE employeeid=@id ORDER BY startDateAndTime ASC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", employeeid);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable);
                Console.WriteLine("Table is ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            //convert the retrieved data to events and save them to the list
            foreach (DataRow row in myTable.Rows)
            {
                Event newEvent = new Event();

                newEvent.eventid = Int32.Parse(row["eventid"].ToString());
                newEvent.employeeid = Int32.Parse(row["employeeid"].ToString());
                newEvent.title = row["title"].ToString();
                newEvent.startDateAndTime = DateTime.Parse(row["startDateAndTime"].ToString());
                newEvent.endDateAndTime = DateTime.Parse(row["endDateAndTime"].ToString());
                newEvent.description = row["description"].ToString();
                eventList.Add(newEvent);
            }
            Console.WriteLine("*********" + eventList.Count);
            return eventList;  //return the event list
        }

        public IEnumerable<DateTime> searchAvailability(DateTime dateOfMeeting, String[] attendees,int duration)
        {
            int employeeid;
            listOfUsedTimeSlots = new List<List<List<DateTime>>>();
            for (int i = 0;i<attendees.Count();i++)
            {
                Console.WriteLine("attendee1: " + attendees[i]);
                employeeid = Int32.Parse(attendees[i]);
                Console.WriteLine("userid in accounts: " + employeeid);
                Console.WriteLine("date" + dateOfMeeting.ToString());
               
                DataTable myTable = new DataTable();
                string connStr = "server=157.89.28.29;user=student;database=csc340_db;port = 3306; password = Maroon@21?";
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "SELECT startDateAndTime,endDateAndTime FROM mupparajuevent WHERE employeeid=@id AND startDate=@dateOfMeeting";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", employeeid);
                    cmd.Parameters.AddWithValue("@dateOfMeeting", dateOfMeeting.Date);
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                    myAdapter.Fill(myTable);
                    Console.WriteLine("Table is ready.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
                

                employeeEvents = new List<List<DateTime>>();
                foreach (DataRow row in myTable.Rows)
                {
                    employeeEventStartTimeAndEndtime = new List<DateTime>();
                    Console.WriteLine(row["startDateAndTime"].ToString());
                    Console.WriteLine(row["endDateAndTime"].ToString());
                    employeeEventStartTimeAndEndtime.Add(DateTime.Parse(row["startDateAndTime"].ToString()));
                    employeeEventStartTimeAndEndtime.Add(DateTime.Parse(row["endDateAndTime"].ToString()));
                    if (employeeEventStartTimeAndEndtime.Count() != 0)
                        employeeEvents.Add(employeeEventStartTimeAndEndtime);
                }
            
                if(employeeEvents.Count() != 0)
                    listOfUsedTimeSlots.Add(employeeEvents);          
            }

            duration = 60;
                    
            availableTimeSlotsForAllEmployees = new List<List<DateTime>>();
            
            for (int i = 0;i< listOfUsedTimeSlots.Count();i++)
            {
                availableTimeSlotsForEachEmployee = new List<DateTime>();
                for (int j=0;j< listOfUsedTimeSlots[i].Count();j++)
                {
                    
                    for (DateTime meeting = start; meeting < end; meeting = meeting.AddMinutes(duration))
                    {
                        DateTime endTime = meeting.AddMinutes(duration);
                        if ((meeting.CompareTo(listOfUsedTimeSlots[i][j][0]) < 0) && (endTime.CompareTo(listOfUsedTimeSlots[i][j][0]) < 0))
                        {
                            availableTimeSlotsForEachEmployee.Add(meeting);
                        }
                        else if(meeting.CompareTo(listOfUsedTimeSlots[i][j][1]) > 0)
                        {
                            availableTimeSlotsForEachEmployee.Add(meeting);
                        }

                    }
                  
                }
                
                availableTimeSlotsForAllEmployees.Add(availableTimeSlotsForEachEmployee);
            }
            
            for (int i =0;i< availableTimeSlotsForAllEmployees.Count();i++)
            {
               
                if (availableTimeSlotsForAllEmployees.Count() == 1)
                {
                    intersectionList = availableTimeSlotsForAllEmployees[0];
                }
                else
                {
                    if (i == 0)
                    {
                        intersectionList = availableTimeSlotsForAllEmployees[0].Intersect(availableTimeSlotsForAllEmployees[1]);
                    }
                    else
                    {
                        intersectionList = availableTimeSlotsForAllEmployees[i].Intersect(intersectionList);
                    }
                }
            }
            return intersectionList;
         
        }


        public String getEventTitle()
        {
            return title;
        }

        public int getEventID()
        {
            return eventid;
        }

        public String getEventDescription()
        {
            return description;
        }

        public DateTime getEventStartDateAndTime()
        {
            return startDateAndTime;
        }

        public DateTime getEventEndDateAndTime()
        {
            return endDateAndTime;
        }

        public void editEvent(String title, DateTime startDateAndTime, DateTime endDateAndTime, String description, int eventID)
        {
            string connStr = "server=157.89.28.29;user=student;database=csc340_db;port = 3306; password = Maroon@21?";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {

                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "update mupparajuevent set title=@title,startDateAndTime=@startDateAndTime,endDateAndTime=@endDateAndTime,description=@description where eventid=@eventid;";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                Console.WriteLine("sql value" + sql);


                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@startDateAndTime", startDateAndTime);
                cmd.Parameters.AddWithValue("@endDateAndTime", endDateAndTime);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@eventid", eventID);

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        public void deleteEvent(int eventid)
        {
            string connStr = "server=157.89.28.29;user=student;database=csc340_db;port = 3306; password = Maroon@21?";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {

                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "delete from mupparajuevent where eventid=@eventid;";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@startDateAndTime", startDateAndTime);
                cmd.Parameters.AddWithValue("@endDateAndTime", endDateAndTime);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@eventid", eventid);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
