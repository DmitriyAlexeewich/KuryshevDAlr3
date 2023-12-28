namespace KuryshevDAlr3.Domain.AVL
{
    public class AVLTreeNode<T> : Node<T> where T : IComparable
    {
        public AVLTreeNode(T value) : base(value)
        {
        }

        public override bool TryAdd(T value)
        {
            var compareResult = Value.CompareTo(value);

            if (compareResult == 0)
                return false;

            if (compareResult < 0)
            {
                if (Left == null)
                {
                    Left = new AVLTreeNode<T>(value);
                    return true;
                }

                if (!Left.TryAdd(value))
                    return false;

                Left = BalanceNode(Left);
                return true;
            }

            if (Right == null)
            {
                Right = new AVLTreeNode<T>(value);
                return true;
            }

            if (!Right.TryAdd(value))
                return false;

            Right = BalanceNode(Right);
            return true;
        }

        public Node<T> BalanceNode(Node<T> node)
        {
            int factor = BalanceFactor(node);
            if (factor > 1)
            {
                if (BalanceFactor(node.Left) > 0)
                {
                    return RotateLL(node);
                }
                else
                {
                    return RotateLR(node);
                }
            }
            else if (factor < -1)
            {
                if (BalanceFactor(node.Right) > 0)
                {
                    return RotateRL(node);
                }
                else
                {
                    return RotateRR(node);
                }
            }

            return node;
        }

        public override Node<T> Delete(Node<T> node, T value)
        {
            Node<T> parent;

            if (node == null)
                return null;

            var compareResult = node.Value.CompareTo(value);

            if (compareResult == 0)
            {
                if (node.Right != null)
                {
                    parent = node.Right;

                    while (parent.Left != null)
                        parent = parent.Left;

                    node.Value = parent.Value;

                    node.Right = Delete(node.Right, parent.Value);

                    if (BalanceFactor(node) == 2)
                    {
                        if (BalanceFactor(node.Left) >= 0)
                            node = RotateLL(node);
                        else
                            node = RotateLR(node);
                    }

                    return node;
                }
                else
                    return node.Left;
            }

            if (compareResult < 0)
            {
                node.Left = Delete(node.Left, value);
                if (BalanceFactor(node) == -2)
                {
                    if (BalanceFactor(node.Right) <= 0)
                        node = RotateRR(node);
                    else
                        node = RotateRL(node);
                }

                return node;
            }

            node.Right = Delete(node.Right, value);
            if (BalanceFactor(node) == 2)
            {
                if (BalanceFactor(node.Left) >= 0)
                    node = RotateLL(node);
                else
                    node = RotateLR(node);
            }

            return node;
        }

        private int BalanceFactor(Node<T> node)
        {
            var left = GetHeight(node.Left);
            var right = GetHeight(node.Right);
            var factor = left - right;

            return factor;
        }

        private int GetHeight(Node<T> node)
        {
            var height = 0;

            if (node != null)
            {
                var left = GetHeight(node.Left);
                var right = GetHeight(node.Right);

                var max = left > right ? left : right;

                height = max + 1;
            }

            return height;
        }

        private Node<T> RotateRR(Node<T> node)
        {
            var tempNode = node.Right;

            node.Right = tempNode.Left;
            tempNode.Left = node;

            return tempNode;
        }

        private Node<T> RotateLL(Node<T> node)
        {
            var tempNode = node.Left;

            node.Left = tempNode.Right;
            tempNode.Right = node;

            return tempNode;
        }

        private Node<T> RotateLR(Node<T> node)
        {
            var tempNode = node.Left;

            node.Left = RotateRR(tempNode);

            return RotateLL(node);
        }

        private Node<T> RotateRL(Node<T> node)
        {
            var tempNode = node.Right;

            node.Right = RotateLL(tempNode);

            return RotateRR(node);
        }
    }
}
