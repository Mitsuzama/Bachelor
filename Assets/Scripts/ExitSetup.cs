using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ExitSetup : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Stores the button toggle used to enable strafing movement.")]
    XRPushButton m_ExitButton;
    
    protected void OnEnable()
    {
        
        m_ExitButton.onPress.AddListener(EnableExit);
    }

    protected void OnDisable()
    {
        m_ExitButton.onPress.RemoveListener(EnableExit);
    }

    void EnableExit()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Debug.Log("Application Exit");
    }
}
