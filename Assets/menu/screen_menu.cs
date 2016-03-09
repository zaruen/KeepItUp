/*
	purpose: class that controls the menu (first screen shown in the game)
	
	Game Template
	(c) 2015 Appmobix Ltd
	support@appmobix.com
	www.appmobix.com
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class screen_menu : MonoBehaviour {
	
	public Image title;
	public Text hiscore/*, score*/;
	public GameObject helpinfo;
	bool bActive = true;
	
	public void OnButtonPress_Play()
	{
		Lib.LoadScene(SHARED.GAMESCENE);
	}
	
	void showHelp(bool b)
	{
		helpinfo.SetActive(b);
		bActive = b;
	}
	
	public void OnButtonPress_HelpButton()
	{
		showHelp(!bActive);
	}
	
	
	float x=0f;
	void Update()
	{
		x += Time.deltaTime * 100f;
		float t = Mathf.Sin(x*Mathf.Deg2Rad);
		t = -Mathf.Abs(t);
		t = 1f + t/3f;
		title.rectTransform.localScale = new Vector2(t,t);
	}
	
	// Use this for initialization
	void Start ()
	{
		MonoObj.Create();
		SHARED s = SHARED.GetInstance();
		hiscore.text = "Hiscore : " + Lib.ConvNumtoStrThousandSep(s.data.hiscore);
		//score.text = "Score : " + Lib.ConvNumtoStrThousandSep(s.data.score);
		showHelp(false);
	}
}
