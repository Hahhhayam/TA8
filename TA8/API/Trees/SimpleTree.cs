using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA8.API.Interfaces;

namespace TA8.API.Trees
{
    internal class SimpleTree : ITree
    {
        public List<int> NumList { get; set; }
        private Node? root;
        public SimpleTree(List<int> numList)
        {
            NumList = numList;
            foreach (int num in NumList)
            {
                AddElement(num);
            }
        }
        public bool FindElement(int element)
        {
            return FindRecursive(root, element);
        }
        private bool FindRecursive(Node? node, int element)
        {
            if (node == null)
            {
                return false;
            }
            if (node.Value == element)
            {
                return true;
            }
            if (element < node.Value)
            {
                return FindRecursive(node.Left, element);
            }
            else
            {
                return FindRecursive(node.Right, element);
            }
        }

        public void AddElement(int element)
        {
            if (root == null)
            {
                root = new Node(element);
            }
            else
            {
                AddRecursive(root, element);
            }

        }
        private void AddRecursive(Node node, int element)
        {
            if (element < node.Value)
            {
                if (node.Left == null)
                {
                    node.Left = new Node(element);
                }
                else
                {
                    AddRecursive(node.Left, element);
                }
            }
            else if (element > node.Value)
            {
                if (node.Right == null)
                {
                    node.Right = new Node(element);
                }
                else
                {
                    AddRecursive(node.Right, element);
                }
            }
        }

        public void RemoveElement(int element)
        {
            root = RemoveRecursive(root, element);
        }
        private Node? RemoveRecursive(Node? node, int element)
        {
            if (node == null)
            {
                return null;
            }
            else if (element < node.Value)
            {
                node.Left = RemoveRecursive(node.Left, element);
            }
            else if (element > node.Value)
            {
                node.Right = RemoveRecursive(node.Right, element);
            }
            else
            {
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    Node temp = FindMin(node.Right);
                    node.Value = temp.Value;
                    node.Right = RemoveRecursive(node.Right, temp.Value);
                }
            }

            return node;
        }
        public List<int> GetDirections(int element)
        {
            if (!FindElement(element))
            {
                AddElement(element);
            }
            List<int> directions = new List<int>();
            return GetDirectionsRecursive(root, element, directions);
        }
        private List<int> GetDirectionsRecursive(Node node, int element, List<int> directions)
        {
            directions.Add(node.Value);
            if (node.Value > element)
            {
                GetDirectionsRecursive(node.Left, element, directions);
            }
            else if (node.Value < element)
            {
                GetDirectionsRecursive(node.Right, element, directions);
            }
            return directions;
        }
        private Node FindMin(Node node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        public class Node
        {
            public int Value;
            public Node? Left;
            public Node? Right;
            public Node(int value)
            {
                Value = value;
            }
        }

    }
}
