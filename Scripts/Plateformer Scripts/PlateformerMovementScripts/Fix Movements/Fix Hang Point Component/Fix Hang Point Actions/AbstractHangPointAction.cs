using UnityEngine;

namespace TodMopel
{
    public abstract class AbstractHangPointAction : ScriptableObject
    {
		[HideInInspector] public FixHangPointBehaviors fixHangPointComponent;
        public abstract void OnStartConnectionDo(GameObject HangedObject);
        public abstract void OnStayConnectedDo(GameObject HangedObject);
        public abstract void OnDisconnectDo(GameObject HangedObject);
        public static FixHangPointBehaviors GetFixHangPointComponent(GameObject HangedObject)
        {
            return HangedObject.GetComponent<FixHangPointBehaviors>();
        }
    }
}

//CODE À METTRE DANS LES SCRIPTS QUI HÉRITENT DE CET OBJET SCRIPTÉ AFIN D'Y METTRE LES ACTIONS PERSONNALISÉES

//[CreateAssetMenu(fileName = "ClassName FixHangPointAction", menuName = "Tod Unity ToolBox/Movements/FixMovement/FixHangPoint Actions/ClassName", order = 0)]
//public class ClassName : AbstractHangPointAction
//{
//	public override void OnStartConnectionDo(GameObject HangedObject)
//	{
//		fixHangPointComponent = GetFixHangPointComponent(HangedObject);
//	}
//	public override void OnStayConnectedDo(GameObject HangedObject)
//	{
//		fixHangPointComponent = GetFixHangPointComponent(HangedObject);
//	}
//	public override void OnDisconnectDo(GameObject HangedObject)
//	{
//		fixHangPointComponent = GetFixHangPointComponent(HangedObject);
//	}
//}