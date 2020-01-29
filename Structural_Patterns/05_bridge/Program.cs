using System;

namespace _05_bridge
{
    //what client see
    class Abstraction
    {
        Bridge bridge;

        public Abstraction(Bridge implementation)
        {
            bridge = implementation;
        }

        //a method that is called by the client
        public string Operation()
        {
            return "Abstraction" + " <<< BRIDGE >>>> " + bridge.OperationImp();
        }
    }

    //interface defining those parts of Abstraction that may vary
    interface Bridge
    {
        // a method in the Bridge that is called from the Operation in the Abstraction
        string OperationImp();
    }

    //implementation of bridge interface
    class ImplementationA : Bridge
    {
        public string OperationImp()
        {
            return "ImplementationA";
        }
    }

    //implementation of bridge interface
    class ImplementationB : Bridge
    {
        public string OperationImp()
        {
            return "ImplementationB";
        }
    }


    class Program
    {
        static void Main()
        {
            var abstractionA = new Abstraction(new ImplementationA());
            var abstractionB = new Abstraction(new ImplementationB());
            Console.WriteLine(abstractionA.Operation());
            Console.WriteLine(abstractionB.Operation());
        }
    }
}
