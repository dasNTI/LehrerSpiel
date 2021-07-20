using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Progress
{
    [System.Serializable]
    public static class Levels
    {
        static string path = Application.persistentDataPath + "/levels.data";

        public static void Save(bool[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static bool[] Load()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            bool[] data = formatter.Deserialize(stream) as bool[];
            stream.Close();

            return data;
        }
    }
}
