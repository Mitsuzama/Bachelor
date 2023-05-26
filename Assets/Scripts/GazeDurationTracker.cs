using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Item;
using Logger;


public class GazeDurationTracker : MonoBehaviour
{
    private ItemInfo itemInfo;
    int currentStatus = Status.NONE;

    [Tooltip("timpul de privire a unui produs")]
    public float gazeDuration = 0f;

    [Tooltip("timpul necesar pentru relevanta")]
    public float requiredGazeDuration = 3f;

    public UnityEvent gazeDurationIsMeaningful;

    void Start()
    {
        itemInfo = GetComponent<ItemInfo>();

        if(itemInfo == null)
        {
            Debug.Log("I can not access the item information for" + gameObject.name);
        }
        //gazeDurationIsMeaningful.Add
    }

    void Update()
    {
        XRBaseInteractor interactor = GetComponentInParent<XRBaseInteractor>();
        if (GetComponent<XRBaseInteractable>().isSelected)
        {
            gazeDuration += Time.deltaTime;
            if (gazeDuration >= requiredGazeDuration)
            {
                //Debug.Log("AM INTRAT IN : (gazeDuration >= requiredGazeDuration)");
                GazeCompleted();
            }
        }
    }


    void GazeCompleted()
    {
        Debug.Log("AM INTRAT IN :GazeCompleted");
        if (currentStatus == Status.NONE && itemInfo.ItemInCart == false)
        {
            currentStatus = Status.TEMPTATION;
            DataLogger.SaveEventsToJson(currentStatus, gazeDuration, itemInfo);
        }
        else if (currentStatus == Status.TEMPTATION && itemInfo.ItemInCart == true)
        {
            currentStatus = Status.BUY;
            DataLogger.SaveEventsToJson(currentStatus, gazeDuration, itemInfo);
        }
        else if (currentStatus == Status.TEMPTATION && itemInfo.ItemInCart == false)
        {
            currentStatus = Status.TEMPTATION;
            DataLogger.SaveEventsToJson(currentStatus, gazeDuration, itemInfo);
        }
        else if (currentStatus == Status.BUY && itemInfo.ItemInCart == false)
        {
            currentStatus = Status.TEMPTATION;
            DataLogger.SaveEventsToJson(currentStatus, gazeDuration, itemInfo);
        }
        else if (currentStatus == Status.BUY && itemInfo.ItemInCart == true)
        {
            currentStatus = Status.BUY;
            DataLogger.SaveEventsToJson(currentStatus, gazeDuration, itemInfo);
        }
    }
}