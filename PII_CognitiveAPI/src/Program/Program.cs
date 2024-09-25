using System;

namespace Ucu.Poo.Cognitive;

class Program
{
    static void Main(string[] args)
    {
        CognitiveFace cog = new CognitiveFace(false);
        cog.Recognize(@"jane.jpg");
        FoundFace(cog);
        cog.Recognize(@"bill.jpg");
        FoundFace(cog);
        cog.Recognize(@"yacht.jpg");
        FoundFace(cog);

    }

    static void FoundFace(CognitiveFace cog)
    {
        if (cog.FaceFound)
        {
            Console.WriteLine("Face Found!");
            if (cog.GlassesFound)
            {
                Console.WriteLine("Has glasses 🤓");
            }
            else
            {
                Console.WriteLine("No glasses");
            }
        }
        else
            Console.WriteLine("No Face Found");
    }
}