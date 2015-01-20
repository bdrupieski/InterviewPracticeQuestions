using System;

namespace CrackingTheCodingInterview.Chapter4
{
    public class BinaryTree<T> where T : IComparable<T>
    {
        public BinaryTreeNode<T> Root { get; private set; }

        public void Insert(T data)
        {
            if (Root == null)
            {
                Root = new BinaryTreeNode<T> { Data = data };
            }
            else
            {
                Insert(Root, data);
            }
        }

        private static void Insert(BinaryTreeNode<T> subtreeRoot, T data)
        {
            if (data.CompareTo(subtreeRoot.Data) < 0)
            {
                if (subtreeRoot.Left == null)
                {
                    subtreeRoot.Left = new BinaryTreeNode<T> { Data = data };
                }
                else
                {
                    Insert(subtreeRoot.Left, data);
                }
            }
            else if (data.CompareTo(subtreeRoot.Data) > 0)
            {
                if (subtreeRoot.Right == null)
                {
                    subtreeRoot.Right = new BinaryTreeNode<T> { Data = data };
                }
                else
                {
                    Insert(subtreeRoot.Right, data);
                }
            }
        }
    }
}