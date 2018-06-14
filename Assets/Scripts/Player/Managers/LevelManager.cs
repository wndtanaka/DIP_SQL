using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        Transform map;
        [SerializeField]
        Texture2D[] mapData;
        [SerializeField]
        MapElement[] mapElements;
        [SerializeField]
        Sprite defaultTile;

        Dictionary<Point, GameObject> waterTiles = new Dictionary<Point, GameObject>();

        Vector3 WorldStartPos
        {
            get
            {
                return Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
            }
        }

        // Use this for initialization
        void Start()
        {
            GenerateMap();
        }

        // Update is called once per frame

        void GenerateMap()
        {
            int height = mapData[0].height;
            int width = mapData[0].width;

            for (int i = 0; i < mapData.Length; i++)
            {
                for (int x = 0; x < mapData[i].width; x++)
                {
                    for (int y = 0; y < mapData[i].height; y++)
                    {
                        Color c = mapData[i].GetPixel(x, y);
                        MapElement newElement = System.Array.Find(mapElements, e => e.MyColor == c);

                        if (newElement != null)
                        {
                            float xPos = WorldStartPos.x + (defaultTile.bounds.size.x * x);
                            float yPos = WorldStartPos.x + (defaultTile.bounds.size.y * y);
                            GameObject go = Instantiate(newElement.MyElementPrefab);
                            go.transform.position = new Vector2(xPos, yPos);
                            go.transform.parent = map;

                            if (newElement.MyTileTag == "Water")
                            {
                                waterTiles.Add(new Point(x, y), go);
                            }
                            if (newElement.MyTileTag == "Tree")
                            {
                                go.GetComponent<SpriteRenderer>().sortingOrder = height * 2 - y * 2;
                            }
                        }
                    }
                }
            }
            CheckWater();
        }
        public string TileCheck(Point currentPoint)
        {
            string composition = string.Empty;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        if (waterTiles.ContainsKey(new Point(currentPoint.MyX + x, currentPoint.MyY + y)))
                        {
                            composition += "W";
                        }
                        else
                        {
                            composition += "E";
                        }
                    }
                }
            }
            //Debug.Log(composition);
            return composition;
        }
        public void CheckWater()
        {
            foreach (KeyValuePair<Point, GameObject> tile in waterTiles)
            {
                string composition = TileCheck(tile.Key);
            }
        }
    }
    [System.Serializable]
    public class MapElement
    {
        [SerializeField]
        string tileTag;
        [SerializeField]
        private Color color;
        [SerializeField]
        private GameObject elementPrefab;

        public GameObject MyElementPrefab
        {
            get
            {
                return elementPrefab;
            }
        }

        public Color MyColor
        {
            get
            {
                return color;
            }
        }

        public string MyTileTag
        {
            get
            {
                return tileTag;
            }
        }
    }
    public struct Point
    {
        public int MyX { get; set; }
        public int MyY { get; set; }

        public Point(int x, int y)
        {
            this.MyX = x;
            this.MyY = y;
        }
    }
}