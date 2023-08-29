using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    private Vector3 m_Direction = Vector3.zero;

    [Header("Camera Rotate")]
    [SerializeField] private GameObject m_followTarget;
    [SerializeField] private Vector3 m_look = Vector3.zero;
    [SerializeField] private float m_rotationPower = 3f;
    [SerializeField] private bool m_isGamepad = false;
    [SerializeField] private bool m_isMouse = false;

    #endregion

    #region PublicMethod
    public void OnMovement(InputAction.CallbackContext _context)
    {
        Vector2 input = _context.ReadValue<Vector2>();

        if(input != null)
        {
            if(input != Vector2.zero)
            {
                m_Direction = new Vector3(input.x, 0f, input.y);
            }
        }
    }

    public void OnLook(InputAction.CallbackContext _context)
    {
        CheckInputType(_context.control.device.name);

        if (m_isGamepad == true)
        {
            m_rotationPower = 1;
        }

        if (m_isMouse == true)
        {
            m_rotationPower = 0.1f;
        }

        m_look = _context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        #region shoulderview camera
        {
            m_followTarget.transform.rotation *= Quaternion.AngleAxis(m_look.x * m_rotationPower, Vector3.up);
            m_followTarget.transform.rotation *= Quaternion.AngleAxis(-m_look.y * m_rotationPower, Vector3.right);

            var angles = m_followTarget.transform.localEulerAngles;
            angles.z = 0;

            var angle = m_followTarget.transform.localEulerAngles.x;

            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }

           m_followTarget.transform.localEulerAngles = angles;
        }
        #endregion
    }
    #endregion

    #region PrivateMethod
    private void CheckInputType(string _type)
    {
        if (_type == "Mouse")
        {
            m_isMouse = true;
            m_isGamepad = false;
        }
        else
        {

            m_isGamepad = true;
            m_isMouse = false;
        }
    }
    #endregion
}
