using System;

namespace _03_proxy__virtual_
{
    public interface ISubject
    {
        string Request();
    }

    //private class
    class Subject
    {
        public string Request()
        {
            return "Subject Request " + "Choose left door\n";
        }
    }


    public class Proxy : ISubject
    {
        Subject subject;

        public string Request()
        {
            // A virtual proxy creates the object only on its first method call
            if (subject == null)
            {
                Console.WriteLine("Subject inactive");
                subject = new Subject();
            }
            Console.WriteLine("Subject active");
            return "Proxy: Call to " + subject.Request();
        }
    }

    public class ProtectionProxy : ISubject
    {
        // An authentication proxy first asks for a password
        Subject subject;
        string password = "Abracadabra";

        public string Authenticate(string supplied)
        {
            if (supplied != password)
                return "Protection Proxy: No access";
            else
                subject = new Subject();
            return "Protection Proxy: Authenticated";
        }

        public string Request()
        {
            if (subject == null)
                return "Protection Proxy: Authenticate first";
            else return "Protection Proxy: Call to " +
            subject.Request();
        }
    }



    class Program
    {
        static void Main()
        {   
            //proxy example
            ISubject virtualProxy = new Proxy();
            Console.WriteLine(virtualProxy.Request());
            Console.WriteLine(virtualProxy.Request());

            //authentication proxy example
            ProtectionProxy authenticationProxy = new ProtectionProxy();
            Console.WriteLine(authenticationProxy.Request());
            Console.WriteLine((authenticationProxy as ProtectionProxy).Authenticate("Secret"));
            Console.WriteLine((authenticationProxy as ProtectionProxy).Authenticate("Abracadabra"));
            Console.WriteLine(authenticationProxy.Request());
        }
    }
}
