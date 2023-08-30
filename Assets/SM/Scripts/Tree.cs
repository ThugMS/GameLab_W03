using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
	#region PublicVariables
	#endregion
	#region PrivateVariables
    [SerializeField] private TimeInfluenced time;
	#endregion
	#region PublicMethod

	public bool Chop()
	{
		if (time.m_objectTime == 1)
		{
			transform.parent.gameObject.SetActive(false);
			//chop
			return true;
		}
		return false;
	}

    #endregion
    #region PrivateMethod	
    #endregion
}
