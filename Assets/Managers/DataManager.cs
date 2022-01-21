using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class DataManager
{
    [SerializeField] public List<MaterialProperties> materialPropertiesList = new List<MaterialProperties>();
    //[System.NonSerialized] public static readonly string saveDataPathPc = Application.persistentDataPath + "/save.txt";
    //[System.NonSerialized] public static readonly string saveDataPathMobile = Application.dataPath + "/save.txt";
    //[System.NonSerialized] public static string saveDataPathMobile = saveDataPathMobile = Application.persistentDataPath + "/save.txt";
    



    
    public string ToJson()
    {
        
        return JsonUtility.ToJson(this);
    }
    public DataManager LoadFromJson(string aJson)
    {
        DataManager dm = JsonUtility.FromJson < DataManager > (aJson);
        return dm;
    }
    public void Save()
    {
        //string datas = GetFilePath( "save");
       // string dataPath = Application.dataPath + "/save.txt";
        string dataPath = GetFilePath("save");
       
        string json = ToJson();
        File.WriteAllText(dataPath, json);
        //Debug.Log(json);
        GameManager.THIS.dropDownMenu.RefreshDropDownMenu();

        
    }
    public DataManager Load()
    {

        //string dataPath = GetFilePath("save");
        //string dataPath = Application.dataPath + "/save.txt";
        string dataPath = GetFilePath("save");
        if (File.Exists(dataPath))
        {
            string saveString = File.ReadAllText(dataPath);
            DataManager dm = LoadFromJson(saveString);
            
            //dataManagerin içindeki variablelarý set et!
            return dm;
        }
        else
        {


            Save();
            string saveString = File.ReadAllText(dataPath);
            DataManager dm = LoadFromJson(saveString);
            return dm;
            //save file yoksa deðerlerin initiallarýný gir
            //E1 = 115;
        }
        //Debug.Log(E1);
    }
    public void ResetList()
    {
        materialPropertiesList.Clear();
    }

    /* public void SavePrefs()
     {
         PlayerPrefs.SetFloat("CURRENT_XP", CURRENT_XP);
         PlayerPrefs.SetInt("CURRENT_GOLDBAR", CURRENT_GOLDBAR);
     }
     public void LoadPrefs()
     {
         CURRENT_XP = PlayerPrefs.GetFloat("CURRENT_XP", 0f);
         CURRENT_GOLDBAR = PlayerPrefs.GetInt("CURRENT_GOLDBAR", 0);
     }
     public void ResetPrefs()
     {
         PlayerPrefs.SetFloat("CURRENT_XP", 0f);
         PlayerPrefs.SetInt("CURRENT_GOLDBAR", 0);
     }*/
    private static string GetFilePath(string FileName = "")
    {
        string filePath;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        // mac
         filePath = Application.persistentDataPath;

        if (FileName != "")
            filePath = filePath+ "/" +FileName + ".txt";
         
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        // windows
        //filePath = Path.Combine(Application.persistentDataPath, ("data/"));
        filePath = Application.persistentDataPath;
        //filePath = Application.dataPath + "/" ;

        if (FileName != "")
            filePath = filePath+ "/" +FileName + ".txt";
        Debug.Log(filePath);
        //DirectoryInfo di = Directory.CreateDirectory(filePath);

#elif UNITY_ANDROID
        // android
        //filePath = Path.Combine(Application.persistentDataPath, ("data/" ));
        filePath = Application.persistentDataPath;
        if(FileName != "")
            filePath = filePath+ "/" +FileName + ".txt";
         //filePath = Application.dataPath + "/" + "data/" ;
          //filePath = Path.Combine(filePath, (FileName + ".txt"));
       // if (FileName != "")
           // filePath = filePath+FileName + ".txt";
       // DirectoryInfo di = Directory.CreateDirectory(filePath);
#elif UNITY_IOS
         // ios
         filePath = Application.persistentDataPath;

        if (FileName != "")
            filePath = filePath+ "/" +FileName + ".txt";
#else
        filePath = Application.dataPath +FileName + ".txt";
#endif
        return filePath;
    }
}
 
