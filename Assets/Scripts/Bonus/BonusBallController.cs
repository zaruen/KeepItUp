using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BonusBallController : MonoBehaviour
{
	public float visibleTime = 2;
	public float effectDuration = 5;
	public Text timerTextPrefab;
	public Ball ball;

	protected Rigidbody2D rb2d;

	private float timeLeft;
	private Text timerTextInstance;
	private Renderer rend;

	protected bool isEffectOn = false;

	// Use this for initialization
	void Start ()
	{
		rb2d = ball.GetComponent<Rigidbody2D>();
		timeLeft = visibleTime;
		timerTextInstance = Instantiate (timerTextPrefab);
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate(){
		
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0 && !isEffectOn) {
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
		if (isEffectOn)
			return;
		if (other.gameObject.CompareTag("player")) {
			rend.enabled = false;
			isEffectOn = true;
			StartCoroutine (ApplyEffect(other.gameObject));
		}
	}

	protected virtual IEnumerator ApplyEffect(GameObject gameObj){
		Debug.Log ("Default ApplyEffect");
		yield return new WaitForSeconds (effectDuration);
		isEffectOn = false;
		Destroy (this.gameObject);
	}

	protected void BonusPunch(){
		if (rb2d.velocity.y >= 0)
			return;
		rb2d.AddForce(new Vector2(0f,ball.yForceDirection));
	}
}

