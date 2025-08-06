using System.Xml.Schema;

public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else if (value > Data)
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        if (value == Data)
            return true;
        else if (value < Data)
        {
            if (Left is not null)
                return Left.Contains(value);
            else
                return false;
        }
        else if (value > Data)
        {
            if (Right is not null)
                return Right.Contains(value);
            else
                return false;
        }
        else
            return false;
    }

    public int GetHeight()
    {
        if (Left is null && Right is null)
            return 1;
        int currentHeight;
        int leftHeight = 0;
        if (Left is not null)
            leftHeight = Left.GetHeight();
        int rightHeight = 0;
        if (Right is not null)
            rightHeight = Right.GetHeight();
        if (leftHeight >= rightHeight)
            currentHeight = leftHeight;
        else
            currentHeight = rightHeight;
        return 1 + currentHeight;
    }
}