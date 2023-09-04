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
        if (UIManager.instance.m_isPause == true)
            return;

        if (isChangingTime)
			return;

		if(PlayerManager.instance.m_isGround == false)
		{
			return;
		}

		if (context.performed)
		{
			int timeCount = TimeManager.s_Instance.m_timeCount;
			if (timeCount < 1)
				return;

			isChangingTime = true;
			TimeManager.s_Instance.MinusTIme();
			StartCoroutine(nameof(IE_EnableChangingTime));
		}
	}

	public void OnAddTimeKey(InputAction.CallbackContext context)
	{
		if (UIManager.instance.m_isPause == true)
			return;

		if (isChangingTime)
			return;

        if (PlayerManager.instance.m_isGround == false)
        {
            return;
        }

        if (context.performed)
		{
			
			int timeCount = TimeManager.s_Instance.m_timeCount;
			if (timeCount > 1)
				return;
			isChangingTime = true;
			TimeManager.s_Instance.PlusTime();
			StartCoroutine(nameof(IE_EnableChangingTime));
		}
	}


	#endregion
	#region PrivateMethod
	IEnumerator IE_EnableChangingTime()
	{
		yield return new WaitForSeconds(TimeManager.s_Instance.m_skipTimeLength);
		isChangingTime = false;
	}
	#endregion
}