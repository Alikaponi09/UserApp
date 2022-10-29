using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp
{
    class Person
    {
        private static int index = 0;
        public int ID { get; }
        public string Name { get; protected set; }
        public string LastName { get; protected set; }

        public Person()
        {
            ID = index++;
        }

        public Person(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
            ID = index++;
        }

        public Person(int id, string name, string lastName)
        {
            Name = name;
            LastName = lastName;
            ID = id;
        }

        public void SetName(string str) 
        {
            Name = str;
        }

        public void SetLastName(string str)
        {
            LastName = str;
        }
    }
}
