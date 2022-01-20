using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameManager : MonoBehaviour //, ISaveable
{
    [System.NonSerialized] public static GameManager THIS = null;
    [System.NonSerialized] public DataManager dataManager = null;
    [SerializeField] public  UIManager UIManager = null;
    [SerializeField] public  MacromechanicsAnalysisofLamina macroMechanicsMenu = null;
    //[SerializeField] public  GameObject macroMechanicsLaminaMenu = null;
    [SerializeField] public  MicromechanicsAnalysisofLamina microMechanicsMenu = null;
    [SerializeField] public  DataBaseMenu dataBaseMenu = null;
    [SerializeField] public  DropDownMenu dropDownMenu = null;
    [SerializeField] public  DropDownMenuLaminate dropDownMenuLaminate = null;
    [SerializeField] public  GameObject dropDownMenuButton = null;
    
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        THIS = this;
        dataManager = new DataManager();
        /*
        MaterialProperties mat1 = new MaterialProperties( "mat1", 25, 21, 21, 21, 21, 12, 12, 12, 12, 33, 33, 33, 33, 33  );
        
        dataManager.materialPropertiesList.Add(mat1);
        MaterialProperties mat2 = new MaterialProperties("mat2", 25, 21, 21, 21, 21, 12, 12, 12, 12, 33, 33, 33, 33, 33);

        dataManager.materialPropertiesList.Add(mat2);
        
        //dataManager.Save();*/
        dataManager.ResetList();
        dataManager = dataManager.Load();
    }
   
}
