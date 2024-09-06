using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    private Node rootNode;

    public void SetRootNode(Node node)
    {
        rootNode = node;
    }

    // Process the behavior tree
    public Node.Status Process()
    {
        if (rootNode != null)
        {
            return rootNode.Process();
        }
        return Node.Status.Failure; // Return failure if root node is null
    }

    //private void Update()
    //{
    //    if (rootNode != null)
    //    {
    //        rootNode.Process();
    //    }
    //}
}
