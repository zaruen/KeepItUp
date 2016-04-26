using UnityEngine;
using System.Collections;

public class Destructor : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player")) return;
        Destroy(other.gameObject);
    }
}
