
namespace KuryshevDAlr3.Domain
{
    public class Node<T> where T : IComparable
    {
        public Node<T> ParentNode { get; set; }
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public virtual bool TryAdd(T value)
        {
            var compareResult = Value.CompareTo(value);

            if (compareResult == 0)
                return false;

            if (compareResult < 0)
            {
                if (Left == null)
                {
                    Left = new Node<T>(value);
                    return true;
                }

                if (!Left.TryAdd(value))
                    return false;

                return true;
            }

            if (Right == null)
            {
                Right = new Node<T>(value);
                return true;
            }

            if (!Right.TryAdd(value))
                return false;

            return true;
        }

        public virtual Node<T> Delete(Node<T> node, T value)
        {
            return null;
        }
    }
}
