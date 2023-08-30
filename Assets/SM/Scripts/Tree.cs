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

	public void Chop()
	{
		if (time.m_objectTime == 1)
		{
			transform.parent.gameObject.SetActive(false);
			//chop
		}
	}

    #endregion
    #region PrivateMethod	
    #endregion
}
