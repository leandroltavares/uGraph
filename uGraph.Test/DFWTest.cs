using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace uGraph.Test
{
    
    public class DFTTest
    {
        [Fact]
        public void DFTWithNullInitialVertexArgumentShouldThrowArgumentNullException()
        {
            Graph<object, object> graph = new Graph<object, object>();
            graph.AddVertex(new object());

            var firstVertex = graph.Vertices[0];

            Assert.Throws<ArgumentNullException>(() => graph.DFT(firstVertex, null));
        }

        [Fact]
        public void DFTWithNullActionArgumentShouldThrowArgumentNullException()
        {
            Graph<object, object> graph = new Graph<object, object>();
            graph.AddVertex(new object());

            var firstVertex = graph.Vertices[0];

            Assert.Throws<ArgumentNullException>(() => graph.DFT(firstVertex, null));
        }

        //[Fact]
        //public void DFTSampleTest1()
        //{
        //    Graph<int, object> graph = new Graph<int, object>();

        //    for (int i = 0; i < 4; i++)
        //    {
        //        graph.AddVertex(i);
        //    }

        //    graph.AddEdge(0, 1, null);
        //    graph.AddEdge(0, 2, null);
        //    graph.AddEdge(1, 2, null);
        //    graph.AddEdge(2, 0, null);
        //    graph.AddEdge(2, 3, null);
        //    graph.AddEdge(3, 3, null);

        //    var firstVertex = graph.FirstOrDefault(v => v == 1);

        //    string output = string.Empty;

        //    graph.DFT(firstVertex, (v) => output += v.Info.ToString());

        //    Assert.Equal("2013", output);
        //}
    }
}
