/*
	purpose: pseudo-rand gen, from C
	
	Game Template
	(c) 2015 Appmobix Ltd
	support@appmobix.com
	www.appmobix.com
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System;

public class Rand {
	
	const long tmp_maxrand = 0x7fff;
	
	long holdrand;
	int rand()
	{
		long r = (((holdrand = holdrand * 214013L + 2531011L) >> 16) & 0x7fff);
		return (int) r;
	}
	
	public static Rand Create()
	{
		Rand pthis = new Rand();
		pthis.setSeed(1);
		return pthis;
	}
	
	public static Rand Create(int iseed)
	{
		Rand pthis = new Rand();
		pthis.setSeed(iseed);
		return pthis;
	}
	
	public void setSeed(int i)
	{
		holdrand = i;
	}
	
	/*
	public void setSeedWithTime()
	{
		setSeed(Lib.GetTickCount());
	}
	// */
	
	public int getNextInt()
	{
		//int r = (int) (getNextFloat() * 2147483647.0f);
		int r = rand ();
		if (r<0)
			r=-r;//vimp fix!
		return r;
	}
	
	//return 0<=value<=maxv-1
	public int getNextInt(int maxv)
	{
		return getNextInt(0,maxv-1);
	}
	
	//return minv<=value<=maxv
	public int getNextInt(int minv, int maxv)
	{
		int range = maxv-minv+1;
		return minv + (getNextInt() % range);
	}

	/*
	public int getNextInt(int a, int b)
	{
		int a0 = Math.Min(a,b);
		int b0 = Math.Max(a,b);
		return (int) (getNextFloat() * 4294967295.0f);
	}
	// */
	
	public bool getNextBool()
	{
		return getNextFloat()>=0.5;
	}
	
	public float getNextFloat()
	{
		//UnityEngine.Random.seed = iseed;
		//float r = UnityEngine.Random.value;
		//iseed = UnityEngine.Random.seed;
		return ((float)rand()) / ((float)tmp_maxrand);
	}
};