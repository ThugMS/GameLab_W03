using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    #region PublicVariables
    public GameObject m_target;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    private void FixedUpdate()
    {
        transform.position = m_target.transform.position;
    }
    #endregion

    #region PrivateMethod
    #endregion
}
