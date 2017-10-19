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


public class UILoader : MonoBehaviour {

	private static UILoader s_inst;

	private UITexture progressFore;
	private UILabel progressCount;

	private static UILoader Inst
	{
		get
		{
			if (s_inst == null)
			{
				GameObject _UI = (GameObject)Instantiate(Resources.Load("UI/UILoader", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
				_UI.name = "UILoader";

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

	public static UILoader GetInst()
	{
		return s_inst;	
	}

	public static void SetActive(bool bActive)
	{
		UILoader ui = null;

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
	void Awake(){
		//Reference
		progressFore = transform.FindChild("Bottom").FindChild("progress_fg").GetComponent<UITexture>();
		progressCount = transform.FindChild("Bottom").FindChild("progressCountLbl").GetComponent<UILabel>();

		_loadedSymDict = new Dictionary<EUtil.Symbol, bool>();

		for (int i = 0; i< (int)EUtil.Symbol.MAX; i++)
        {
			_loadedSymDict.Add((EUtil.Symbol)i, false);
        }   
	}

	void Start() {
		WebLoad(currentSymbol);
	}

	private Dictionary<EUtil.Symbol, bool> _loadedSymDict = null;
	EUtil.Symbol currentSymbol = EUtil.Symbol.banana;
	int current;
	void LoadedData() {

		int max = (int)EUtil.Symbol.MAX;

		if (_loadedSymDict.ContainsKey (currentSymbol) && _loadedSymDict [currentSymbol]) {
			++currentSymbol;

			current = (int)currentSymbol;
			float percent = (float)current / (float)max;

			SetProgressValue (percent);
            
			if (currentSymbol == EUtil.Symbol.MAX) {
				//START GAME
				StartCoroutine(GoToMaingame());
			} else {
				WebLoad(currentSymbol);
            }
		} 
	}

	IEnumerator GoToMaingame() {
		yield return new WaitForSeconds(3);
		UIGame.SetActive(true);
		UILoader.SetActive(false);
	}

	void SetProgressValue(float val)
	{
		s_inst.SetValue(val);
	}

	void SetValue(float val)
	{
		newValue = val;

	}
	float oldValue = 0;
	float newValue = 0;
	string text = "";
	void Update() {
		if (newValue > oldValue) {
			float curValue = Mathf.Lerp(oldValue, newValue, Time.deltaTime * 4);
			progressFore.fillAmount = curValue;
			int textVal = (int)(curValue * 100);
			text = string.Format("{0}%", textVal);
			progressCount.text = text;
			oldValue = curValue;
			if (newValue - 0.01f < oldValue)
			{
				oldValue = newValue;
				curValue = newValue;
				progressFore.fillAmount = oldValue;
				textVal = (int)(curValue * 100);
				text = string.Format("{0}%", textVal);
				progressCount.text = text;
			}
		}

	}

	void WebLoad(EUtil.Symbol _sym) {

		SymbolModel symModel = SymbolLoader.instance.GetByIndex(_sym.ToString());

		string localPath = Application.persistentDataPath + "/" + symModel.pName + ".png";
		if (File.Exists(localPath))
		{

             SetLoadedData(_sym, true);
		}
		else
		{
			StartCoroutine(DownloadSymbol(symModel.pLink, symModel.pName, _sym));
		}
	}



	IEnumerator DownloadSymbol(string path, string szName, EUtil.Symbol _sym)
	{
		bool isSuccess = false;
		WWW www = new WWW(path);
		yield return www;

		if (www.error != null)
		{
			Debug.Log("Image Download Error-> " + path + ".." + www.error);
		}

		if (www.isDone)
		{
			Debug.Log("Image Down isDone =>" + path);
			isSuccess = true;
		}


		if (www.bytes.Length > 1)
		{
			string localPath = Application.persistentDataPath + "/" + szName + ".png";

			FileStream fileStream = new FileStream(localPath, FileMode.Create);
			fileStream.Write(www.bytes, 0, www.bytes.Length);
			fileStream.Close();
		}
		

		www.Dispose();
		www = null;

		yield return new WaitForSeconds(0.5f);
		SetLoadedData(_sym, isSuccess);


	}

 	void SetLoadedData(EUtil.Symbol _sym, bool loaded = true)
	{
		if (_loadedSymDict.ContainsKey(_sym))
		{
			_loadedSymDict[_sym] = loaded;
		}
		LoadedData();
	}
	
}


