using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region PublicVariables
    public int m_time = 1;
    #endregion

    #region PrivateVariables
    [Header("Camera Rotate")]
    [SerializeField] protected GameObject m_followTransform;
    [SerializeField] protected Vector2 m_look = Vector2.zero;
    [SerializeField] protected float m_rotationPower = 3f;
    [SerializeField] protected bool m_isGamepad = false;
    [SerializeField] protected bool m_isMouse = false;

    [Header("Move")]
    [SerializeField] protected Vector3 m_Direction = Vector3.zero;
    [SerializeField] protected Quaternion m_nextRotation = Quaternion.identity;
    [SerializeField] protected float m_rotationLerp = 0.8f;
    [SerializeField] protected Rigidbody m_rigidbody;
    [SerializeField] protected float m_speed = 5f;
    [SerializeField] protected bool m_isMove = false;
    #endregion

    #region PublicMethod
    public void OnMovement(InputAction.CallbackContext _context)
    {
        Vector2 input = _context.ReadValue<Vector2>();

        m_Direction = new Vector3(input.x, 0f, input.y);
        m_isMove = true;
    }

    public void OnLook(InputAction.CallbackContext _context)
    {
        CheckInputType(_context.control.device.name);

        if (m_isGamepad == true)
        {
            m_rotationPower = 2;
        }

        if (m_isMouse == true)
        {
            m_rotationPower = 1f;
        }

        m_look = _context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if(m_Direction == Vector3.zero)
        {
            m_isMove = false;
        }
    }

    private void FixedUpdate()
    {
        #region Camera
        {
            m_followTransform.transform.rotation *= Quaternion.AngleAxis(m_look.x * m_rotationPower, Vector3.up);
            m_followTransform.transform.rotation *= Quaternion.AngleAxis(-m_look.y * m_rotationPower, Vector3.right);

            var angles = m_followTransform.transform.localEulerAngles;
            angles.z = 0;

            var angle = m_followTransform.transform.localEulerAngles.x;

            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }

           m_followTransform.transform.localEulerAngles = angles;
        }
        #endregion

        #region Move

        if(m_isMove == true)
        {
                m_nextRotation = Quaternion.Euler(new Vector3(0, m_nextRotation.eulerAngles.y, 0));

                Vector2 movedirection = new Vector2(m_Direction.x, m_Direction.z);
                Vector2 a = new Vector2(0, 1f);
                float angle = Vector2.Angle(a, movedirection);
                if (movedirection.x < 0)
                {
                    angle *= -1f;
                }

                transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, m_nextRotation.eulerAngles.y + angle, 0), transform.rotation, m_rotationLerp);

                Move();   
        }
        else
        {
            m_rigidbody.angularVelocity = new Vector3(0, 0, 0);
            m_rigidbody.velocity = Vector3.zero;
        }
        m_nextRotation = Quaternion.Lerp(m_followTransform.transform.rotation, m_nextRotation, m_rotationLerp);

        
        #endregion
    }

    protected virtual void Move()
    {
        Vector3 moveAmout = transform.forward * m_speed * Time.deltaTime;
        Vector3 nextPosition = m_rigidbody.position + moveAmout;
        m_rigidbody.MovePosition(nextPosition);
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
