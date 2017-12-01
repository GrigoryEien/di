using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Drawing;


namespace TagsCloudVisualization {
    [TestFixture]
    public class CircularCloudLayouter_should {
        private CircularCloudLayouter layouter;
        [SetUp]
        public void SetUp() {
            layouter = new CircularCloudLayouter(new Point(20, 20));
        }

        [Test]
        public void CreateNewLayouter_WhenPointIsPassed() {
            layouter = new CircularCloudLayouter(new Point(20, 20));
        }
        [Test]
        public void ReturnRectangle_WhenPassedSize() {
            var size = new Size(20,20);

            var supposedlyRect = layouter.PutNextRectangle(size);
            
            Assert.IsInstanceOf<Rectangle>(supposedlyRect);
        }

        [Test]
        public void AvoidRectangleIntersection_WhenPuttingSecondRectangle()
        {
            var size = new Size(20,20);
            var firstRect = layouter.PutNextRectangle(size);
            var secondRect = layouter.PutNextRectangle(size);

            Assert.False(firstRect.IntersectsWith(secondRect));
        }

	    [Test]
	    public void AvoidRectangleIntersection_OnHundredRandomRectangles()
	    {
		    var rnd = new Random();
		    Size size;
		    var rects = new List<Rectangle>();
			for (var i = 0; i < 100; i++)
		    {
			    size = new Size(rnd.Next(1,100),rnd.Next(1,100));
			    rects.Add(layouter.PutNextRectangle(size));
		    }

			for (var i=0; i <rects.Count-1; i++)
				for (var j=i+1; j< rects.Count;j++)
					Assert.False(rects[i].IntersectsWith(rects[j]));
	    }

	}
}