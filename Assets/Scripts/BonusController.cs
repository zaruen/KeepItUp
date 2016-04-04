using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Created By Thomas 
 */

public class BonusController : MonoBehaviour {
	
	public Camera cam;
	public screen_game screenController;
	public GameObject bonusBall;
	public float timeBetweenBonus;
	public float visibleTime;
	[Range(0,1)]
	public float bonusAreaWideness;

	private float maxWidth;
	private float maxHeight;
	private Renderer rend;

	// Use this for initialization
	void Start () {

		if (cam == null) {
			cam = Camera.main;
		}

		rend = bonusBall.GetComponent<Renderer> ();

		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0f);
		Vector3 targetDimension = cam.ScreenToWorldPoint (upperCorner );

		float ballWidth = rend.bounds.extents.x; 
		float ballHeight = rend.bounds.extents.y;

		maxWidth = (targetDimension.x - ballWidth) * bonusAreaWideness;
		maxHeight = (targetDimension.y - ballHeight);

		Debug.Log ("Start spawning");
		StartCoroutine (Spawn ());
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

			Instantiate (bonusBall, spawnPosition, spawnRotation);

			yield return new WaitForSeconds (Random.Range (visibleTime + timeBetweenBonus, visibleTime + timeBetweenBonus + 10.0f));
			//yield return new WaitForSeconds (Random.Range (1.0f, 2.0f));
		}
	}
}
