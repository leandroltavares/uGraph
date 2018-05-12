using System.Collections.Generic;
using Xunit;
using System.Linq;
using uGraph.Test.Util;

namespace uGraph.Test
{
    public class EqualityTest
    {
        [Fact]
        public void WhenNullGraphThenEqualsShouldReturnFalse()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();

            Assert.False(firstGraph.Equals(null));
        }

        [Fact]
        public void WhenSameInstanceGraphThenEqualsShouldReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);

            Assert.True(firstGraph.Equals(firstGraph));
        }

        [Fact]
        public void WhenBothGraphsAreEmptyThenEqualsShouldReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            Assert.True(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphWithDifferentVertexCountThenEqualsShouldReturnFalse()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);

            Assert.False(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphWithVertexWithDifferentEdgeCountThenEqualsShouldReturnFalse()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);
            secondGraph.AddEdge(42, 21, 0);

            Assert.False(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphWithSameVertexCountAndSameEdgeCountAndDifferentInfoThenEqualsShouldReturnFalse()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddVertex(100);
            firstGraph.AddVertex(200);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);
            secondGraph.AddVertex(100);
            secondGraph.AddVertex(200);

            firstGraph.AddEdge(42, 21, 0);
            firstGraph.AddEdge(42, 21, -1);
            firstGraph.AddEdge(42, 100, 0);
            firstGraph.AddEdge(42, 200, 0);
            firstGraph.AddEdge(42, 42, 0);

            firstGraph.AddEdge(21, 100, int.MaxValue);
            firstGraph.AddEdge(42, 200, int.MaxValue);


            secondGraph.AddEdge(42, 21, 0);
            secondGraph.AddEdge(42, 21, 0); //Different info from first Graph
            secondGraph.AddEdge(42, 100, 0);
            secondGraph.AddEdge(42, 200, 0);
            secondGraph.AddEdge(42, 42, 0);

            firstGraph.AddEdge(21, 100, int.MaxValue);
            firstGraph.AddEdge(42, 200, int.MaxValue);


            Assert.False(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphWithSameVertexCountAndSameEdgeCountAndEqualInfoAddedOnSameOrdersThenEqualsShouldReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddVertex(100);
            firstGraph.AddVertex(200);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);
            secondGraph.AddVertex(100);
            secondGraph.AddVertex(200);

            firstGraph.AddEdge(42, 21, 0);
            firstGraph.AddEdge(42, 21, -1);
            firstGraph.AddEdge(42, 100, 0);
            firstGraph.AddEdge(42, 200, 0);
            firstGraph.AddEdge(42, 42, 0);

            firstGraph.AddEdge(21, 100, int.MaxValue);
            firstGraph.AddEdge(42, 200, int.MaxValue);


            secondGraph.AddEdge(42, 21, 0);
            secondGraph.AddEdge(42, 21, -1);
            secondGraph.AddEdge(42, 100, 0);
            secondGraph.AddEdge(42, 200, 0);
            secondGraph.AddEdge(42, 42, 0);

            secondGraph.AddEdge(21, 100, int.MaxValue);
            secondGraph.AddEdge(42, 200, int.MaxValue);


            Assert.True(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphWithSameVertexCountAndSameEdgeCountAndEqualInfoAddedOnDifferentOrdersThenEqualsShouldReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddVertex(100);
            firstGraph.AddVertex(200);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);
            secondGraph.AddVertex(100);
            secondGraph.AddVertex(200);

            firstGraph.AddEdge(42, 21, 0);
            firstGraph.AddEdge(42, 21, -1);

            firstGraph.AddEdge(42, 100, 0);
            firstGraph.AddEdge(42, 200, 0);
            firstGraph.AddEdge(42, 42, 0);

            firstGraph.AddEdge(21, 100, int.MaxValue);
            firstGraph.AddEdge(42, 200, int.MaxValue);

            secondGraph.AddEdge(42, 21, -1); //Inverted order
            secondGraph.AddEdge(42, 21, 0);

            secondGraph.AddEdge(42, 100, 0);
            secondGraph.AddEdge(42, 200, 0);
            secondGraph.AddEdge(42, 42, 0);

            secondGraph.AddEdge(42, 200, int.MaxValue); //Inverted order
            secondGraph.AddEdge(21, 100, int.MaxValue);

            Assert.True(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphWithSameVertexCountAndSameEdgeCountButDifferentOriginsThenEqualsShouldReturnFalse()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddVertex(100);
            firstGraph.AddVertex(200);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);
            secondGraph.AddVertex(100);
            secondGraph.AddVertex(200);

            firstGraph.AddEdge(42, 21, 0);
            firstGraph.AddEdge(42, 21, -1);
            firstGraph.AddEdge(42, 100, 0);
            firstGraph.AddEdge(42, 200, 0);
            firstGraph.AddEdge(42, 42, 0);

            firstGraph.AddEdge(42, 21, 0);

            secondGraph.AddEdge(42, 21, 0);
            secondGraph.AddEdge(42, 21, -1);
            secondGraph.AddEdge(42, 100, 0);
            secondGraph.AddEdge(42, 200, 0);
            secondGraph.AddEdge(42, 42, 0);

            secondGraph.AddEdge(21, 42, 0); //Differente from first graph

            Assert.False(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenBothGraphsHaveSameDisconnectedComponentsThenEqualsShouldReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);

            Assert.True(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphsHaveDifferentDisconnectedComponentsButSameVertexCountThenEqualsShouldReturnFalse()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(100);
            firstGraph.AddVertex(200);

            secondGraph.AddVertex(-100);
            secondGraph.AddVertex(-200);

            Assert.False(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphsHaveSameVertexButEdgeIsReversedThenEqualsShouldReturnFalse()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddEdge(21, 42, -1);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);
            secondGraph.AddEdge(42, 21, -1);

            Assert.False(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenFirstGraphIsASubSetOfSecondGraphThenEqualsShouldReturnFalse()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddEdge(21, 42, -1);
            firstGraph.AddEdge(42, 21, -1);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);
            secondGraph.AddEdge(42, 21, -1);

            Assert.False(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenSecondGraphIsASubSetOfFirstGraphThenEqualsShouldReturnFalse()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddEdge(21, 42, -1);
            firstGraph.AddEdge(42, 21, -1);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);
            secondGraph.AddEdge(42, 21, -1);

            Assert.False(secondGraph.Equals(firstGraph));
        }

        [Fact]
        public void WhenGraphsAreEqualAndVertexAndEdgeAdditionOrderAreEqualThenEqualsShouldReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);

            firstGraph.AddEdge(21, 42, -1);
            firstGraph.AddEdge(42, 21, -1);

            secondGraph.AddVertex(42);
            secondGraph.AddVertex(21);

            secondGraph.AddEdge(21, 42, -1);
            secondGraph.AddEdge(42, 21, -1);

            Assert.True(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphsAreEqualRegardlessAdditionOrderThenEqualsShouldReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddEdge(21, 42, -1);
            firstGraph.AddEdge(42, 21, -1);

            secondGraph.AddVertex(21);
            secondGraph.AddVertex(42);
            secondGraph.AddEdge(42, 21, -1); //Added int different order
            secondGraph.AddEdge(21, 42, -1);

            Assert.True(firstGraph.Equals(secondGraph));
        }


        [Fact]
        public void WhenGraphsAreEqualAndContainsEqualDisconnectComponentsRegardlessAdditionOrderThenEqualsShouldReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddVertex(100);
            firstGraph.AddVertex(200);

            firstGraph.AddEdge(21, 42, -1);
            firstGraph.AddEdge(42, 21, -1);

            secondGraph.AddVertex(21);
            secondGraph.AddVertex(42);
            secondGraph.AddVertex(100);
            secondGraph.AddVertex(200);

            secondGraph.AddEdge(42, 21, -1); //Added in different order
            secondGraph.AddEdge(21, 42, -1);

            Assert.True(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphsAreEqualAndContainsDifferentDisconnectComponentsRegardlessAdditionOrderThenEqualsShouldReturnFalse()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(-10);
            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);

            firstGraph.AddEdge(21, 42, -1);
            firstGraph.AddEdge(42, 21, -1);

            secondGraph.AddVertex(21);
            secondGraph.AddVertex(42);
            secondGraph.AddVertex(30);

            secondGraph.AddEdge(42, 21, -1);
            secondGraph.AddEdge(21, 42, -1);

            Assert.False(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphsAreEqualsComplexScenarioThenShouldEqualsReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddVertex(100);
            firstGraph.AddVertex(200);
            firstGraph.AddVertex(int.MaxValue);
            firstGraph.AddVertex(int.MaxValue);

            firstGraph.AddEdge(21, 42, -1);
            firstGraph.AddEdge(42, 21, -1);
            firstGraph.AddEdge(21, 21, 0);
            firstGraph.AddEdge(42, 42, 0);
            firstGraph.AddEdge(int.MaxValue, int.MaxValue, int.MinValue);


            secondGraph.AddVertex(21);
            secondGraph.AddVertex(42);
            secondGraph.AddVertex(100);
            secondGraph.AddVertex(200);
            secondGraph.AddVertex(int.MaxValue);
            secondGraph.AddVertex(int.MaxValue);

            secondGraph.AddEdge(42, 21, -1); //Added in different order
            secondGraph.AddEdge(21, 42, -1);
            secondGraph.AddEdge(42, 42, 0);
            secondGraph.AddEdge(21, 21, 0);
            secondGraph.AddEdge(int.MaxValue, int.MaxValue, int.MinValue);


            Assert.True(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphsAreEqualsComplexScenario2ThenShouldEqualsReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            firstGraph.AddVertex(42);
            firstGraph.AddVertex(21);
            firstGraph.AddVertex(100);
            firstGraph.AddVertex(200);
            firstGraph.AddVertex(int.MaxValue);
            firstGraph.AddVertex(int.MaxValue);

            firstGraph.AddEdge(21, 42, -1);
            firstGraph.AddEdge(42, 21, -1);
            firstGraph.AddEdge(21, 21, 0);
            firstGraph.AddEdge(42, 42, 0);
            firstGraph.AddEdge(int.MaxValue, int.MaxValue, int.MinValue);
            firstGraph.AddEdge(int.MaxValue, int.MaxValue, int.MinValue);


            secondGraph.AddVertex(21);
            secondGraph.AddVertex(42);
            secondGraph.AddVertex(100);
            secondGraph.AddVertex(200);
            secondGraph.AddVertex(int.MaxValue);
            secondGraph.AddVertex(int.MaxValue);

            secondGraph.AddEdge(42, 21, -1); //Added in different order
            secondGraph.AddEdge(21, 42, -1);
            secondGraph.AddEdge(42, 42, 0);
            secondGraph.AddEdge(21, 21, 0);
            secondGraph.AddEdge(int.MaxValue, int.MaxValue, int.MinValue);
            secondGraph.AddEdge(int.MaxValue, int.MaxValue, int.MinValue);


            Assert.True(firstGraph.Equals(secondGraph));
        }

        [Fact]
        public void WhenGraphsAreEqualsComplexScenario3ThenShouldEqualsReturnTrue()
        {
            Graph<int, int> firstGraph = new Graph<int, int>();
            Graph<int, int> secondGraph = new Graph<int, int>();

            for (int i = 0; i < 10; i++)
            {
                firstGraph.AddVertex(i);
                secondGraph.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                firstGraph.AddEdge(1, i, int.MaxValue);
                firstGraph.AddEdge(2, i, int.MaxValue);
                firstGraph.AddEdge(3, i, int.MaxValue);
                firstGraph.AddEdge(4, i, int.MaxValue);
                firstGraph.AddEdge(5, i, int.MaxValue);


                secondGraph.AddEdge(2, i, int.MaxValue);
                secondGraph.AddEdge(3, i, int.MaxValue);
                secondGraph.AddEdge(5, i, int.MaxValue);
                secondGraph.AddEdge(1, i, int.MaxValue);
                secondGraph.AddEdge(4, i, int.MaxValue);
            }


            Assert.True(firstGraph.Equals(secondGraph));
        }

        //    [Fact]
        //    public void WhenGraphsAreEqualsComplexAndLargeScenarioThenShouldEqualsReturnTrue()
        //    {
        //        Graph<int, int> firstGraph = new Graph<int, int>();
        //        Graph<int, int> secondGraph = new Graph<int, int>();

        //        var verticesInfo = Enumerable.Range(0, 100).ToList();

        //        for (int i = 0; i < verticesInfo.Count; i++)
        //        {
        //            firstGraph.AddVertex(verticesInfo[i]);
        //        }

        //        RandomUtil.Shuffle<int>(verticesInfo);

        //        for (int i = 0; i < verticesInfo.Count; i++)
        //        {
        //            secondGraph.AddVertex(verticesInfo[i]);
        //        }

        //        var edges = RandomUtil.RandomTrio(0, 100, 100);

        //        for (int i = 0; i < edges.Count; i++)
        //        {
        //            firstGraph.AddEdge(edges[i].Item1, edges[i].Item2, edges[i].Item3);
        //        }

        //        edges.Shuffle();

        //        for (int i = 0; i < edges.Count; i++)
        //        {
        //            secondGraph.AddEdge(edges[i].Item1, edges[i].Item2, edges[i].Item3);
        //        }

        //        Assert.True(firstGraph.Equals(secondGraph));
        //    }
    }
}
