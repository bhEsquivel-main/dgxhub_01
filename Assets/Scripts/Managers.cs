//-------------------------------//
//		AUTHOR: BurN Esquivel    //
//		Date: October 19, 2017   //
//								 //
//-------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour {

	void Awake() {

		//initialize data
		SymbolLoader.instance.Initialise();

	}



	void Start(){
		//initialize
		UILoader.SetActive(true);
	}




}
