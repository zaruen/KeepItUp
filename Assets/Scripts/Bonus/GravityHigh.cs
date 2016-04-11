using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GravityHigh : BonusBallController
{
    protected override IEnumerator ApplyEffect(GameObject gameObj){
		Debug.Log ("High grav");

		BonusPunch ();

        var initialGravityScale = rb2d.gravityScale;
        var initialYForce = ball.yForceDirection;

		SetGravity(rb2d, 4.5f, 1500);

        yield return new WaitForSeconds (effectDuration);

		if (rb2d != null) {
			SetGravity(rb2d, initialGravityScale, initialYForce);

			BonusPunch ();

			isEffectOn = false;
			Destroy (this.gameObject);
		}
		Debug.Log ("End high grav");
	}

    private void SetGravity(Rigidbody2D rb2d, float gravityScale, int yForceDirection)
    {
        rb2d.gravityScale = gravityScale;
        ball.yForceDirection = yForceDirection;
    }
}

