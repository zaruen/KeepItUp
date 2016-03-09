/*
	purpose: gameobject so coroutines can be created from static procedures
	
	Game Template
		(c) 2015 Appmobix Ltd
	support@appmobix.com
	www.appmobix.com
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.EventSystems;

using System;

using System.Collections.Generic;

//purp: allow static procs to call mono procs safely.

public class MonoObj : MonoBehaviour {
	
	public static MonoBehaviour pmono;
	
	public static void Create()
	{
		GameObject go = Lib.CreateEmpty();
		go.name = "MonoObj";
		go.AddComponent<MonoObj>();
	}
	
	void Awake()
	{
		pmono = this;
	}
};