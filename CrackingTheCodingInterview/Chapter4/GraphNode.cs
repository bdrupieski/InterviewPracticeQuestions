using System.Collections.Generic;

namespace CrackingTheCodingInterview.Chapter4
{
    public class GraphNode
    {
        public string Name { get; private set; }
        public List<GraphNode> Nodes { get; private set; }
        public int? Component { get; set; }

        public GraphNode(string name)
        {
            Name = name;
            Nodes = new List<GraphNode>();
        }
    }
}