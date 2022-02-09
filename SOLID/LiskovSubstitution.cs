using System;
using System.Threading.Tasks;
using Shared;

namespace SOLID
{
    public class LiskovSubstitution : ExampleBase
    {
        public LiskovSubstitution()
        {
            SectionName = "Liskov Substitution Principle";
        }

        public static int Area(Rectangle1 r) => r.Width * r.Height;
        public static int Area(Rectangle2 r) => r.Width * r.Height;

        protected override void ExecuteCode()
        {
            //Building some rectangles
            var rc = new Rectangle1(2,3);
            Console.WriteLine($"{rc} has an area of {Area(rc)}");

            //How about a square based on the rectangle implementation?
            var sq = new Square1();
            sq.Width = 4;
            Console.WriteLine($"{sq} has an area of {Area(sq)}");

            //But wait, we unbox it, the width is only being set.
            Rectangle1 sqrec = new Square1();
            sqrec.Width = 4;
            Console.WriteLine($"{sqrec} has an area of {Area(sqrec)}");
            Console.WriteLine($"{nameof(sqrec)} looks a bit odd?");

            //This principle dictates that even if you act on a super/base class, the operation and bahaviour should be the same.
            //To fix, is there an override? rather than a new in the properties
            Rectangle2 sqrec2 = new Square2();
            sqrec2.Width = 4;
            Console.WriteLine($"{sqrec2} has an area of {Area(sqrec2)}");
            Console.WriteLine($"{nameof(sqrec2)} looks better!");

        }
    }

    #region bad

    public class Rectangle1 
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Rectangle1()
        {
            
        }

        public Rectangle1(int width, int height)
        {
            Width =  width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    
    //Now we created based on Rectangle
    public class Square1 : Rectangle1 
    {
        public new int Width {
            set 
            {
                //Oh no
                base.Width = base.Height = value;
            }
        }

        public new int Height {
            set 
            {
                base.Height = base.Height = value;
            }
        }
    }

    #endregion

    #region good
    public class Rectangle2 
    {
        //Changed to virtual
        public virtual int Height { get; set; }
        public virtual int Width { get; set; }

        public Rectangle2()
        {
            
        }

        public Rectangle2(int width, int height)
        {
            Width =  width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    //Now we created based on Rectangle
    public class Square2 : Rectangle2 
    {
        //Changed to override
        public override int Width {
            set 
            {
                //Oh no
                base.Width = base.Height = value;
            }
        }

        public override int Height {
            set 
            {
                base.Height = base.Height = value;
            }
        }
    }
    #endregion
}