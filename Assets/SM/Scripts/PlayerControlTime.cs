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

	private bool isChangingTime = false;
	#endregion
	#region PublicMethod
	public void OnMinusTimeKey(InputAction.CallbackContext context)
	{
		if (isChangingTime)
			return;
		if (context.performed)
		{
			isChangingTime = true;
			int timeCount = TimeManager.s_Instance.m_timeCount;
			if (timeCount < 1)
				return;
			TimeManager.s_Instance.MinusTIme();
			Invoke(nameof(EnableChangeTime), TimeManager.s_Instance.m_skipTimeLength);
		}
	}

	public void OnAddTimeKey(InputAction.CallbackContext context)
	{
		if (isChangingTime)
			return;
		if (context.performed)
		{
			isChangingTime = true;
			int timeCount = TimeManager.s_Instance.m_timeCount;
			if (timeCount > 1)
				return;
			TimeManager.s_Instance.PlusTime();
			Invoke(nameof(EnableChangeTime), TimeManager.s_Instance.m_skipTimeLength);
		}
	}


	#endregion
	#region PrivateMethod
	private void EnableChangeTime()
	{
		isChangingTime = false;
	}
    #endregion
}
