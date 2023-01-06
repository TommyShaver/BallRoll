using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Coroutine myCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        myCoroutine = StartCoroutine(myTest(10));
    }

    IEnumerator myTest(int countLimit)
    {
        int i = 0;
        while(i < countLimit)
        {
            i++;
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Finished");
    }
}
