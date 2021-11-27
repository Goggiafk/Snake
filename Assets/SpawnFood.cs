using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject notApple;
    public GameObject applePrefab;
    public void SpawnApple()
    {
        GameObject obj = Instantiate(notApple);
        
        obj.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 7.5f, Random.Range(-2.5f, 2.5f));
        StartCoroutine(DelayFunc(obj));
    }

    private IEnumerator DelayFunc(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        if (obj != null)
        {
            GameObject apple = Instantiate(applePrefab);
            apple.transform.position = obj.transform.position;
            Destroy(obj);
        }
        
        
    }

    public void Start()
    {
        SpawnApple();
    }
}
