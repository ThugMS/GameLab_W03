using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour
{
	#region PublicVariables
	#endregion
	#region PrivateVariables
	[SerializeField] private GameObject m_firePlace;
    [SerializeField] private GameObject m_firePrefab;
    [SerializeField] private Transform rayStartTransform;
    private bool m_isInFireRange = false;
    #endregion
    #region PublicMethod
    public bool SetFire()
    {
        if (!m_isInFireRange)
            return false;
        GameObject obj = Instantiate(m_firePrefab, m_firePlace.transform);
        obj.SetActive(true);
        obj.transform.parent = null;
        obj.transform.localScale = Vector3.one;
        return true;
    }
    #endregion
    #region PrivateMethod

    private void OnDisable()
    {
        m_firePlace.SetActive(false);
    }
    private void Update()
    {
        // 레이를 생성하고 방향을 설정합니다.
        float rayRange = 10f;
        Ray ray = new Ray(rayStartTransform.position, rayStartTransform.forward);

        RaycastHit hit;

        m_firePlace.SetActive(false);
        m_isInFireRange = false;
        int layerMask = 1 << LayerMask.NameToLayer("Ground");

        // 레이캐스트를 수행하고 충돌 여부를 확인합니다.
        if (Physics.Raycast(ray, out hit, rayRange, layerMask))
        {
            // 레이의 시작 지점과 충돌 지점을 사용하여 방향 벡터를 계산합니다.
            Vector3 direction = hit.point - ray.origin;

            // 레이의 각도를 계산합니다.
            Vector3 hitNormal = hit.normal;
            /*
            if (angle >= 100f || angle <= 30f)
            {
                Debug.DrawRay(rayStartTransform.position, rayStartTransform.forward * rayRange, Color.red);
                return;
            }
            */

            if (hit.normal.y == 1f)
            {
                float roundedX = (Mathf.Round(hit.point.x / 5f)) * 5f + 2.5f;
                float roundedY = (Mathf.Round(hit.point.y / 5f)) * 5f;
                float roundedZ = (Mathf.Round(hit.point.z / 5f)) * 5f;

                m_firePlace.SetActive(true);
                m_firePlace.transform.position = new Vector3(roundedX, roundedY, roundedZ);
                m_firePlace.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                m_isInFireRange = true;
                Debug.DrawRay(rayStartTransform.position, rayStartTransform.forward * rayRange, Color.green);
            }
        }
        else
        {
            Debug.DrawRay(rayStartTransform.position, rayStartTransform.forward * rayRange, Color.red);
        }
    }
    #endregion
}
