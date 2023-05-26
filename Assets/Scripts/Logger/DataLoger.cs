using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Item;
//using Newtonsoft.Json;

namespace Logger
{
    public class JSONData
    {
        public EventData eventData;
        public int _id;
        public string nume;
        public string descriere;
        public float pret;
        public NutritionalInfo nutritional_info;
        //public List<EventData> vector_actiuni;
    }
    public class EventData
    {
        public string Timestamp { get; set; }
        public string ActionType { get; set; }
        public float ActionDuration { get; set; }
    }

    public static class DataLogger
    {
        public static void SaveEventsToJson(int actionType, float duration, ItemInfo itemInfo)
        {
            string saveFilePath = "SavedEvents.json";
            //string saveFilePath = "Assets/Scripts/Logger/SavedEvents.json";
            JSONData savedData;

            if (File.Exists(saveFilePath))
            {
                savedData = JsonUtility.FromJson<JSONData>(File.ReadAllText(saveFilePath));
                //savedData.vector_actiuni = new List<EventData>();

                bool objectExists = (savedData._id == itemInfo.GetInstanceID() ) && (savedData.nume == itemInfo.ItemName);

                //daca obiectul nu exista -> creez unul
                if (!objectExists)
                {
                    
                    savedData._id = itemInfo.GetInstanceID();
                    savedData.nume = itemInfo.ItemName;
                    savedData.descriere = itemInfo.ItemDescription;
                    savedData.pret = itemInfo.ItemPrice;
                    savedData.nutritional_info = itemInfo.ItemNutritionalInfo;
                }

                EventData newAction = new EventData();
                newAction.ActionType = (actionType == 1) ? "TEMPTATION" : "BUY";
                Debug.Log("ActionType" + newAction.ActionType.ToString());
                newAction.Timestamp = System.DateTime.UtcNow.ToString("yyyy-MM-dd");
                Debug.Log("timestamp" + newAction.Timestamp.ToString());
                newAction.ActionDuration = duration;
                Debug.Log("duration" + newAction.ActionDuration.ToString());

                //savedData.vector_actiuni.Add(newAction);
                savedData.eventData = newAction;
                Debug.Log("newAction: " + newAction.ToString());
                //Debug.Log("savedData.vector_actiuni: " + savedData.vector_actiuni.ToString());
            }
            else
            {
                JSONData newData = new JSONData();
                //newData.vector_actiuni = new List<EventData>();

                newData._id = itemInfo.GetInstanceID();
                newData.nume = itemInfo.ItemName;
                newData.descriere = itemInfo.ItemDescription;
                newData.pret = itemInfo.ItemPrice;
                newData.nutritional_info = itemInfo.ItemNutritionalInfo;

                EventData newAction = new EventData();
                newAction.ActionType = (actionType == 1) ? "TEMPTATION" : "BUY";
                newAction.Timestamp = System.DateTime.UtcNow.ToString("yyyy-MM-dd");
                newAction.ActionDuration = duration;

                newData.eventData = newAction;

                //newData.vector_actiuni.Add(newAction);

                savedData = newData;
            }

            string json = JsonUtility.ToJson(savedData, true);
            Debug.Log("ACESTA E JSONUL: " + json);
            File.WriteAllText(saveFilePath, json);
        }
        

        /*public static void SaveEventsToJson(int actionType, float duration, ItemInfo itemInfo)
        {
            Debug.Log("AM INTRAT");
            string saveFilePath = "SavedEvents.json";
            List<JSONData> savedDataList;

            if (File.Exists(saveFilePath))
            {
                string jsonData = File.ReadAllText(saveFilePath);
                //savedDataList = JsonUtility.FromJson<List<JSONData>>(jsonData);
                savedDataList = JsonConvert.DeserializeObject<List<JSONData>>(jsonData);
            }
            else
            {
                savedDataList = new List<JSONData>();
            }

            JSONData savedData = savedDataList
                .Find(data => data._id == itemInfo.GetInstanceID() && data.nume == itemInfo.ItemName);

            if (savedData == null)
            {
                savedData = new JSONData();
                savedData._id = itemInfo.GetInstanceID();
                savedData.nume = itemInfo.ItemName;
                savedData.descriere = itemInfo.ItemDescription;
                savedData.pret = itemInfo.ItemPrice;
                savedData.nutritional_info = itemInfo.ItemNutritionalInfo;

                savedData.vector_actiuni = new List<EventData>();
                savedDataList.Add(savedData);
            }

            EventData newAction = new EventData();
            newAction.ActionType = (actionType == 1) ? "TEMPTATION" : "BUY";
            newAction.Timestamp = System.DateTime.UtcNow.ToString("yyyy-MM-dd");
            newAction.ActionDuration = duration;

            savedData.vector_actiuni.Add(newAction);

            //string json = JsonUtility.ToJson(savedDataList, true);
            string json = JsonConvert.SerializeObject(savedDataList, Formatting.Indented);

            Debug.Log("ACESTA E JSONUL: " + json);
            File.WriteAllText(saveFilePath, json);
        }*/
    }
}
