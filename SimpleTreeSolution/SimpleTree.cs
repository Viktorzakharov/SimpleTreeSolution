using System.Collections.Generic;

namespace SimpleTreeSolution
{
    public class SimpleTree
    {
        public readonly Node Root;

        public SimpleTree(Node root)
        {
            Root = CreateRoot(root);
        }

        Node CreateRoot(Node root)
        {
            root.PrevNode = null;
            root.Level = 0;
            SetNextNodesLevels(root.NextNodes);
            return root;
        }

        public bool AddNode(Node parent, Node child)
        {
            if (!IsNodeInTree(parent) || IsNodeInTree(child)) return false;
            parent.NextNodes.Add(child);
            child.PrevNode = parent;
            child.Level = parent.Level + 1;
            SetNextNodesLevels(child.NextNodes);
            return true;
        }

        public bool DeleteNode(Node node)
        {
            if (!IsNodeInTree(node) || Root.Equals(node)) return false;
            node.PrevNode.NextNodes.Remove(node);
            node.PrevNode = null;
            return true;
        }

        public List<Node> GetAllNodes()
        {
            var list = new List<Node>() { Root };
            return AllNodes(Root, list);
        }

        List<Node> AllNodes(Node parentNode, List<Node> list)
        {
            foreach (var node in parentNode.NextNodes)
            {
                list.Add(node);
                if (node.NextNodes.Count != 0) AllNodes(node, list);
            }
            return list;
        }

        public List<Node> GetAllNodesByValue(object value)
        {
            var list = new List<Node>();
            if (Root.Value.Equals(value)) list.Add(Root);
            return AllNodesByValue(Root, list, value);
        }

        List<Node> AllNodesByValue(Node parentNode, List<Node> list, object value)
        {
            foreach (var node in parentNode.NextNodes)
            {
                if (node.Value.Equals(value)) list.Add(node);
                if (node.NextNodes.Count != 0) AllNodesByValue(node, list, value);
            }
            return list;
        }

        public bool MoveNode(Node newParentNode, Node child)
        {
            var node = newParentNode;
            while (node.PrevNode != null)
            {
                node = node.PrevNode;
                if (child.Equals(node)) return false;
            }
            DeleteNode(child);
            AddNode(newParentNode, child);
            return true;
        }

        public int[] GetNodesAndSheetsCount()
        {
            var count = new int[2];
            count[0]++;
            if (Root.NextNodes.Count == 0) count[1]++;
            return NodesAndSheetsCount(Root, count);
        }

        int[] NodesAndSheetsCount(Node parentNode, int[] count)
        {
            foreach (var node in parentNode.NextNodes)
            {
                count[0]++;
                if (node.NextNodes.Count != 0) NodesAndSheetsCount(node, count);
                else count[1]++;
            }
            return count;
        }

        void SetNextNodesLevels(List<Node> nextNodes)
        {
            foreach (var node in nextNodes)
            {
                node.Level = node.PrevNode.Level + 1;
                if (node.NextNodes.Count != 0) SetNextNodesLevels(node.NextNodes);
            }
        }

        public List<Node> GetNodesOnLevel(int level)
        {
            var nodesLevel = new List<Node>();
            if (Root.Level != level) return NodesOnLevel(nodesLevel, level);
            nodesLevel.Add(Root);
            return nodesLevel;
        }

        List<Node> NodesOnLevel(List<Node> list, int level)
        {
            foreach (var node in Root.NextNodes)
            {
                if (node.Level != level - 1) NodesOnLevel(list, level);
                foreach (var e in node.NextNodes) list.Add(e);
            }
            return list;
        }

        public bool IsNodeInTree(Node node)
        {
            while (node.PrevNode != null)
                node = node.PrevNode;
            if (Root.Equals(node)) return true;
            return false;
        }
    }
}
