using UnityEngine;
namespace TodMopel
{
	public abstract class NavigationAction : ScriptableObject
	{
		public abstract void OnSelectionDo(GameObject navItem);
		public abstract void OnUnselectionDo(GameObject navItem);
		public abstract void OnActionnDo(GameObject navItem);
	}
}

//CODE À METTRE DANS LES SCRIPTS QUI HÉRITENT DE CET OBJET SCRIPTÉ AFIN D'Y METTRE LES ACTIONS PERSONNALISÉES

//namespace TodMopel {
	//[CreateAssetMenu(fileName = "ClassName NavigationAction", menuName = "Tod Unity ToolBox/UI/Navigation/NavigationAction/ClassName", order = 0)]
	//public class ClassName : NavigationAction
	//{
	//	public override void OnSelectionDo(GameObject navItem)
	//	{
	//	}
	//	public override void OnUnselectionDo(GameObject navItem)
	//	{
	//	}
	//	public override void OnActionnDo(GameObject navItem)
	//	{
	//	}
	//}
//}