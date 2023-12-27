namespace KuryshevDAlr3.Domain.Binary
{
    public class Node <T> where T : IComparable
    {
        public Side Side { get; }
        public T Value { get; }
        public Node<T> Left { get; private set; }
        public Node<T> Right { get; private set; }

        public Node(Side side, T value)
        {
            Side = side;
            Value = value;
        }

        public void Add(T value)
        {
            var compareResult = value.CompareTo(value);

            if (compareResult == 0)
                return;

            if (compareResult < 0)
                Left = new Node<T>(Side.Left, value);
            
            Right = new Node<T> (Side.Right, value);
        }
    }
}
