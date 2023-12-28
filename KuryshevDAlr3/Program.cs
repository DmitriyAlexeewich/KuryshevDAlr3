using KuryshevDAlr3.Domain;
using KuryshevDAlr3.Domain.AVL;
using KuryshevDAlr3.Domain.RedBlack;
using System.Diagnostics;

var binaryTree = new Tree<int>();
var avlTree = new AVLTree<int>();
var redBlackTree = new RedBlackTree<int>();

AddDefaultTreeValue(redBlackTree);

while (true)
{
    Console.WriteLine("1-Add");
    Console.WriteLine("2-Delete from AVL");
    Console.WriteLine("3-Delete from RedBlack");
    Console.WriteLine("4-BreadthFirstTraversal");
    Console.WriteLine("5-PreOrderTraversal");
    Console.WriteLine("6-InOrderTraversal");
    Console.WriteLine("7-PostOrderTraversal");
    Console.WriteLine("8-Clear");
    Console.WriteLine("0-Exit");

    Console.WriteLine("Comm: ");
    var comm = GetIntFromConsoleReadLine();

    Console.WriteLine();
    Console.WriteLine("-----------");
    Console.WriteLine();

    if (comm == 0)
        break;

    switch (comm)
    {
        case 1:
            Console.WriteLine("Binary tree");
            Console.WriteLine("Enter random sequence length: ");
            var length = GetIntFromConsoleReadLine();
            DisplayExecutionTime(() => { AddRandomSequenceToTree(binaryTree, length); }, "Fill binary tree");

            Console.WriteLine("AVL tree");
            Console.WriteLine("Enter random sequence length: ");
            length = GetIntFromConsoleReadLine();
            DisplayExecutionTime(() => { AddRandomSequenceToTree(avlTree, length); }, "Fill AVL tree");

            Console.WriteLine("RedBlack tree");
            Console.WriteLine("Enter random sequence length: ");
            length = GetIntFromConsoleReadLine();
            DisplayExecutionTime(() => { AddRandomSequenceToTree(redBlackTree, length); }, "Fill RedBlack tree");
            break;
        case 2:
            Console.WriteLine("Enter element value to delete: ");
            var value = GetIntFromConsoleReadLine();
            DisplayExecutionTime(() => { avlTree.Delete(value); }, $"Delete element {value} from AVL tree");
            break;
        case 3:
            Console.WriteLine("Enter element value to delete: ");
            value = GetIntFromConsoleReadLine();
            DisplayExecutionTime(() => { redBlackTree.Delete(value); }, $"Delete element {value} from RedBlack tree");
            break;
        case 4:
            Console.WriteLine("Binary tree");
            binaryTree.BreadthFirstTraversal(binaryTree.Node);
            Console.WriteLine();

            Console.WriteLine("AVL tree");
            avlTree.BreadthFirstTraversal(avlTree.Node);
            Console.WriteLine();

            Console.WriteLine("RedBlack tree");
            avlTree.BreadthFirstTraversal(redBlackTree.Node);
            Console.WriteLine();
            break;
        case 5:
            Console.WriteLine("Binary tree");
            binaryTree.PreOrderTraversal(binaryTree.Node);

            Console.WriteLine("AVL tree");
            avlTree.PreOrderTraversal(avlTree.Node);
            Console.WriteLine();

            Console.WriteLine("RedBlack tree");
            avlTree.PreOrderTraversal(redBlackTree.Node);
            Console.WriteLine();
            break;
        case 6:
            Console.WriteLine("Binary tree");
            binaryTree.InOrderTraversal(binaryTree.Node);

            Console.WriteLine("AVL tree");
            avlTree.InOrderTraversal(avlTree.Node);
            Console.WriteLine();

            Console.WriteLine("RedBlack tree");
            avlTree.InOrderTraversal(redBlackTree.Node);
            Console.WriteLine();
            break;
        case 7:
            Console.WriteLine("Binary tree");
            binaryTree.PostOrderTraversal(binaryTree.Node);
            Console.WriteLine();

            Console.WriteLine("AVL tree");
            avlTree.PostOrderTraversal(avlTree.Node);
            Console.WriteLine();

            Console.WriteLine("RedBlack tree");
            avlTree.PostOrderTraversal(redBlackTree.Node);
            Console.WriteLine();
            break;
        case 8:
            binaryTree.Clear();
            avlTree.Clear();
            redBlackTree.Clear();
            break;
        default:
            break;
    }

    Console.WriteLine();
    Console.WriteLine("----------------------");
    Console.WriteLine();
}

int GetIntFromConsoleReadLine()
{
    var str = Console.ReadLine();

    if (!int.TryParse(str, out var num))
        return 0;

    return num;
}

void AddDefaultTreeValue(Tree<int> tree)
{
    tree.Add(6);
    tree.Add(4);
    tree.Add(3);
    tree.Add(5);
}

void AddRandomSequenceToTree(Tree<int> tree, int length)
{
    var random = new Random();

    for(var i = 0; i< length; i++)
        tree.Add(random.Next(-100,100));
}

void DisplayExecutionTime(Action action, string processName)
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();
    action();
    stopWatch.Stop();
    Console.WriteLine($"Execution time \"{processName}\" is: {stopWatch.ElapsedMilliseconds} ms");
}
