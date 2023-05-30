using System.Collections.Generic;
using Item;

namespace Logger
{
    [System.Serializable]
    public class JSONData
    {
        public string timestamp;
        public string tip_actiune;
        public float durata;
        public int _id;
        public string nume;
        public string descriere;
        public float pret;
        public NutritionalInfo nutritional_info;
    }
}

