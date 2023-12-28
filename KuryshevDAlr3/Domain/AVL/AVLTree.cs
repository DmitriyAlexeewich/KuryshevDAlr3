namespace KuryshevDAlr3.Domain.AVL
{
    public class AVLTree<T> : Tree<T> where T : IComparable
    {
        private AVLTreeNode<T> _balancer;

        public override void Add(T value)
        {
            if (Node is null)
            {
                Node = new AVLTreeNode<T>(value);
                _balancer = new AVLTreeNode<T>(value);

                return;
            }

            if (!Node.TryAdd(value))
                return;

            Node = _balancer.BalanceNode(Node);
        }

        public override void Delete(T value)
        {
            Node = Node.Delete(Node, value);
        }
    }
}
