using UnityEngine;
using System.Collections;

public class TomatoController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.AddForce (new Vector2(200, 250));
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("player")) {
			
		}
	}
}
