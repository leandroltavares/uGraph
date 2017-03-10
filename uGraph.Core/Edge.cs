using System;

namespace uGraph.Core
{
    public class Edge<TVertex, TEdge>
    {
		public Guid Id { get; private set; }

        public TEdge Info { get; set; }

        public Vertex<TVertex, TEdge> Origin { get; internal set; }

        public Vertex<TVertex, TEdge> Destination { get; internal set; }

        internal Edge(TEdge info)
        {
            Id = Guid.NewGuid();
            Info = info;
        }
    }
}
