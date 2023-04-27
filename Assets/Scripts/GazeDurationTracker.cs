using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GazeDurationTracker : MonoBehaviour
{
    [Tooltip("Cat timp se uita utiizatorul la un obiect")]
    public float gazeDuration = 0f;

    [Tooltip("Cat timp este nevoie ca sa salvez timpul / sa aiba o insemnatate")]
    public float requiredGazeDuration = 2f;
    void Start()
    {
        
    }

    void Update()
    {
        XRBaseInteractor interactor = GetComponentInParent<XRBaseInteractor>();
        if (GetComponent<XRBaseInteractable>().isSelected)
        {
            gazeDuration += Time.deltaTime;
            if (gazeDuration >= requiredGazeDuration)
            {
                GazeCompleted();
            }
        }
    }

    void GazeCompleted()
    {
        //Debug.Log("Utilizatorul s-a uitat la obiect cat trebuie");
        // aici aduc ce trebuie facut cu timpul asta
    }
}
