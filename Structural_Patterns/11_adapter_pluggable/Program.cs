using System;

namespace _11_adapter_pluggable
{
    // Existing way requests are implemented
    class Adaptee
    {
        public double Precise(double a, double b)
        {
            return a / b;
        }
    }

    // New standard for requests
    class Target
    {
        public string Estimate(int i)
        {
            return "Estimate is " + (int)Math.Round(i / 3.0);
        }
    }

    // Implementing new requests via old
    class Adapter : Adaptee
    {
        public Func<int, string> Request;

        // Different constructors for the expected targets/adaptees

        // Adapter-Adaptee
        public Adapter(Adaptee adaptee)
        {
            // Set the delegate to the new standard
            Request = delegate (int i)
            {
                return "Estimate based on precision is " +
                (int)Math.Round(Precise(i, 3));
            };
        }

        // Adapter-Target
        public Adapter(Target target)
        {
            // Set the delegate to the existing standard
            Request = target.Estimate;
        }
    }

    class Client
    {
        static void Main()
        {
            Adapter adapter1 = new Adapter(new Adaptee());
            Console.WriteLine(adapter1.Request(5));

            Adapter adapter2 = new Adapter(new Target());
            Console.WriteLine(adapter2.Request(5));
        }
    }
}
