using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehavior : MonoBehaviour
{
    private BehaviorTree behaviorTree;
    private NavMeshAgent agent;

    private bool behaviorComplete = false;  // Flag to check if the behavior is complete

    // Positions in the environment for the AI to navigate to
    public Transform frontDoorPosition;
    public Transform diamondPosition;
    public Transform backDoorPosition;
    public Transform vanPosition;

    private void Start()
    {
        behaviorTree = gameObject.AddComponent<BehaviorTree>();
        agent = GetComponent<NavMeshAgent>();

        // Define Leaf nodes with actions
        Leaf goToFrontDoor = new Leaf("Go to Front Door", GoToFrontDoor);
        Leaf goToDiamond = new Leaf("Go to Diamond", GoToDiamond);
        Leaf goToBackDoor = new Leaf("Go to Back Door", GoToBackDoor);
        Leaf goToVan = new Leaf("Go to Back Van", GoToBackVan);

        // Create a sequence to handle the AI's behavior
        Sequence stealDiamondSequence = new Sequence("Steal Diamond Sequence");
        stealDiamondSequence.AddChild(goToFrontDoor);
        stealDiamondSequence.AddChild(goToDiamond);
        stealDiamondSequence.AddChild(goToBackDoor);
        stealDiamondSequence.AddChild(goToVan);

        // Set the root node for the behavior tree
        behaviorTree.SetRootNode(stealDiamondSequence);
    }

   

    private void Update()
    {
        // Only process the behavior tree if the behavior is not complete
        if (!behaviorComplete)
        {
            Node.Status status = behaviorTree.Process();

            // Mark behavior as complete once all nodes are processed successfully
            if (status == Node.Status.Success)
            {
                behaviorComplete = true;
                Debug.Log("Behavior completed. AI has reached the back door.");
            }
        }
    }

    // Actions return status based on success, failure, or running
    private Node.Status GoToFrontDoor()
    {
        return MoveToPosition(frontDoorPosition.position);
    }

    private Node.Status GoToDiamond()
    {
        return MoveToPosition(diamondPosition.position);
    }

    private Node.Status GoToBackDoor()
    {
        return MoveToPosition(backDoorPosition.position);
    }

    private Node.Status GoToBackVan()
    {
        return MoveToPosition(vanPosition.position);
    }

    // Move the AI to the given position using NavMeshAgent
    private Node.Status MoveToPosition(Vector3 targetPosition)
    {
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component missing from the AI.");
            return Node.Status.Failure;
        }

        // Set the agent's destination
        if (agent.destination != targetPosition)
        {
            agent.SetDestination(targetPosition);
        }

        // Check if the AI has reached the destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                Debug.Log("Reached destination: " + targetPosition);
                return Node.Status.Success;
            }
        }

        return Node.Status.Running;
    }
}
