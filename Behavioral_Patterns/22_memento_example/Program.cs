using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

// TicTacToe
// The game is played on a board between two players.
// In this example, we’ll consider a TicTacToe tutor that pits a human player against the computer.
// The player should be able to go back and try different moves if he/she is not winning. 
// This is an ideal situation for the Memento pattern.
// The Originator is the board, and its state is saved at every turn.
// The Caretaker keeps an array of Mementos so that it can roll back more than one turn. 

namespace _22_memento_example
{
    // Simulates TicTacToe, where the game can roll back any specified number of moves. Mementos are kept at each move. 
    class MementoPattern
    {
        // Originator 
        [Serializable()]
        class Game
        {
            char[] board = { 'X', '1', '2', '3', '4', '5', '6', '7', '8', '9' }; // Nine spaces
            public char Player { get; set; }

            public Game()
            {
                this.Player = 'X';
            }

            public void Play(int pos)
            {
                board[pos] = Player;
                if (Player == 'X') Player = 'O';
                else Player = 'X';
                // Preserve player 
                board[0] = Player;
            }

            // The reference to the memento is passed back to the client 
            public Memento Save()
            {
                Memento memento = new Memento();
                return memento.Save(board);
            }
            public void Restore(Memento memento)
            {
                board = (char[])memento.Restore();
                Player = board[0];
            }

            public void DisplayBoard()
            {
                Console.WriteLine();
                for (int i = 1; i <= 9; i += 3)
                {
                    Console.WriteLine(board[i] + " | " + board[i + 1] + " | " + board[i + 2]);
                    if (i < 6) Console.WriteLine("---------");
                }
            }
        }


        [Serializable()]
        // Serializes by deep copy to memory and back
        class Memento
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            public Memento Save(object o)
            {
                formatter.Serialize(stream, o);
                return this;
            }

            public object Restore()
            {
                stream.Seek(0, SeekOrigin.Begin);
                object o = formatter.Deserialize(stream);
                stream.Close();
                return o;
            }
        }


        class Caretaker
        {
            public Memento Memento { get; set; }
        }


        class Simulator : IEnumerable
        {
            string[] moves = { "5", "3", "1", "6", "9", "U-2", "9", "6", "4", "2", "7", "8", "Q" };
            public IEnumerator GetEnumerator()
            {
                foreach (string element in moves)
                    yield return element;
            }
        }

        // Client
        static void Main()
        {
            Console.WriteLine("Let's practice TicTacToe");
            Console.WriteLine("Commands are:\n1-9 for a position\n" +
                "U-n where n is the number of moves to undo" +
                "\nQ to end");
            Game game = new Game();

            // References to the mementos
            Caretaker[] c = new Caretaker[10];
            game.DisplayBoard();
            int move = 1;
            // Iterator for the moves
            Simulator simulator = new Simulator();

            foreach (string command in simulator)
            {
                Console.Write("Move " + move + " for " + game.Player + ": " + command);
                if (command[0] == 'Q') break;
                // Save at the start of each move
                c[move] = new Caretaker();
                c[move].Memento = game.Save();
                // Check for undo
                if (command[0] == 'U')
                {
                    int back = Int32.Parse(command.Substring(2, 1));
                    if (move - back > 0)
                        game.Restore(c[move - back].Memento);
                    else
                        Console.WriteLine("Too many moves back");
                    move = move - back - 1;
                }
                // Otherwise play 
                else
                    game.Play(Int32.Parse(command.Substring(0, 1)));
                // Update board and move number
                game.DisplayBoard();
                move++;
            }

            Console.WriteLine("Thanks for playing");
        }
    }
}
