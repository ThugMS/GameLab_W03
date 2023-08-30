using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    public static CameraShakeTrigger instance;

    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
    [SerializeField] private float m_shakeTimer;
    #endregion

    #region PublicMethod
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        if(m_shakeTimer > 0)
        {
            m_shakeTimer -= Time.deltaTime;

            if(m_shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
            }
        }
    }

    public void ShakeCamera(float _intensity, float _time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = _intensity;
        m_shakeTimer = _time;
    }
    #endregion

    #region PrivateMethod
    #endregion
}
