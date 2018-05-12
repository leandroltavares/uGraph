using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uGraph
{
    /// <summary>
    /// Generic Graph implementarion based on Adjacency list 
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public class Graph<TVertex, TEdge> : IEnumerable<Vertex<TVertex, TEdge>>
    {
        private List<Vertex<TVertex, TEdge>> vertices;

        private readonly HashSet<Guid> verticesSet;

        public IReadOnlyList<Vertex<TVertex, TEdge>> Vertices
        {
            get
            {
                return vertices.AsReadOnly();
            }
        }

        public int VertexCount { get { return verticesSet.Count; } }

        public int EdgeCount { get; set; }

        public Graph()
        {
            vertices = new List<Vertex<TVertex, TEdge>>();
            verticesSet = new HashSet<Guid>();
        }

        public void AddVertex(Vertex<TVertex, TEdge> vertex)
        {
            vertices.Add(vertex);
            verticesSet.Add(vertex.Id);
        }

        public Vertex<TVertex, TEdge> AddVertex(TVertex info)
        {
            var vertex = new Vertex<TVertex, TEdge> { Info = info };

            AddVertex(vertex);

            return vertex;
        }

        public Edge<TVertex, TEdge> AddEdge(Vertex<TVertex, TEdge> origin, Vertex<TVertex, TEdge> destination, TEdge info)
        {
            var edge = new Edge<TVertex, TEdge>(info);

            edge.Origin = origin;
            edge.Destination = destination;

            origin.Edges.Add(edge);

            EdgeCount++;

            return edge;
        }

        public Edge<TVertex, TEdge> AddEdge<T>(T origin, T destination, TEdge info) where T : TVertex, IEquatable<TVertex>
        {
            var originVertex = FirstOrDefault(v => ((IEquatable<T>)(v)).Equals(origin));

            if (originVertex == null)
                throw new ArgumentNullException(nameof(originVertex));

            var destinationVertex = FirstOrDefault(v => ((IEquatable<T>)(v)).Equals(destination));

            if (destinationVertex == null)
                throw new ArgumentNullException(nameof(destinationVertex));

            return AddEdge(originVertex, destinationVertex, info);
        }

        public Vertex<TVertex, TEdge> FirstOrDefault(Func<TVertex, bool> predicate)
        {
            return vertices.FirstOrDefault(v => predicate(v.Info));
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

        //#region Equality

        //public override int GetHashCode()
        //{
        //    unchecked
        //    {
        //        ulong hash = 2166136261;

        //        foreach(var vertex in vertices)
        //            hash = (hash * 16777619) ^ (ulong)vertices.GetHashCode();

        //        return (int)hash;
        //    }
        //}

        public bool Equals(Graph<TVertex, TEdge> other)
        {
            if (other == null)
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            if (this.VertexCount != other.VertexCount)
                return false;

            var thisOrderedVertices = this.vertices.GroupBy(v => v.Edges.Count());
            var otherOrderedVertices = other.vertices.GroupBy(v => v.Edges.Count());

            HashSet<Vertex<TVertex, TEdge>> otherGraphVisitedVertices = new HashSet<Vertex<TVertex, TEdge>>();

            foreach (var vertexGroupPair in thisOrderedVertices.Zip(otherOrderedVertices, (first, second) => (first, second)))
            {
                foreach (var originVertex in vertexGroupPair.first)
                {
                    var matchingDestinationVertex =
                        vertexGroupPair.second.FirstOrDefault(destinationVertex => destinationVertex.Info.Equals(originVertex.Info)
                                                              && !otherGraphVisitedVertices.Contains(destinationVertex)
                                                              && originVertex.Edges.Count() == destinationVertex.Edges.Count());

                    if (matchingDestinationVertex != null)
                    {

                        otherGraphVisitedVertices.Add(matchingDestinationVertex);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (other.vertices.Count != otherGraphVisitedVertices.Count)
                return false;

            return true;
        }

        //#endregion

        #region Private Methods

        private void ClearVisitedVertices()
        {
            foreach (var vertex in vertices)
            {
                vertex.Visited = false;
            }
        }

        #endregion

        #region Traversal

        /// <summary>
        /// Depth-first traversal
        /// <paramref name="initialVertex">Initial vertex of the traversal</paramref>
        /// <paramref name="action">Action to be executed foreach node</paramref>
        /// </summary>
        public void DepthFirstTraversal(Vertex<TVertex, TEdge> initialVertex, Action<Vertex<TVertex, TEdge>> action)
        {
            if (initialVertex == null)
                throw new ArgumentNullException(nameof(initialVertex));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            Stack<Vertex<TVertex, TEdge>> stack = new Stack<Vertex<TVertex, TEdge>>();

            ClearVisitedVertices();

            stack.Push(initialVertex);

            while (stack.Count > 0)
            {
                var currentVertex = stack.Pop();

                if (!currentVertex.Visited)
                {
                    currentVertex.Visited = true;

                    action(currentVertex);

                    foreach (var edge in currentVertex.Edges)
                    {
                        stack.Push(edge.Destination);
                    }
                }
            }
        }

        /// <summary>
        /// Breadth-first traversal
        /// <paramref name="initialVertex">Initial vertex of the traversal</paramref>
        /// <paramref name="action">Action to be executed foreach node</paramref>
        /// </summary>
        public void BreadthFirstTraversal(Vertex<TVertex, TEdge> initialVertex, Action<Vertex<TVertex, TEdge>> action)
        {
            if (initialVertex == null)
                throw new ArgumentNullException(nameof(initialVertex));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            Queue<Vertex<TVertex, TEdge>> queue = new Queue<Vertex<TVertex, TEdge>>();

            ClearVisitedVertices();

            queue.Enqueue(initialVertex);

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();

                if (!currentVertex.Visited)
                {
                    currentVertex.Visited = true;

                    action(currentVertex);

                    foreach (var edge in currentVertex.Edges)
                    {
                        queue.Enqueue(edge.Destination);
                    }
                }
            }
        }

        #endregion
    }
}
