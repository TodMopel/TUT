using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TodMopel
{
    public class OnBoolVariableChangedSaveSystem : MonoBehaviour
    {
		public VariableListSaveSystem variableList;
		public BoolVariable boolVariable;
		private bool trigger;

		private void Awake()
		{
			trigger = boolVariable.value;
			SaveManager.Init();
			variableList.LoadMyObject();
		}

		private void Update()
		{
			if (trigger != boolVariable.value) {
				variableList.SaveMyObject();
				trigger = boolVariable.value;
			}
		}
	}
}
