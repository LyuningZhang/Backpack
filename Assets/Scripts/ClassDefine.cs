using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.UI;
using System.Xml.Serialization;

namespace ClassDefine
{
    /// <summary>
    /// 物品信息类
    /// </summary>
    [System.Serializable]
    public class Goods
    {
        public int rank;
        public int imageId;

        public Goods()
        {
            imageId = -1;
            rank = -1;
        }
        public Goods(int id)
        {
            imageId = id;
            rank = id;
        }
    }

    /// <summary>
    /// 格子信息类
    /// </summary>
    [System.Serializable]
    public class Grid
    {
        public Goods goods;
        public int amount;

        private Image image;
        private Text text;
        private GridClick gridClick;

        public Grid()
        {
            goods = new Goods(0);
            this.image = null;
            this.text = null;
            this.gridClick = null;
            amount = 0;
        }

        public Grid(Image image, Text text, GridClick gridClick)
        {
            goods = new Goods(0);
            this.image = image;
            this.text = text;
            this.gridClick = gridClick;
            amount = 0;
        }

        public Grid(int id, Image image, Text text, GridClick gridClick)
        {
            if (id < 0 || id > 3)
            {
                Debug.Log("错误的生成物品id");
                return;
            }
            goods = new Goods(id);
            this.image = image;
            this.text = text;
            this.gridClick = gridClick;
            amount = 1;
        }

        /// <summary>
        /// 清空格子的物品
        /// </summary>
        public void Clear()
        {
            goods = new Goods(0);
            amount = 0;
            image.sprite = Resources.Load<Sprite>("Image/" + 0.ToString());
            text.text = "";
        }

        /// <summary>
        /// 将格子更新为新的物品
        /// </summary>
        /// <param name="id">新物品Id</param>
        /// <returns></returns>
        public bool Update(int id)
        {
            if (id < 0 || id > 3)
            {
                Debug.Log("错误的更新物品id");
                return false;
            }
            goods = new Goods(id);
            amount = 1;
            image.sprite = Resources.Load<Sprite>("Image/" + goods.imageId.ToString());
            text.text = amount.ToString();
            return true;
        }

        public Image GetImage()
        {
            return image;
        }

        public Text GetText()
        {
            return text;
        }

        public GridClick GetGridClick()
        {
            return gridClick;
        }
    }

    /// <summary>
    /// 格子列表序列化类
    /// </summary>
    [System.Serializable]
    public class GridData
    {
        [SerializeField]
        public List<Grid> gridList;

        public GridData()
        {
            gridList = new List<Grid>();
        }
    }

    /// <summary>
    /// 序列化数据类
    /// </summary>
    public static class FileSer
    {
        private static string filePath = Application.dataPath + "/PlayerData.xml";

        /// <summary>
        /// 序列化保存文件
        /// </summary>
        /// <param name="playerData"></param>
        /// <returns></returns>
        public static bool Save(GridData playerData)
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
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GridData));
            xmlSerializer.Serialize(streamWriter, playerData);
            streamWriter.Close();
            return true;
        }

        /// <summary>
        /// 反序列化读取数据
        /// </summary>
        /// <param name="playerData"></param>
        /// <returns></returns>
        public static bool Load(ref GridData playerData)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                Debug.Log("不存在本地背包数据文件！");
                return false;
            }
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            if(fileStream.Length <= 0)
            {
                Debug.Log("本地背包数据为空！");
                return false;
            }
            //xml反序列化
            XmlSerializer xml = new XmlSerializer(typeof(GridData));
            playerData = xml.Deserialize(fileStream) as GridData;
            return true;
        }
    }
}
