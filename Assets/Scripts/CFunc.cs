//-------------------------------//
//		AUTHOR: BurN Esquivel    //
//		Date: October 19, 2017   //
//								 //
//-------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CFunc{

	static public int successCount = 0;
	static public int failedCount = 0;

	static public void SaveScore() {
		PlayerPrefs.SetInt("Success", successCount);
		PlayerPrefs.SetInt("Fails", failedCount);
		PlayerPrefs.Save();
	}

	static public void LoadScore(){
		successCount =  PlayerPrefs.GetInt("Success", 0);
		failedCount =  PlayerPrefs.GetInt("Fails", 0);
	}

	static public EUtil.Symbol lastSymbol = EUtil.Symbol.none;

	static public Transform UIChild = null;

	static public void UIChildAttach(GameObject e)
	{
		if (UIChild == null)
			UIChild = GameObject.Find("UIChild").transform;

		e.transform.parent = UIChild.transform;
		e.transform.localPosition = Vector3.zero; 
	}
}
