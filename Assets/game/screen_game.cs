/*
	purpose: class to manage the game
	
	Game Template
	(c) 2015 Appmobix Ltd
	support@appmobix.com
	www.appmobix.com
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class screen_game : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler  {
	
	public SpriteRenderer rootsprite;
	public Text hiscore, score, coinsCount, newScore, bestScore, totalCoins;
	public Button pausebtn, closebtn;
	public Image gameover, go, paused;
	public GameObject ExplosionPrefab;

    public GameObject endPanel;

    public GameObject endGamePanel;

	public int recentCoins;
	
	enum STATE {
		GAMEOVER,
		PLAYING,
		PAUSED
	};
	STATE state = STATE.PLAYING;
	bool bIntroIsVisible;
	
	bool bHiScore = false;
	
	public AudioSource asource;
	
	public bool isGameOver()	{return state==STATE.GAMEOVER;}
	public bool isPaused()		{return state==STATE.PAUSED;}
	public bool isPlaying()		{return state==STATE.PLAYING;}
	
	static screen_game pthis;
	public static screen_game GetInstance() {return pthis;}
	
	const float TIME_SHOWING_INTRO = 3f;
	
	public GameObject getRootSprite()
	{
		return rootsprite.gameObject;
	}
	
	public void increaseScore(int deltaScore)
	{
		if (isGameOver())
			return;
		SHARED s = SHARED.GetInstance();
		s.data.score += deltaScore;
		updateDisplay();
	}
	
	public void doCollision(Vector3 pos)
	{
		Object explosion = Instantiate(ExplosionPrefab, pos, Quaternion.identity);
        StartCoroutine(releaseExplosionGo(explosion));
        
		//asource.Play();
	}
	
	IEnumerator releaseExplosionGo(Object explosion)
	{
		yield return new WaitForSeconds(1f);
		Lib.DestroyObject(explosion);
	}
	
	public void onPlayerDie()
	{
		SHARED s = SHARED.GetInstance();
		if (s.data.score > s.data.hiscore) {
			s.data.hiscore = s.data.score;
			bHiScore = true;
		}

		s.data.coins += recentCoins;

        DisplayEndPanel();

		if (bHiScore || recentCoins > 0) {
			s.save();
		}
		
		if (Lib.debug_invincible)
			return;
			
		//screen_game.GetInstance().onGameOver();
		Lib.DestroyObject(Ball.GetInstance().gameObject);
		setState_gameOver();
		
		updateDisplay();
		
		//SimpleAdScript.GetInstance().showInterstitialAd();
	}

    private void DisplayEndPanel()
    {
        SHARED s = SHARED.GetInstance();
        newScore.text = Lib.ConvNumtoStrThousandSep(s.data.score);
        bestScore.text = Lib.ConvNumtoStrThousandSep(s.data.hiscore);
        totalCoins.text = Lib.ConvNumtoStrThousandSep(s.data.coins);
        endGamePanel.SetActive(true);

        var rectTransform = coinsCount.GetComponent<RectTransform>();
        Lib.MoveAnimated(rectTransform, rectTransform.position, rectTransform.position + new Vector3(200, 0, 0), 3000);
    }

    Vector3 posstart;
	float timeGameOver;
	void setState_gameOver()
	{
		timeGameOver = Time.timeSinceLevelLoad;
		state = STATE.GAMEOVER;
	}
	
	void setState_togglePause()
	{
		state = isPaused() ? STATE.PLAYING : STATE.PAUSED;
	}
	
	void updateDisplay()
	{
		SHARED s = SHARED.GetInstance();
		hiscore.text = "Hiscore : " + Lib.ConvNumtoStrThousandSep(s.data.hiscore);
		score.text = "Score : " + Lib.ConvNumtoStrThousandSep(s.data.score);
		coinsCount.text = "Coins : " + recentCoins;
		
		bool b = Lib.fpart(Time.timeSinceLevelLoad) >= .4f;
		if (isGameOver())
			score.gameObject.SetActive(bHiScore ? b : true);
		pausebtn.gameObject.SetActive(isPlaying() && !bIntroIsVisible);
		closebtn.gameObject.SetActive(isPaused());
		gameover.gameObject.SetActive( isGameOver());
		paused.gameObject.SetActive(b && isPaused());
		go.gameObject.SetActive(b && isPlaying() && bIntroIsVisible);
	}
	
	public void OnButtonPress_Close()
	{
		Lib.LoadScene(SHARED.MENUSCENE);
	}
	
	Vector2[] pausevelocity;
	public void OnButtonPress_Pause()
	{
		Rigidbody2D[] rbarr = UnityEngine.Object.FindObjectsOfType<Rigidbody2D>();
		int n = rbarr.Length;
		if (!isPaused())
			pausevelocity = new Vector2[n];
		for (int i=0; i<n; i++) {
			if (isPlaying()) {
				pausevelocity[i] = rbarr[i].velocity;
				rbarr[i].velocity = new Vector2(0f,0f);
//				rbarr[i].Sleep();
				rbarr[i].constraints = RigidbodyConstraints2D.FreezePosition;
			}
			else {
//				rbarr[i].WakeUp();
				rbarr[i].constraints = RigidbodyConstraints2D.None;
				rbarr[i].velocity = pausevelocity[i];
			}
		}
		
		setState_togglePause();
		updateDisplay();
	}

    public void OnButtonPress_Play()
    {
        endGamePanel.SetActive(false);
    }
	
	void Awake()
	{
		pthis = this;
	}
	
	// Use this for initialization
	void Start ()
	{
		MonoObj.Create();
		
		Lib.ScaleToScreen(rootsprite);
		
		SHARED s = SHARED.GetInstance();
		s.data.score = 0;
		recentCoins = 0;
		
		bIntroIsVisible = true;
		updateDisplay();
		StartCoroutine(co_updateDisplay());

	}
	
	IEnumerator co_updateDisplay()
	{
		for (float t=Time.timeSinceLevelLoad; t+TIME_SHOWING_INTRO>Time.timeSinceLevelLoad; ) {
			yield return new WaitForSeconds(.1f);
			updateDisplay();
		}
		
		bIntroIsVisible = false;
		updateDisplay();
		
		while (true) {
			yield return new WaitForSeconds(.1f);
			updateDisplay();
		}
	}
	
	void IPointerDownHandler.OnPointerDown(PointerEventData e)
	{
		if (isPaused()) {
			OnButtonPress_Pause();
			return;
		}
		
		if (isGameOver() && (Time.timeSinceLevelLoad - timeGameOver >= 0.8f) ) {
			Lib.ReloadScene();
		}
		
		if (isGameOver())
			return;
			
//		Debug.Log ("raw position: " + e.position);
		Vector2 tappos = transScreenPosToRigidBodySpace(e.position);
//		Debug.Log ("Processed position: " + tappos);
		Ball.GetInstance().kickMe(tappos);
		//doCollision(tappos);
	}
	
	void IPointerUpHandler.OnPointerUp(PointerEventData e)
	{
	}
	
	void IDragHandler.OnDrag(PointerEventData e)
	{
	}
	
	void IBeginDragHandler.OnBeginDrag(PointerEventData e)
	{
	}
	
	void IEndDragHandler.OnEndDrag(PointerEventData e)
	{
	}
	
	Vector2 transScreenPosToRigidBodySpace(Vector2 pos)
	{
		Vector2 v = pos;
		v.x = Lib.TransformValueToNewRange(0f,v.x,Lib.GetScreenWidth(),
		                                   -SHARED.SW/2f, SHARED.SW/2f);
		v.y = Lib.TransformValueToNewRange(0f,v.y,Lib.GetScreenHeight(),
		                                   -SHARED.SH/2f, SHARED.SH/2f);
		return v;
	}
}
