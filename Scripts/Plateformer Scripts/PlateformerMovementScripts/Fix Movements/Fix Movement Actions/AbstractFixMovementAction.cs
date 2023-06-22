using UnityEngine;

namespace TodMopel
{
	public abstract class AbstractFixMovementAction : ScriptableObject
	{
		[HideInInspector] public PlateformerFixMovement fixMovementComponent;
		public abstract void OnEnterFixDo(GameObject FixedObject, Vector2 fixPosition);
		public abstract void OnStayFixDo(GameObject FixedObject, Vector2 fixPosition);
		public abstract void OnExitFixDo(GameObject FixedObject, Vector2 fixPosition);
		public static PlateformerFixMovement GetFixMovementComponent(GameObject FixedObject)
		{
			return FixedObject.GetComponent<PlateformerFixMovement>();
		}
	}
}



//CODE À METTRE DANS LES SCRIPTS QUI HÉRITENT DE CET OBJET SCRIPTÉ AFIN D'Y METTRE LES ACTIONS PERSONNALISÉES

//namespace TodMopel
//{
//	[CreateAssetMenu(fileName = "ClassName FixMovementAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixMovement Actions/ClassName", order = 0)]
//public class ClassName : AbstractFixMovementAction
//{
//public override void OnEnterFixDo(GameObject FixedObject, Vector2 fixPosition)
//{
//	fixMovementComponent = GetFixMovementComponent(FixedObject);
//}
//public override void OnStayFixDo(GameObject FixedObject, Vector2 fixPosition)
//{
//	fixMovementComponent = GetFixMovementComponent(FixedObject);
//}
//public override void OnExitFixDo(GameObject FixedObject, Vector2 fixPosition)
//{
//	fixMovementComponent = GetFixMovementComponent(FixedObject);
//}
//}
//}