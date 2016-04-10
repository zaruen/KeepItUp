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
	public float timeBetweenBonus;
	public float visibleTime;
	public float bonusDuration;
	[Range(0,1)]
	public float bonusAreaWideness;

	public GameObject bonusBigBall;
	public GameObject bonusSmallBall;

	private float maxWidth;
	private float maxHeight;
	private Renderer rend;

	// Use this for initialization
	void Start () {

		if (cam == null) {
			cam = Camera.main;
		}
			
		rend = bonusBigBall.GetComponent<Renderer> ();

		SetBonusArea (rend);

		Debug.Log ("Start spawning");
		StartCoroutine (Spawn ());
	}

	private void SetBonusArea(Renderer renderer){
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0f);
		Vector3 targetDimension = cam.ScreenToWorldPoint (upperCorner );

		float ballWidth = rend.bounds.extents.x; 
		float ballHeight = rend.bounds.extents.y;

		maxWidth = (targetDimension.x - ballWidth) * bonusAreaWideness;
		maxHeight = (targetDimension.y - ballHeight);
	}

	IEnumerator Spawn(){
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

			Instantiate (bonus, spawnPosition, spawnRotation);

			yield return new WaitForSeconds (Random.Range (visibleTime + timeBetweenBonus, visibleTime + timeBetweenBonus + 10.0f));
			//yield return new WaitForSeconds (Random.Range (1.0f, 2.0f));
		}
	}

	GameObject SelectBonusToCreate(){
		var random = Random.Range (0, 10);
		Debug.Log ("random " + random);

		if (random <= 5) {
			return bonusBigBall;
		}
		if (random > 5) {
			return bonusSmallBall;
		}

		return bonusBigBall;

//		switch (random) {
//			case 0:
//				return bonusBigBall;
//			case 1:
//				return bonusSmallBall;
//			default:
//				return bonusBigBall;
//		}
	}
}
