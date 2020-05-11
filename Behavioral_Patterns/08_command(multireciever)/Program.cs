using System;

namespace _08_command_multireciever_
{
    class CommandPattern
    {
        delegate void Invoker();
        delegate void InvokerSet(string s);
        static Invoker Execute, Redo, Undo;
        static InvokerSet Set;

        class Command
        {
            public Command(Receiver receiver)
            {
                Set = delegate
                {
                    Console.WriteLine("Not implemented - default of XXX used");
                    receiver.S = "XXX";
                };
                Execute = receiver.Action;
                Redo = receiver.Action;
                Undo = receiver.Reverse;
            }
        }


        class Command2
        {
            public Command2(Receiver2 receiver)
            {
                Set = receiver.SetData;
                Execute = receiver.DoIt;
                Redo = receiver.DoIt;
                Undo = delegate
                {
                    Console.WriteLine("Not Implemented");
                };
            }
        }


        public class Receiver
        {
            string build, oldbuild;
            string s = "some string ";

            public string S
            {
                get { return s; }
                set { s = value; }
            }

            public void Action()
            {
                oldbuild = build;
                build += s;
                Console.WriteLine("Receiver is adding " + build);
            }

            public void Reverse()
            {
                build = oldbuild;
                Console.WriteLine("Receiver is reverting to " + build);
            }
        }


        public class Receiver2
        {
            string build, oldbuild;
            string s;

            public void SetData(string s)
            {
                this.s = s;
            }

            public void DoIt()
            {
                oldbuild = build;
                build += s;
                Console.WriteLine("Receiver is building " + build);
            }
        }

        class Client
        {
            public void ClientMain()
            {
                new Command(new Receiver());
                Execute();
                Redo();
                Undo();
                Set("III");
                Execute();
                Console.WriteLine();
                new Command2(new Receiver2());
                Set("houses ");
                Execute();
                Set("castles ");
                Undo();
                Execute();
                Redo();
            }
        }
        
        static void Main()
        {
            new Client().ClientMain();
        }
    }
}
