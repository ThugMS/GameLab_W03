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
        // collision���� ó������ ��ǥ�� �����ؼ� ������ �ǳ��� ��!
        IBurn burnCheck;
        collision.gameObject.TryGetComponent(out burnCheck);
        if (burnCheck != null)
        {
            burnCheck.Burn();
        }
    }
    #endregion
}
