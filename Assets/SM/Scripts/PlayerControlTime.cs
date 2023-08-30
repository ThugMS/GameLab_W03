using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlTime : MonoBehaviour
{
	#region PublicVariables
	#endregion
	#region PrivateVariables
	private InputAction inputPlusKey;
	private InputAction inputMinusKey;
	#endregion
	#region PublicMethod
	public void OnMinusTimeKey(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			int timeCount = TimeManager.s_Instance.m_timeCount;
			if (timeCount < 1)
				return;
			TimeManager.s_Instance.MinusTIme();
		}
	}

	public void OnAddTimeKey(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			int timeCount = TimeManager.s_Instance.m_timeCount;
			if (timeCount > 1)
				return;
			TimeManager.s_Instance.PlusTime();
		}
	}


	#endregion
	#region PrivateMethod
	#endregion
}
