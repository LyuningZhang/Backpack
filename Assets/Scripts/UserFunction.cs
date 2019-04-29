using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;
using System;
using System.IO;
using System.Xml.Serialization;

[CustomLuaClass]
public static class UserFunction
{
    public static string path = Application.dataPath + "/Ab/image";

    public static object[] GetImage()
    {
        var assetBundle = AssetBundle.LoadFromFile(path);
        if (assetBundle == null)
        {
            return null;
        }
        object[] sprite = new object[4];
        sprite[0] = assetBundle.LoadAsset("0", typeof(Sprite));
        sprite[1] = assetBundle.LoadAsset("1", typeof(Sprite));
        sprite[2] = assetBundle.LoadAsset("2", typeof(Sprite));
        sprite[3] = assetBundle.LoadAsset("3", typeof(Sprite));
        return sprite;
    }

    private static string filePath = Application.dataPath + "/PlayerData.xml";

    public static bool Save(string playerData)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }
        StreamWriter streamWriter;
        streamWriter = fileInfo.CreateText();
        if (playerData == null)
        {
            Debug.Log("背包数据为空！");
            return false;
        }
        //xml序列化
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));
        xmlSerializer.Serialize(streamWriter, playerData);
        streamWriter.Close();
        return true;
    }

    public static string Load()
    {
        FileInfo fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
        {
            Debug.Log("不存在本地背包数据文件！");
            return "";
        }
        FileStream fileStream = new FileStream(filePath, FileMode.Open);
        if (fileStream.Length <= 0)
        {
            Debug.Log("本地背包数据为空！");
            return "";
        }
        //xml反序列化
        XmlSerializer xml = new XmlSerializer(typeof(string));
        string result = xml.Deserialize(fileStream) as string;
        return result;
    }
}