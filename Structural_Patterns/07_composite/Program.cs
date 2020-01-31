using System;
using System.Collections.Generic;
using System.Text;

namespace _07_composite
{
    // The Interface
    public interface IComponent<T>
    {
        T Name { get; set; }

        void Add(IComponent<T> c);
        IComponent<T> Remove(T s);
        string Display(int depth);
        IComponent<T> Find(T s);        
    }

    // The Component
    public class Component<T> : IComponent<T>
    {
        public T Name { get; set; }

        public Component(T name)
        {
            this.Name = name;
        }

        // not applicable for Component (only for Composite)
        public void Add(IComponent<T> c)
        {
            Console.WriteLine("Cannot add to an item");
        }

        // not applicable for Component (only for Composite)
        public IComponent<T> Remove(T s)
        {
            Console.WriteLine("Cannot remove directly");
            return this;
        }

        public string Display(int depth)
        {
            return new String('-', depth) + this.Name + "\n";
        }

        public IComponent<T> Find(T s)
        {
            if (s.Equals(this.Name))
                return this;
            else
                return null;
        }
    }

    // The Composite
    public class Composite<T> : IComponent<T>
    {
        //lis of components and composites
        List<IComponent<T>> list;
        public T Name { get; set; }
        IComponent<T> holder = null;

        public Composite(T name)
        {
            this.Name = name;
            this.list = new List<IComponent<T>>();
        }

        public void Add(IComponent<T> c)
        {
            this.list.Add(c);
        }

        // Finds the item from a particular point in the structure, remove it from the list structure held locally and returns the composite from which it was removed
        // if not found, return the point as given        
        public IComponent<T> Remove(T s)
        {
            this.holder = this;
            IComponent<T> p = this.holder.Find(s);

            if (this.holder != null)
            {
                (this.holder as Composite<T>).list.Remove(p);
                return this.holder;
            }
            else
                return this;
        }

        // Recursively looks for an item
        // Returns its reference or else null
        public IComponent<T> Find(T s)
        {
            this.holder = this;
            if (this.Name.Equals(s)) return this;

            IComponent<T> found = null;

            foreach (IComponent<T> c in this.list)
            {
                found = c.Find(s);
                if (found != null) break;
            }
            return found;
        }

        // Displays items in a format indicating their level in the composite structure
        public string Display(int depth)
        {
            StringBuilder s = new StringBuilder(new String('-', depth));
            s.Append("Set " + this.Name + " length :" + this.list.Count + "\n");

            foreach (IComponent<T> component in this.list)
            {
                s.Append(component.Display(depth + 2));
            }
            return s.ToString();
        }
    }
}

