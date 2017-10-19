//-------------------------------//
//		AUTHOR: BurN Esquivel    //
//		Date: October 19, 2017   //
//								 //
//-------------------------------//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


public class UIGame : MonoBehaviour
{


	private static UIGame s_inst;

	private UIButton spinBtn;
	private UITexture sampleIMage;
	private UIPopupList popupList;


	private Dictionary<int, Texture2D> textureList = new Dictionary<int,Texture2D>();
	public List<int> randNUmList = new List<int>();
	private List<SymbolModel> symList = new List<SymbolModel>();
	private bool hasPressedSpin = false;
	private float spinDuration = 2, cTime = 0;
	public int currentIndex = 0;
	
	private static UIGame Inst
	{
		get
		{
			if (s_inst == null)
			{
				GameObject _UI = (GameObject)Instantiate(Resources.Load("UI/UIGame", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
				_UI.name = "UIGame";

				CFunc.UIChildAttach(_UI);

				_UI.gameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
				_UI.gameObject.transform.localScale = Vector3.one;
			}
			return s_inst;
		}
	}


	public static bool GetActive()
	{
		if (s_inst == null)
			return false;

		return s_inst.gameObject.activeSelf;
	}

	public static UIGame GetInst()
	{
		return s_inst;
	}

	public static void SetActive(bool bActive)
	{
		UIGame ui = null;

		if (bActive)
		{
			ui = Inst;
		}
		else
		{
			ui = s_inst;
			if (ui)
			{
				if (ui.gameObject.activeSelf)
					ui.gameObject.SetActive(false);
				Destroy(ui.gameObject);
			}

		}
	}

	//----------------------------------------------------------------------//

	void OnEnable()
	{
		s_inst = this;
	}

	//----------------------------------------------------------------------//

	void OnDisable()
	{
		s_inst = null;
		Resources.UnloadUnusedAssets();

	}

	void Awake() {
		spinBtn = transform.FindChild("Bottom").FindChild("spinBtn").GetComponent<UIButton>();
		sampleIMage = transform.FindChild("Center").FindChild("texture_image").GetComponent<UITexture>();
		popupList = transform.FindChild("Bottom").FindChild("listBtn").GetComponent<UIPopupList>();

	}

	void Start() {

		LoadSymbolList();
	}



	void LoadSymbolList() {
		symList = SymbolLoader.instance.GetSymbolList();

		foreach (SymbolModel sm in symList) {
			popupList.items.Add(sm.pName);
			LoadSymbolImages(sm.pID,sm.pName);
			randNUmList.Add(sm.pID);
		}
	}

	void LoadSymbolImages(int pId, string p_name) { 
		var fileName = Application.persistentDataPath + "/" + p_name + ".png";
		var bytes = File.ReadAllBytes(fileName);
		var texture = new Texture2D(250, 250);
		texture.LoadImage( bytes );
		textureList.Add(pId,texture);
	}



	public void OnBtnClick() {

		string btnName = UIButton.current.name;

		switch (btnName) { 
			case "spinBtn":
				Spin();
				break;
			default:
				break;
		}
	}

	void Spin() {
		spinBtn.isEnabled = false;
		cTime = 0;
		hasPressedSpin = true;
		ShuffleTextureList(randNUmList);

	}

	void FixedUpdate() {

		if (hasPressedSpin) {
			if (cTime < spinDuration)
			{
				sampleIMage.mainTexture = textureList[randNUmList[currentIndex]];
				cTime += Time.deltaTime;
			}
			else {
				sampleIMage.mainTexture = textureList[randNUmList[currentIndex]];
                CheckSelectedSpin();
				hasPressedSpin = false;
				spinBtn.isEnabled = true;
			}

			if (hasPressedSpin)
			{
				currentIndex++;
				if (currentIndex == (int)EUtil.Symbol.MAX)
					currentIndex = 0;
			}
		}
	}



	public void ShuffleTextureList(List<int> list)
	{
		int n = list.Count;

		System.Random rnd = new System.Random();
		while (n > 0)
		{
			int k = n - 1;
			k = (rnd.Next(0, n) % n);
			n--;
			int val = list[k];
			list[k] = list[n];
			list[n] = val;

		}
	}


	void CheckSelectedSpin() {
		if (((EUtil.Symbol)randNUmList[currentIndex]).ToString() == popupList.value)
		{
			Debug.Log("SUCCESS");
		}
		else {
			Debug.Log("FAILED");
		}
	}


}