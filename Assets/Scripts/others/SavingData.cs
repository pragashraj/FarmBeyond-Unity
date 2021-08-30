using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavingData
{
    public static void SavePlayer(Data script)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.fallen";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, script);
        stream.Close();
    }

    public static Data LoadPlayer()
    {
        string path = Application.persistentDataPath + "/data.fallen";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
