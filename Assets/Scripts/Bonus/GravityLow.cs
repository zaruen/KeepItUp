using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BigBall : BonusBallController
{
	protected override IEnumerator ApplyEffect(GameObject gameObj){
		Debug.Log ("Low grav");
		//var rb2d = ball.GetComponent<Rigidbody2D> ();
		//rb2d.gravityScale = 0.5f;
		//ball.yForceDirection = 300;
		yield return new WaitForSeconds (effectDuration);
		Debug.Log ("End low grav");
		isEffectOn = false;
		Destroy (this.gameObject);
	}
}

