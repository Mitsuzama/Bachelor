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

    // You can quit a game in Unity by calling the Application.Quit function, 
    // which will close a running application. However, while this works to end a built application, 
    // Application Quit is ignored when running the game in Play Mode in the editor.


    void EnableExit()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        // Application.Quit();
        Debug.Log("Application Exit");
    }
}
