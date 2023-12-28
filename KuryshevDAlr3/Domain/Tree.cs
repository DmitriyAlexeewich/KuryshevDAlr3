namespace KuryshevDAlr3.Domain
{
    public class Tree<T> where T : IComparable
    {
        public Tree() { }

        public Node<T> Node { get; protected set; }

        public virtual void Add(T value)
        {
            if (Node is null)
            {
                Node = new Node<T>(value);
                return;
            }

            Node.TryAdd(value);
        }

        public void Clear()
        {
            Node = null;
        }

        public void BreadthFirstTraversal(Node<T> root)
        {
            for (var i = 0; true; i++)
            {
                var levelNodes = GetNodesByLevel(root, i);

                if (levelNodes is null || !levelNodes.Any() || levelNodes.All(x => x == null))
                    return;

                Console.WriteLine(string.Join(' ', levelNodes.Select(x => x is null ? "null" : x.ToString())));
            }
        }

        public void PreOrderTraversal(Node<T> root)
        {
            if (root is null)
                return;

            Console.Write($"{root.Value} ");
            PreOrderTraversal(root.Left);
            PreOrderTraversal(root.Right);
        }

        public void InOrderTraversal(Node<T> root)
        {
            if (root is null)
                return;

            InOrderTraversal(root.Left);
            Console.Write($"{root.Value} ");
            InOrderTraversal(root.Right);
        }

        public void PostOrderTraversal(Node<T> root)
        {
            if (root is null)
                return;

            PostOrderTraversal(root.Left);
            PostOrderTraversal(root.Right);
            Console.Write($"{root.Value} ");
        }

        public virtual void Delete(T value)
        {
            return;
        }

        private Node<T>[] GetNodesByLevel(Node<T> root, int level)
        {
            if (root is null)
                return new Node<T>[] { null };

            if (level == 0)
                return new Node<T>[] { root };

            var leftNodes = GetNodesByLevel(root.Left, level - 1);
            var rightNodes = GetNodesByLevel(root.Right, level - 1);

            leftNodes = leftNodes.Concat(rightNodes).ToArray();

            return leftNodes;
        }

    }
}
