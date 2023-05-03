using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA8.API.Interfaces;
using static TA8.API.Trees.SimpleTree;

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
            SortedList.Add(element);
            SortedList.Sort();
            root = BuildBalancedTree(SortedList);
        }
        public void RemoveElement(int element)
        {
            SortedList.Remove(element);
            root = BuildBalancedTree(SortedList);
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
    }
}
