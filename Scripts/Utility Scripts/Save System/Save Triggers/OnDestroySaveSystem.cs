using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel {
	public class OnDestroySaveSystem : MonoBehaviour
	{
		public VariableListSaveSystem variableList;

		private void Awake()
		{
			SaveManager.Init();
			variableList.LoadMyObject();
		}

		private void OnDestroy()
		{
			variableList.SaveMyObject();
		}
	}
}
