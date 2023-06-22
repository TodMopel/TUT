using System;
using System.Collections;
using UnityEngine;

namespace TodMopel {
	public class PlateformerStaminaManagement : MonoBehaviour
	{
		public AvatarController Controller;

		public StaminaStat StaminaStat;
		public ProgressBarBehavior StaminaBar;

		public FloatVariable numbTapToRecover;
		private float tapValueToRecover;

		public BoolVariable avatarFixed, avatarWalled, avatarStunt, avatarAccrobatic;

		private void Start()
		{
			SetupStaminaValues();
		}

		private void Update()
		{
			UpdateStaminaValues();
			ControlStaminaValues();
		}

		private void UpdateStaminaValues()
		{
			StaminaStat.currentStaminaValue.value = Mathf.Clamp(CollectStaminaValues(), -1, StaminaStat.StaminaValue.value);
			UpdateStaminaBarValue();
		}

		private float CollectStaminaValues()
		{
			float newValue = StaminaStat.currentStaminaValue.value;
			newValue = RemoveStaminaValue(newValue);
			newValue = AddStaminaValue(newValue);
			return newValue;
		}

		private float AddStaminaValue(float newValue)
		{
			bool stateControl = avatarAccrobatic.value && !avatarFixed.value && !avatarStunt.value && !avatarWalled.value;
			if (stateControl && newValue < StaminaStat.StaminaValue.value)
				newValue += Time.deltaTime;
			else if (Controller.onGround && !avatarStunt.value)
				newValue += Time.deltaTime * 3;

			return newValue;
		}

		private float RemoveStaminaValue(float newValue)
		{
			if (avatarFixed.value) {
				if (StaminaStat.currentStaminaValue.value <= StaminaStat.lastStandStaminaValue)
					newValue -= Time.deltaTime * .5f;
				else
					newValue -= Time.deltaTime * .9f;
			}
			if (avatarWalled.value) {
				newValue -= Time.deltaTime * .1f;
			}

			return newValue;
		}

		private void ControlStaminaValues()
		{

			if (StaminaStat.currentStaminaValue.value < StaminaStat.StaminaValue.value)
				StaminaBar.gameObject.SetActive(true);
			else
				StaminaBar.gameObject.SetActive(false); // Start Coroutine fade bar

			if (StaminaStat.currentStaminaValue.value <= 0 && !avatarStunt.value)
				StuntTrigger();
			if (avatarStunt.value)
				StuntBehaviors();
		}

		private void StuntBehaviors()
		{
			if (Controller.canMove)
				Controller.canMove = false;
			if (StaminaStat.currentStaminaValue.value >= StaminaStat.StaminaValue.value)
				StartCoroutine(RecoverStunt());
			else if (Controller.inputActionStart)
				StaminaStat.currentStaminaValue.value += Mathf.Clamp(tapValueToRecover + (StaminaStat.currentStaminaValue.value / 100), -1, StaminaStat.StaminaValue.value);
		}

		private IEnumerator RecoverStunt()
		{
			yield return new WaitForSeconds(.3f);
			avatarStunt.value = false;
			Controller.canMove = true;
		}

		private void StuntTrigger()
		{
			avatarStunt.value = true;
			tapValueToRecover = StaminaStat.StaminaValue.value / numbTapToRecover.value;
		}

		private void UpdateStaminaBarValue()
		{
			StaminaBar.progressBarValues.current.value = StaminaStat.currentStaminaValue.value;
		}

		private void OnEnable()
		{
			StaminaBar.gameObject.SetActive(true);
			SetupStaminaValues();
		}
		private void OnDisable()
		{
			StaminaBar.gameObject.SetActive(false);
		}
		private void SetupStaminaValues() => StaminaStat.currentStaminaValue.value =
							StaminaBar.progressBarValues.maximum = StaminaStat.StaminaValue.value;

	}
}
