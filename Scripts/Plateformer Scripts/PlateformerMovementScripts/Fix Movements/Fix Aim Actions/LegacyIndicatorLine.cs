using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TodMopel
{
    public class LegacyIndicatorLine : MonoBehaviour
    {
		[SerializeField] private string AVATAR_OBJ_NAME = "Hold Object";
        [HideInInspector] public GameObject AvatarObject;
        private LineRenderer AimLine;

        private void Start()
        {
            AvatarObject = GameObject.Find(AVATAR_OBJ_NAME);
            AimLine = GetComponent<LineRenderer>();
        }
        private void Update()
        {
            DrawAim(AvatarObject);
        }
        private void DrawAim(GameObject Object)
        {
            AimLine.SetPosition(0, transform.position);
            if ((Vector2)transform.position + (Vector2)Object.transform.position != Vector2.zero)
                AimLine.SetPosition(1, Object.transform.position);
            else
                AimLine.SetPosition(1, transform.position);
        }
    }
}
