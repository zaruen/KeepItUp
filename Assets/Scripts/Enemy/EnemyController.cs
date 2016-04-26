using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var transform = gameObject.GetComponent<Transform>();
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(100, 0, 0), 10 * Time.deltaTime);
	}
}
