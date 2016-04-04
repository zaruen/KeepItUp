using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BonusBallController : MonoBehaviour
{
	public float visibleTime;
	public Text timerTextPrefab;

	private float timeLeft;
	private Text timerTextInstance;

	// Use this for initialization
	void Start ()
	{
		timeLeft = visibleTime;
		timerTextInstance = Instantiate (timerTextPrefab);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate(){
		
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			timeLeft = 0;
			Destroy (timerTextInstance.gameObject);
			Destroy (this.gameObject);
		}

		UpdateText ();
	}

	void UpdateText(){
		timerTextInstance.text = "Time Left: " + Mathf.RoundToInt (timeLeft);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("player")) {
			Destroy (this.gameObject);
			StartCoroutine (DoubleSize(other.gameObject));
		}
	}

	IEnumerator DoubleSize(GameObject gameObj){
		Debug.Log ("Double Size");
		var initialScale = gameObj.transform.localScale;
		gameObj.transform.localScale = new Vector3(2f,2f,0f);
		yield return new WaitForSeconds (1.0f);
		gameObj.transform.localScale = initialScale;
		Debug.Log ("End Double Size");
	}
}

