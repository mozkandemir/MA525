using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownMenuLaminate : MonoBehaviour
{
    [SerializeField] public List<TMPro.TMP_Dropdown> dropDownList = null;
    [SerializeField] public List<GameObject> materialList = null;
    [System.NonSerialized] public List<string> nameList = new List<string>();
    private void Awake()
    {
       // GameManager.THIS.dataManager.materialPropertiesList = new List<MaterialProperties>();
    }
    private void Start()
    {

        if (GameManager.THIS.dataManager.materialPropertiesList != null && GameManager.THIS.dataManager.materialPropertiesList.Count > 0)
        {

            RefreshDropDownMenuList();
        }
    }
    public void RefreshDropDownMenuList()
    {
        nameList = new List<string>();

        foreach (MaterialProperties element in GameManager.THIS.dataManager.materialPropertiesList)
        {
            nameList.Add(element.materialName);
            //dd.options.Add(new TMPro.TMP_Dropdown.OptionData(element.materialName)); 
        }
        foreach (TMPro.TMP_Dropdown dd in dropDownList) { 
            dd.options.Clear();
            

            dd.AddOptions(nameList);
        }
    }
}
