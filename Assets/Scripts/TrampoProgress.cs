using UnityEngine;
using System.Collections;

public class TrampoProgress : MonoBehaviour {
	public GameObject trampo;

	private float lifeTime = 4f;
	private float timeLeft;
	private Vector3 initialBarScale;


	// Use this for initialization
	void Start () {
		timeLeft = lifeTime;
		initialBarScale = gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			timeLeft = 0;
			Destroy (trampo);
			Destroy (this.gameObject);
		} else {
			var newBarWidthRatio = 1 - ((lifeTime - timeLeft) / lifeTime);
			Debug.Log ("progress: " + newBarWidthRatio);
			gameObject.transform.localScale = new Vector3 (initialBarScale.x * newBarWidthRatio, initialBarScale.y, initialBarScale.z); 
		}
	}
}
