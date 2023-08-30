using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeInfluenced : MonoBehaviour
{
	#region PublicVariables
	#endregion
	public int m_objectTime
	{
		get
		{
			return m_time;
		}
		set
		{
			m_time = value;
			UpdateTimeState();
		}
	}
	public abstract void UpdateTimeState();

	public void PlusTime()
	{
		++m_objectTime;
	}
	public void MinusTime()
	{
		--m_objectTime;
	}
	#region PrivateVariables

	private int m_time;
	#endregion
	#region PublicMethod
	#endregion
	#region PrivateMethod
	#endregion
}
