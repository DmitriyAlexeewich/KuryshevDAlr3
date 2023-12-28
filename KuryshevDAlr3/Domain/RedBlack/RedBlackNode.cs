namespace KuryshevDAlr3.Domain.RedBlack
{
    public class RedBlackNode<T> : Node<T> where T : IComparable
    {
        public Color Color { get; set; }

        public RedBlackNode(T value, Color color) : base(value)
        {
            Color = color;
        }

        public RedBlackNode(T value) : base(value)
        {
        }
    }
}
