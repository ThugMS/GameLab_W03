using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region PublicVariables
    public static UIManager instance;

    public bool m_isPause = false;
    #endregion

    #region PrivateVariables
    [SerializeField] private GameObject m_panel;
    
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
        if(m_isPause == true) 
        {
            PlayerManager.instance.SetOnPanel();
            Cursor.visible = true;
        }
        else
        {
            PlayerManager.instance.SetOffPanel();
            Cursor.visible = false;
        }
    }

    public void OnMenu(InputAction.CallbackContext _context)
    {
        if (_context.started == false)
            return;
        Debug.Log("yes");
        if (m_panel.activeSelf == true)
        {
            m_panel.SetActive(false);
            m_isPause = false;
        }
        else
        {
            m_panel.SetActive(true);
            m_isPause = true;
        }
    }

    public void Resume()
    {
        m_panel.SetActive(false);
        m_isPause = false;
    }

    public void Restart()
    {
        m_panel.SetActive(false);
        m_isPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    #endregion

    #region PrivateMethod
    #endregion
}
