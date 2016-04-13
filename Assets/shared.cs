/*
	purpose: class to hold shared constants and persistent data
	
	Game Template
	(c) 2015 Appmobix Ltd
	support@appmobix.com
	www.appmobix.com
*/

using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SHARED /*: UnityEngine.Object */{

	public static string path = "saved_data2xx.dat";	//name of file to hold data
	
	//public static string APPNAME = "";
	public static string GAMESCENE = "game";	//scene file
	public static string MENUSCENE = "menu";	//menu file
	
	public static float SW=6f, SH=9.4f;	//screen width/height
	
	[System.Serializable]
	public struct SHARED_DATA {
		public Int64 score, hiscore, coins;
	};
	public SHARED_DATA data;
	
	static SHARED pthis = new SHARED();
	
	bool bHasLoadedOnce = false;
	public static SHARED GetInstance()
	{
		if (!pthis.bHasLoadedOnce)
			pthis.bHasLoadedOnce = pthis.load();
		return pthis;	
	}

	bool bhasprep = false;
	
	//vimp fix for iOS, else serializer wont work!
	void perpareSerializerForIOS()
	{
		if (bhasprep)
			return;
		bhasprep = true;
		if (Application.platform == RuntimePlatform.IPhonePlayer){
			//System.Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
		}
	}
	
	public bool save()
	{
		bool r = true;
		string path2 = Lib.GetTempPath() + "/" + path;
		
		perpareSerializerForIOS();
		
		//open file & save
		BinaryFormatter b = new BinaryFormatter();
		FileStream f = null;
		try {
			f = File.Create(path2);
			b.Serialize(f,data);
		}
		catch (IOException e) {
			//static IOException e2 = e.;
			//r = false;
		}
		
		if (f!=null)
			f.Close();
		
		return r;
	}
	
	bool load()
	{
		bool r = loadInternal();
		if (!r) {
			initWithDefaults();
			r = save();
		}
		return r;
	}
	
	void initWithDefaults()
	{
		data.score = 0;
		data.hiscore = 0;
		data.coins = 0;
	}
		
	bool loadInternal()
	{
		bool r = true;
		string path2 = Lib.GetTempPath() + "/" + path;
		
		if (!File.Exists(path2))
			return false;
		
		perpareSerializerForIOS();
		
		//open file
		BinaryFormatter b = new BinaryFormatter();
		FileStream f = null;
		try {
			f = File.OpenRead(path2);
		}
		catch (IOException e) {
			//e = e;
			r = false;
		}
		
		//avoid exception if size == 0
		if (f.Length == 0)
			r = false;
		
		//load file
		if (r==true)
		try {
			data = (SHARED_DATA) b.Deserialize(f);
		}
		catch (IOException e) {
			r = false;
		}
		
		if (f!=null)
			f.Close();
		
		return r;
	}
	
};
