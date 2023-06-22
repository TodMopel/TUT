using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel {
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "Tod Unity ToolBox/Variables/BoolVariable", order = 1)]
    public class BoolVariable : ScriptableObject
    {
        public bool value;
    }
}