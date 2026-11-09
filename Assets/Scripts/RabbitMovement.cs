using System.Collections;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    public Transform lanternParent;
    public float moveSpeed = 2f;
    public float waitTime = 2f;
    public bool loopLanterns;

    private Transform[] lanterns;
    private int currentLantern;
    private bool isWaiting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lanterns = new Transform[lanternParent.childCount];
        for(int i = 0; i < lanternParent.childCount; i++)
        {
            lanterns[i] = lanternParent.GetChild(i);
        }
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
        Transform target = lanterns[currentLantern];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position,target.position) < 1f)
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
