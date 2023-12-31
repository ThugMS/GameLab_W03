using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeInfluenced : MonoBehaviour
{
	#region PublicVariables
	#endregion
	public int m_ObjectTime
	{
		private get
		{
			return m_objectTime;
		}
		set
		{
			m_objectTime = value;
			UpdateTimeState();
		}
	}
	public abstract void UpdateTimeState();

	public void PlusTime()
	{
		++m_ObjectTime;
	}
	public void MinusTime()
	{
		--m_ObjectTime;
	}

	public void SetStartTime(int _num)
	{
		m_objectTime = _num;
	}

	public int GetObjectTIme()
    {
		return m_ObjectTime;
    }

	#region PrivateVariables

	private int m_objectTime = -1;
    #endregion
    #region PublicMethod
    #endregion
    #region PrivateMethod

    public virtual void Start()
    {
		ObjectManager.s_Instance.AddObject(this);

    }
    #endregion
}

