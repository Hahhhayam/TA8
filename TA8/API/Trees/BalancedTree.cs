using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA8.API.Interfaces;

namespace TA8.API.Trees
{
    internal class BalancedTree : ITree
    {
        public List<int> NumList { get; set; }
        public List<int> SortedList { get; set; }
        private Node? root;
        public BalancedTree(List<int> numList)
        {
            NumList = numList;
            SortedList = new List<int>(numList);
            SortedList.Sort();
            root = BuildBalancedTree(SortedList);
        }
        public Node? BuildBalancedTree(List<int> sortedList)
        {
            root = BuildBalancedTreeRecursive(sortedList, 0, sortedList.Count - 1);
            return root;
        }
        private Node? BuildBalancedTreeRecursive(List<int> sortedList, int start, int end)
        {
            if (start > end)
            {
                return null;
            }
            int mid = (start + end) / 2;
            Node node = new Node(sortedList[mid]);
            node.Left = BuildBalancedTreeRecursive(sortedList, start, mid - 1);
            node.Right = BuildBalancedTreeRecursive(sortedList, mid + 1, end);
            return node;
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
                root = AddRecursive(root, element);
            }

        }
        private Node? AddRecursive(Node node, int element)
        {
            if (node == null)
            {
                return new Node(element);
            }
            if (element < node.Value)
            {
                node.Left = AddRecursive(node.Left, element);
            }
            else
            {
                node.Right = AddRecursive(node.Right, element);
            }
            return Balance(node);
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
                Node? tempLeft = node.Left;
                Node? tempRight = node.Right;
                node = null;
                if (tempRight == null)
                {
                    return tempLeft;
                }
                Node? min = FindMin(tempRight);
                min.Right = RemoveMin(tempRight);
                min.Left = tempLeft;
                return Balance(min);
            }
            return Balance(node);
        }
        private Node FindMin(Node node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }
        private Node? RemoveMin(Node? node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            node.Left = RemoveMin(node.Left);
            return Balance(node);
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
        private int HeightOrNull(Node? node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;
        }
        private int BFactor (Node? node)
        {
            return HeightOrNull(node.Right) - HeightOrNull(node.Left);
        }
        private void FixHeight(Node? node)
        {
            int hl = HeightOrNull(node.Left);
            int hr = HeightOrNull(node.Right);
            if (hl > hr)
            {
                node.Height = hl + 1;
            }
            else
            {
                node.Height = hr + 1;
            }
        }
        private Node? RotateRight(Node? node)
        {
            Node? temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            FixHeight(node);
            FixHeight(temp);
            return temp;
        }
        private Node? Balance (Node? node)
        {
            FixHeight(node);
            if (BFactor(node) == 2)
            {
                if (BFactor(node.Right) < 0)
                {
                    node.Right = RotateRight(node.Right);
                }
                return RotateLeft(node);
            }
            if (BFactor(node) == -2)
            {
                if (BFactor(node.Left) > 0)
                {
                    node.Left = RotateLeft(node.Left);
                }
                return RotateRight(node);
            }
            return node;
        }
        private Node? RotateLeft (Node? node)
        {
            Node? temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            FixHeight(node);
            FixHeight(temp);
            return temp;
        }
        public class Node
        {
            public int Value;
            public Node? Left;
            public Node? Right;
            public int Height;
            public Node(int value)
            {
                Value = value;
                Height = 1;
            }
        }
    }
}
