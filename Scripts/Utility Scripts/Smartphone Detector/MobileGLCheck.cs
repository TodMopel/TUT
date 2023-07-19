using System.Runtime.InteropServices;
using UnityEngine;
namespace TodMopel
{
	public class MobileGLCheck : MonoBehaviour
	{
		#region check if mobile on webGL
		[DllImport(dllName: "__Internal")]
		private static extern bool IsMobile();
		public bool isMobile()
		{
#if !UNITY_EDITOR && UNITY_WEBGL
	return IsMobile();
#endif
			return false;
		}
		#endregion
	}
}