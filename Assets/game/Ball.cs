/*
	purpose: class that controls the human player/ball
	
	Game Template
	(c) 2015 Appmobix Ltd
	support@appmobix.com
	www.appmobix.com
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
	Rigidbody2D rb;
	static Ball pthis;
	public AudioSource asource;
	public AudioClip kick, bounce;

	public int xForceDirection;
	public int yForceDirection;
	public int tapTolerance = 1;
	
	void playSound(AudioClip c)
	{
		asource.clip = c;
		asource.Play();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (Lib.debug_skipCollisions)
			return;
	}
	
	public static Ball GetInstance()
	{
		return pthis;
	}
	
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		pthis = this;
		
		rb.AddForce(new Vector2(0f,yForceDirection));
	}
	
	public Vector2 getPosition()
	{
		return rb.position;
	}
	
	public Vector2 getVelocity()
	{
		return rb.velocity;
	}
	
	public void setPlayerVelocity(float dx, float dy)
	{
		rb.velocity = new Vector2(dx,dy);
	}
	
	void FixedUpdate()
	{
		float w=SHARED.SW, h=SHARED.SH;
		
		Vector2 p = rb.position;
		Vector2 v = rb.velocity;
		
		//elastically bounce off the wall
		if (p.x<=-w/2f)	{
			v.x=Mathf.Abs(v.x);
			playSound(bounce);
		}
		if (p.x>=w/2f) {
			v.x=-Mathf.Abs(v.x);
			playSound(bounce);
		}
		rb.velocity = v;
		
		rb.position = p;
		if (p.y<-h/2f)
			screen_game.GetInstance().onPlayerDie();
	}
	
	public void kickMe(Vector2 tappos)
	{
		Rigidbody2D rb2 = Ball.GetInstance().gameObject.GetComponent<Rigidbody2D>();
		
		//ignore if ball travelling upwards
		if (rb2.velocity.y>=0)
			return;
			
		//tap too far
//		Debug.Log ("rb2 position: "+rb2.position);
//		Debug.Log ("distance bwtn tap and rb2: "+Lib.pythag(tappos,rb2.position));
		if (Lib.pythag(tappos,rb2.position)>=0.8f * tapTolerance)
			return;
		
		float dx = -(tappos.x - rb2.position.x) * 100f;
		rb2.AddForce(new Vector2(dx * xForceDirection, yForceDirection * 1f));
		
		screen_game game = screen_game.GetInstance();
		game.doCollision(tappos);
		var score = game.increaseScore(1);

        //if (score % 10 == 0 && xForceDirection <= 5)
        //{
        //    xForceDirection ++;
        //}

		playSound(kick);
	}
}