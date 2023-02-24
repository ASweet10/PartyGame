using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class FindPathAStar : MonoBehaviour
{
    public Transform seeker, target;
    GridScript grid;
    public List<GameObject> activePlayers = new List<GameObject>();
    private void Awake() {
        grid = gameObject.GetComponent<GridScript>();
    }
    private void Update() {
        FindPath(seeker.position, target.position);
    }


    public void FindPath(Vector3 start, Vector3 end)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Node startNode = grid.NodeFromWorldPoint(start);
        Node endNode = grid.NodeFromWorldPoint(end);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        //Hash set is like a dictionary but has only keys
        // Used to simply check if item exists or not
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if(currentNode == endNode)
            {
                RetracePath(startNode, endNode);
                sw.Stop();
                if(sw.ElapsedMilliseconds != 0)
                {
                    UnityEngine.Debug.Log("Path found in: " + sw.ElapsedMilliseconds + " ms");
                }
            }

            foreach (Node neighbor in grid.GetNeighbors(currentNode))
            {
                if(!neighbor.walkable || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if(newMovementCostToNeighbor < currentNode.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, endNode);
                    neighbor.parent = currentNode;

                    if(!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
    }

    void RetracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        grid.path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        } else {
            return 14 * distX + 10 * (distY - distX);
        }
    }

    //Consider moving this to game manager script
    // Observer pattern, when player takes trap damage
    // this function is called and this script is simply given a new target
    public Transform ChooseTargetByHealthPercentage()
    {
        float current = 100;
        Transform newTarget = null;
        for(int i = 0; i < activePlayers.Count; i++)
        {
            var health = activePlayers[i].GetComponent<PlayerHealth>().CurrentHealth;
            if(health < current)
            {
                current = health;
                newTarget = activePlayers[i].transform;
            }
        }

        return newTarget;
    }
}
