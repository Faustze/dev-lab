Console.WriteLine("старт");
long v = 0;
long first = GC.GetAllocatedBytesForCurrentThread();
for (int i = 0; i < 1_000_000; i++)
    checked
    {
        v += i;
    }
long second = GC.GetAllocatedBytesForCurrentThread();
Console.WriteLine($"int {second - first}");
Console.WriteLine($"v={v}");

long v2 = 0;
object o = v2;
long firstObj = GC.GetAllocatedBytesForCurrentThread();
for (int i = 0; i < 1_000_000; i++)
    checked
    {
        v2 += i;
        o = v2;
    }
long secondObj = GC.GetAllocatedBytesForCurrentThread();
Console.WriteLine($"object int {secondObj - firstObj}");
Console.WriteLine($"v2={v2}");
Console.WriteLine($"o={o}");
