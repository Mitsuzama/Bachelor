using System.Collections.Generic;

namespace Logger
{
    [System.Serializable]
    public struct Status
    {
        public const int NONE = 0;
        public const int TEMPTATION = 1;
        public const int BUY = 2;
        public const int LAST = 3;
    }
}