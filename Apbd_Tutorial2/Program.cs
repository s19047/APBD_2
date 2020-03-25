using Apbd_Tutorial2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;


namespace Apbd_Tutorial2
{
    public class Program
    {

       public  static void Main(string[] args)
        {
            //path for my dataset
            string dataPath = @args[0];

            //path for the resulting xml/json files
            string destinationPath = args[1];
            
            //mode of serialization
            string mode = args[2];

            //author
            string author = "Ahmad Alaziz";
           

            //streamwriter to log errors
            using var sw = new StreamWriter(@"log.txt");

             
            string resultFileName = "result";
            
            Boolean noErrors = true;
            var fi = new FileInfo(dataPath);

            using (var stream = new StreamReader(fi.OpenRead()))
            {
              
                students Students = new students();
                var activeStudies = new List<activeStudy>();
              

                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    noErrors = true;
                    string[] columns = line.Split(",");
                    //error handling if length is not 9 
                    if (columns.Length != 9)
                    {
                        //log that there needs to be 9 columns present
                        sw.WriteLine($"entry with student number : s{columns[4]} doesn't have 9 columns");
                        noErrors = false;
                    }
                    else
                    {
                        for (int i = 0; i < columns.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(columns[i]))
                            {
                                sw.WriteLine($"Element with studfent number s{columns[4]} has column number {i} empty");
                                noErrors = false;
                            }
                        }
                    }
                    if (noErrors) {
                        var study = new Study
                        {
                            name = columns[2],
                            mode = columns[3],

                        };
                        activeStudy AS = new activeStudy
                        {
                            name = columns[2],
                            
                        };

                        // go through the list of active studies , if any duplicates are found , increase number of students
                        //else just add a new one , I realize this is unfortunatly a costly solution 

                        Boolean duplicateFound = false;
                       for(int i = 0;i<activeStudies.Count && !duplicateFound; i++)
                        {

                            if(activeStudies[i].name == AS.name)
                            {
                                duplicateFound = true;
                                ++activeStudies[i].numOfStudents;
                            }
                            
                        }
                        if (!duplicateFound)
                        {
                            AS.numOfStudents = 1;
                            activeStudies.Add(AS);
                        }
                       

                        var student = new Student
                        {
                            indexNumber = "s" + columns[4],
                            fName = columns[0],
                            lName = columns[1],
                            BirthDate = DateTime.Parse(columns[5]),
                            Email = columns[6],
                            Mother = columns[7],
                            Father = columns[8],
                            study = study
                        };

                 
                        //if any student was repeated print error
                        if (!Students.listOfStudents.Add(student))
                        {
                            //log error
                            sw.WriteLine($"student with student number s{columns[4]} was not added successfully to the list");
                        }
                    }

                }
                University uni = new University
                {
                    author = author,
                    time = DateTime.Now,
                    students = Students,
                    ActiveStudies = activeStudies

                };

                Console.WriteLine("number of students :"+Students.listOfStudents.Count);

                Serialize(destinationPath,uni,mode,resultFileName);
                

            }

        }

        private static void Serialize(String destPath,University uni, String mode, String resultFile)
        {
            var writer = new FileStream(Path.Combine(destPath, (resultFile + ".xml")), FileMode.Create);
            if (string.Equals(mode, "xml", StringComparison.OrdinalIgnoreCase))
            {
                
                var serializer = new XmlSerializer(typeof(University), new XmlRootAttribute("University"));
                serializer.Serialize(writer, uni);
            }
            else if (string.Equals(mode, "json", StringComparison.OrdinalIgnoreCase))
            {
               
                String resultJson = JsonSerializer.Serialize(uni);
                File.WriteAllText(Path.Combine(destPath, (resultFile + ".json")), resultJson);
            }
        }
    }
}
