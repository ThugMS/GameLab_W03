using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, ISwitchConnectedObjects
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [SerializeField] private bool m_isSwitchConnected= false;
    [SerializeField] private Transform m_door;
    [SerializeField] private Transform m_effect;
    [SerializeField] private string direction;
    #endregion
    #region PublicMethod
    public void InteractStart()
    {
        if (!m_isSwitchConnected)
        {
            Debug.Log("door");

            if (m_door != null)
            {
                RotateDoor();
            }
            else
            {
                Debug.Log("No child object 'door' found.");
            }
        }
    }
    #endregion
    #region PrivateMethod
    private void Start()
    {
        m_door = transform.Find("door");
        m_effect = transform.Find("effect");
        m_effect.gameObject.SetActive(false);
    }

    private void RotateDoor()
    {
        Tween tween = m_door.DOLocalRotate(new Vector3(0f, 100f, 0f), 1.0f);
        m_effect.localScale = Vector3.zero; 
        m_effect.gameObject.SetActive(true);
        m_effect.DOScale(new Vector3(1f,1f,1f),2.0f);
        tween.WaitForCompletion();
        m_isSwitchConnected = true;
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && m_isSwitchConnected)
        {
            LoadSceneManager.s_Instance.ChangeScene(direction);
        }
    }

}