using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConnectedUpBlock : MonoBehaviour, ISwitchConnectedObjects
{
	#region PublicVariables
	#endregion
	#region PrivateVariables
	private bool m_isActed = false;
	[SerializeField] private float m_offset = 5f;

    public void InteractStart()
    {
		UP();
    }
    #endregion
    #region PublicMethod
    public void UP()
	{
		if (m_isActed)
			return;
		m_isActed = true;
		transform.DOLocalMoveY(transform.localPosition.y + m_offset, TimeManager.s_Instance.m_skipTimeLength);
	}
	#endregion
	#region PrivateMethod
	#endregion
}
