using System.Collections.Generic;

public class Node
{
    public enum Status
    {
        Success,
        Failure,
        Running
    }

    public string name;
    public List<Node> children = new List<Node>();
    public int currentChildIndex = 0;

    public Node(string name)
    {
        this.name = name;
    }

    public void AddChild(Node child)
    {
        children.Add(child);
    }

    public virtual Status Process()
    {
        if (children.Count == 0)
            return Status.Failure;

        return children[currentChildIndex].Process();
    }
}
