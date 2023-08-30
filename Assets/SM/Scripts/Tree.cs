using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
	#region PublicVariables
	#endregion
	#region PrivateVariables
	private TimeInfluenced time;
	#endregion
	#region PublicMethod

	public void Chop()
	{
		if (time.m_objectTime == 1)
		{
			gameObject.SetActive(false);
			//chop
		}
	}

    #endregion
    #region PrivateMethod
    private void OnEnable()
    {
		TryGetComponent(out time);
    }
    #endregion
}
