using Apbd_Tutorial2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Apbd_Tutorial2
{
    public class Program
    {
       public  static void Main(string[] args)
        {
            var path = @args[0];
            Console.WriteLine(path);
            //streamwriter to log errors
            using var sw = new StreamWriter(@"log.txt");
            // Files to be deleted    
            string resultFile = "result.xml";
            string destinationPath = args[1];
            Boolean noErrors = true;
            var fi = new FileInfo(path);

            //read file

            //using releases resources afte rthey have been used thus dispose
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                var listOfStudents = new HashSet<Student>(new customComparer());
                var listOfStudies = new HashSet<Study>(new customComparer());

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
                        var student = new Student
                        {
                            indexNumber = "s" + columns[4],
                            fName = columns[0],
                            lName = columns[1],
                            BirthDate = DateTime.Parse(columns[5]),
                            Email = columns[6],
                            Mother = columns[7],
                            Father = columns[8]
                        };

                        var study = new Study
                        {
                            name = columns[2],
                            mode = columns[3],

                        };

                        study.numberOfStudents++;
                        listOfStudies.Add(study);
                        //if any student was repeated print error
                        if (!listOfStudents.Add(student))
                        {
                            //log error
                            sw.WriteLine($"student with student number s{columns[4]} was not added successfully to the list");
                        }
                    }

                
                    
                }
                
                Console.WriteLine("number of students :"+listOfStudents.Count);
                var writer = new FileStream(Path.Combine(args[1], resultFile), FileMode.Create);
                var serializer = new XmlSerializer(typeof(HashSet<Student>),new XmlRootAttribute("University"));
                serializer.Serialize(writer, listOfStudents);

            }

        }
    }
}
