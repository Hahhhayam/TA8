using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA8.API.Interfaces;

namespace TA8.API.Trees
{
    internal class RedAndBlackTree : ITree
    {
        public List<int> NumList { get; set; }
        public Node? root;
        public RedAndBlackTree(List<int> numList)
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
            root = AddRecursive(root, element);
            root.IsRed = false;
        }
        private Node AddRecursive(Node? node, int element)
        {
            if (node == null)
            {
                return new Node(element);
            }

            if (element < node.Value)
            {
                node.Left = AddRecursive(node.Left, element);
            }
            else if (element > node.Value)
            {
                node.Right = AddRecursive(node.Right, element);
            }

            if (IsRedAndExists(node.Right) && !IsRedAndExists(node.Left))
            {
                node = RotateLeft(node);
            }

            if (IsRedAndExists(node.Left) && IsRedAndExists(node.Left.Left))
            {
                node = RotateRight(node);
            }

            if (IsRedAndExists(node.Left) && IsRedAndExists(node.Right))
            {
                FlipColors(node);
            }
            return node;
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

            if (element < node.Value)
            {
                node.Left = RemoveRecursive(node.Left, element);
            }
            else if (element > node.Value)
            {
                node.Right = RemoveRecursive(node.Right, element);
            }
            else
            {
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                else if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    Node smallest = node.Right;
                    while (smallest.Left != null)
                    {
                        smallest = smallest.Left;
                    }

                    node.Value = smallest.Value;
                    node.Right = RemoveRecursive(node.Right, smallest.Value);
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

        public class Node
        {
            public int Value;
            public Node? Left;
            public Node? Right;
            public bool IsRed;
            public Node(int element)
            {
                Value = element;
                IsRed = true;
            }
        }
        private bool IsRedAndExists(Node? node)
        {
            if (node == null)
            {
                return false;
            }
            return node.IsRed;
        }
        private Node RotateLeft(Node node)
        {
            Node? temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            temp.IsRed = node.IsRed;
            node.IsRed = true;
            return temp;
        }
        private Node RotateRight(Node node)
        {
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.IsRed = node.IsRed;
            node.IsRed = true;
            return temp;
        }
        private void FlipColors(Node node)
        {
            node.IsRed = true;
            node.Left.IsRed = false;
            node.Right.IsRed = false;
        }
    }
}
