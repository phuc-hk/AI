using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    private System.Func<Status> action;

    public Leaf(string name, System.Func<Status> action) : base(name)
    {
        this.action = action;
    }

    public override Status Process()
    {
        return action.Invoke();
    }
}

