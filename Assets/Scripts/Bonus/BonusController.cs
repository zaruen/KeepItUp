using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
 * Created By Thomas 
 */

public class BonusController : MonoBehaviour {
	
	public Camera cam;
	public screen_game screenController;
	public float timeBetweenCoin;
	public float coinVisibleTime;
	public float bonusDuration;
	[Range(0,1)]
	public float bonusAreaWideness;

    public Ball ball;

	public GameObject bonusBallBig;
	public GameObject bonusBallSmall;
    public GameObject bonusGravityLow;
	public GameObject bonusGravityHigh;

	public GameObject coinPrefab;

	private float maxWidth;
	private float maxHeight;
	private Renderer rend;

	// Use this for initialization
	void Start () {

		if (cam == null) {
			cam = Camera.main;
		}
			
		rend = bonusBallBig.GetComponent<Renderer> ();

		SetBonusArea (rend);

//		Debug.Log ("Start spawning");
		StartCoroutine (SpawnCoin ());
	}

	private void SetBonusArea(Renderer renderer){
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0f);
		Vector3 targetDimension = cam.ScreenToWorldPoint (upperCorner );

		float ballWidth = rend.bounds.extents.x; 
		float ballHeight = rend.bounds.extents.y;

		maxWidth = (targetDimension.x - ballWidth) * bonusAreaWideness;
		maxHeight = (targetDimension.y - ballHeight);
	}

	IEnumerator SpawnBonus(){
		yield return new WaitForSeconds (2.0f);
		//playing = true;
		while (screenController.isPlaying()) {
			Vector3 spawnPosition = new Vector3 (
				Random.Range(-maxWidth, maxWidth), 
				Random.Range(-maxHeight, maxHeight), 
				0f
			);

			Quaternion spawnRotation = Quaternion.identity;

			var bonus = SelectBonusToCreate ();

			var bonusCreated = (GameObject) Instantiate (bonus, spawnPosition, spawnRotation);
		    var bonusCreatedController = bonusCreated.GetComponent<BonusBallController>();
		    bonusCreatedController.ball = ball;

			yield return new WaitForSeconds (Random.Range (coinVisibleTime + timeBetweenCoin, coinVisibleTime + timeBetweenCoin + 10.0f));
		}
	}

	IEnumerator SpawnCoin(){
		//yield return new WaitForSeconds (5.0f);
		while (screenController.isPlaying ()) {
			Vector3 spawnPosition = new Vector3 (
				Random.Range(-maxWidth, maxWidth), 
				Random.Range(-maxHeight, maxHeight), 
				0f
			);

			Quaternion spawnRotation = Quaternion.identity;

			Instantiate (coinPrefab, spawnPosition, spawnRotation);

			yield return new WaitForSeconds (Random.Range (coinVisibleTime + timeBetweenCoin, coinVisibleTime + timeBetweenCoin + 10.0f));

		}
	}

	GameObject SelectBonusToCreate(){
		var random = Random.Range (0, 100);
		Debug.Log ("random " + random);

	    if (random > 25 && random <= 50)
	    {
            return bonusGravityHigh;
	    }
		if (random > 50 && random <= 75)
		{
			return bonusGravityLow;
		}
		if (random <= 25) {
            return bonusBallBig;
		}
		if (random > 75) {
            return bonusBallSmall;
		}

		return bonusBallBig;
	}
}