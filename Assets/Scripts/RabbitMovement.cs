using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RabbitMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float waitTime = 2f;

    private Queue<Transform> objectsToMoveTowards = new Queue<Transform>();
    private bool isWaiting;
    private NavMeshAgent navAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting) 
            return;

        // Move to Lantern
        MoveToObject();
    }

    // put object you want the rabbit to move towards to in this function
    public void AddToMoveQueue(GameObject obj)
    {
        objectsToMoveTowards.Enqueue(obj.transform);
    }
    void MoveToObject()
    {
        if (objectsToMoveTowards.Count == 0)
            return;
        Vector3 currentObject = objectsToMoveTowards.Peek().position;
        navAgent.SetDestination(currentObject);

        //(This code here is for basic movement without AI navigation
        //transform.position = Vector3.MoveTowards(transform.position, 
        //    target // move on x and z axis only
        //    , moveSpeed * Time.deltaTime);

        //find distance (if the lantern is floating then don't calculate y position distance)
        float distance = Vector3.Distance(transform.position, new Vector3(currentObject.x, transform.position.y, currentObject.z));
        if (distance < .1f)
        {
            StartCoroutine(WaitAtObject());
        }
    }

    IEnumerator WaitAtObject()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        objectsToMoveTowards.Dequeue();
        isWaiting = false;
        
    }
}
