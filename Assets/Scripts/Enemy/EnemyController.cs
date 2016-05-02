using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public int speed = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var transform = gameObject.GetComponent<Transform>();
		transform.Translate(Vector3.right * speed * Time.deltaTime);
		Debug.Log (transform.position.x);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("player")) {
			var rb2d = gameObject.GetComponent<Rigidbody2D> ();
			rb2d.isKinematic = false;
		}
	}
}
