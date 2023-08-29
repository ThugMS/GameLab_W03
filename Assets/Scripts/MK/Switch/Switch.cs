using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField]private bool m_isSwitchOn = false;
    [SerializeField]private GameObject m_switchObject;
    private List<GameObject> switchObjects = new List<GameObject>();
    #endregion

    #region PublicMethod
    public void TurnSwitch(bool isSwitchOn)
    {
        if(!m_isSwitchOn)
        m_isSwitchOn = isSwitchOn;

        foreach (GameObject obj in switchObjects)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
            }
            obj.GetComponent<ISwitchConnectedObjects>().InteractStart();
        }
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