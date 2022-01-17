using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class ExportTerrainCSV
{
    static Dictionary<string, int> savelist = new Dictionary<string, int>();
    static Dictionary<int, GameObject> loadlist = new Dictionary<int, GameObject>();

    [MenuItem("CustomExport/Terrain")]
    static public void ExportTerrainHierarchy()
    {
        savelist.Clear();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Terrain");

        if (!Directory.Exists(Application.dataPath + "/Export/HierarchyTerrain/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Export/HierarchyTerrain/");
        }

        using (StreamWriter sr = new StreamWriter(Application.dataPath + "/Export/HierarchyTerrain/" + "terrainH.csv"))
        {
            string lineData = string.Empty;


            for (int i = 0; i < objs.Length; i++)
            {
                lineData = (i + 1).ToString();
                lineData += ",";
                lineData += objs[i].name;
                lineData += ",";

                if (objs[i].transform.parent.gameObject.name == "TerrainObject")
                {
                    lineData += "-1"; 
                }
                else
                {
                    GameObject parentObj = objs[i].transform.parent.gameObject;
                    lineData += savelist[parentObj.name].ToString();
                }
                

                lineData += ",";
                lineData += objs[i].transform.position.x;
                lineData += ",";
                lineData += objs[i].transform.position.y;
                lineData += ",";
                lineData += objs[i].transform.position.z;
                lineData += ",";

                lineData += objs[i].transform.localEulerAngles.x;
                lineData += ",";
                lineData += objs[i].transform.localEulerAngles.y;
                lineData += ",";
                lineData += objs[i].transform.localEulerAngles.z;
                lineData += ",";

                lineData += objs[i].transform.localScale.x;
                lineData += ",";
                lineData += objs[i].transform.localScale.y;
                lineData += ",";
                lineData += objs[i].transform.localScale.z;

                savelist.Add(objs[i].name, (i + 1));

                sr.WriteLine(lineData);

                lineData = string.Empty;
            }

            

            sr.Close();
        }
    }

    [MenuItem("CustomExport/LoadTerrain(Test)")]
    static public void LoadTerrain()
    {
        string path = Application.dataPath + "/Resources/TerrainTalbe/" + "terrainH.csv";

        using (StreamReader sr = new StreamReader(path))
        {
            string linedata = string.Empty;

            GameObject terrainParent = GameObject.Find("TerrainObject");
            if (terrainParent == null)
            {
                terrainParent = new GameObject("TerrainObject");
                terrainParent.transform.position = Vector3.zero;
            }

            while ((linedata = sr.ReadLine()) != null)
            {
                string[] data = linedata.Split(',');

                int index = int.Parse(data[0]);
                string name = data[1];
                int parentindex = int.Parse(data[2]);

                float xPos = float.Parse(data[3]);
                float yPos = float.Parse(data[4]);
                float zPos = float.Parse(data[5]);

                float xRot = float.Parse(data[6]);
                float yRot = float.Parse(data[7]);
                float zRot = float.Parse(data[8]);

                float xScale = float.Parse(data[9]);
                float yScale = float.Parse(data[10]);
                float zScale = float.Parse(data[11]);

                GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(name));

                obj.transform.SetParent(terrainParent.transform);

                if (parentindex != -1)
                {
                    obj.transform.SetParent(loadlist[parentindex].transform);
                }


                obj.name = name;
                obj.transform.position = new Vector3(xPos, yPos, zPos);
                obj.transform.localEulerAngles = new Vector3(xRot, yRot, zRot);
                obj.transform.localScale = new Vector3(xScale, yScale, zScale);

                loadlist.Add(index, obj);
            }

            loadlist.Clear();

            Debug.Log(linedata);

            sr.Close();
        }
    }
}
