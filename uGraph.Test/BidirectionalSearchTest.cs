using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace uGraph.Test
{
    public class BidirectionalSearchTest
    {
        [Fact]
        public void BSWithNullInitialVertexArgumentShouldThrowArgumentNullException()
        {
            Graph<object, object> graph = new Graph<object, object>();
            graph.AddVertex(42);

            Assert.Throws<ArgumentNullException>(() => graph.BidirectionalSearch(null, null));
        }

        [Fact]
        public void BSWithNullTargetVertexArgumentShouldThrowArgumentNullException()
        {
            Graph<object, object> graph = new Graph<object, object>();
            graph.AddVertex(42);

            Assert.Throws<ArgumentNullException>(() => graph.BidirectionalSearch(graph.FirstOrDefault(x => true), null));
        }

        [Fact]
        public void BidirectionalSearchWithConnectedInitialAndTargetVertexShouldReturnTrue()
        {
            Graph<int, object> graph = new Graph<int, object>();

            for (int i = 0; i <= 14; i++)
                graph.AddVertex(i);

            graph.AddEdge(0, 4, null);
            graph.AddEdge(1, 4, null);
            graph.AddEdge(2, 5, null);
            graph.AddEdge(3, 5, null);
            graph.AddEdge(4, 6, null);
            graph.AddEdge(5, 6, null);
            graph.AddEdge(6, 7, null);
            graph.AddEdge(7, 8, null);
            graph.AddEdge(8, 9, null);
            graph.AddEdge(8, 10, null);
            graph.AddEdge(9, 11, null);
            graph.AddEdge(9, 12, null);
            graph.AddEdge(10, 13, null);
            graph.AddEdge(10, 14, null);

            var initialVertex = graph.FirstOrDefault(v => v == 0);
            var targetVertex = graph.FirstOrDefault(v => v == 14);

            var search = graph.BidirectionalSearch(initialVertex, targetVertex);

            Assert.True(search);
        }

        [Fact]
        public void BidirectionalSearchWithDisconnectedInitialAndTargetVertexShouldReturnFalse()
        {
            Graph<int, object> graph = new Graph<int, object>();

            for (int i = 0; i <= 14; i++)
                graph.AddVertex(i);

            graph.AddEdge(0, 4, null);
            graph.AddEdge(1, 4, null);
            graph.AddEdge(2, 5, null);
            graph.AddEdge(3, 5, null);
            graph.AddEdge(4, 6, null);
            graph.AddEdge(5, 6, null);
            graph.AddEdge(7, 8, null);
            graph.AddEdge(8, 9, null);
            graph.AddEdge(8, 10, null);
            graph.AddEdge(9, 11, null);
            graph.AddEdge(9, 12, null);
            graph.AddEdge(10, 13, null);
            graph.AddEdge(10, 14, null);

            var initialVertex = graph.FirstOrDefault(v => v == 0);
            var targetVertex = graph.FirstOrDefault(v => v == 14);

            var search = graph.BidirectionalSearch(initialVertex, targetVertex);

            Assert.False(search);
        }
    }
}
