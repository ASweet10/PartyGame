using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Maze : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform destination;
    public float speed = 10f;
    private bool readyToExecute = true;
    private bool wallToRight = false;
    private bool done = false;

    private void Update() 
    {
        if(!done)
        {
            NavigateMaze();
        }    
    }

    private void NavigateMaze()
    {
        float distance = Vector3.Distance(destination.position, transform.position);
        if(distance > 0.1f)
        {
            transform.LookAt(destination);
            Vector3 movement = transform.forward * Time.deltaTime * speed;
            agent.Move(movement);
        }
        /*
        if(readyToExecute)
        {
            CheckSensors();
            readyToExecute = false;
        }
        */
    }

    private void CheckSensors()
    {

    }
}
