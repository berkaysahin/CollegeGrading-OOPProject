/****************************************************************************
**					SAKARYA UNIVERSITY
**				FACULTY OF COMPUTER AND INFORMATION SCIENCES
**				COMPUTER ENGINEERING
**				OBJECT ORIENTED PROGRAMMING COURSE
**					2019-2020 FALL SEMESTER
**	
**				HOMEWORK NUMBER...........: 01
**				STUDENT NAME..............: BERKAY ŞAHİN
**				STUDENT NUMBER............: G191210302
**              COURSE GROUP..............: 2C Group
****************************************************************************/


using System;
using System.IO;

namespace NDPOdev
{
    class Program
    {
        public static int AA = 0, BA = 0, BB = 0, CB = 0, CC = 0, DC = 0, DD = 0, FD = 0, FF = 0;
        public static double[] coefficients;
        public static string[,] students;
        public static double[] studentAverages;
        public static string[] studentLetterGrades;
        public static int numberOfStudents = -1;

        public static void readInputTxt()
        {
            // THERE ARE HOW MANY STUDENTS IN THE FIRST ROW, WE FIND THIS AND DEFINE OUR SERIES SIZE ACCORDING TO IT --------
            string path = @"input.txt"; // We specify the path of the file that we will read.

            FileStream fs1 = new FileStream(path, FileMode.Open, FileAccess.Read); // Created a file stream object.
            StreamReader sw1 = new StreamReader(fs1); // Created a StreamReader object for reading.

            string line = sw1.ReadLine(); // Put the line in text to the variable

            while (line != null) // We will read the file line by line for calculate how many students are there
            {
                numberOfStudents++;
                line = sw1.ReadLine();
            }


            studentLetterGrades = new string[numberOfStudents];
            studentAverages = new double[numberOfStudents];
            coefficients = new double[4];
            students = new string[numberOfStudents, 7];

            sw1.Close(); // Close file
            fs1.Close();
            // --------------------------------------------------------------------------------------------


            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read); // Created a file stream object.
            StreamReader sw = new StreamReader(fs); // Created a StreamReader object for reading.

            line = "";
            line = sw.ReadLine(); // Put the line in text to the variable

            int currentLine = 1;
            while (line != null) // We will read the file line by line
            {
                string[] student = line.Split(' '); // We trim the line from the spaces and assign it to a string series
                if (currentLine == 1) // In the first line of the input, exams and assignments have % effects, we will draw them.
                {
                    coefficients[0] = Convert.ToDouble(student[4]);
                    coefficients[1] = Convert.ToDouble(student[5]);
                    coefficients[2] = Convert.ToDouble(student[6]);
                    coefficients[3] = Convert.ToDouble(student[7]);
                }
                else // Student information is receiving as a matrix 
                {
                    students[currentLine - 2, 0] = student[0];
                    students[currentLine - 2, 1] = student[1];
                    students[currentLine - 2, 2] = student[2];
                    students[currentLine - 2, 3] = student[3];
                    students[currentLine - 2, 4] = student[4];
                    students[currentLine - 2, 5] = student[5];
                    students[currentLine - 2, 6] = student[6];
                }

                currentLine++;

                line = sw.ReadLine(); // Take the next line
            } // After reading the last line, we finished the reading process.

            sw.Close(); // Close file
            fs.Close();


            for (int i = 0; i < numberOfStudents; i++) // Will return as many as the number of students
            {
                // Calculate students averages
                studentAverages[i] = ((Convert.ToDouble(students[i, 3]) * Convert.ToDouble(coefficients[0])) / 100) +
                    ((Convert.ToDouble(students[i, 4]) * Convert.ToDouble(coefficients[1])) / 100) +
                    ((Convert.ToDouble(students[i, 5]) * Convert.ToDouble(coefficients[2])) / 100) +
                    ((Convert.ToDouble(students[i, 6]) * Convert.ToDouble(coefficients[3])) / 100);

                // Calculate letter grade
                studentLetterGrades[i] = calculateLetterGrade(Convert.ToDouble(studentAverages[i]));
            }

            writeOutputTxt(students, studentAverages, studentLetterGrades); // When the process is finished, we print the information to both console and output.txt.
        }

        private static string calculateLetterGrade(double avg)
        {
            // it is checked according to our letter grade rules and increases the number by 1 according to the letter grade
            // and return the letter grade

            if (avg >= 90 && avg <= 100)
            {
                AA++;
                return "AA";
            }
            else if (avg >= 85 && avg < 90)
            {
                BA++;
                return "BA";
            }
            else if (avg >= 80 && avg < 85)
            {
                BB++;
                return "BB";
            }
            else if (avg >= 75 && avg < 80)
            {
                CB++;
                return "CB";
            }
            else if (avg >= 65 && avg < 75)
            {
                CC++;
                return "CC";
            }
            else if (avg >= 58 && avg < 65)
            {
                DC++;
                return "DC";
            }
            else if (avg >= 50 && avg < 58)
            {
                DD++;
                return "DD";
            }
            else if (avg >= 40 && avg < 50)
            {
                FD++;
                return "FD";
            }
            else
            {
                FF++;
                return "FF";
            }
        }

        private static void writeOutputTxt(string[,] student, double[] studentAvg, string[] studentLetterGrade)
        {
            string path = @"output.txt"; // We specify the path of the file that we will read.

            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write); // Created a file stream object.
            StreamWriter sw = new StreamWriter(fs); // Created a StreamWriter object for reading.

            // Results will be printed to file and console.

            for (int i = 0; i < numberOfStudents; i++) // Will return as many as the number of students
            {
                sw.WriteLine(student[i, 0] + " " + student[i, 1] + " " + student[i, 2] + " - Student Average: " + studentAvg[i] + " - Letter Grade: " + studentLetterGrade[i]);
                Console.WriteLine(student[i, 0] + " " + student[i, 1] + " " + student[i, 2] + " - Student Average: " + studentAvg[i] + " - Letter Grade: " + studentLetterGrade[i]);
            }

            sw.WriteLine("\n- LETTER GRADE STATUS OF THE CLASS -");
            sw.WriteLine("AA: " + AA.ToString() + " Person - " + Convert.ToInt32((AA * 100) / numberOfStudents) + "% OF THE CLASS");
            sw.WriteLine("BA: " + BA.ToString() + " Person - " + Convert.ToInt32((BA * 100) / numberOfStudents) + "% OF THE CLASS");
            sw.WriteLine("BB: " + BB.ToString() + " Person - " + Convert.ToInt32((BB * 100) / numberOfStudents) + "% OF THE CLASS");
            sw.WriteLine("CB: " + CB.ToString() + " Person - " + Convert.ToInt32((CB * 100) / numberOfStudents) + "% OF THE CLASS");
            sw.WriteLine("CC: " + CC.ToString() + " Person - " + Convert.ToInt32((CC * 100) / numberOfStudents) + "% OF THE CLASS");
            sw.WriteLine("DC: " + DC.ToString() + " Person - " + Convert.ToInt32((DC * 100) / numberOfStudents) + "% OF THE CLASS");
            sw.WriteLine("DD: " + DD.ToString() + " Person - " + Convert.ToInt32((DD * 100) / numberOfStudents) + "% OF THE CLASS");
            sw.WriteLine("FD: " + FD.ToString() + " Person - " + Convert.ToInt32((FD * 100) / numberOfStudents) + "% OF THE CLASS");
            sw.WriteLine("FF: " + FF.ToString() + " Person - " + Convert.ToInt32((FF * 100) / numberOfStudents) + "% OF THE CLASS");

            Console.WriteLine("\n- LETTER GRADE STATUS OF THE CLASS -");
            Console.WriteLine("AA: " + AA.ToString() + " Person - " + Convert.ToInt32((AA * 100) / numberOfStudents) + "% OF THE CLASS");
            Console.WriteLine("BA: " + BA.ToString() + " Person - " + Convert.ToInt32((BA * 100) / numberOfStudents) + "% OF THE CLASS");
            Console.WriteLine("BB: " + BB.ToString() + " Person - " + Convert.ToInt32((BB * 100) / numberOfStudents) + "% OF THE CLASS");
            Console.WriteLine("CB: " + CB.ToString() + " Person - " + Convert.ToInt32((CB * 100) / numberOfStudents) + "% OF THE CLASS");
            Console.WriteLine("CC: " + CC.ToString() + " Person - " + Convert.ToInt32((CC * 100) / numberOfStudents) + "% OF THE CLASS");
            Console.WriteLine("DC: " + DC.ToString() + " Person - " + Convert.ToInt32((DC * 100) / numberOfStudents) + "% OF THE CLASS");
            Console.WriteLine("DD: " + DD.ToString() + " Person - " + Convert.ToInt32((DD * 100) / numberOfStudents) + "% OF THE CLASS");
            Console.WriteLine("FD: " + FD.ToString() + " Person - " + Convert.ToInt32((FD * 100) / numberOfStudents) + "% OF THE CLASS");
            Console.WriteLine("FF: " + FF.ToString() + " Person - " + Convert.ToInt32((FF * 100) / numberOfStudents) + "% OF THE CLASS");


            sw.Flush(); // txt created

            sw.Close(); // Close file
            fs.Close();
        }

        static void Main(string[] args)
        {
            readInputTxt(); // Call the function to read the data in input.txt and make calculations and print operations.

            Console.WriteLine("\nThe results are in the folder with the program.");
            Console.WriteLine("Printed to the output.txt file.");

            Console.ReadKey();
        }
    }
}
