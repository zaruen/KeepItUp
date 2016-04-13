using UnityEngine;
using System.Collections;

public class CoinsGenerator : MonoBehaviour {
	public screen_game screenController;
	public GameObject coinPrefab;

	// Use this for initialization
	void Start () {
	
	}

	IEnumerator Spawn(){
		yield return new WaitForSeconds (5.0f);
		while (screenController.isPlaying ()) {
		
		}
	}

}
