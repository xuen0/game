using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RabbitMovement : MonoBehaviour
{
    public Transform lanternParent;
    public float moveSpeed = 2f;
    public float waitTime = 2f;
    public bool loopLanterns;

    private Transform[] lanterns;
    private int currentLantern;
    private bool isWaiting;
    private NavMeshAgent navAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lanterns = new Transform[lanternParent.childCount];
        for(int i = 0; i < lanternParent.childCount; i++)
        {
            lanterns[i] = lanternParent.GetChild(i);
        }
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting) 
            return;

        // Move to Lantern
        MoveToLantern();
    }

    void MoveToLantern()
    {
        Vector3 target = new Vector3(lanterns[currentLantern].position.x, transform.position.y, lanterns[currentLantern].position.z);
        navAgent.SetDestination(target);
        
        //(This code here is for basic movement without AI navigation
        //transform.position = Vector3.MoveTowards(transform.position, 
        //    target // move on x and z axis only
        //    , moveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position,target) < .1f)
        {
            StartCoroutine(WaitAtLantern());
        }
    }

    IEnumerator WaitAtLantern()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        do
        {
            currentLantern = 
                        loopLanterns ? // if looping is enabled
                        (currentLantern + 1) % lanterns.Length // go to next lantern or return to first lantern if this is last lantern
                        // else
                        : Mathf.Min(currentLantern + 1, lanterns.Length - 1);  // go to next lantern or stop if this is last lantern
        } while (!lanterns[currentLantern].GetComponent<Lantern>().isLightOn);

        isWaiting = false;
        
    }
}
