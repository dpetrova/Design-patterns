using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_singleton
{
    public sealed class Singleton
    {
        // Private Constructor
        Singleton() { }

        // Private object instantiated with private constructor
        static readonly Singleton instance = new Singleton();

        // Public static property to get the object
        public static Singleton Instance
        {
            get { return instance; }
        }
    }

    class Program
    {
        static void Main()
        {
            //create instance
            Singleton s1 = Singleton.Instance;

            //instead of
            //Singleton s1 = new Singleton();
        }
    }
}
