using System;

namespace _07_command
{
    class CommandPattern
    {
        delegate void Invoker();

        static Invoker Execute, Undo, Redo;

        class Command
        {
            public Command(Receiver receiver)
            {
                Execute = receiver.Action;
                Redo = receiver.Action;
                Undo = receiver.Reverse;
            }
        }


        public class Receiver
        {
            string build, oldbuild;
            string s = "some string ";

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


        static void Main()
        {
            new Command(new Receiver());
            Execute();
            Redo();
            Undo();
            Execute();
        }
    }
}
