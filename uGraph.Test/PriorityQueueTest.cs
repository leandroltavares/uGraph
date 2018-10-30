using System;
using Xunit;

namespace uGraph.Test
{
    public class PriorityQueueTest
    {
        [Fact]
        public void DequeueEmptyQueueShouldThrowException()
        {
            PriorityQueue<int, int> queue = new PriorityQueue<int, int>();

            Assert.Throws<Exception>(() => queue.Dequeue());
        }

        [Fact]
        public void BasicTest()
        {
            PriorityQueue<int, int> queue = new PriorityQueue<int, int>();

            queue.Enqueue(1, 42);

            Assert.Equal(1, queue.Count);

            Assert.Equal(42, queue.Dequeue());

            Assert.Equal(0, queue.Count);
        }

        [Fact]
        public void ComplexTest()
        {
            PriorityQueue<int, int> queue = new PriorityQueue<int, int>();

            queue.Enqueue(1, 84);
            queue.Enqueue(1, 42);
            queue.Enqueue(1, 21);

            queue.Enqueue(3, 100);
            queue.Enqueue(3, 200);

            queue.Enqueue(2, 1000);
            queue.Enqueue(2, 2000);

            Assert.Equal(7, queue.Count);

            Assert.Equal(84, queue.Dequeue());
            Assert.Equal(42, queue.Dequeue());
            Assert.Equal(21, queue.Dequeue());

            Assert.Equal(1000, queue.Dequeue());
            Assert.Equal(2000, queue.Dequeue());

            Assert.Equal(100, queue.Dequeue());
            Assert.Equal(200, queue.Dequeue());

            Assert.Equal(0, queue.Count);
        }
    }
}
