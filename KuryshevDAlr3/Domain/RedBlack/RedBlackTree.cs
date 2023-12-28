namespace KuryshevDAlr3.Domain.RedBlack
{
    public class RedBlackTree<T> : Tree<T> where T : IComparable
    {

        public override void Add(T value)
        {
            if (Node is null)
            {
                Node = new RedBlackNode<T>(value, Color.Black);
                return;
            }

            RedBlackNode <T> temp = null;
            var newItem = new RedBlackNode<T>(value);
            var node = Node;
            var compareResult = 0;

            while (node != null)
            {
                temp = (RedBlackNode<T>)node;

                compareResult = node.Value.CompareTo(newItem.Value);

                if (compareResult < 0)
                    node = node.Left;
                else
                    node = node.Right;
            }

            newItem.ParentNode = temp;
            compareResult = newItem.Value.CompareTo(temp.Value);

            if (temp == null)
                Node = newItem;
            else if (compareResult > 0)
                temp.Left = newItem;
            else
                temp.Right = newItem;

            newItem.Left = null;
            newItem.Right = null;
            newItem.Color = Color.Red;
            Balance(newItem);
        }

        public override void Delete(T value)
        {
            //first find the node in the tree to delete and assign to item pointer/reference
            var item = Find((RedBlackNode<T>)Node, value);
            RedBlackNode<T> X = null;
            RedBlackNode<T> Y = null;

            if (item == null)
                return;

            if (item.Left == null || item.Right == null)
                Y = item;
            else
                Y = TreeSuccessor(item);

            if (Y.Left != null)
                X = (RedBlackNode<T>)Y.Left;
            else
                X = (RedBlackNode<T>)Y.Right;

            if (X != null)
                X.ParentNode = (RedBlackNode<T>)Y;

            if (Y.ParentNode == null)
                Node = X;
            else if (Y == Y.ParentNode.Left)
                Y.ParentNode.Left = X;
            else
                Y.ParentNode.Left = X;

            if (Y != item)
                item.Value = Y.Value;

            if (Y.Color == Color.Black)
                DeleteBalance(X);

        }

        private void LeftRotate(Node<T> node)
        {
            if (node is null)
                return;

            var temp = node.Right;
            node.Right = temp.Left;

            if (temp.Left != null)
                temp.Left.ParentNode = node;

            if (temp != null)
                temp.ParentNode = node.ParentNode;

            if (node.ParentNode == null)
                Node = temp;
            else
            {
                if (node == node.ParentNode.Left)
                    node.ParentNode.Left = temp;
                else
                    node.ParentNode.Right = temp;
            }

            temp.Left = node;
            
            if (node != null)
                node.ParentNode = temp;

        }
        
        private void RightRotate(Node<T> node)
        {
            var temp = node.Left;
            node.Left = temp.Right;

            if (temp.Right != null)
                temp.Right.ParentNode = node;

            if (temp != null)
                temp.ParentNode = node.ParentNode;

            if (node.ParentNode == null)
                Node = temp;

            if (node == node.ParentNode.Right)
                node.ParentNode.Right = temp;

            if (node == node.ParentNode.Left)
            {
                node.ParentNode.Left = temp;
            }

            temp.Right = node;

            if (node != null)
                node.ParentNode = temp;
        }

        private void Balance(RedBlackNode<T> node)
        {
            var parent = (RedBlackNode<T>)node.ParentNode;

            while (node != Node && parent.Color == Color.Red)
            {
                if (node.ParentNode.ParentNode != null && node.ParentNode == node.ParentNode.ParentNode.Left)
                {
                    var temp = (RedBlackNode<T>)node.ParentNode.ParentNode.Right;
                    if (temp != null && temp.Color == Color.Red)
                    {
                        var pTemp = (RedBlackNode<T>)node.ParentNode;
                        pTemp.Color = Color.Black;
                        temp.Color = Color.Black;

                        var p2Temp = (RedBlackNode<T>)node.ParentNode.ParentNode;
                        p2Temp.Color = Color.Red;
                        node = (RedBlackNode<T>)node.ParentNode.ParentNode;
                    }
                    else
                    {
                        if (node == (RedBlackNode<T>)node.ParentNode.Right)
                        {
                            node = (RedBlackNode<T>)node.ParentNode;
                            LeftRotate(node);
                        }

                        var p3Temp = (RedBlackNode<T>)node.ParentNode;
                        p3Temp.Color = Color.Black;

                        var p4Temp = (RedBlackNode<T>)node.ParentNode.ParentNode;
                        p4Temp.Color = Color.Red;
                        RightRotate(node.ParentNode.ParentNode);
                    }

                }
                else
                {
                    RedBlackNode<T> temp = null;

                    temp = (RedBlackNode<T>)node.ParentNode?.ParentNode?.Left;
                    if (temp != null  && temp.Color == Color.Black)
                    {
                        var p1Temp = (RedBlackNode<T>)node.ParentNode;

                        p1Temp.Color = Color.Red;
                        temp.Color = Color.Red;

                        var p2Temp = (RedBlackNode<T>)node.ParentNode.ParentNode;
                        p2Temp.Color = Color.Black;
                        node = (RedBlackNode<T>)node.ParentNode.ParentNode;
                    }
                    else
                    {
                        if (node == (RedBlackNode<T>)node.ParentNode.Left)
                        {
                            node = (RedBlackNode<T>)node.ParentNode;
                            RightRotate(node);
                        }

                        var p3Temp = (RedBlackNode<T>)node.ParentNode;
                        var p4Temp = (RedBlackNode<T>)node.ParentNode.ParentNode;
                        p3Temp.Color = Color.Black;
                        
                        if(p4Temp != null)
                            p4Temp.Color = Color.Red;
                        
                        LeftRotate(node.ParentNode.ParentNode);

                    }

                }

                ((RedBlackNode<T>)Node).Color = Color.Black;
            }
        }

        private RedBlackNode<T> Find(RedBlackNode<T> node, T value)
        {
            if (node is null)
                return null;

            var compareResult = node.Value.CompareTo(value);

            if (compareResult == 0)
                return node;

            if (compareResult < 0)
                return Find((RedBlackNode<T>)node.Left, value);

            return Find((RedBlackNode<T>)node.Right, value);
        }

        private RedBlackNode<T> TreeSuccessor(RedBlackNode<T> node)
        {
            if (node.Left != null)
            {
                while (node.Left.Left != null)
                    node = (RedBlackNode<T>)node.Left;

                if (node.Left.Right != null)
                    node = (RedBlackNode<T>)node.Left.Right;

                return node;
            }
            else
            {
                var temp = node.ParentNode;
                while (temp != null && node == temp.Right)
                {
                    node = (RedBlackNode<T>)temp;
                    temp = temp.ParentNode;
                }

                return (RedBlackNode<T>)temp;
            }
        }

        private void DeleteBalance(RedBlackNode<T> node)
        {
            while (node != null && node != Node && node.Color == Color.Black)
            {
                if (node == (RedBlackNode<T>)node.ParentNode.Left)
                {
                    var W = (RedBlackNode<T>)node.ParentNode.Right;

                    if (W.Color == Color.Red)
                    {
                        W.Color = Color.Black;

                        var p1Temp = (RedBlackNode<T>)node.ParentNode;
                        p1Temp.Color = Color.Red;
                        LeftRotate(node.ParentNode);
                        W = (RedBlackNode<T>)node.ParentNode.Right;
                    }

                    var lTemp = (RedBlackNode<T>)W.Left;
                    var r1Temp = (RedBlackNode<T>)W.Right;

                    if (lTemp.Color == Color.Black && r1Temp.Color == Color.Black)
                    {
                        W.Color = Color.Red;
                        node = (RedBlackNode<T>)node.ParentNode;
                    }
                    else if (r1Temp.Color == Color.Black)
                    {
                        lTemp.Color = Color.Black;
                        W.Color = Color.Red;
                        RightRotate(W);
                        W = (RedBlackNode<T>)node.ParentNode.Right;
                    }

                    var p2Temp = (RedBlackNode<T>)node.ParentNode;
                    r1Temp = (RedBlackNode<T>)W.Right;

                    W.Color = p2Temp.Color;
                    p2Temp.Color = Color.Black;
                    r1Temp.Color = Color.Black;
                    LeftRotate(node.ParentNode);
                    Node = node;
                }
                else
                {
                    var W = (RedBlackNode<T>)node.ParentNode.Left;
                    if (W.Color == Color.Red)
                    {
                        W.Color = Color.Black;
                        var p3Temp = (RedBlackNode<T>)node.ParentNode;
                        p3Temp.Color = Color.Red;
                        RightRotate(node.ParentNode);
                        W = (RedBlackNode<T>)node.ParentNode.Left;
                    }

                    var r2Temp = (RedBlackNode<T>)W.Right;
                    var l2Temp = (RedBlackNode<T>)W.Left;

                    if (r2Temp.Color == Color.Black && l2Temp.Color == Color.Black)
                    {
                        W.Color = Color.Black;
                        node = (RedBlackNode<T>)node.ParentNode;
                    }
                    else if (l2Temp.Color == Color.Black)
                    {
                        r2Temp.Color = Color.Black;
                        W.Color = Color.Red;
                        LeftRotate(W);
                        W = (RedBlackNode<T>)node.ParentNode.Left;
                    }

                    var p2Temp = (RedBlackNode<T>)node.ParentNode;
                    l2Temp = (RedBlackNode<T>)W.Left;

                    W.Color = p2Temp.Color;
                    p2Temp.Color = Color.Black;
                    l2Temp.Color = Color.Black;
                    RightRotate(node.ParentNode);
                    node = (RedBlackNode<T>)Node;
                }
            }

            if (node != null)
                node.Color = Color.Black;
        }
    }
}
