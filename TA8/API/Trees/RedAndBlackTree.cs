using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            Node node = AddRecursive(ref root, element, null);
            InsertCase1(node);
            while (root != null && root.Parent != null)
            {
                root = root.Parent;
            }
        }
        private Node AddRecursive(ref Node? node, int element, Node? parent)
        {
            if (node == null)
            {
                node = new Node(element, parent);
                if (parent != null)
                {
                    if (element < parent.Value)
                    {
                        parent.Left = node;
                    }
                    else
                    {
                        parent.Right = node;
                    }
                }
                return node;
            }
            if (element < node.Value)
            {
                return AddRecursive(ref node.Left, element, node);
            }
            else
            {
                return AddRecursive(ref node.Right, element, node);
            }
        }
        void InsertCase1(Node node)
        {
            if (node.Parent == null)
            {
                node.IsRed = false;
            }
            else
            {
                InsertCase2(node);
            }
        }
        void InsertCase2(Node node)
        {
            if (node.Parent.IsRed)
            {
                InsertCase3(node);
            }
        }
        void InsertCase3(Node node)
        {
            Node? uncle = Uncle(node);
            if (IsRedAndExists(uncle))
            {
                node.Parent.IsRed = false;
                uncle.IsRed = false;
                Node grandparent = Grandparent(node);
                grandparent.IsRed = true;
                InsertCase1(grandparent);
            }
            else
            {
                InsertCase4(node);
            }
        }
        void InsertCase4(Node? node)
        {
            Node? grandparent = Grandparent(node);
            if (EqualValues(node, node.Parent.Right) && EqualValues(node.Parent, grandparent.Left))
            {
                RotateLeft(node.Parent);
                node = node.Left;
            }
            else if (EqualValues(node, node.Parent.Left) && EqualValues(node.Parent, grandparent.Right))
            {
                RotateRight(node.Parent);
                node = node.Right;
            }
            InsertCase5(node);
        }
        void InsertCase5(Node? node)
        {
            Node? grandparent = Grandparent(node);
            node.Parent.IsRed = false;
            grandparent.IsRed = true;
            if (EqualValues(node, node.Parent.Left) && EqualValues(node.Parent, grandparent.Left))
            {
                RotateRight(grandparent);
            }
            else
            {
                RotateLeft(grandparent);
            }
        }

        public void RemoveElement(int element)
        {
            Node node = RemoveRecursive(root, element);
            Node? child;
            if (node.Right == null)
            {
                child = node.Left;
            }
            else
            {
                child = node.Right;
            }
            if (!node.IsRed)
            {
                if (IsRedAndExists(child))
                {
                    Delete(node, child);
                    child.IsRed = false;
                }
                else
                {
                    if (child != null)
                    {
                        Delete(node, child);
                        DeleteCase1(child);
                    }
                    else
                    {
                        DeleteCase1(node);
                        Delete(node, child);
                    }
                }
            }
            else
            {
                Delete(node, child);
            }
            while (root != null && root.Parent != null)
            {
                root = root.Parent;
            }
        }

        private void Delete(Node node, Node? child)
        {
            if (child != null)
            {
                child.Parent = node.Parent;
            }
            if (node.Parent != null)
            {
                if (EqualValues(node, node.Parent.Left))
                {
                    node.Parent.Left = child;
                }
                else
                {
                    node.Parent.Right = child;
                }
            }
        }

        private void DeleteCase1(Node node)
        {
            if (node.Parent != null)
            {
                DeleteCase2(node);
            }
        }
        private void DeleteCase2(Node node)
        {
            Node? sibling = Sibling(node);
            if (IsRedAndExists(sibling))
            {
                node.Parent.IsRed = true;
                sibling.IsRed = false;
                if (EqualValues(node, node.Parent.Left))
                {
                    RotateLeft(node.Parent);
                }
                else
                {
                    RotateRight(node.Parent);
                }
            }
            DeleteCase3(node);
        }
        private void DeleteCase3(Node node)
        {
            Node sibling = Sibling(node);
            if (!node.Parent.IsRed && !sibling.IsRed && !IsRedAndExists(sibling.Left) && !IsRedAndExists(sibling.Right))
            {
                sibling.IsRed = true;
                DeleteCase1(node.Parent);
            }
            else
            {
                DeleteCase4(node);
            }
        }
        private void DeleteCase4(Node node)
        {
            Node sibling = Sibling(node);
            if (node.Parent.IsRed && !sibling.IsRed && !IsRedAndExists(sibling.Left) && !IsRedAndExists(sibling.Right))
            {
                sibling.IsRed = true;
                node.Parent.IsRed = false;
            }
            else
            {
                DeleteCase5(node);
            }
        }
        private void DeleteCase5(Node node)
        {
            Node sibling = Sibling(node);
            if (!sibling.IsRed)
            {
                if (EqualValues(node, node.Parent.Left) && !IsRedAndExists(sibling.Right) && IsRedAndExists(sibling.Left))
                {
                    sibling.IsRed = true;
                    sibling.Left.IsRed = false;
                    RotateRight(sibling);
                }
                else if (EqualValues(node, node.Parent.Right) && !IsRedAndExists(sibling.Left) && IsRedAndExists(sibling.Right))
                {
                    sibling.IsRed = true;
                    sibling.Right.IsRed = false;
                    RotateLeft(sibling);
                }
            }
            DeleteCase6(node);
        }
        private void DeleteCase6(Node node)
        {
            Node sibling = Sibling(node);
            sibling.IsRed = node.Parent.IsRed;
            node.Parent.IsRed = false;
            if (EqualValues(node, node.Parent.Left))
            {
                if (sibling.Right != null)
                {
                    sibling.Right.IsRed = false;
                }
                RotateLeft(node.Parent);
            }
            else
            {
                if (sibling.Left != null)
                {
                    sibling.Left.IsRed = false;
                }
                RotateRight(node.Parent);
            }
        }

        private Node? RemoveRecursive(Node? node, int element)
        {
            if (element < node.Value)
            {
                return RemoveRecursive(node.Left, element);
            }
            else if (element > node.Value)
            {
                return RemoveRecursive(node.Right, element);
            }
            else
            {
                if (node.Left == null && node.Right == null)
                {
                    return node;
                }
                else if (node.Left == null)
                {
                    Node temp = Smallest(node.Right);
                    node.Value = temp.Value;
                    return temp;
                }
                else if (node.Right == null)
                {
                    Node temp = Largest(node.Left);
                    node.Value = temp.Value;
                    return temp;
                }
                else
                {
                    Node temp = Smallest(node.Right);
                    node.Value = temp.Value;
                    return temp;
                }
            }
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
            public Node? Parent;
            public Node(int element, Node? parent)
            {
                Value = element;
                IsRed = true;
                Parent = parent;
            }
        }
        private Node? Grandparent(Node? node)
        {
            if (node != null && node.Parent != null)
            {
                return node.Parent.Parent;
            }
            else
            {
                return null;
            }
        }
        private Node? Uncle(Node? node)
        {
            Node? grandparent = Grandparent(node);
            if (grandparent == null)
            {
                return null;
            }
            if (EqualValues(node.Parent, grandparent.Left))
            {
                return grandparent.Right;
            }
            else
            {
                return grandparent.Left;
            }
        }
        private void RotateLeft(Node node)
        {
            Node? temp = node.Right;
            temp.Parent = node.Parent;
            if (node.Parent != null)
            {
                if (EqualValues(node.Parent.Left, node))
                {
                    node.Parent.Left = temp;
                }
                else
                {
                    node.Parent.Right = temp;
                }
            }
            node.Right = temp.Left;
            if (temp.Left != null)
            {
                temp.Left.Parent = node;
            }
            node.Parent = temp;
            temp.Left = node;
        }
        private void RotateRight(Node node)
        {
            Node? temp = node.Left;
            temp.Parent = node.Parent;
            if (node.Parent != null)
            {
                if (EqualValues(node.Parent.Left, node))
                {
                    node.Parent.Left = temp;
                }
                else
                {
                    node.Parent.Right = temp;
                }
            }
            node.Left = temp.Right;
            if (temp.Right != null)
            {
                temp.Right.Parent = node;
            }
            node.Parent = temp;
            temp.Right = node;
        }
        private Node? Smallest(Node node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }
        private Node? Largest(Node node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }
            return node;
        }
        private Node? Sibling(Node node)
        {
            if (EqualValues(node, node.Parent.Left))
            {
                return node.Parent.Right;
            }
            else
            {
                return node.Parent.Left;
            }
        }
        private bool EqualValues(Node? node1, Node? node2)
        {
            if (node1 == null || node2 == null)
            {
                return false;
            }
            else
            {
                return (node1.Value == node2.Value);
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
    }
}
