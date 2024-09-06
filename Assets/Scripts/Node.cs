using System.Collections.Generic;

public class Node
{
    public enum Status
    {
        sucsess, failure, running
    }

    public string name;
    public List<Node> children;
    public int currentChildIndex;

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
        return children[currentChildIndex].Process();
    }
}
