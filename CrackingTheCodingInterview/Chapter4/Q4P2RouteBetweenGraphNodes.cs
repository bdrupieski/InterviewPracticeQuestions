using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter4
{
    /// <summary>
    /// Given a directed graph, design an algorithm to find out whether there is a route
    /// between two nodes.
    /// </summary>
    public class Q4P2RouteBetweenGraphNodes
    {
        public static bool RouteExists(GraphNode start, GraphNode end)
        {
            var visitedNodes = new HashSet<GraphNode>();
            var nodesToVisit = new Queue<GraphNode>();
            nodesToVisit.Enqueue(start);

            while (!nodesToVisit.IsEmpty())
            {
                var currentNode = nodesToVisit.Dequeue();

                if (currentNode == end)
                {
                    return true;
                }

                visitedNodes.Add(currentNode);

                foreach (var node in currentNode.Nodes)
                {
                    nodesToVisit.Enqueue(node);
                }
            }

            return false;
        }

        public static string DotRepresentation(params GraphNode[] nodes)
        {
            var sb = new StringBuilder();

            sb.AppendLine("digraph unix {");
            sb.AppendLine("size=\"9,9\";");
            sb.AppendLine("node [color=lightblue2, style=filled];");

            MarkComponentsForNodes(nodes);

            foreach (var connectedComponents in nodes.GroupBy(x => x.Component))
            {
                var graph = connectedComponents.ToList();

                if (graph.Count == 1)
                {
                    sb.AppendLine($"{connectedComponents.First().Name};");
                }
                else
                {
                    var visited = new HashSet<GraphNode>();
                    foreach (var graphNode in graph)
                    {
                        BuildDotRepresentation(graphNode, sb, visited);
                    }
                }
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        private static void MarkComponentsForNodes(IEnumerable<GraphNode> nodes)
        {
            int component = 1;

            foreach (var graphNode in nodes)
            {
                if (graphNode.Component == null)
                {
                    graphNode.Component = component;
                    component++;
                    MarkComponentsForNode(graphNode);
                }
            }
        }

        private static void MarkComponentsForNode(GraphNode node)
        {
            foreach (var graphNode in node.Nodes)
            {
                if (graphNode.Component == null)
                {
                    graphNode.Component = node.Component;
                    MarkComponentsForNode(graphNode);
                }
            }
        }

        private static void BuildDotRepresentation(GraphNode start, StringBuilder sb,
            HashSet<GraphNode> visitedNodes)
        {
            if (!visitedNodes.Contains(start))
            {
                visitedNodes.Add(start);

                foreach (var graphNode in start.Nodes)
                {
                    sb.AppendLine($"{start.Name} -> {graphNode.Name};");
                    BuildDotRepresentation(graphNode, sb, visitedNodes); 
                }
            }
        }

        [TestFixture]
        public class Q4P2RouteBetweenGraphNodesTests
        {
            [Test]
            public void RouteExistsTest()
            {
                var a = new GraphNode("A");
                var b = new GraphNode("B");
                var c = new GraphNode("C");
                var d = new GraphNode("D");
                var e = new GraphNode("E");
                var f = new GraphNode("F");
                var g = new GraphNode("G");
                var h = new GraphNode("H");
                var i = new GraphNode("I");
                var j = new GraphNode("J");

                a.Nodes.Add(b);
                a.Nodes.Add(c);

                b.Nodes.Add(d);
                b.Nodes.Add(c);

                c.Nodes.Add(a);
                c.Nodes.Add(e);

                e.Nodes.Add(f);

                f.Nodes.Add(g);
                f.Nodes.Add(a);
                f.Nodes.Add(b);

                h.Nodes.Add(i);

                Console.WriteLine(DotRepresentation(a, b, c, d, e, f, g, h, i, j));

                Assert.True(RouteExists(a, f));
                Assert.True(RouteExists(a, b));
                Assert.True(RouteExists(b, a));
                Assert.True(RouteExists(c, d));
                Assert.False(RouteExists(d, f));
                Assert.False(RouteExists(g, f));
                Assert.False(RouteExists(g, h));
                Assert.True(RouteExists(f, a));
                Assert.False(RouteExists(j, i));
                Assert.False(RouteExists(i, h));
            }
        }
    }
}