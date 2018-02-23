using System;
using Xunit;

namespace uGraph.Test
{
    public class BasicGraphTest
    {
        [Fact]
        public void WhenNewGraphIsCreatedThenVertexCountShouldBeZero()
        {
            Graph<object, object> graph = new Graph<object, object>();
            Assert.Equal(0, graph.VertexCount);
        }

        [Fact]
        public void WhenOneVertexWasAddedThenVertexCountShouldBeOne()
        {
            Graph<object, object> graph = new Graph<object, object>();

            graph.AddVertex(new object());

            Assert.Equal(1, graph.VertexCount);
        }

        [Fact]
        public void WhenTwoVertexAndOneEdgeWereAddedThenVertexCountShouldBeOne()
        {
            Graph<object, object> graph = new Graph<object, object>();

            var v1 = graph.AddVertex(new object());
            var v2 = graph.AddVertex(new object());

            graph.AddEdge(v1, v2, new object());

            Assert.Equal(2, graph.VertexCount);
            Assert.Equal(1, graph.EdgeCount);
        }
    }
}
