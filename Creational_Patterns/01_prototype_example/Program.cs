using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


// Photo Archive 

namespace _01_prototype_example
{
    [Serializable()]
    public abstract class IPrototype<T>
    {
        // Shallow copy
        public T Clone()
        {
            return (T)this.MemberwiseClone();
        }

        // Deep Copy
        public T DeepCopy()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);
            T copy = (T)formatter.Deserialize(stream);
            stream.Close();
            return copy;
        }
    }

    // The Interface  
    public interface IComponent<T>
    {
        void Add(IComponent<T> c);
        IComponent<T> Remove(T s);
        string Display(int depth);
        IComponent<T> Find(T s);
        IComponent<T> Share(T s, IComponent<T> home);
        string Name { get; set; }
    }


    // The Composite
    [Serializable()]
    public class Composite<T> : IPrototype<IComponent<T>>, IComponent<T>
    {
        List<IComponent<T>> list;
        public string Name { get; set; }

        public Composite(string name)
        {
            Name = name; list = new List<IComponent<T>>();
        }

        public void Add(IComponent<T> c)
        {
            list.Add(c);
        }

        // Finds the item from a particular point in the structure and returns the composite from which it was removed
        // If not found, return the point as given
        public IComponent<T> Remove(T s)
        {
            holder = this;
            IComponent<T> p = holder.Find(s);
            if (holder != null)
            {
                (holder as Composite<T>).list.Remove(p);
                return holder;
            }
            else
            {
                return this;
            }
        }

        IComponent<T> holder = null;

        // Recursively looks for an item    
        // Returns its reference or else null   
        public IComponent<T> Find(T s)
        {
            holder = this;
            if (Name.Equals(s)) return this;
            IComponent<T> found = null;
            foreach (IComponent<T> c in list)
            {
                found = c.Find(s);
                if (found != null) break;
            }
            return found;
        }

        public IComponent<T> Share(T set, IComponent<T> toHere)
        {
            IPrototype<IComponent<T>> prototype = this.Find(set) as IPrototype<IComponent<T>>;
            IComponent<T> copy = prototype.DeepCopy() as IComponent<T>;
            toHere.Add(copy);
            return toHere;
        }

        // Displays items in a format indicating their level in the composite structure
        public string Display(int depth)
        {
            String s = new String('-', depth) + "Set " + Name + " length :" + list.Count + "\n";
            foreach (IComponent<T> component in list)
            {
                s += component.Display(depth + 2);
            }
            return s;
        }
    }

    // The Component  
    [Serializable()]
    public class Component<T> : IPrototype<IComponent<T>>, IComponent<T>
    {
        public string Name { get; set; }
        public Component(string name)
        {
            Name = name;
        }

        public void Add(IComponent<T> c)
        {
            Console.WriteLine("Cannot add to an item");
        }

        public IComponent<T> Remove(T s)
        {
            Console.WriteLine("Cannot remove directly");
            return this;
        }

        public string Display(int depth)
        {
            return new String('-', depth) + Name + "\n";
        }

        public IComponent<T> Find(T s)
        {
            if (s.Equals(Name)) return this;
            else return null;
        }

        public IComponent<T> Share(T set, IComponent<T> toHere)
        {
            IPrototype<IComponent<T>> prototype = this.Find(set) as IPrototype<IComponent<T>>;
            IComponent<T> copy = prototype.Clone() as IComponent<T>;
            toHere.Add(copy);
            return toHere;
        }
    }

    // The Client  
    class Client
    {
        static void Main()
        {
            IComponent<string> album = new Composite<string>("Album");
            IComponent<string> point = album;
            IComponent<string> archive = new Composite<string>("Archive");
            string[] s; string command, parameter;

            // Create and manipulate a structure     
            StreamReader instream = new StreamReader("prototype.dat");
            do
            {
                string t = instream.ReadLine();
                Console.WriteLine("\t\t\t\t" + t);
                s = t.Split();
                command = s[0];
                if (s.Length > 1) parameter = s[1];
                else parameter = null;
                switch (command)
                {
                    case "AddSet":
                        IComponent<string> c = new Composite<string>(parameter);
                        point.Add(c);
                        point = c;
                        break;
                    case "AddPhoto": point.Add(new Component<string>(parameter)); break;
                    case "Remove": point = point.Remove(parameter); break;
                    case "Find": point = album.Find(parameter); break;
                    case "Display":
                        if (parameter == null) Console.WriteLine(album.Display(0));
                        else Console.WriteLine(archive.Display(0));
                        break;
                    case "Archive": archive = point.Share(parameter, archive); break;
                    case "Retrieve": point = archive.Share(parameter, album); break;
                    case "Quit": break;
                }
            }
            while (!command.Equals("Quit"));
        }
    }
}
