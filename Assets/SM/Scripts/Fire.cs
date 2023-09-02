using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    #endregion
    #region PublicMethod

    #endregion
    #region PrivateMethod
    private void OnCollisionEnter(Collision collision)
    {
        // collision으로 처리할지 좌표값 정리해서 지울지 의논할 것!
        IBurn burnCheck;
        collision.gameObject.TryGetComponent(out burnCheck);
        if (burnCheck != null)
        {
            burnCheck.Burn();
        }
    }
    #endregion
}
