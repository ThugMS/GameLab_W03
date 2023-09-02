using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private bool m_isSwitchOn = false;
    [SerializeField] private GameObject m_switchObject;
    [SerializeField] private List<GameObject> switchObjects = new List<GameObject>();
    [SerializeField] private Animator m_animator;
    #endregion

    #region PublicMethod
    public void TurnSwitch()
    {
        if (!m_isSwitchOn)
        {
            m_isSwitchOn = true;
        }
        
        foreach (GameObject obj in switchObjects)
        {
            if (obj != null)
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                }
                obj.GetComponent<ISwitchConnectedObjects>().InteractStart();
            }
        }

        m_animator.SetTrigger("Pressed");
    }
    #endregion

    #region PrivateMethod
    private void Start()
    {
        FindObjectsWithTag(transform, "SwitchObject");
    }

    private void FindObjectsWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                switchObjects.Add(child.gameObject);
            }

            FindObjectsWithTag(child, tag);
        }
    }
    #endregion
}