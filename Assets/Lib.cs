/*
	purpose: library routines to support C#/mono
	
	Game Template
	(c) 2015 Appmobix Ltd
	support@appmobix.com
	www.appmobix.com
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
//using System.Collections;
//using System.Collections.Generic;

using System;


//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;
//using UnityEngine;
//using System.Collections;

//using UnityEngine;
//using System.Collections;

using System.Collections.Generic;
//using System;

[System.Serializable]
public class TAG {
	public int ivalue;
	public GameObject go;
	public System.Object o;
	public TAG(int i)			{clear();	this.ivalue = i;}
	public TAG(GameObject go)	{clear();	this.go=go;}
	public TAG(System.Object o)	{clear();	this.o=o;}
	void clear() {ivalue=0; go=null; o=null;}
};

//serializable version of Vector2()
[System.Serializable]
public class Vector2_s {
	public float x,y;
	public Vector2_s() {x = y = 0f;}
	public Vector2_s(Vector2 v) {x = v.x;y = v.y;}
	public Vector2 toVector2() { return new Vector2(x,y);}
};

public struct COL {
	public int r,g,b,a;
	public static COL WHITE = new COL(255,255,255);
	public static COL BLACK = new COL(0,0,0);
	public static COL RED = new COL(255,0,0);
	public static COL GREEN = new COL(0,255,0);
	public static COL BLUE = new COL(0,0,255);
	public static COL MAGENTA = new COL(255,0,255);
	public static COL YELLOW = new COL(255,255,0);
	public COL(int r2,int g2,int b2, int a2)
	{
		this.r = r2;
		this.g = g2;
		this.b = b2;
		this.a = a2;
	}
	public COL(int r2,int g2,int b2)
	{
		this.r = r2;
		this.g = g2;
		this.b = b2;
		this.a = 255;
	}
	// /*
		public Color toColor()
		{
			Color c = new Color();
			c.r = r / 255.0f;
			c.g = g / 255.0f;
			c.b = b / 255.0f;
			c.a = a / 255.0f;
			return c;
		}
	// */
};

struct FadeParams {
	public Image image;
	public Text txt;
	public COL c0, c1;
	public float duration;
};

struct MOVEANIMATED_P {
	public RectTransform p;
	public Vector3 p0, p1;
	public float duration;
};

public struct POINT {
	public int x,y;
};

public struct RECT {
	public int left,top;
	public int right,bottom;
};

public class Lib /*: MonoBehaviour*/ {
	
	//public const bool debug = false;
	
public const bool AllowMultiplayerRaces = false;
	
//all values below should be 'false' in release mode
public const bool debug = false;
public const bool debug_skipCollisions = false;	//in
public const bool debug_invincible = false;	//in
	
	
	public static int UNDEFINED_INT = 0;
	public static System.Object UNDEFINED_PTR = null;
	
	//private static MonoBehaviour pmono = null;
	
//public static MonoBehaviour GetMonoObj() {return pmono;}
	
	/*
	public static long Abs(long i)
	{
		return (i<0) ? (-i) : i;
	}
	*/
	
	public static Int64 Abs(Int64 v)
	{
		return v<0 ? (-v) : v;
	}
	
	/*
		public static Int64 Abs64(Int64 i)
	{
		return (i<0) ? (-i) : i;
	}
	*/
	
	public static Int64 Sign(Int64 v)
	{
		return v<0 ? (-1) : 1;
	}
	
	public static int Sign(int v)
	{
		return v<0 ? (-1) : 1;
	}
	
	public static float Sign(float v)
	{
		return v<0f ? (-1) : 1;
	}
	
	public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
	{
return a + (b-a)*t;
		/*
		Vector3 r;
		r.x = Mathf.Lerp(a.x,b.x,t);
		r.y = Mathf.Lerp(a.y,b.y,t);
		r.z = Mathf.Lerp(a.z,b.z,t);
		return r;
		*/
	}
	
	public static Int64 MoveTowards(Int64 current, Int64 target, Int64 maxDelta)
	{
		if (Lib.Abs(target - current) <= maxDelta)
			return target;
		return current + Lib.Sign(target - current) * maxDelta;
	}
	
	public static int MoveTowards(int current, int target, int maxDelta)
	{
		if (Mathf.Abs(target - current) <= maxDelta)
			return target;
		return current + Lib.Sign(target - current) * maxDelta;
	}
	
	public static void Init(MonoBehaviour pmonoObj)
	{
		//pmono = pmonoObj;
	}
	
	public static void DontDestroyOnLoad(UnityEngine.Object o)
	{
		UnityEngine.Object.DontDestroyOnLoad(o);
	}
	
	public static string GetCurrentSceneName()
	{
		return SceneManager.GetActiveScene().name;
	}
	
	//loads the level. returns when level is loaded
	public static void LoadSceneSync(string levelname)
	{
		SceneManager.LoadScene(levelname);
	}
	
	public static void LoadScene(string levelname)
	{
		MonoObj.pmono.StartCoroutine(LoadSceneInternal(levelname));
	}
		
		private static IEnumerator LoadSceneInternal(string levelname)
		{
			AsyncOperation asyncOp = SceneManager.LoadSceneAsync(levelname);
			if (asyncOp==null)
				goto done;
			
			while (!asyncOp.isDone){
				//Debug.Log("Loading: " + asyncOp.progress.ToString());
				yield return null;
			}
			
			//Debug.Log("Done Loading: " + levelname + " " + DateTime.Now);
			
			done:
			yield return null;
		}

	public static void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public static void Fade(Image g, COL c0, COL c1, int duration)
	{
		FadeParams p;
		p.image = g;
		p.txt = null;
		p.c0 = c0;
		p.c1 = c1;
		p.duration = ((float)duration) / 1000.0f;
		MonoObj.pmono.StartCoroutine(Lib.FadeNow(p));
	}
	
	public static void Fade(Text t, COL c0, COL c1, int duration)
	{
		FadeParams p;
		p.image = null;
		p.txt = t;
		p.c0 = c0;
		p.c1 = c1;
		p.duration = ((float)duration) / 1000.0f;
		MonoObj.pmono.StartCoroutine(Lib.FadeNow(p));
	}
	
	static IEnumerator FadeNow(FadeParams p)
	{
		Image go=p.image;
		Text t=p.txt;
		COL c0=p.c0, c1=p.c1;
		float duration=p.duration;
		
		//start colours. 0 to 1.0
		float r=c0.r/255.0f, g=c0.g/255.0f, b=c0.b/255.0f, a=c0.a/255.0f;
		
		float dr,dg,db,da;//deltas:0 to 1.0
		dr = (c1.r-c0.r)/255.0f;
		dg = (c1.g-c0.g)/255.0f;
		db = (c1.b-c0.b)/255.0f;
		da = (c1.a-c0.a)/255.0f;
		
		Color col = new Color();
		//print ("col-start");
		
		float timestart = Time.time;
		float timeend = timestart + duration;
		float timecur, percent;
		while (true) {
			timecur = Time.time;
			if (timecur > timeend)
				break;
			
			percent = (timecur - timestart) / duration;
			col.r = Mathf.Clamp (r + percent*dr, 0.0f, 1.0f);
			col.g = Mathf.Clamp (g + percent*dg, 0.0f, 1.0f);
			col.b = Mathf.Clamp (b + percent*db, 0.0f, 1.0f);
			col.a = Mathf.Clamp (a + percent*da, 0.0f, 1.0f);
			if (go!=null)
				go.color = col;
			else
				t.color = col;
			yield return new WaitForSeconds(0.01f);
		}
		
		//final fix bcos time might have elapsed and fade didnt complete
		if (go!=null)	go.color = c1.toColor();
		if (t!=null)	t.color = c1.toColor();
		
		//print ("col-end" + (Time.time - timestart));
	}

	public static void Move(Image p, Vector3 pos)
	{
		Move(p.rectTransform,pos);
	}
	
	public static void MoveAnimated(Image p, Vector3 pos, int duration)
	{
		if (p==null)
			return;
		RectTransform rt = p.rectTransform; 
		MoveAnimated(rt, rt.position, pos, duration);
	}
	
	public static void MoveAnimated(Image p, Vector3 pos0, Vector3 pos1, int duration)
	{
		MoveAnimated(p.rectTransform,pos0,pos1,duration);
	}
			
	//-------
	public static void Move(RectTransform prop, Vector3 pos)
	{
		prop.position = pos;
	}
	
	public static void MoveAnimated(RectTransform p, Vector3 pos, int duration)
	{
		MoveAnimated(p, p.position, pos, duration);
	}
	
	public static void MoveAnimated(RectTransform p, Vector3 pos0, Vector3 pos1, int duration)
	{
		MOVEANIMATED_P param;
		param.p = p;
		param.p0 = pos0;
		param.p1 = pos1;
		param.duration = ((float)duration)/1000.0f;
		MonoObj.pmono.StartCoroutine(MoveAnimated_Internal(param));
	}
	
	static IEnumerator MoveAnimated_Internal(MOVEANIMATED_P param)
	{
		RectTransform p=param.p;
		Vector3 p0=param.p0, p1=param.p1;
		float duration=param.duration;
		
		
		float dx,dy,dz;//deltas
		dx = p1.x - p0.x;
		dy = p1.y - p0.y;
		dz = p1.z - p0.z;
		
		Vector3 v;
		
		float timestart = Time.time;
		float timeend = timestart + duration;
		float timecur, percent;
		while (true) {
			timecur = Time.time;
			
			percent = Mathf.Min((timecur - timestart) / duration, 1.0f);
			v.x = p0.x + dx * percent;
			v.y = p0.y + dy * percent;
			v.z = p0.z + dz * percent;
			Move(p,v);
			
			if (timecur >= timeend)
				break;
			yield return new WaitForSeconds(0.01f);
		}
		
		//print ("col-end" + (Time.time - timestart));
		yield return null;
	}					
	
	/*
	public static void stubbt(string s)
	{
		Debug.Log("stubbt():: " + s);
	}
	*/
	
	public class HEVENTSYSTEM {
		public EventSystem e;
		public PointerInputModule[] pm;
	};
	
	public static HEVENTSYSTEM EventSystem_AllocHandle()
	{
		EventSystem e = EventSystem.current;
		if (!e) {
			//Debug.Log("bad event");
			return null;
		}
		
		PointerInputModule[] pm = e.GetComponents<PointerInputModule>();
		if (pm==null) {
			//Debug.Log("bad event2");
			return null;
		}
		
		HEVENTSYSTEM r = new HEVENTSYSTEM();
		r.e = e;
		r.pm = pm;
		return r;
	}
	
	//used to enable/disable touch/mouse msg to be sent. useful esp when animating
	public static void EventSystem_EnableAllControls(HEVENTSYSTEM h, bool b=true)
	{
		for (int i=0; i<h.pm.Length; i++)
			h.pm[i].enabled = b;
		h.e.enabled = b;
	}
	
	public static string GetTempPath()
	{
		string p = Application.persistentDataPath;
		return p;
	}
	
    /*
    public static char[] toChar(byte[] b)
    {
        int n = b.Length;
        char[] c = new char[n];
        for (int i=0; i<n; i++)
            c[i] = (char) b[i];
        return c;
    }
    // */
    
    public static string ConvToString(byte[] b)
    {
        int n = b.Length;
        string s = "";
        for (int i=0; i<n; i++)
            s = s + ((char) b[i]).ToString();
        return s;
    }
    
	public static string ConvToString(Vector3 v)
	{
		return	"{" +
				v.x.ToString() + ", " +
				v.y.ToString() + ", " +
				v.z.ToString() +
				"}";
	}
	
    //convert string to byte array
    public static byte[] ConvToByte(string v)
    {
        char[] c = v.ToCharArray();
        int n = c.Length;
        byte[] b = new byte[n];
        
        for (int i=0; i<n; i++)
            b[i] = (byte) c[i];
        
        return b;
    }
    
	const string COMMA_SEP = ",";
	public static Vector3 ConvToVector(string s)
	{
		int i0,i1;
		
		//ensure surrounded by braces
		if (Lib.debug) {
			if (s.Substring(0,1)!="{" || s.Substring(s.Length-1,1)!="}")
				goto err;
		}
		
		i0 = s.IndexOf(COMMA_SEP,0);	if (i0<=0) goto err;	//if not found or found in first char
		i1 = s.IndexOf(COMMA_SEP,i0+1);	if (i1==-1) goto err;
		
		/*
		Vector3 r = new Vector3();
		string rx = s.Substring(1,i0-1);
		string ry = s.Substring(i0+1,i1-i0-1);
		string rz = s.Substring(i1+1,s.Length-i1-2);
		*/
		
		return new Vector3(
			Lib.ConvToFloat(s.Substring(1,i0-1)),
			Lib.ConvToFloat(s.Substring(i0+1,i1-i0-1)),
			Lib.ConvToFloat(s.Substring(i1+1,s.Length-i1-2))
		);
		
	err:
		Debug.LogError("bad vec params");
		return new Vector3(0f,0f,0f);
	}
    
	/*
	public static string ConvNumtoStr(Int64 v)
	{
		return String.Format("{0,9:N0}",v);
	}
	// */
	
	public static string ConvNumtoStrThousandSep(int v)
	{
		return ConvNumtoStrThousandSep((Int64)v);	//lazy!
	}
	
	public static string ConvNumtoStrThousandSep(Int64 v)
	{
		return String.Format("{0:N0}",v);
	}
	
	public static bool IsAlpha(char c)
	{
		return (c>='A' && c<='Z') || (c>='a' && c<='z');
	}
	
	public static bool IsNumeric(char c)
	{
		return (c>='0' && c<='9');
	}
	
	public static bool IsAlphaNumeric(char c)
	{
		return IsAlpha(c) || IsNumeric(c);
	}
	
	public static string Left(string s, int nchars)
	{
		return s.Length<=nchars ? s : s.Substring(0,nchars);
	}
	
	public static float CFLOAT(int i)
	{
		return (float) i;
	}
	
	public static float CFLOAT(float i)
	{
		return i;
	}
	
	public static int CINT(float i)
	{
		return (int) i;
	}
	
	//returns "value" aligned by "align". i.e.
	//ALIGNHIGH(0,4)==0	ALIGNHIGH(1,4)==1	ALIGNHIGH(3,4)==3
	//ALIGNHIGH(4,4)==4	ALIGNHIGH(5,4)==8	ALIGNHIGH(6,4)==8	
	
	//#define ALIGNLOW(value,align) ( ((value)/(align)) * (align) )
	public static int ALIGNLOW(int v, int a)
	{
		return (v / a) * a;
	}
	
	public static float ALIGNLOW(float v, float a)
	{
		return (float) ALIGNLOW((int)v,(int)a);
	}
	
	//#define ALIGNHIGH(value,align) (   (((value) + ((align)-1))/(align)) * (align)   )
	public static int ALIGNHIGH(int v, int a)
	{
		int value=v, align=a;
		return (   (((value) + ((align)-1))/(align)) * (align)   );
	}
	
	public static GameObject GetChild(GameObject g, int i)
	{
		Transform t = g.transform;
		return (i>=t.childCount) ? null : t.GetChild(i).gameObject;
	}
	
	public static GameObject GetChildFirst(GameObject g)
	{
		return GetChild(g,0);
	}
	
	public static GameObject[] GetChildren(GameObject g)
	{
		Transform t = g.transform;
		int n = t.childCount;
		GameObject[] c = new GameObject[n];
		for (int i=0; i<n; i++)
			c[i] = t.GetChild(i).gameObject;
		return c;
	}
	
	public static bool HasChildren(GameObject go)
	{
		//return go.transform.GetChild(0)!=null;	//access vio if no child
		return go.transform.childCount!=0;
	}
	
	/*
	//uses simple <transform.Find> which doesnt search nested gameobjects
	public static GameObject FindChild(GameObject go, string name)
	{
		Transform t = go.transform.Find(name);
		return (t==null) ? null : t.gameObject;
	}
	*/
	
	//search nested gameobjects
	public static GameObject FindChildEx(GameObject go, string name)
	{
		Transform t = go.transform;
		foreach (Transform child in t) {
			GameObject childgo = child.gameObject;
			if (child.name==name)
				return childgo;
			if (!HasChildren(childgo))
				continue;
			GameObject go2 = FindChildEx(childgo,name);
			if (go2!=null)
				return go2;
		}
		return null;
	}
	
	//search only root gos
	public static GameObject FindGameObjectRoot(string goname)
	{
		Transform[] trans = UnityEngine.Object.FindObjectsOfType<Transform>();
		GameObject g;
		
		foreach (Transform t in trans) {
			if (t.parent!=null)
				continue;
			g = t.gameObject;
			if (g.name==goname)
				return g;
		}
		
		return null;
	}
	
	//note: cant return inactive gameobjects. icbb to inv!
	public static GameObject[] GetRootGameObjects()
	{
		Transform[] trans = UnityEngine.Object.FindObjectsOfType<Transform>();
		
		int n=trans.Length;
		GameObject[] r = new GameObject[n];
		int i=0;
		
		foreach (Transform t in trans) {
			if (t.parent==null) {
				r[i]=t.gameObject;
				i++;
			}
		}
		
		Lib.ArrayRedim(ref r, i);
		
		return r;
	}
	
	//note: cant return inactive gameobjects. icbb to inv!
	public static GameObject[] GetGameObjects()
	{
		Transform[] trans = UnityEngine.Object.FindObjectsOfType<Transform>();
		
		int n=trans.Length;
		GameObject[] r = new GameObject[n];
		int i=0;
		
		foreach (Transform t in trans) {
			r[i]=t.gameObject;
			i++;
		}
		
		Lib.ArrayRedim(ref r, i);
		
		return r;
	}
	
	public static int EnforceRange(int a, int b, int c)
	{
		return b<a ? a : (b>c ? c : b);
	}

	public static float EnforceRange(float a, float b, float c)
	{
		return b<a ? a : (b>c ? c : b);
	}
	
	public static bool RangeIsEnforced(int min, int val, int max)
	{
		return val>=min && val<=max;
	}
	
	public static int GetScreenWidth()
	{
		return Screen.width;
	}
	
	public static int GetScreenHeight()
	{
		return Screen.height;
	}
	
	public static AudioClip LoadFromResFolder_AudioClip(string path)
	{
		/*for async, use:
		ResourceRequest request = Resources.LoadAsync(file);
		yield return request;
		AudioClip clip = request.asset as AudioClip;
		*/
		return Resources.Load(path) as AudioClip;
	}
	
	public static GameObject LoadFromResFolder_Prefab(string prefabname)
	{
		UnityEngine.Object res = Resources.Load(prefabname);
		if (!res) {
			Debug.LogError("Couldn't load object " + prefabname);
			return null;
		}
		
		GameObject g = GameObject.Instantiate(res) as GameObject;
		
		return g;
	}
	
	// /*
	public static int GetTickCount()
	{
		DateTime d = DateTime.Now;
		int r=(int) d.Ticks;
		return r;
	}
	// */
	
	public static Int64 GetTickCount64()
	{
		return (Int64) (DateTime.UtcNow.Subtract(new DateTime(1970,1,1))).TotalMilliseconds;
		
		/*
		DateTime d = DateTime.Now;
		return d.Millisecond +
			d.Second * 1000 +
			d.Minute * 1000 * 60 +
			d.Hour * 1000 * 60 * 60;
		
		int r=(int) d.Ticks;	//err
		return r;
		// */
	}
	
	/*
	public static int GetTickCount()
	{
		DateTime d = DateTime.Now;
		
		int r = 0;
		r+=d.Millisecond;
		r+=d.Second*1000;
		r+=d.Minute*60*1000;
		r+=d.Hour*60*60*1000;
		
		return r;
	}
	*/
	
	public static float pythag(Vector3 a, Vector3 b)
	{
		float x = b.x - a.x;
		float y = b.y - a.y;
		float z = b.z - a.z;
		return Mathf.Sqrt(x*x + y*y + z*z);
	}
	
	public static bool ArrayRedim<T>(ref T[] array, int nentries)
	{
		Array.Resize(ref array,nentries);
		return array.Length==nentries;
	}

	public static void ArrayClear<T>(ref T[] arr, int istart, int len)
	{
		for (int i=0; i<len; i++)
			arr[istart+i]=default(T);
	}
	
	//remove the first nElements in an array
	public static void ArrayRemoveFirst<T>(ref T[] arr, int nElements=1)
	{
		int n = arr.Length;
		if (nElements>=n)
			Array.Resize(ref arr,0);
		else {
			for (int i=0; i<n-nElements; i++)
				arr[i] = arr[i+nElements];
			Array.Resize(ref arr,n-nElements);
		}
	}
	
	public static bool ArrayAppendEntry<T>(ref T[] array, ref T entry)
	{
		T[] entries = new T[1];
		entries[0] = entry;
		return ArrayAppendEntries(ref array,ref entries);
	}
	
	public static bool ArrayAppendEntries<T>(ref T[] array, ref T[] entries)
	{
		return ArrayInsert(ref array, ref entries, array.Length);
		/*
		int n0=array.Length, n1=entries.Length;
		int n=n0+n1;
		if (!Lib.ArrayRedim(ref array,n))
			return false;
		for (int i=0; i<n1; i++) {
			array[n0+i] = entries[i];
		}
		return true;
		// */
	}
	
	//left-shift entries (between <ileft,iright> by <nshift>
	static void ArrayShiftLeft<T>(ref T[] arr, int ileft, int iright, int nshift)
	{
		//copy in proper manner
		for (int i=ileft; i<=iright; i++)
			arr[i-nshift] = arr[i];
	}
	
	//right-shift entries (between <ileft,iright> by <nshift>
	static void ArrayShiftRight<T>(ref T[] arr, int ileft, int iright, int nshift)
	{
		//copy in reversed manner
		for (int i=iright; i>=ileft; i--)
			arr[i+nshift] = arr[i];
	}
	
	public static bool ArrayInsert<T>(ref T[] dest, ref T src, int idest)
	{
		T[] src2 = new T[1];
		src2[0] = src;
		return ArrayInsert(ref dest,ref src2,idest);
	}
	
	public static bool ArrayInsert<T>(ref T[] dest, ref T[] src, int idest)
	{
		int nsrc=src.Length;
		int ndest0=dest.Length;
		int ndest1=ndest0+nsrc;
		
		if (idest>ndest0)
			return false;
		
		if (nsrc==0)
			return true;
		
		if (!Lib.ArrayRedim(ref dest,ndest1))
			return false;
		
		//shift entries to right
		ArrayShiftRight(ref dest, /*ileft*/idest, /*iright*/ndest0-1, /*relmove*/nsrc);
		
		//copy src to dest
		for (int i=0; i<nsrc; i++) {
			dest[idest+i]=src[i];
		}
		
		return true;
	}
	
	public static bool ArrayContainsEntry(int[] array, int entry)
	{
		return ArrayGetEntryIndexInt(array,entry)!=-1;
	}
	
	public static int ArrayGetEntryIndexInt(int[] array, int entry)
	{
		//return Array.Find(array,entry);
		int n = array.Length;
		for (int i=0; i<n; i++) {
			if (array[i]==entry)
				return i;
		}
		return -1;
	}
	
	public static int ArrayGetEntryIndex<T>(ref T[] array, ref T entry)
	{
		//return Array.Find(array,entry);
		int n = array.Length;
		for (int i=0; i<n; i++) {
			if (array[i].Equals(entry))
				return i;
		}
		return -1;
	}
	
	static Rand rnd_arr = null;
	public static void ArrayRandomize(ref int[] arr)
	{
		if (rnd_arr==null)
			rnd_arr = Rand.Create(Lib.GetTickCount());
		
		int n = arr.Length;
		for (int i=0; i<n; i++) {
			int i0=i;
			int i1=Math.Abs(rnd_arr.getNextInt()) % n;
			if (i0==i1)
				i1 = (i1 + 1) % n;
			Lib.SWAP(ref arr[i0],ref arr[i1]);
		}
	}
	
	//copy nentries from arr into a new array
	public static T[] ArrayDup<T>(ref T[] arr, int istart, int nentries)
	{
		T[] sub = new T[nentries];
		for (int i=0; i<nentries; i++)
			sub[i] = arr[istart+i];
		return sub;
	}
	
	//copies arrsrc to arrdest. starts writing at idest
	public static void ArrayWrite<T>(ref T[] arrdest, ref T[] arrsrc, int idest)
	{
		int nsrc=arrsrc.Length;
		for (int i=0; i<nsrc; i++)
			arrdest[idest+i] = arrsrc[i];
	}
	
	//fill arrdest with an entry
	public static void ArrayFill<T>(ref T[] arrdest, ref T arrsrc)
	{
		int nsrc=arrdest.Length;
		for (int i=0; i<nsrc; i++)
			arrdest[i] = arrsrc;
	}
	
	//left-roll entries in the array by <nroll>
	public static void ArrayROL<T>(ref T[] arr, int nroll)
	{
		int n = arr.Length;
		
		nroll %= n;
		if (nroll==0)
			return;
		
		//dup the entries that will be replaced by the left-shift
		T[] sub = ArrayDup(ref arr,0,nroll);
		
		int ileft=nroll, iright=n-1, nshift=nroll;
		ArrayShiftLeft(ref arr, ileft, iright, nshift);
		
		//place the the dup entries in their proper place
		ArrayWrite(ref arr, ref sub, n-nroll);
	}
	
	public static void ArrayReverse<T>(ref T[] arr)
	{
		int n = arr.Length, n2=n/2;
		for (int i=0; i<n2; i++)
			Lib.SWAP(ref arr[i], ref arr[n-i-1]);
	}
	
	public static void dump<T>(T[] a)
	{
		int n = a.Length;
		string s = "";
		for (int i=0; i<n; i++)
			s = s + a[i] + " ";
		Debug.Log("len=" + n + ". " + s);
	}
	
	public static GameObject CreateEmpty()
	{
		GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//g.transform.position = new Vector3(0f,0f,0f);
		BoxCollider c = g.GetComponent<BoxCollider>();
		if (c!=null) {
			//UnityEngine.GameObject.Destroy(c);//cant be called in edit mode
			UnityEngine.GameObject.DestroyImmediate(c);//cant destroy components
			//UnityEngine.GameObject.DestroyObject(c);//cant destroy components
		}
		
		MeshRenderer mr = g.GetComponent<MeshRenderer>();
		if (mr!=null)
			UnityEngine.GameObject.DestroyImmediate(mr);
		
		MeshFilter mf = g.GetComponent<MeshFilter>();
		if (mf!=null)
			UnityEngine.GameObject.DestroyImmediate(mf);
			
		return g;
	}
	
	public static GameObject CreateCube(Vector3 pos)
	{
		GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
		g.transform.position = pos;
		BoxCollider c = g.GetComponent<BoxCollider>();
		if (c!=null) {
			//UnityEngine.GameObject.Destroy(c);//cant be called in edit mode
			UnityEngine.GameObject.DestroyImmediate(c);//cant destroy components
			//UnityEngine.GameObject.DestroyObject(c);//cant destroy components
		}
		return g;
	}
	
	/*
	tests whether two angles are near each other
	note: in c#, angles are measured clockwise, with 12 c'clock being angle0 and 11 o'clock approx 350 degs
	code works intelligently.
	considers that when a=350,b=10, then function(350,10,25) ought to return true bcos they are less than 25 degs apart!
	*/
	public static bool AngleIsNear(float a, float b, float maxdifference)
	{
		float c=maxdifference;
		
		if (Mathf.Abs(a-b) <= c)		return true;
		if (Mathf.Abs((a+360f)-b) <= c)	return true;
		if (Mathf.Abs(a-(b+360f)) <= c)	return true;
		
		return false;
	}
	
	public static void SetRotation(Transform t, Vector3 v)
	{
		Quaternion q = t.rotation;
		q.eulerAngles = v;
		t.rotation = q;
	}
	
	public static void SetRotationLocal(Transform t, Vector3 v)
	{
		Quaternion q = t.localRotation;
		q.eulerAngles = v;
		t.localRotation = q;
	}
	
	/*
	//q.ToAngleAxis(out angle, out axis);
	public static void RotateTowards(Transform pthis, Vector3 targetpos, float rotspeed)
	{
		Vector3 relpos = targetpos - pthis.position;
		Quaternion rot = Quaternion.LookRotation(relpos);
		pthis.rotation = Quaternion.Slerp(pthis.rotation, rot, rotspeed);
		//pthis.transform.Translate(0f,0f,0.5f*Time.deltaTime,Space.Self);
	}
	// */
	
	/*
	//rotates <prigidbody> in the x-z plane (birds-eye view) so it faces <target>
	public static void RotateTowards(Rigidbody prigidbody, Vector3 targetpos, float rotspeed)
	{
		//give vector to direction of target
		Vector3 inv = prigidbody.transform.InverseTransformPoint(targetpos);
		
		//calc angle by which to rotate
		float ang = Mathf.Atan2(inv.x,inv.z) * Mathf.Rad2Deg;
		
		//calc rotation velocity for each frame
		Vector3 rotvel = (Vector3.up * ang) * rotspeed;
		
		//calc deltavel. i.e. required - current
		Vector3 deltavel = rotvel - prigidbody.angularVelocity;
		
		//{Force, Acceleration, Impulse, VelocityChange}
		//avoid Impulse bcos it doesnt play well with some colliders
		prigidbody.AddTorque(deltavel, ForceMode.Force);
	}
	// */
	
	public static bool ArrayInsert<T>(ref T[] array, int index, int count=1)
	{
		int n = array.Length;
		int newlen = n+count;
		
		if (count<=0 || index<0 || index>n)
			return false;//bad param, so quit
		
		//create space for it
		Array.Resize(ref array,newlen);
		if (array.Length!=newlen) {
			Array.Resize(ref array,n);//nnbla!
			return false;
		}
		
		int isrc, idst;
		
		//move entries
		isrc=n-1; idst=newlen-1;
		for (int i=0; i<(n-index); i++) {
			array[idst] = array[isrc];
			idst--;
			isrc--;
		}
		
		//clear bad entries/
		idst = index;
		for (int i=0; i<count; i++)
			array[idst+i]=default(T);
		
		return true;
	}
	
	public static void ArrayRemoveEntry<T>(ref T[] arr, ref T entry)
	{
		int i=Lib.ArrayGetEntryIndex(ref arr, ref entry);
		if (i!=-1)
			Lib.ArrayRemove(ref arr,i,1);
	}
	
	public static void ArrayRemove<T>(ref T[] array, int index, int count=1)
	{
		int n = array.Length;
		
		//invalid params
		if (index<0 || index>=n || count<=0)
			return;
		
		//get safe indices
		int ifirst = index;
		int ilast = Math.Min(index + count - 1,n-1);//last entry to be removed
		
		//overwrite entries to be removed
		int nmove = n-ilast-1;	//"(n-1)-(ilast+1)+1"
		int isrc=ilast+1, idst=ifirst;
		for (int i=0; i<nmove; i++) {
			array[idst] = array[isrc];
			isrc++;
			idst++;
		}
		
		//resize it
		int nremoved = ilast-ifirst+1;
		Array.Resize(ref array,n-nremoved);
	}
	
	public static int ConvToInt(string s)
	{
		int r=0;
		//return Convert.ToInt32(s);	//excepts if invalid chars
		return int.TryParse(s,out r) ? r : 0;
	}
	
	public static Int64 ConvToInt64(string s)
	{
		Int64 r=0;
		//return Convert.ToInt64(s);	//excepts if invalid chars
		return Int64.TryParse(s,out r) ? r : 0;
	}
	
	public static float ConvToFloat(string s)
	{
		float r=0;
		//return Convert.ToInt32(s);	//excepts if invalid chars
		return float.TryParse(s,out r) ? r : 0f;
	}
	
	/*
	//assumes s of the form: "{x,y,z}" i.e. "{1.1,2.2,3.3}"
//todo icbb: optimize so uses index and not tokenize(). see OnlineComms.toVector()
	public static Vector3 ConvToVector3(string s)
	{
		string[] s2 = strlist.tokenize(s);
		if (s2.Length!=3)
			return Vector3.zero;
		float x = ConvToFloat(s2[0].Substring(1));
		float y = ConvToFloat(s2[1]);
		float z = ConvToFloat(s2[2].Substring(0,s2[2].Length-1));
		return new Vector3(x,y,z);
	}
	*/
	
	public static float sum(float[] arr)
	{
		int n = arr.Length;
		float r = 0f;
		for (int i=0; i<n; i++)
			r+=arr[i];
		return r;
	}
	
	public static float mean(float[] arr)
	{
		int n = arr.Length;
		return n==0 ? 0.0f : (sum(arr)/(float)n);
	}
	
	public static float standardDeviation(float[] arr)
	{
		int n = arr.Length;
		if (n<=1)
			return 0f;
		
		float m = mean (arr);
		
		float sumMeanDiff = 0f;
		for (int i=0; i<n; i++) {
			sumMeanDiff += Lib.SQR(arr[i] - m);
		}
		
		return Mathf.Sqrt(sumMeanDiff/CFLOAT(n-1));
	}

	public static GameObject GetParent(GameObject g)
	{
		return g.transform.parent.gameObject;
	}
	
	public static void SetParent(GameObject parent, GameObject child)
	{
		if (child==null)
			return;	//avoid crashing unity!
		Transform tparent = (parent==null) ? null : parent.gameObject.transform;
		//child.transform.parent = tparent;	//shows log msg in inspector
		child.transform.SetParent(tparent,true);
	}
	
	public static void SetParent(GameObject parent, GameObject child, bool bPreserveRelativeChildPos)
	{
		if (child==null)
			return;	//avoid crashing unity!
			
		if (!bPreserveRelativeChildPos)
			SetParent(parent,child);
		else {
			Vector3 pos = child.transform.localPosition;
			Quaternion rot = child.transform.localRotation;
			SetParent(parent,child);
			child.transform.localPosition = pos;
			child.transform.localRotation = rot;
		}
	}
	
	public static GameObject GetParentRoot(GameObject g)
	{
		return g.transform.root.gameObject;
	}
	
	public static bool IsOdd(int i)
	{
		return (i % 2)==1;
	}
	
	public static bool IsEven(int i)
	{
		return (i % 2)==0;
	}
	
	public static void Log(string s)
	{
		Debug.Log(s + ".         Time=" + GetTickCount());
	}
	
	public static void Log(float v)
	{
		Log(v.ToString());
	}
	
	public static void Log(Vector3 v)
	{
		Log(v.ToString());
	}
	
	public static void LogError(string s)
	{
		Debug.LogError(s + ".         Time=" + GetTickCount());
	}
	
	public static string RepeatChar(char ch, int len)
	{
		string r = "";
		for (int i=0; i<len; i++)
			r = r + ch;
		return r;
	}
	
    /*
    public static void Swap(ref int x, ref int y)
    {
        int temp = x;
        x = y;
        y = temp;
    }
    // */
    
	public static void SWAP<T>(ref T a, ref T b)
	{
		T c = a;
		a = b;
		b = c;
	}
	
	public static RECT SimplifyRect(RECT rc)
	{
		RECT r = rc;
		if (r.left>r.right)
			SWAP(ref r.left,ref r.right);
		if (r.top>r.bottom)
			SWAP(ref r.top,ref r.bottom);
		return r;
	}
	
	public static bool ISWITHIN(int a, int b, int c)
	{
		return b>=a && b<=c;
	}
	
	public static bool ISWITHIN(float a, float b, float c)
	{
		return b>=a && b<=c;
	}
	
	public static bool ISWITHIN(RECT rc, POINT pt)
	{
		rc = SimplifyRect(rc);
		return ISWITHIN(rc.left,pt.x,rc.right) && ISWITHIN(rc.top,pt.y,rc.bottom);
	}
	
	public static bool IsWithinCircle(Vector2 pos, float radius)
	{
		return ((SQR(pos.x) + SQR(pos.y)) <= SQR(radius));
	}
	
	public static bool IsWithinCircle(Vector2 origin, Vector2 pos, float radius)
	{
		pos.x-=origin.x;
		pos.y-=origin.y;
		return IsWithinCircle(pos,radius);
	}
	
	public static float SQR(float v)
	{
		return v*v;
	}
	
	public static Int64 Max(Int64 a, Int64 b)
	{
		return a>b ? a : b;
	}
	
	//return index of minimum entry
	public static int imin(float a, float b)
	{
		return a<=b ? 0 : 1;
	}
	
	//return index of minimum entry
	public static int imin(ref float[] arr)
	{
		int n=arr.Length;
		float f=arr[0];
		int r=0;
		for (int i=1; i<n; i++) {
			if (f<=arr[i])
				continue;
			r=i;
			f=arr[i];
		}
		return r;
	}
	
	//return index of minimum entry
	public static int imin(float a, float b, float c)
	{
		float[] v = {a,b,c};
		return imin(ref v);
	}
	
	public static bool IsPowerOfTwo(int x)
	{
		return (x & (x - 1)) == 0;
	}
	
	public static float fpart(float v)
	{
		return v - ((float)(int)v);
	}
	
	//returns index of closest object
	public static int Closest(Vector3 origin, Vector3[] objects)
	{
		int n = objects.Length;
		
		//calc distance_sqr
		float[] f = new float[n];
		for (int i=0; i<n; i++)
			f[i] = Vector3.SqrMagnitude(objects[i] - origin);
		
		return Lib.imin(ref f);
	}
	
	public static int Closest(GameObject origin, GameObject[] objects)
	{
		int n = objects.Length;
		Vector3[] v = new Vector3[n];
		for (int i=0; i<n; i++)
			v[i] = objects[i].transform.position;
		return Closest(origin.transform.position, v);
	}
	
	float SinDeg(float deg)
	{
		float rad = deg * Mathf.Deg2Rad;
		return Mathf.Sin(rad);
	}
	
	public static Int64 Min(Int64 a, Int64 b)
	{
		return a<b ? a : b;
	}
	
	public static void DestroyObject(UnityEngine.Object g)
	{
		if (g!=null)
			UnityEngine.GameObject.DestroyObject(g);
	}
	
	public static void DestroyObjectImmediate(UnityEngine.Object g)
	{
		if (g!=null)
			UnityEngine.GameObject.DestroyImmediate(g);
	}
	
	/*
	given a value (500) within a range (300 to 700) i.e. min==300, value=500, max=700,
	a transformation to a new range (3 to 7) will return the value of 5
	*/
	public static float TransformValueToNewRange(
		float oldMin, float value, float oldMax,
		float newMin, float newMax)
	{
		float oldpercent = (value-oldMin) / (oldMax-oldMin);
		return newMin + (newMax - newMin)*oldpercent;
	}
	/*
	public static float TransformValueToNewRange(
		float value, float oldMax,
		float newMax)
	{
		return TransformValueToNewRange(0f,value,oldMax, 0f,newMax);
	}
	*/
	
	public static bool IsRunningWindows()
	{
		#if UNITY_STANDALONE_WIN
		//return Application.isEditor;
		//return Debug.isDebugBuild;
		return true;
		#endif
		return false;
	}
	
	public static bool IsRunningInEditor()
	{
		#if UNITY_EDITOR
			//return Application.isEditor;
			//return Debug.isDebugBuild;
			return true;
		#endif
		return false;
	}
	
	public static void Sleep(int t)
	{
		System.Threading.Thread.Sleep(t);
	}
	
	public static string GetOperatingSystemName()
	{
		return Application.platform.ToString();
	}
	
    //vimp for comms between Drifter C# and iOS code
    public static string ToStringList(string[] strarr, string sep=",")
    {
        int n = strarr.Length;
        string r = "";
        for (int i=0; i<n; i++) {
            r = (i>0) ? (r + sep + strarr[i]) : strarr[i];
        }
        return r;
    }
    
	public static bool GetPinchGestureValue(ref float val)
	{
		if (Input.touchCount!=2)
			return false;
		
		Touch zero = Input.GetTouch(0), one = Input.GetTouch(1);
		
		Vector2 zprevpos = zero.position - zero.deltaPosition;
		Vector2 oprevpos = one.position - one.deltaPosition;
		
		float prevdeltamag = (zprevpos - oprevpos).magnitude;
		float deltamag = (zero.position - one.position).magnitude;
		
		float magdif = prevdeltamag - deltamag;
		
		val = magdif;
		return true;
	}
	
	public static void PurgeMemory()
	{
		for (int i=0; i<2; i++) {
			Resources.UnloadUnusedAssets();
			System.GC.Collect();
		}
	}
	
	public static bool KeyIsDown(KeyCode keycode)
	{
		return Input.GetKey(keycode);	//GetKeyDown
	}
	
	/// <summary>
	/// Resize the attached sprite according to the camera view
	/// </summary>
	/// <param name="keepAspect">bool : if true, the image aspect ratio will be retained</param>
	public static void ScaleToScreen(SpriteRenderer sr, bool keepAspect=false)
	{
		//SpriteRenderer sr = GetComponent<SpriteRenderer>();
		sr.transform.localScale = new Vector3(1, 1, 1);
		// example of a 640x480 sprite
		float width = sr.sprite.bounds.size.x; // 4.80f
		float height = sr.sprite.bounds.size.y; // 6.40f
		// and a 2D camera at 0,0,-10
		float worldScreenHeight = Camera.main.orthographicSize * 2f; // 10f
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width; // 10f
		Vector3 imgScale = new Vector3(1f, 1f, 1f);
		// do we scale according to the image, or do we stretch it?
		if (keepAspect)
		{
			Vector2 ratio = new Vector2(width / height, height / width);
			if ((worldScreenWidth / width) > (worldScreenHeight / height))
			{
				// wider than tall
				imgScale.x = worldScreenWidth / width;
				imgScale.y = imgScale.x * ratio.y;
			}
			else
			{
				// taller than wide
				imgScale.y = worldScreenHeight / height;
				imgScale.x = imgScale.y * ratio.x;
			}
		}
		else
		{
			imgScale.x = worldScreenWidth / width;
			imgScale.y = worldScreenHeight / height;
		}
		// apply change
		sr.transform.localScale = imgScale;
	}
	
};
