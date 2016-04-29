using UnityEngine;
using System.Collections;

public class Destructor : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Desctructor Trigger");
        if (other.gameObject.CompareTag("player")) return;
        Destroy(other.gameObject);
    }
}
