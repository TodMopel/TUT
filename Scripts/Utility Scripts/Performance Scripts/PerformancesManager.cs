using UnityEngine;

namespace TodMopel
{
    public class PerformancesManager : MonoBehaviour
    {
		public int frameRate = 60;
		public int verticalSyncCount = 0;

		void Awake()
        {
            QualitySettings.vSyncCount = verticalSyncCount;
            Application.targetFrameRate = frameRate;
		}
    }
}