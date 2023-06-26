using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Item;
using Logger;

namespace Logger
{
    private bool isCreated = false;
    
    public class FileCreator
    {
        public string FileName;

        public static string CreateUniqueName()
        {
            string fileName = "SavedEvents_" + DateTime.Now.ToString("MM-dd-yyyy-HH") + ".json";
            string filePath = Path.Combine(Application.dataPath, "SavedEvents", fileName);
            List<ItemData> eventList = new List<ItemData>();
            string json = JsonConvert.SerializeObject(eventList, Formatting.Indented);
            File.WriteAllText(filePath, json);

            Debug.Log("FileCreator: Am creat fisierul: " + filePath);
        }

        public void Start()
        {
            if (File.Exists(Path.Combine("SavedEvents", FileName)))
            {
                Debug.Log("FileCreator: Fisierul a fost deja creat!");
                return;
            }
            
            if(!isCreated)
            {
                CreateUniqueName();
                isCreated = true;
            }
        }
    }
}