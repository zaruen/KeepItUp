using UnityEngine;
using System.Collections;

public class CoinsController : MonoBehaviour {

	public screen_game screenGame;
	public int coinValue = 1;

	void Start(){
		var screenGameGameObjects = GameObject.FindGameObjectsWithTag ("screen_game");
		var sg = screenGameGameObjects[0];
		screenGame = sg.GetComponent<screen_game> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("player")) {
			screenGame.recentCoins += coinValue;
			Destroy (this.gameObject);
		}
	}
}
