using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Item;
using Logger;

namespace Logger
{
    public class FileCreator : MonoBehaviour
    {
        private static bool isCreated = false;
        private static string filePath;
        private static List<JSONData> savedDataList = new List<JSONData>();

        private static JSONData CreateJsonDocument(int tip_actiune, float durata, ItemInfo itemInfo)
        {
            JSONData jsonDocument = new JSONData();

            DateTimeOffset dateTime = DateTimeOffset.UtcNow;
            long unixTimestamp = dateTime.ToUnixTimeMilliseconds();

            jsonDocument.timestamp = unixTimestamp.ToString();
            jsonDocument.tip_actiune = (tip_actiune == 1) ? "TEMPTATION" : "BUY"; ;
            jsonDocument.durata = durata;
            jsonDocument._id = Math.Abs(itemInfo.GetInstanceID());
            jsonDocument.nume = itemInfo.ItemName;
            jsonDocument.descriere = itemInfo.ItemDescription;
            jsonDocument.pret = itemInfo.ItemPrice;
            jsonDocument.nutritional_info = itemInfo.ItemNutritionalInfo;

            return jsonDocument;
        }

        private void CreateUniqueName()
        {
            string fileName = "SavedEvents_" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".json";
            filePath = Path.Combine(Application.dataPath, "SavedEvents", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.Create(filePath).Dispose();
            }
            Debug.Log("CreateUniqueName: FileCreator: Am creat fisierul: " + filePath);
        }

        public void Start()
        {
            if(!isCreated)
            {
                CreateUniqueName();
                isCreated = true;
            }
        }

        public static void SaveEventsToJson(int actionType, float duration, ItemInfo itemInfo)
        {

            if (itemInfo == null)
            {
                throw new ArgumentNullException("SaveEventsToJson: itemInfo");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The file was not found!", filePath);
            }

            try
            {
                JSONData newEntry = CreateJsonDocument(actionType, duration, itemInfo);
                if(itemInfo.ItemNutritionalInfo.glucide > 30.0f || itemInfo.ItemNutritionalInfo.grasimi > 5.0f || itemInfo.ItemNutritionalInfo.grasimiSaturate > 2.5f)
                {
                    savedDataList.Add(newEntry);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }

        void OnApplicationQuit()
        {
            try 
            {
                string json = JsonConvert.SerializeObject(savedDataList, Formatting.Indented);
                File.WriteAllText(filePath, string.Empty);
                using(StreamWriter sw = System.IO.File.AppendText(filePath))
                {
                    sw.WriteLine(json);
                    Debug.Log("Am scris in json!");
                }
            }

            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }
        
    }
}