using System.Collections.Generic;

namespace SimpleTreeSolution
{
    public class Node
    {
        public object Value;
        public Node PrevNode;
        public List<Node> NextNodes;
        public int Level;

        public Node(object value)
        {
            Value = value;
            PrevNode = null;
            NextNodes = new List<Node>();
            Level = 0;
        }

        public void Clean()
        {
            Value = null;
            PrevNode = null;
            NextNodes = new List<Node>();
            Level = 0;
        }
    }



}
