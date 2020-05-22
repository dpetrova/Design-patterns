using System;
using System.Reflection;

namespace _17_visitor
{
    class Element
    {
        public Element Next { get; set; }
        public Element Link { get; set; }
        public Element() { }
        public Element(Element next)
        {
            this.Next = next;
        }
    }

    class ElementWithLink : Element
    {
        public ElementWithLink(Element link, Element next)
        {
            Next = next;
            Link = link;
        }
    }

    // abstract Visitor
    abstract class IVisitor
    {
        public void ReflectiveVisit(Element element)
        {
            // Use reflection to find and invoke the correct Visit method   
            Type[] types = new Type[] { element.GetType() };
            MethodInfo methodInfo = this.GetType().GetMethod("Visit", types);
            if (methodInfo != null)
            {
                methodInfo.Invoke(this, new object[] { element });
            }
            else
            {
                Console.WriteLine("Unexpected Visit");
            }
        }
    }

    // Visitor
    class CountVisitor : IVisitor
    {
        public int Count { get; set; }
        public void CountElements(Element element)
        {
            ReflectiveVisit(element);
            if (element.Link != null) CountElements(element.Link);
            if (element.Next != null) CountElements(element.Next);
        }

        //Elements with links are not counted
        public void Visit(ElementWithLink element)
        {
            Console.WriteLine("Not counting");
        }

        // Only Elements are counted 
        public void Visit(Element element)
        {
            Count++;
        }
    }

    // Client
    class Client
    {
        static void Main()
        {
            // Set up the object structure 
            Element objectStructure =
                new Element(
                    new Element(
                        new ElementWithLink(
                            new Element(
                                new Element(
                                    new ElementWithLink(
                                        new Element(null),
                                        new Element(null)))),
                            new Element(
                                new Element(
                                    new Element(null))))));
            Console.WriteLine("Count the Elements");
            CountVisitor visitor = new CountVisitor();
            visitor.CountElements(objectStructure);
            Console.WriteLine("Number of Elements is: " + visitor.Count);
        }
    }
}




