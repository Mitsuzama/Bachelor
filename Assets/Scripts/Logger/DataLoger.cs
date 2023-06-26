using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Item;
using Logger;

namespace Logger
{
    public static class DataLogger
    {
        private static string CreateUniqueName()
        {
            return DateTime.Now.ToString("MM-dd-yyyy--HH-mm-ss");
        }

        public static JSONData CreateJsonDocument(int tip_actiune, float durata, ItemInfo itemInfo)
        {
            JSONData jsonDocument = new JSONData();

            DateTimeOffset dateTime = DateTimeOffset.UtcNow;
            long unixTimestamp = dateTime.ToUnixTimeMilliseconds();

            jsonDocument.timestamp = unixTimestamp.ToString();
            jsonDocument.tip_actiune = (tip_actiune == 1) ? "TEMPTATION" : "BUY"; ;
            jsonDocument.durata = durata;
            jsonDocument._id = itemInfo.GetInstanceID();
            jsonDocument.nume = itemInfo.ItemName;
            jsonDocument.descriere = itemInfo.ItemDescription;
            jsonDocument.pret = itemInfo.ItemPrice;
            jsonDocument.nutritional_info = itemInfo.ItemNutritionalInfo;

            return jsonDocument;
        }

        public static void SaveEventsToJson(int actionType, float duration, ItemInfo itemInfo)
        {
            string name = "SavedEvents_" + CreateUniqueName();
            string saveFilePath = "SavedEvents_" + CreateUniqueName() + ".json";
            List<JSONData> savedDataList;
            Debug.Log("AM INTRAT");

            if (itemInfo == null)
            {
                throw new ArgumentNullException("SaveEventsToJson: itemInfo");
            }

            if (!File.Exists(saveFilePath))
            {
                throw new FileNotFoundException("The file was not found!", saveFilePath);
            }

            var length = new System.IO.FileInfo(saveFilePath).Length;
            if (length == 0) //daca nu am nimic scris in fisier, initializez
            {
                savedDataList = new List<JSONData>();
            }
            else // daca am ceva in fisier, preiau datele
            {
                string readText = File.ReadAllText(saveFilePath);
                savedDataList = JsonConvert.DeserializeObject<List<JSONData>>(readText);
            }

            try
            {
                JSONData newEntry = CreateJsonDocument(actionType, duration, itemInfo);
                savedDataList.Add(newEntry);
                string json = JsonConvert.SerializeObject(savedDataList, Formatting.Indented);

                Debug.Log("ACESTA E JSONUL: " + json);
                File.WriteAllText(saveFilePath, json);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }
    }
}
