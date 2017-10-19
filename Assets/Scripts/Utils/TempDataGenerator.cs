using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.Text;
using System.IO;

public class TempDataGenerator : MonoBehaviour
{

	public static string JSON_FILE_NAME = "";

	public static JSONNode GetConfigForNODE(JSONNode p_node, string jsonFileName)
	{
		JSON_FILE_NAME = jsonFileName;
		string filepath = "";
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			filepath = Application.persistentDataPath + @"/" + JSON_FILE_NAME + ".json";
		else
			filepath = Application.dataPath + @"/Resources/data/" + JSON_FILE_NAME + ".json";


		if (File.Exists(filepath))
		{

			StreamReader streamReader = new StreamReader(filepath);
			string text = "";
			while (!streamReader.EndOfStream)
				text += streamReader.ReadLine();
			streamReader.Close();

			if (text != null)
				p_node = JSONNode.Parse(text);
			else
				p_node = LoadDefault(p_node);
		}
		else
		{
			p_node = LoadDefault(p_node);
		}

		return p_node;
	}

	//Load default
	private static JSONNode LoadDefault(JSONNode p_node)
	{
		TextAsset jsonText = (TextAsset)Resources.Load("data/" + JSON_FILE_NAME, typeof(TextAsset));
		p_node = JSONNode.Parse(jsonText.text);
		return p_node;
	}
}