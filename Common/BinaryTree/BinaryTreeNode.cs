using System;

namespace Common.BinaryTree
{
    public class BinaryTreeNode<T>
    {
        public T Data { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public void Print()
        {
            Print("", true);
        }

        private void Print(String prefix, bool isTail)
        {
            Console.WriteLine(prefix + (isTail ? "└── " : "├── ") + Data);

            var end = Left ?? Right;

            Right?.Print(prefix + (isTail ? "    " : "│   "), Right == end);
            Left?.Print(prefix + (isTail ? "    " : "│   "), Left == end);
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}