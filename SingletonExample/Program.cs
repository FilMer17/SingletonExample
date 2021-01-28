using System;
using System.Collections.Generic;

namespace SingletonExample
{
    class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public string SSN { get; set; }

        public Person(string fname, string lname, DateTime bday, string ssn)
        {
            Firstname = fname;
            Lastname = lname;
            Birthday = bday;
            SSN = ssn;
        }

        public override string ToString()
        {
            return $"{Firstname} {Lastname} | {Birthday:MM/dd/yyyy} | {SSN}";
        }
    }

    class Database
    {
        static Dictionary<int, Person> data;
        static Database oneInstance = null;

        static readonly object lockObject = new object();

        Database()
        {
            Id = 0;
            data = new Dictionary<int, Person>
            {
                { Id++, new Person("Petr", "Novák", new DateTime(1998, 7, 21), "980721") }
            };
        }

        public int Id { get; set; }
        public static Database Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (oneInstance == null)
                    {
                        oneInstance = new Database();
                    }
                }

                return oneInstance;
            }
        }

        public void AddData(Person person)
        {
            data.Add(Id++, person);
        }

        public void ShowData()
        {
            foreach (KeyValuePair<int, Person> item in data)
            {
                Console.WriteLine(item.Key.ToString() + " " + item.Value.ToString());
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Database database1 = Database.Instance;
            Database database2 = Database.Instance;

            database1.ShowData(); // První odkaz na jednu databázy

            if (ReferenceEquals(database1, database2))
                Console.WriteLine("\nJedna databáze\n");
            else
                Console.WriteLine("\nDvě databáze?!\n");

            database1.AddData(new Person("Petr", "Novák", new DateTime(1998, 7, 21), "980721"));
            database1.AddData(new Person("Petr", "Novák", new DateTime(1998, 7, 21), "980721"));

            database2.ShowData(); //Druhý odkaz na jednu databázy

            Console.ReadKey();
        }
    }
}
