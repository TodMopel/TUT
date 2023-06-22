using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "VariableListSaveSystem", menuName = "Tod Unity ToolBox/SaveSystem/SaveVariableList", order = 1)]
	public class VariableListSaveSystem : ScriptableObject
	{
		public string SAVE_NAME = "SaveName";

		public List<FloatVariable> floatStatList;
		public List<BoolVariable> boolStatList;
		public List<StringVariables> stringStatList;

		private class SaveObject
		{
			public List<float> floatList;
			public List<bool> boolList;
			public List<string> stringList;
		}
		SaveObject saveObject;
		public void SaveMyObject()
		{
			saveObject = new SaveObject {
				floatList = getFloatList(floatStatList),
				boolList = getBoolList(boolStatList),
				stringList = getStringList(stringStatList)
			};

			string jsonSave = JsonUtility.ToJson(saveObject);
			SaveManager.Save(SAVE_NAME, jsonSave);
		}

		public void LoadMyObject()
		{
			string jsonSave = SaveManager.Load(SAVE_NAME);
			if (jsonSave != null) {
				saveObject = JsonUtility.FromJson<SaveObject>(jsonSave);
				GetFloatVariableList(saveObject.floatList);
				GetBoolVariableList(saveObject.boolList);
				GetStringVariableList(saveObject.stringList);
			} else {
				SaveMyObject();
			}
		}


		private List<float> getFloatList(List<FloatVariable> list)
		{
			List<float> floatList = new();
			for (int i = 0; i < list.Count; i++) {
				floatList.Add(list[i].value);
			}
			return floatList;
		}
		private List<bool> getBoolList(List<BoolVariable> list)
		{
			List<bool> boolList = new();
			for (int i = 0; i < list.Count; i++) {
				boolList.Add(list[i].value);
			}
			return boolList;
		}
		private List<string> getStringList(List<StringVariables> list)
		{
			List<string> stringList = new();
			for (int i = 0; i < list.Count; i++) {
				stringList.Add(list[i].value);
			}
			return stringList;
		}

		private void GetFloatVariableList(List<float> list)
		{
			for (int i = 0; i < list.Count; i++) {
				floatStatList[i].value = list[i];
			}
		}
		private void GetBoolVariableList(List<bool> list)
		{
			for (int i = 0; i < list.Count; i++) {
				boolStatList[i].value = list[i];
			}
		}
		private void GetStringVariableList(List<string> list)
		{
			for (int i = 0; i < list.Count; i++) {
				stringStatList[i].value = list[i];
			}
		}
	}
}
