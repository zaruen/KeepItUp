using UnityEngine;
using System.Collections;

public class CoinsController : MonoBehaviour {

	public screen_game screenGame;
	public int coinValue = 1;
    public float visibleTime = 2;

    public GameObject coinEffect;

    private float timeLeft;

	void Start(){
		var screenGameGameObjects = GameObject.FindGameObjectsWithTag ("screen_game");
		var sg = screenGameGameObjects[0];
		screenGame = sg.GetComponent<screen_game> ();
	    timeLeft = visibleTime;
	}


    void FixedUpdate()
    {

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 )
        {
            timeLeft = 0;
            if (gameObject != null)
            {
                Destroy(this.gameObject);                
            }
        }

    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("player")) {
			screenGame.recentCoins += coinValue;
		    var effect = Instantiate(coinEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(effect, 3.0f);
			Destroy (this.gameObject);
		}
	}
}
