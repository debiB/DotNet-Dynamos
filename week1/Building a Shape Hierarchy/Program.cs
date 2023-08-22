using System;

public class Shape
{
    private string name;
    protected double x;
    protected double y;

    public Shape(string Name, double X, double Y)
    {
        name = Name;
        x = X;
        y = Y;
    }

    public virtual double CalculateArea()
    {
        return x * y;
    }

    public string GetName()
    {
        return name;
    }
}

public class Circle : Shape
{
    public double radius;

    public Circle(string name, double rad) : base(name, rad, 0)
    {
        radius = rad;
    }

    public override double CalculateArea()
    {
        return Math.PI * radius * radius;
    }
}

public class Rectangle : Shape
{
    public double width;
    public double height;

    public Rectangle(string name, double w, double h) : base(name, w, h)
    {
        width = w;
        height = h;
    }

    public override double CalculateArea()
    {
        return width * height;
    }
}

public class Triangle : Shape
{
    public double baseLength;
    public double triangleHeight;

    public Triangle(string name, double b, double h) : base(name, b, h)
    {
        baseLength = b;
        triangleHeight = h;
    }

    public override double CalculateArea()
    {
        return 0.5 * baseLength * triangleHeight;
    }
}

class Program
{
    static void Main()
    {
        Circle c = new Circle("Circle", 5);
        Rectangle r = new Rectangle("Rectangle", 3, 2);
        Triangle t = new Triangle("Triangle", 5, 5);

        PrintShapeArea(c);
        PrintShapeArea(r);
        PrintShapeArea(t);
    }

    public static void PrintShapeArea(Shape s)
    {
        Console.WriteLine($"{s.GetName()} - {s.CalculateArea()}");
    }
}
