using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GravityLow : BonusBallController
{
    protected override IEnumerator ApplyEffect(GameObject gameObj){
		Debug.Log ("Low grav");

		BonusPunch ();
        var initialGravityScale = rb2d.gravityScale;
        var initialYForce = ball.yForceDirection;

		SetGravity(rb2d, 0.5f, 300);

        yield return new WaitForSeconds (effectDuration);

		if (rb2d != null) {
			SetGravity(rb2d, initialGravityScale, initialYForce);
			BonusPunch ();

			Debug.Log ("End low grav");
			isEffectOn = false;
			Destroy (this.gameObject);
		}
	}

    private void SetGravity(Rigidbody2D rb2d, float gravityScale, int yForceDirection)
    {
        rb2d.gravityScale = gravityScale;
        ball.yForceDirection = yForceDirection;
    }
}

