using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Item;
using Logger;


public class GazeDurationTracker : MonoBehaviour
{
    private float lastGazeDuration = 0f;
    private ItemInfo itemInfo;
    private int currentStatus = Status.NONE;
    private bool isSameStatus = false;

    [Tooltip("timpul de privire a unui produs")]
    public float gazeDuration = 0f;

    [Tooltip("timpul necesar pentru relevanta")]
    public float requiredGazeDuration = 3f;

    void Start()
    {
        itemInfo = GetComponent<ItemInfo>();

        if(itemInfo == null)
        {
            Debug.Log("I can not access the item information for " + gameObject.name);
        }
    }

    void Update()
    {
        XRBaseInteractor interactor = GetComponentInParent<XRBaseInteractor>();
        if (GetComponent<XRBaseInteractable>().isSelected)
        {
            gazeDuration += Time.deltaTime;
            if(isSameStatus == true && (gazeDuration - lastGazeDuration) < 1f)
            {
                //pass
            }
            else if (gazeDuration >= requiredGazeDuration)
            {
                GazeCompleted();
            }
        }
    }


    void GazeCompleted()
    {
        isSameStatus = true;

        if (currentStatus == Status.NONE)
        {
            currentStatus = Status.TEMPTATION;
            isSameStatus = false;
        }
        else if (currentStatus == Status.BUY && !itemInfo.ItemInCart)
        {
            currentStatus = Status.TEMPTATION;
            isSameStatus = false;
        }
        else if (currentStatus == Status.TEMPTATION)
        {
            if (itemInfo.ItemInCart)
            {
                currentStatus = Status.BUY;
                isSameStatus = false;
            }
        }

        if(!isSameStatus)
        {
            FileCreator.SaveEventsToJson(currentStatus, gazeDuration, itemInfo);
        }
        
        lastGazeDuration = gazeDuration;
    }
}