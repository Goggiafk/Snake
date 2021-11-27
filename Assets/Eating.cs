using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    public SnakeTail componentSnakeTail;
    public SpawnFood foodController;
    public GameObject tailPart;
    private void OnTriggerEnter(Collider other)
    {
            
        switch (other.gameObject.tag)
        {
            

            case "emptyApple":
                Destroy(other);
                break;

            case "food":
                Destroy(other.gameObject);
                foodController.SpawnApple();
                componentSnakeTail.AddCircle();
                componentSnakeTail.AddCircle();
                componentSnakeTail.AddCircle();
                componentSnakeTail.AddCircle();
                componentSnakeTail.AddCircle();
                break;
           
        }
    }
}
