/* Mars Lander Simulation 
 * Adam Ingram 2/28/19 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient; 

namespace mars_lander_OOP
{
    // Base class
    class Lander
    {
        protected int xBurst = 0; 
        protected int y1Burst = 0;
        protected int y2Burst = 0;
        protected int height = 0;
        protected int offCenter = 0; 
      
        // Empty Constructor 
        public Lander() { }

        // Greeting
        public void greeting()
        {
            Console.WriteLine("***Hello and welcome to the Mars Lander Simulation!***");
            Console.WriteLine("\n Team iDontCare's Mars Lander Project R121314198913\n        _______           \n    .adOOOOOOOOOba.\n   dOOOOOOOOOOOOOOOb\n  dOOOOOOOOOOOOOOOOOb\n dOOOOOOOOOOOOOOOOOOOb\n| OOOOOOOOOOOOOOOOOOOOO |\nOP'~YOOOOOOOOOOOP~`YO\nOO     `YOOOOOP'     OO\nOOb      `OOO'      dOO\nYOOo      OOO      oOOP\n`OOOo     OOO     oOOO'\n `OOOb._, dOOOb._, dOOO'\n  `OOOOOOOOOOOOOOOOO'\n   OOOOOOOOOOOOOOOOO\n   YOOOOOOOOOOOOOOOP\n   `OOOOOOOOOOOOOOO'\n    `OOOOOOOOOOOOO'\n     `OOOOOOOOOOO'\n       `~OOOOO~'   ");
            Console.Write("\n\n Hit enter to begin....  ");
            Console.ReadLine(); 
        }

        // The count down 
        public void countDown() 
        {
            for (int i = 5; i > 0; i--)
            {
                Console.WriteLine("\nCountdown: " + i);
            }

            // Console.WritLine statement to say the PARACHUTE RELEASED
            Console.WriteLine("\nBegin parachute detachment...");
            Console.WriteLine("\nParachute detatched...");

            // Console.WriteLine statement to say GCS ONLINE 
            Console.WriteLine("\nGCS engaged and online...");
        }
    }

    // Subclass 
    class Stages : Lander
    {
        // Empty Constructor 
        public Stages() { }

        // Stage One Throttle Burst
        public void stageOne()
        {
            string connString = "Data source = localhost; Database = mars_lander_project; User Id = root; Password = Jhon316!";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            Random rnd = new Random();
            height = rnd.Next(41, 100); // Height from planet 
            offCenter = rnd.Next(-10, 10);

            while (offCenter > 0)
            {
                y2Burst++;
                command.CommandText = "Insert into marslander_data (y) Values('-1')";
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                offCenter--;
            }
            while (offCenter < 0)
            {
                y1Burst++;
                // Insert Data to MySQL Workbench
                command.CommandText = "Insert into marslander_data (y) Values('1')";
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                offCenter++;
            }
            while (height > 40)
            {
                xBurst++;               
                // Insert Data to MySQL Workbench 
                command.CommandText = "Insert into marslander_data (x) Values('1')";
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                height--;
            }
            Console.WriteLine("Total Vertical bursts: " + xBurst + "\nTotal Right Bursts: " + y1Burst + "\nTotal Left Bursts: " + y2Burst);
            Console.WriteLine("Lander is above the landing zone...");      
        }

        // Stage 2: Predefined Altitude 
        public void stageTwo()
        {
            string connString = "Data source = localhost; Database = mars_lander_project; User Id = root; Password = Jhon316!";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();

            height = 40;

            Console.WriteLine("\nShutting down engines and beginning terminal descent to planet...");
            for (height = 40; height > 0; height -= 5)
            {
                Console.WriteLine("\nTerminal descent in progress...Current distance to landing zone: " + height);
                // Insert Data to MySQL Workbench
                command.CommandText = "Insert into marslander_data (altitude_height) Values('-5')";
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        // Stage 3: Touchdown and retrive all data and print it to the consloe 
        public void stageThree()
        {
            Console.WriteLine("\nDescent complete...Landing successful!!!");
            Console.WriteLine("\nSending Data to Database...");
            Console.WriteLine("\nData transfer complete...");
        }

    }

    // Subclass 
    class Printing_Data : Lander 
    {
        // Empty Constructor 
        public Printing_Data() { }

        // Methods for printing data from database
        public void printVerticleThurst()
        {
            string connString = "Data source = localhost; Database = mars_lander_project; User Id = root; Password = Jhon316!";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select x from marslander_data";
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("\nVerticle Thurst used:");
            while (reader.Read())
            {
                Console.Write(reader["x"].ToString());
            }
            conn.Close();
        }

        public void printDirectionalThrust()
        {
            string connString = "Data source = localhost; Database = mars_lander_project; User Id = root; Password = Jhon316!";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select y from marslander_data";
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("\n\nDirectional Thrust Used:");
            while (reader.Read())
            {
                Console.Write(reader["y"].ToString());
            }
            conn.Close();
        }

        public void printDistanceFromTouchdown()
        {
            string connString = "Data source = localhost; Database = mars_lander_project; User Id = root; Password = Jhon316!";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select altitude_height from marslander_data";
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("\n\nDistance from touchdown");
            while (reader.Read())
            {
                Console.Write(reader["altitude_height"].ToString());
            }
            conn.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Lander lander1 = new Lander();
            Stages deploy = new Stages();
            Printing_Data print = new Printing_Data();
            lander1.greeting(); 
            lander1.countDown(); 
            deploy.stageOne();
            deploy.stageTwo();
            deploy.stageThree();
            print.printVerticleThurst();
            print.printDirectionalThrust();
            print.printDistanceFromTouchdown(); 

            Console.WriteLine("\n\nEnter to terminate...  ");
            Console.ReadLine();
        }
    }
}
