using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTreeSolution
{
    class Program
    {
        static void Main()
        {
            var random = new Random();
            var root = new Node(random.Next(50));
            var tree = new SimpleTree(root);
            AddNode(tree, root, 2, random);
            foreach (var e in root.NextNodes)
                AddNode(tree, e, 2, random);

            Console.WriteLine("\t\t\t{0}", tree.Root.Value);
            Console.WriteLine("\t{0}\t\t\t\t{1}", tree.Root.NextNodes[0].Value, tree.Root.NextNodes[1].Value);
            Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", tree.Root.NextNodes[0].NextNodes[0].Value,
                                                          tree.Root.NextNodes[0].NextNodes[1].Value,
                                                          tree.Root.NextNodes[1].NextNodes[0].Value,
                                                          tree.Root.NextNodes[1].NextNodes[0].Value);
        }

        static void AddNode(SimpleTree tree, Node parentNode, int count, Random random)
        {
            for (int i = 0; i < count; i++)
                tree.AddNode(parentNode, new Node(random.Next(50)));
        }
    }
}
