using System;
using System.Collections.Generic;

// We’ll model the action of a bank that is careful about allowing withdrawals of large amounts. 
// Trusty Bank’s business rules for handling withdrawals are:
//  • Clerks can handle withdrawals of up to $1,000. 
//  • Supervisors can handle withdrawals of up to $4,000. 
//  • The bank manager can handle withdrawals of up to $9,000. 
//Any amount larger than $9,000 has to be divided into several withdrawals.
//The bank has several clerks on duty at any one time(up to 10), and usually 3 supervisors; there is only one manager.

namespace _06_chain_of_responsibility_example
{
    class ChainPatternExample
    {
        enum Levels
        {
            Manager,
            Supervisor,
            Clerk
        }

        class Structure
        {
            public int Limit { get; set; }
            public int Positions { get; set; }
        }

        class Handler
        {
            Levels level;
            int id;

            public Handler(int id, Levels level)
            {
                this.id = id;
                this.level = level;
            }

            public string HandleRequest(int data)
            {
                if (data < structure[level].Limit)
                {
                    return "Request for " + data + " handled by " + level + " " + id;
                }
                else if (level > First)
                {
                    Levels nextLevel = --level;
                    int which = choice.Next(structure[nextLevel].Positions);
                    return handlersAtLevel[nextLevel][which].HandleRequest(data);
                }
                else
                {
                    Exception chainException = new ChainException();
                    chainException.Data.Add("Limit", data);
                    throw chainException;
                }
            }
        }
        
        public class ChainException : Exception
        {
            public ChainException() { }
        }

        void AdjustChain() { }

        static Random choice = new Random(11);

        static Levels First
        {
            get { return ((Levels[])Enum.GetValues(typeof(Levels)))[0]; }
        }

        static Dictionary<Levels, Structure> structure = new Dictionary<Levels, Structure> {
        { Levels.Manager, new Structure { Limit = 9000, Positions = 1 } },
        { Levels.Supervisor, new Structure { Limit = 4000, Positions = 3 } },
        { Levels.Clerk, new Structure { Limit = 1000, Positions = 10 } }
    };

        static Dictionary<Levels, List<Handler>> handlersAtLevel = new Dictionary<Levels, List<Handler>> {
        { Levels.Manager, new List<Handler>() },
        { Levels.Supervisor, new List<Handler>() },
        { Levels.Clerk, new List<Handler>() }
    };


        void RunTheOrganization()
        {
            Console.WriteLine("Trusty Bank opens with");
            foreach (Levels level in Enum.GetValues(typeof(Levels)))
            {
                for (int i = 0; i < structure[level].Positions; i++)
                {
                    handlersAtLevel[level].Add(new Handler(i, level));
                }
                Console.WriteLine(structure[level].Positions + " " + level + "(s) who deal up to a limit of " + structure[level].Limit);
            }
            Console.WriteLine();

            int[] amounts = { 50, 2000, 1500, 10000, 175, 4500, 2000 };
            foreach (int amount in amounts)
            {
                try
                {
                    int which = choice.Next(structure[Levels.Clerk].Positions);
                    Console.Write("Approached Clerk " + which + ". ");
                    Console.WriteLine(handlersAtLevel[Levels.Clerk][which].HandleRequest(amount));
                    AdjustChain();
                }
                catch (ChainException e)
                {
                    Console.WriteLine("\nNo facility to handle a request of " + e.Data["Limit"] + "\nTry breaking it down into smaller requests\n");
                }
            }
        }

        static void Main()
        {
            new ChainPatternExample().RunTheOrganization();
        }
    }
}
