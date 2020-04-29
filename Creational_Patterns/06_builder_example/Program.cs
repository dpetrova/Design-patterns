using System;
using System.Threading;

namespace _06_builder_example
{
    // Abstract Factory : Builder Implementation
    interface IBuilder<Brand> where Brand : IBrand
    {
        IBag CreateBag();
    }


    // Abstract Factory now the Builder
    class Builder<Brand> : IBuilder<Brand> where Brand : IBrand, new()
    {
        Brand myBrand;

        public Builder()
        {
            myBrand = new Brand();
        }

        public IBag CreateBag()
        {
            return myBrand.CreateBag();
        }
    }


    // Product 1
    interface IBag
    {
        string Properties { get; set; }
    }


    // Concrete Product 1
    class Bag : IBag
    {
        public string Properties { get; set; }
    }


    // Directors
    interface IBrand
    {
        IBag CreateBag();
    }

    class Gucci : IBrand
    {
        public IBag CreateBag()
        {
            Bag b = new Bag();
            Program.DoWork("Cut Leather", 250);
            Program.DoWork("Sew leather", 1000);
            b.Properties += "Leather";
            Program.DoWork("Create Lining", 500);
            Program.DoWork("Attach Lining", 1000);
            b.Properties += " lined";
            Program.DoWork("Add Label", 250);
            b.Properties += " with label";
            return b;
        }
    }


    class Poochy : IBrand
    {
        public IBag CreateBag()
        {
            Bag b = new Bag();
            Program.DoWork("Hire cheap labour", 200);
            Program.DoWork("Cut Plastic", 125);
            Program.DoWork("Sew Plastic", 500);
            b.Properties += "Plastic";
            Program.DoWork("Add Label", 100);
            b.Properties += " with label";
            return b;
        }
    }


    class Client<Brand> where Brand : IBrand, new()
    {
        public void ClientMain()
        {
            IBuilder<Brand> factory = new Builder<Brand>();
            DateTime date = DateTime.Now;
            Console.WriteLine("I want to buy a bag!");
            IBag bag = factory.CreateBag();
            Console.WriteLine("I got my Bag which took " + DateTime.Now.Subtract(date).TotalSeconds * 5 + " days");
            Console.WriteLine("  with the following properties " + bag.Properties + "\n");
        }
    }


    static class Program
    {
        static void Main()
        {
            // Call Client twice 
            new Client<Poochy>().ClientMain();
            new Client<Gucci>().ClientMain();
        }

        public static void DoWork(string workitem, int time)
        {
            Console.Write("" + workitem + ": 0%");
            Thread.Sleep(time);
            Console.Write("....25%");
            Thread.Sleep(time);
            Console.Write("....50%");
            Thread.Sleep(time);
            Console.WriteLine("....100%");
        }
    }
}
