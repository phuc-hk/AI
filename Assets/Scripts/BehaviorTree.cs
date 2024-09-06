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

    private void Update()
    {
        if (rootNode != null)
        {
            rootNode.Process();
        }
    }
}
