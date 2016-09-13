using System;
using NUnit.Framework;
using UnityEngine;
using UnityFunctions;

namespace UnityFunctionsTests
{
    [TestFixture]
    public class IntersectionTests
    {
        [Test]
        public void Can_find_intersection_Between_2D_Triangle_And_Line_Segment()
        {
            var t1in2d = new Vector2(0,0);
            var t2in2d = new Vector2(0.241f,0.06999999f);
            var t3in2d = new Vector2(-0.713f,0.859f);
            var w1in2d = new Vector2(-0.013f,0.359f);
            var w2in2d = new Vector2(-0.413f,0.359f);
//Console.WriteLine(t1in2d.x+","+t1in2d.y+" "+t2in2d.x+","+t2in2d.y+" "+t3in2d.x+","+t3in2d.y+" "+w1in2d.x+","+w1in2d.y+" "+w2in2d.x+","+w2in2d.y+" ");

            Vector2 int2d;
            var r = 
                fun.intersection.Between2DTriangleAndLineSegment(
                    ref t1in2d, ref t2in2d, ref t3in2d,
                    ref w1in2d, ref w2in2d, out int2d);

            Assert.That(r,Is.True);
        }

        [Test]
        public void Can_find_intersection_Between_2D_Lines()
        {
            var t2 = new Vector2(0.241f,0.06999999f);
            var t3 = new Vector2(-0.713f,0.859f);
            var line1 = new Vector2(-0.013f,0.359f);
            var line2 = new Vector2(-0.413f,0.359f);
            Vector2 curr;
            var r =
                fun.intersection.Between2DLines(ref t2, ref t3, ref line1, ref line2, out curr);
            Assert.That(r,Is.True);
        }
    }
}