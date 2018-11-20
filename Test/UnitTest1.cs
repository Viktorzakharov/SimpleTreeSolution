using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleTreeSolution;


namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        static Dictionary<Node, object> Dictionary = new Dictionary<Node, object>();
        static SimpleTree Tree = GenerateTree();
        static Node ParentNode = Tree.Root.NextNodes[2].NextNodes[1];
        static Node AddDelete = new Node(55);
        static Node MoveNode = Tree.Root.NextNodes[1];
        static int[] NodesAndSheets = new int[] { 10, 5 };

        [TestMethod]
        public void TestAdd()
        {
            Assert.IsTrue(Tree.AddNode(ParentNode, AddDelete));
            Assert.IsTrue(ParentNode.NextNodes.IndexOf(AddDelete) >= 0);
        }

        [TestMethod]
        public void TestDelete()
        {
            Assert.IsTrue(Tree.DeleteNode(AddDelete));
            Assert.IsTrue(ParentNode.NextNodes.IndexOf(AddDelete) == -1);
        }

        [TestMethod]
        public void TestAllNodes()
        {
            foreach (var node in Tree.GetAllNodes())
                Assert.IsTrue(Dictionary.ContainsKey(node));
        }

        [TestMethod]
        public void TestAllNodesByValue()
        {
            var count = 0;
            foreach (var e in Dictionary.Values)
                if (MoveNode.Value.Equals(e)) count++;
            var list = Tree.GetAllNodesByValue(MoveNode.Value);
            Assert.IsTrue(list.Count == count);
            foreach (var e in list)
                Assert.AreEqual(MoveNode.Value, e.Value);
        }

        [TestMethod]
        public void TestMove()
        {
            Assert.IsTrue(Tree.MoveNode(ParentNode, MoveNode));
            Assert.IsTrue(Tree.Root.NextNodes.IndexOf(MoveNode) == -1);
            Assert.IsTrue(ParentNode.NextNodes.IndexOf(MoveNode) >= 0);
        }

        [TestMethod]
        public void TestNodesAndSheetsCount()
        {
            var array = Tree.GetNodesAndSheetsCount();
            Assert.AreEqual(NodesAndSheets[0], array[0]);
            Assert.AreEqual(NodesAndSheets[1], array[1]);
        }

        static SimpleTree GenerateTree()
        {
            var random = new Random();
            var root = new Node(random);

            Dictionary.Add(root, root.Value);
            CreateNextNodes(root, Dictionary, 3, random);
            for (int i = 0; i < root.NextNodes.Count; i++)
                CreateNextNodes(root.NextNodes[i], Dictionary, 2, random);
            return new SimpleTree(root);
        }

        static void CreateNextNodes(Node node, Dictionary<Node, object> dictionary, int count, Random random)
        {
            for (int i = 0; i < count; i++)
            {
                var newNode = new Node(random.Next(50)) { PrevNode = node };
                node.NextNodes.Add(newNode);
                dictionary.Add(newNode, newNode.Value);
            }
        }
    }
}
