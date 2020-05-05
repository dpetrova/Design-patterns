using System;

namespace _05_chain_of_responsibility
{
    class Handler
    {
        Handler next;
        int id;
        public int Limit { get; set; }

        public Handler(int id, Handler handler)
        {
            this.id = id;
            this.Limit = id * 1000;
            this.next = handler;
        }

        public string HandleRequest(int data)
        {
            if (data < this.Limit)
                return "Request for " + data + " handled at level " + this.id;
            else if (this.next != null)
                return next.HandleRequest(data);
            else return ("Request for " + data + " handled BY DEFAULT at level " + this.id);
        }
    }

    class Program
    {
        static void Main()
        {
            Handler start = null;
            for (int i = 5; i > 0; i--)
            {
                Console.WriteLine("Handler " + i + " deals up to a limit of " + i * 1000);
                start = new Handler(i, start);
            }

            int[] a = { 50, 2000, 1500, 10000, 175, 4500 };

            foreach (int i in a)
            {
                Console.WriteLine(start.HandleRequest(i));
            }
        }
    }    
}
