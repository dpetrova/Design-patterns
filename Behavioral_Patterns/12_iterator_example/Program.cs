using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Suppose we have a family tree. 
//The goal of the example is to traverse the tree and select people with various criteria for printing.
//Family trees are usually represented as binary trees, 
//with the left link to the firstborn and the next link to the right to the next sibling. 


namespace _12_iterator_example
{
    class Node<T>
    {
        public Node() { }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public T Data { get; set; }
        public Node(T d, Node<T> left, Node<T> right)
        {
            Data = d;
            Left = left;
            Right = right;
        }
    }

    class Tree<T>
    {
        Node<T> root;
        public Tree() { }
        public Tree(Node<T> head)
        {
            root = head;
        }
        public IEnumerable<T> Preorder
        {
            get { return ScanPreorder(root); }
        }

        // Enumerator with Filter   
        public IEnumerable<T> Where(Func<T, bool> filter)
        {
            foreach (T p in ScanPreorder(root))
            {
                if (filter(p) == true)
                {
                    yield return p;
                }
            }
        }

        // Enumerator with T as Person
        private IEnumerable<T> ScanPreorder(Node<T> root)
        {
            //uses recursion to traverse the tree as follows: 
            //  visit a tree 
            //      visit the root 
            //      visit the left tree 
            //      visit the right tree
            yield return root.Data;
            if (root.Left != null)
            {
                foreach (T p in ScanPreorder(root.Left))
                {
                    yield return p;
                }
            }

            if (root.Right != null)
            {
                foreach (T p in ScanPreorder(root.Right))
                {
                    yield return p;
                }
            }
        }
    }

    class Person
    {
        public Person(string name, int birth)
        {
            this.Name = name;
            this.Birth = birth;
        }

        public string Name { get; set; }
        public int Birth { get; set; }

        public override string ToString()
        {
            return $"{this.Name}, {this.Birth}";
        }
    }

    class Program
    {
        static void Main()
        {
            var family = new Tree<Person>(
                new Node<Person>(
                    new Person("Tom", 1950),
                    new Node<Person>(
                        new Person("Peter", 1976),
                        new Node<Person>(
                            new Person("Sarah", 2000),
                            null,
                            new Node<Person>(
                                new Person("James", 2002),
                                null,
                                null
                            ) // no more siblings James
                        ),
                        new Node<Person>(
                            new Person("Robert", 1978),
                            null,
                            new Node<Person>(
                                new Person("Mark", 1982),
                                new Node<Person>(
                                    new Person("Carrie", 2005),
                                    null,
                                    null
                                ),
                                null
                            ) // no more siblings Mark
                        )
                    ),
                    null
                ) // no siblings Tom
            );            

            var selection = from p in family 
                            where p.Birth > 1980 
                            orderby p.Name 
                            select p;

            foreach (Person p in family.Preorder)
            {
                Console.Write(p + "  ");
                Console.WriteLine("\n");
            }
        }
    }
}
