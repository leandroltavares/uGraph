using System.Collections.Generic;
using System.Linq;
using System;

namespace uGraph
{
    public class PriorityQueue<TKey, TValue> //where TKey : IComparer<TKey>
    {
        protected SortedDictionary<TKey, Queue<TValue>> queue;

        public IComparer<TKey> Comparer { get; protected set; }

        public int Count { get; protected set; }

        public PriorityQueue(IComparer<TKey> comparer)
        {
            this.Comparer = comparer;
            queue = new SortedDictionary<TKey, Queue<TValue>>(comparer);
            Count = 0;
        }

        public PriorityQueue()
            :this(Comparer<TKey>.Default)
        {
      
        }

        public void Enqueue(TKey key, TValue value)
        {
            if(!queue.ContainsKey(key))
            {
                queue.Add(key, new Queue<TValue>());
            }

            queue[key].Enqueue(value);
            Count++;
        }

        public TValue Dequeue()
        {
            if (Count == 0)
                throw new Exception("Queue does not contain any element.");

            var first = queue.First();

            var currentQueue = queue[first.Key];

            var value = currentQueue.Dequeue();

            if(currentQueue.Count == 0)
            {
                queue.Remove(first.Key);
            }

            Count--;
            return value;

        }

    }
}