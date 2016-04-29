using UnityEngine;
using System.Collections;

public class AdjustPositionAlongRatioScript : MonoBehaviour {

	public float _originalRatio = 5f/4f;

	// Use this for initialization
	void Start () {
	

		if (_originalRatio != 0 && Screen.width != 0)
		{
		this.transform.position = new Vector3
			(
				this.transform.position.x * ((float) Screen.width / Screen.height) / _originalRatio,
				this.transform.position.y,
				this.transform.position.z
				);
		}
	}
}
