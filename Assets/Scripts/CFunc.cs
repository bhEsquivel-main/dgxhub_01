//-------------------------------//
//		AUTHOR: BurN Esquivel    //
//		Date: October 19, 2017   //
//								 //
//-------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CFunc{

	static public Transform UIChild = null;

	static public void UIChildAttach(GameObject e)
	{
		if (UIChild == null)
			UIChild = GameObject.Find("UIChild").transform;

		e.transform.parent = UIChild.transform;
		e.transform.localPosition = Vector3.zero; 
	}
}
