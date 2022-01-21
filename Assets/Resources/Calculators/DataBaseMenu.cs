using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseMenu : MonoBehaviour
{   
    [Header("Inputs")]
    [SerializeField] public TMPro.TMP_InputField inputE1;
    [SerializeField] public TMPro.TMP_InputField inputE2;
    [SerializeField] public TMPro.TMP_InputField inputG1;
    [SerializeField] public TMPro.TMP_InputField inputv12;
    [SerializeField] public TMPro.TMP_InputField inputa1;
    [SerializeField] public TMPro.TMP_InputField inputa2;
    [SerializeField] public TMPro.TMP_InputField inputb1;
    [SerializeField] public TMPro.TMP_InputField inputb2;
    [SerializeField] public TMPro.TMP_InputField inputThickness;
    [SerializeField] public TMPro.TMP_InputField inputLongitudinalTensileStrength;
    [SerializeField] public TMPro.TMP_InputField inputLongitudinalCompressiveStrength;
    [SerializeField] public TMPro.TMP_InputField inputTransverseTensileStrength;
    [SerializeField] public TMPro.TMP_InputField inputTransverselCompressiveStrength;
    [SerializeField] public TMPro.TMP_InputField inputShearStrength;
    [SerializeField] public TMPro.TMP_InputField inputMaterialName;

    [System.NonSerialized] private double E1;
    [System.NonSerialized] private double E2;
    [System.NonSerialized] private double v12;
    [System.NonSerialized] private double G12;
    [System.NonSerialized] private double a1;
    [System.NonSerialized] private double a2;
    [System.NonSerialized] private double b1;
    [System.NonSerialized] private double b2;
    [System.NonSerialized] private bool save = true;
    [System.NonSerialized] private double thickness;
    [System.NonSerialized] private double longitudinalTensileStrength;
    [System.NonSerialized] private double longitudinalCompressiveStrength;
    [System.NonSerialized] private double transverseTensileStrength;
    [System.NonSerialized] private double transverselCompressiveStrength;
    [System.NonSerialized] private double shearStrength;
    [System.NonSerialized] private string materialName;
    [SerializeField] private RectTransform allItemsDataBase;


    private void Update()
    {

        if (allItemsDataBase.offsetMin.y < 0.0f)
        {

            allItemsDataBase.offsetMin = new Vector2(allItemsDataBase.offsetMin.x, 0.0f);
            allItemsDataBase.offsetMax = new Vector2(allItemsDataBase.offsetMax.x, 0.0f);


        }
        else if (allItemsDataBase.offsetMin.y > 1350.0f)
        {
            allItemsDataBase.offsetMin = new Vector2(allItemsDataBase.offsetMin.x, 1350.0f);
            allItemsDataBase.offsetMax = new Vector2(allItemsDataBase.offsetMax.x, 1350.0f);
        }
    }

        private float GetValue(TMPro.TMP_InputField tmp)
    {
        string text = tmp.text;
        float.TryParse(text, out float value);
        return value;

    }
    private bool IsNullOrEmpty(TMPro.TMP_InputField tmp)
    {
        if (string.IsNullOrEmpty(tmp.text))
        {
            tmp.image.color = Color.red;
            return true;
        }
        else
        {
            tmp.image.color = Color.white;
            return false;
        }
    }
    
    public void Save()
    {
        save = false;
        if (!IsNullOrEmpty(inputE1) &&
           !IsNullOrEmpty(inputE2) &&
           !IsNullOrEmpty(inputG1) &&
           !IsNullOrEmpty(inputv12) &&
           !IsNullOrEmpty(inputa1) &&
           !IsNullOrEmpty(inputa2) &&
           !IsNullOrEmpty(inputb1) &&
           !IsNullOrEmpty(inputb2) &&
           !IsNullOrEmpty(inputThickness) &&
           !IsNullOrEmpty(inputLongitudinalTensileStrength) &&
           !IsNullOrEmpty(inputLongitudinalCompressiveStrength) &&
           !IsNullOrEmpty(inputTransverseTensileStrength) &&
           !IsNullOrEmpty(inputTransverselCompressiveStrength)&&
           !IsNullOrEmpty(inputShearStrength) &&
           !IsNullOrEmpty(inputMaterialName) 
          )
        {
            E1 = GetValue(inputE1);
            E2 = GetValue(inputE2);
            v12 = GetValue(inputv12);
            //sDebug.Log(v12);
            G12 = GetValue(inputG1);
            a1 = GetValue(inputa1);
            a2 = GetValue(inputa2);
            b1 = GetValue(inputb1);
            b2 = GetValue(inputb2);
            thickness = GetValue(inputThickness);
            longitudinalTensileStrength = GetValue(inputLongitudinalTensileStrength);
            longitudinalCompressiveStrength = GetValue(inputLongitudinalCompressiveStrength);
            transverseTensileStrength = GetValue(inputTransverseTensileStrength);
            transverselCompressiveStrength = GetValue(inputTransverselCompressiveStrength);
            shearStrength = GetValue(inputShearStrength);
            materialName = inputMaterialName.text;
            save = true;
        }
        if (save)
        {
            
            MaterialProperties material = new MaterialProperties(materialName, E1, E2, v12, G12, a1, a2, b1, b2, thickness,
                longitudinalTensileStrength, longitudinalCompressiveStrength, transverseTensileStrength, 
                transverselCompressiveStrength, shearStrength
                );
            bool isAlreadyExists = false;
            int location = -1;
            foreach (MaterialProperties mp in GameManager.THIS.dataManager.materialPropertiesList)
            {
                if (mp.materialName.Equals(material.materialName))
                {
                    isAlreadyExists = true;
                    location = GameManager.THIS.dataManager.materialPropertiesList.IndexOf(mp);
                }
            }
            if (!isAlreadyExists)
            {
                GameManager.THIS.dataManager.materialPropertiesList.Add(material);
            }
            else 
            {
                if (location == -1)
                {
                    throw new Exception("Something went wrong!!");
                }
                else
                {
                    GameManager.THIS.dataManager.materialPropertiesList[location] = material;
                }
                
            }
            GameManager.THIS.dataManager.Save();
            GameManager.THIS.dropDownMenu.RefreshDropDownMenu();
        }
        else
        {
            GameManager.THIS.dropDownMenu.RefreshDropDownMenu();
        }

    }
    public void RemoveMaterial()
    {
        if (GameManager.THIS.dataManager.materialPropertiesList.Count > 0)
        {
            GameManager.THIS.dataManager.materialPropertiesList.RemoveAt(GameManager.THIS.dropDownMenu.dd.value);
        }
        GameManager.THIS.dataManager.Save();
    }
    public void CreateNewMaterial()
    {

        inputE1.text = "";
        inputE2.text = "";
        inputG1.text = "";
        inputv12.text ="";
        inputa1.text = "";
        inputa2.text = "";
        inputb1.text = "";
        inputb2.text = "";
        inputThickness.text = "";
        inputLongitudinalTensileStrength.text = "";
        inputLongitudinalCompressiveStrength.text = "";
        inputTransverseTensileStrength.text = "";
        inputTransverselCompressiveStrength.text = "";
        inputShearStrength.text = "";
        inputMaterialName.text = "";

    }
}
