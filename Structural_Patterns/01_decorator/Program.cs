using System;

namespace _01_decorator
{

    interface IComponent
    {
        string Operation();
    }

    class Component : IComponent
    {
        public string Operation()
        {
            return "I am walking ";
        }
    }

    class DecoratorA : IComponent
    {
        IComponent component;

        public DecoratorA(IComponent c)
        {
            component = c;
        }

        public string Operation()
        {
            string s = component.Operation();
            s += "and listening to Classic FM ";
            return s;
        }
    }

    class DecoratorB : IComponent
    {
        IComponent component;
        public string addedState = "past the Coffee Shop ";

        public DecoratorB(IComponent c)
        {
            component = c;
        }

        public string Operation()
        {
            string s = component.Operation();
            s += "to school ";
            return s;
        }

        public string AddedBehavior()
        {
            return "and I bought a cappuccino ";
        }
    }


    class Program
    {
        static void Main()
        {
            // basic component
            IComponent component = new Component();
            // A-decorated
            IComponent decoratorA = new DecoratorA(component);
            // B-decorated
            IComponent decoratorB = new DecoratorB(component);
            // B-A-decorated
            IComponent decoratorBA = new DecoratorB(new DecoratorA(component));
            // Explicit B-Decorator
            DecoratorB b = new DecoratorB(new Component());
            // A-B-decorated
            IComponent decoratorAB = new DecoratorA(b);

            Console.WriteLine("Basic component: {0}", component.Operation());
            Console.WriteLine("A-decorated: {0}", decoratorA.Operation());
            Console.WriteLine("B-decorated: {0}", decoratorB.Operation());
            Console.WriteLine("B-A-decorated: {0}", decoratorBA.Operation());
            Console.WriteLine("A-B-decorated: {0}", decoratorAB.Operation());

            // Invoking its added state and added behavior
            Console.WriteLine("\t\t\t" + b.addedState + b.AddedBehavior());
        }
    }
}
