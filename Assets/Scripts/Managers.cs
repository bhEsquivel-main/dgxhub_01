//-------------------------------//
//		AUTHOR: BurN Esquivel    //
//		Date: October 19, 2017   //
//								 //
//-------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{


	private static Managers instance = null;
	private SoundManager sm;

	public static Managers Instance
	{
		get
		{
			return instance;
		}
	}

	private void Awake()
	{
		// if the singleton hasn't been initialized yet
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}

		instance = this;
		DontDestroyOnLoad(this.gameObject);

        Reference();
		SymbolLoader.instance.Initialise();
	}


	void Reference(){
		sm = GetComponent<SoundManager>();
		sm.Initialize();
	}

	public SoundManager SoundMgr
	{ 
		get
		{
			return sm;
		}
	}


	void Start(){
		//initialize
		UILoader.SetActive(true);
	}





}
