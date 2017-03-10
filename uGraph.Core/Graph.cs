using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace uGraph.Core
{
    public class Graph<TVertex, TEdge> : IEnumerable<Vertex<TVertex, TEdge>>
    {
        private List<Vertex<TVertex, TEdge>> vertices;

        private HashSet<Guid> verticesSet;

        public Graph()
        {
            vertices = new List<Vertex<TVertex, TEdge>>();
        }

        public void AddVertex(Vertex<TVertex, TEdge> vertex)
        {
            vertices.Add(vertex);
        }

        public Vertex<TVertex, TEdge> AddVertex(TVertex info)
        {
            var vertex = new Vertex<TVertex, TEdge> { Info = info };

            vertices.Add(vertex);

            return vertex;
        }

        public Edge<TVertex, TEdge> AddEdge(Vertex<TVertex, TEdge> origin, Vertex<TVertex, TEdge> destination, TEdge info)
        {
            var edge = new Edge<TVertex, TEdge>(info);

            edge.Origin = origin;
            edge.Destination = destination;

            origin.Edges.Add(edge);

            return edge;
        }

        public bool Contains<T>(T value) where T : TVertex, IEquatable<TVertex>
        {
            return vertices.Any(v => v.Info.Equals(value));
        }

        public bool Contains(Vertex<TVertex, TEdge> vertex)
        {
            return vertices.Any(v => v.Equals(vertex));
        }

        public IEnumerator<Vertex<TVertex, TEdge>> GetEnumerator()
        {
            return vertices.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
			return vertices.GetEnumerator();
        }
    }
}
