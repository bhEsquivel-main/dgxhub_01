//-------------------------------//
//		AUTHOR: BurN Esquivel    //
//		Date: October 19, 2017   //
//								 //
//-------------------------------//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;


public class SymbolModel {

	public int pID;
	public string pName;
	public string pLink;
	public int score;

}



public class SymbolLoader
{

	public Dictionary<string, SymbolModel> symbolDict;

	private bool _init = false;
	public bool isDataLoaded = false;

	private JSONNode node;

	private static SymbolLoader _instance;
	public static SymbolLoader instance
	{
		get { return _instance ?? (_instance = new SymbolLoader()); }
	}

	public void Initialise()
	{
		if (_init)
			return;
		_init = true;
		symbolDict = new Dictionary<string, SymbolModel>();
		LoadInitData();

	}

	private void LoadInitData()
	{

		if (node == null)
			node = TempDataGenerator.GetConfigForNODE(node, "SymbolData");

		for (int i = 0; i < node.Count; i++)
		{
			SymbolModel sm = new SymbolModel();

			sm.pID = int.Parse(node[i]["pID"].ToString().Replace("\"", ""));
			sm.pLink = node[i]["link"].ToString().Replace("\"", "");
			sm.pName = node[i]["name"].ToString().Replace("\"", "");
			sm.score = int.Parse(node[i]["point"].ToString().Replace("\"", ""));

			Add(sm);
		}

		isDataLoaded = true;
		Debug.Log("Symbol Loaded");


	}

	private void Add(SymbolModel p_item)
	{
		if (p_item == null)
			return;
		symbolDict.Add(p_item.pName, p_item);
	}

	public SymbolModel GetByIndex(string p_index)
	{
		if (!symbolDict.ContainsKey(p_index))
			return null;
		return symbolDict[p_index];
	}

	public List<SymbolModel> GetSymbolList()
	{
		List<SymbolModel> list = new List<SymbolModel>();
		foreach (KeyValuePair<string, SymbolModel> tp in symbolDict)
		{
			list.Add(tp.Value);
		}
		return list;
	}

	public int GetSymbolCount() {
		return symbolDict.Count;
	}



}