using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace _02_decorator_example
{
    // The original Photo class
    public class Photo : Form
    {
        Image image;

        public Photo()
        {
            image = new Bitmap("jug.jpg");
            this.Text = "Lemonade";
            this.Paint += new PaintEventHandler(Drawer);
        }

        public virtual void Drawer(Object source, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, 30, 20);
        }
    }

    // This simple border decorator adds a colored border of fixed size
    class BorderedPhoto : Photo
    {
        Photo photo;
        Color color;

        public BorderedPhoto(Photo p, Color c)
        {
            photo = p;
            color = c;
        }

        public override void Drawer(Object source, PaintEventArgs e)
        {
            photo.Drawer(source, e);
            e.Graphics.DrawRectangle(new Pen(color, 10), 25, 15, 215, 225);
        }
    }

    class TaggedPhoto : Photo
    {
        Photo photo;
        string tag;
        int number;
        static int count;
        List<string> tags = new List<string>();

        public TaggedPhoto(Photo p, string t)
        {
            photo = p;
            tag = t;
            tags.Add(t);
            number = ++count;
        }

        public override void Drawer(Object source, PaintEventArgs e)
        {
            photo.Drawer(source, e);
            e.Graphics.DrawString(tag, new Font("Arial", 16), new SolidBrush(Color.Black), new PointF(80, 100 + number * 20));
        }

    }


    class Program
    {
        static void Main()
        {
            // Application.Run acts as a simple client                       

            // Compose a photo with one TaggedPhoto and a yellow BorderedPhoto
            Photo photo = new Photo();
            TaggedPhoto tag = new TaggedPhoto(photo, "Jug");
            BorderedPhoto composition = new BorderedPhoto(tag, Color.Yellow);
            Application.Run(composition);
        }
    }
}
