using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallBig : BonusBallController
{
	protected override IEnumerator ApplyEffect(GameObject gameObj){
		Debug.Log ("Double Size");
		gameObj.transform.localScale = new Vector3(2f,2f,0f);
		yield return new WaitForSeconds (effectDuration);
	    if (gameObj != null)
	    {
            gameObj.transform.localScale = new Vector3(1f, 1f, 0f);
            Debug.Log("End Double Size");
            isEffectOn = false;
            Destroy(this.gameObject);
	    }
		
	}
}

