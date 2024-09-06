public class Sequence : Node
{
    public Sequence(string name) : base(name) { }

    public override Status Process()
    {
        while (currentChildIndex < children.Count)
        {
            Status childStatus = children[currentChildIndex].Process();

            switch (childStatus)
            {
                case Status.Running:
                    return Status.Running;

                case Status.Failure:
                    return Status.Failure;

                case Status.Success:
                    currentChildIndex++;
                    break;
            }
        }

        currentChildIndex = 0;
        return Status.Success;
    }
}
