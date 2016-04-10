using UnityEngine;
using System.Collections;

public class BallSmall : BonusBallController
{
	protected override IEnumerator ApplyEffect(GameObject gameObj){
		Debug.Log ("Reduce Size");
		gameObj.transform.localScale = new Vector3(0.5f,0.5f,0f);
		yield return new WaitForSeconds (effectDuration);
		gameObj.transform.localScale = new Vector3(1f,1f,0f);
		Debug.Log ("End Reduce Size");
		isEffectOn = false;
		Destroy (this.gameObject);
	}
}

