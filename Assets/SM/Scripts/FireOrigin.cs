using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOrigin : TimeInfluenced
{
	#region PublicVariables
	#endregion
	#region PrivateVariables
	[SerializeField] private List<GameObject> m_fireObjects;
	[SerializeField] private int m_startTime = 1;

	[SerializeField] private int m_Index 
	{
		get
		{
			return m_index;
		}
		set
		{
			m_fireObjects[m_index].SetActive(false);
			m_index = value;
			m_fireObjects[m_index].SetActive(true);
		}
	}

	private int m_index;

	#endregion
	#region PublicMethod
	public override void UpdateTimeState()
	{
		Debug.Log(GetObjectTIme());
		m_Index = GetObjectTIme();
	}
	#endregion
	#region PrivateMethod
	private void OnEnable()
	{
		SetStartTime(m_startTime);
		UpdateTimeState();
		ObjectManager.s_Instance.AddObject(this);
	}
	#endregion
}
