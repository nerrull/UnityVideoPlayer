using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GetFileList : MonoBehaviour
{

    public string EskerImageFolder = "Media/EskerImage";
    public string EskerVideoFolder = "Media/EskerVideo";
    public string LowBatteryImageFolder = "Media/LowBatteryImage";
    public MainController master;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    List<string> GetFilePaths(string root)
    {
        var fullPath = Application.dataPath + "/" + root;
        var files = Directory.GetFiles(fullPath);
        var fileList = new List<string>();
        foreach(var file in Directory.GetFiles(fullPath))
        {
            if (file.EndsWith(".meta"))
            {
                continue;
            }
            var fn = file.Replace('\\', '/');

            int index = fn.LastIndexOf("/");
            string localPath = "Assets/" + root;
            

            if (index > 0)
            {
                localPath += fn.Substring(index);
            }
            fileList.Add(localPath.Replace('\\', '/'));
        }
        return fileList;
    }
    public void PopulateFileLists()
    {
        master.EskerImages = GetFilePaths(EskerImageFolder);
        master.EskerVideos = GetFilePaths(EskerVideoFolder);
        master.LowBatteryImages = GetFilePaths(LowBatteryImageFolder);
    }

}

