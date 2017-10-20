using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResult : MonoBehaviour
{

	private static UIResult s_inst;

	private UILabel resultLbl;
	private UIButton repeatBtn;
	private bool isSuccess;

	private static UIResult Inst
	{
		get
		{
			if (s_inst == null)
			{
				GameObject _UI = (GameObject)Instantiate(Resources.Load("UI/UIResult", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
				_UI.name = "UIResult";

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

	public static UIResult GetInst()
	{
		return s_inst;
	}

	public static void SetActive(bool bActive, bool _result = false)
	{
		UIResult ui = null;

		if (bActive)
		{
			ui = Inst;
			ui.isSuccess = _result;
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
		repeatBtn = transform.FindChild("Bottom").FindChild("repeatBtn").GetComponent<UIButton>();
		resultLbl = transform.FindChild("Center").FindChild("resultLbl").GetComponent<UILabel>();
		repeatBtn.gameObject.SetActive(false);
		resultLbl.gameObject.SetActive(false);
	}

	void Start() {
		StartCoroutine(AnimateResult());
	}

	IEnumerator AnimateResult() {
		yield return null;
		resultLbl.gameObject.SetActive(true);
		if (isSuccess)
		{
			resultLbl.text = "YOU WIN!";
		}
		else {
			resultLbl.text = "TRY AGAIN!";
		}


		yield return new WaitForSeconds(0.8f);
		repeatBtn.gameObject.SetActive(true);
	}


	public void OnBtnClick(){


		string btnName = UIButton.current.name;


		switch (btnName) { 
			case "repeatBtn":
				repeatBtn.isEnabled = false;
				StartCoroutine(Repeat());
				break;

		}
	}

	IEnumerator Repeat() {
		yield return new WaitForSeconds(0.3f);
		UIGame.SetActive(true);
		UIResult.SetActive(false);
	}
}

