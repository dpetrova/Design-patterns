﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_builder
{
    class Product
    {
        List<string> parts = new List<string>();

        public void Add(string part)
        {
            parts.Add(part);
        }

        public void Display()
        {
            Console.WriteLine("\nProduct Parts -------");

            foreach (string part in parts)
            {
                Console.Write(part);
            }

            Console.WriteLine();
        }
    }


    class Director
    {
        // Build a Product from several parts 
        public void Construct(IBuilder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
            builder.BuildPartB();
        }
    }

    interface IBuilder
    {
        void BuildPartA();
        void BuildPartB();
        Product GetResult();
    }


    class Builder1 : IBuilder
    {
        private Product product = new Product();

        public void BuildPartA()
        {
            product.Add("PartA ");
        }

        public void BuildPartB()
        {
            product.Add("PartB ");
        }

        public Product GetResult()
        {
            return product;
        }
    }


    class Builder2 : IBuilder
    {
        private Product product = new Product();

        public void BuildPartA()
        {
            product.Add("PartX ");
        }

        public void BuildPartB()
        {
            product.Add("PartY ");
        }

        public Product GetResult()
        {
            return product;
        }
    }    


    public class Client
    {
        public static void Main()
        {
            // Create one director and two builders
            Director director = new Director();
            IBuilder b1 = new Builder1();
            IBuilder b2 = new Builder2();

            // Construct two products
            director.Construct(b1);
            Product p1 = b1.GetResult();
            p1.Display();

            director.Construct(b2);
            Product p2 = b2.GetResult();
            p2.Display();
        }
    }
}
