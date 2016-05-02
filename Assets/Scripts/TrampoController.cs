using UnityEngine;
using System.Collections;

public class TrampoController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("player")) {
			var rb2d = other.gameObject.GetComponent<Rigidbody2D> ();
			var velocity = rb2d.velocity;
			rb2d.velocity = new Vector2 (0, -velocity.y);
		}
	}
}
