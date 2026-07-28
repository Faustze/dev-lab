Point pt = new Point();
pt.X = 1;
Box bx = new Box();
bx.X = 1;

// We can set to [] field because array element access returns a variable, not a copy.
Point[] onePointList = new Point[1];
onePointList[0] = pt;
onePointList[0].X = 5;
Console.WriteLine(onePointList[0].X);

// [CS1612] Here we don't change a return value, we walk through it to the object.
List<Point> lpt = new List<Point>();
lpt.Add(pt);
var p = lpt[0];
p.X = 5;
lpt[0] = p; // so we must write the modified copy back explicitly
Console.WriteLine(lpt[0].X);

// We return address here by lbx[0].X and we don't change it, just walk to it and change the object.
List<Box> lbx = new List<Box>();
lbx.Add(bx);
Console.WriteLine(lbx[0].X = 5);

// pt.X stays unchanged because Point is a struct passed by value - the method mutates a local copy
MutatePoint(pt, 2);
Console.WriteLine($"pt.X After MutatePoint: {pt.X}");

// pt.X stays unchanged because ReplacePoint reassigns its local parameter, which doesn't affect the caller's variable
ReplacePoint(pt, 3);
Console.WriteLine($"pt.X After ReplacePoint: {pt.X}");

// ref is not required here
MutateBox(ref bx, 2);
Console.WriteLine($"bx.X After MutateBox: {bx.X}");

// bx changes to point to the new object because ref makes b an alias for the caller's variable, so reassigning b reassigns bx too
ReplaceBox(ref bx, 3);
Console.WriteLine($"bx.X After ReplaceBox: {bx.X}");

void MutatePoint(Point p, int Value)
{
    p.X = Value;
}

void ReplacePoint(Point p, int Value)
{
    p = new Point();
    p.X = Value;
}

void MutateBox(ref Box b, int Value)
{
    b.X = Value;
}

void ReplaceBox(ref Box b, int Value)
{
    b = new Box();
    b.X = Value;
}

public struct Point
{
    public int X;
    public int Y;
}

class Box
{
    public int X;
    public int Y;
}
