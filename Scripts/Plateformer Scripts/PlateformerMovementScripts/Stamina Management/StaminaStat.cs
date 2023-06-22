using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
	[CreateAssetMenu(fileName = "New StaminaStat", menuName = "Tod Unity ToolBox/Movements/StaminaStat", order = 1)]
    public class StaminaStat : ScriptableObject
    {
        public FloatVariable StaminaValue;
        public FloatVariable currentStaminaValue;

        public float lastStandStaminaValue => StaminaValue.value * .2f;
    }
}