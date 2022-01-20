using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField] public TMPro.TMP_Dropdown dd = null;
    [System.NonSerialized] public List<string> nameList = new List<string>();
    private void Start()
    {
       
        if(GameManager.THIS.dataManager.materialPropertiesList != null && GameManager.THIS.dataManager.materialPropertiesList.Count > 0) 
        {

            RefreshDropDownMenu();
            GameManager.THIS.dataBaseMenu.inputE1.text = GameManager.THIS.dataManager.materialPropertiesList[0].E1.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputE2.text = GameManager.THIS.dataManager.materialPropertiesList[0].E2.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputG1.text = GameManager.THIS.dataManager.materialPropertiesList[0].G12.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputv12.text = GameManager.THIS.dataManager.materialPropertiesList[0].v12.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputa1.text = GameManager.THIS.dataManager.materialPropertiesList[0].a1.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputa2.text = GameManager.THIS.dataManager.materialPropertiesList[0].a2.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputb1.text = GameManager.THIS.dataManager.materialPropertiesList[0].b1.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputb2.text = GameManager.THIS.dataManager.materialPropertiesList[0].b2.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputThickness.text = GameManager.THIS.dataManager.materialPropertiesList[0].thickness.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputLongitudinalTensileStrength.text = GameManager.THIS.dataManager.materialPropertiesList[0].longitudinalTensileStrength.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputLongitudinalCompressiveStrength.text = GameManager.THIS.dataManager.materialPropertiesList[0].longitudinalCompressiveStrength.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputTransverseTensileStrength.text = GameManager.THIS.dataManager.materialPropertiesList[0].transverseTensileStrength.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputTransverselCompressiveStrength.text = GameManager.THIS.dataManager.materialPropertiesList[0].transverselCompressiveStrength.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputShearStrength.text = GameManager.THIS.dataManager.materialPropertiesList[0].shearStrength.ToString().Replace('.', ',');
            GameManager.THIS.dataBaseMenu.inputMaterialName.text = GameManager.THIS.dataManager.materialPropertiesList[0].materialName.ToString().Replace('.', ',');
            GameManager.THIS.macroMechanicsMenu.inputE1.text = GameManager.THIS.dataManager.materialPropertiesList[0].E1.ToString().Replace('.', ',');
            GameManager.THIS.macroMechanicsMenu.inputE2.text = GameManager.THIS.dataManager.materialPropertiesList[0].E2.ToString().Replace('.', ',');
            GameManager.THIS.macroMechanicsMenu.inputG1.text = GameManager.THIS.dataManager.materialPropertiesList[0].G12.ToString().Replace('.', ',');
            GameManager.THIS.macroMechanicsMenu.inputv12.text = GameManager.THIS.dataManager.materialPropertiesList[0].v12.ToString().Replace('.', ',');
            GameManager.THIS.macroMechanicsMenu.inputv21.text = ((GameManager.THIS.dataManager.materialPropertiesList[0].E2 / GameManager.THIS.dataManager.materialPropertiesList[dd.value].E1) * GameManager.THIS.dataManager.materialPropertiesList[dd.value].v12).ToString().Replace('.', ','); ;
            GameManager.THIS.macroMechanicsMenu.inputa1.text = GameManager.THIS.dataManager.materialPropertiesList[0].a1.ToString().Replace('.', ',');
            GameManager.THIS.macroMechanicsMenu.inputa2.text = GameManager.THIS.dataManager.materialPropertiesList[0].a2.ToString().Replace('.', ',');
            GameManager.THIS.macroMechanicsMenu.inputb1.text = GameManager.THIS.dataManager.materialPropertiesList[0].b1.ToString().Replace('.', ',');
            GameManager.THIS.macroMechanicsMenu.inputb2.text = GameManager.THIS.dataManager.materialPropertiesList[0].b2.ToString().Replace('.', ',');
        }
    }
    public void RefreshDropDownMenu()
    {

        //
        //dd.RefreshShownValue();
        dd.options.Clear();
        nameList = new List<string>();

        foreach (MaterialProperties element in GameManager.THIS.dataManager.materialPropertiesList)
        {
            nameList.Add(element.materialName);
            //dd.options.Add(new TMPro.TMP_Dropdown.OptionData(element.materialName)); 
        }
        dd.AddOptions(nameList);
    }
   
    public void GetValues()
    {
        if (GameManager.THIS.dataManager.materialPropertiesList.Count > 0)
        {
            if (GameManager.THIS.dataBaseMenu.gameObject.activeSelf)
            {

                GameManager.THIS.dataBaseMenu.inputE1.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].E1.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputE2.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].E2.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputG1.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].G12.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputv12.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].v12.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputa1.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].a1.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputa2.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].a2.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputb1.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].b1.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputb2.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].b2.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputThickness.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].thickness.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputLongitudinalTensileStrength.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].longitudinalTensileStrength.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputLongitudinalCompressiveStrength.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].longitudinalCompressiveStrength.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputTransverseTensileStrength.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].transverseTensileStrength.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputTransverselCompressiveStrength.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].transverselCompressiveStrength.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputShearStrength.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].shearStrength.ToString().Replace('.', ',');
                GameManager.THIS.dataBaseMenu.inputMaterialName.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].materialName.ToString().Replace('.', ',');
                 GameManager.THIS.dataBaseMenu.inputE1.image.color = Color.white;
                 GameManager.THIS.dataBaseMenu.inputE2.image.color = Color.white;
                 GameManager.THIS.dataBaseMenu.inputG1.image.color = Color.white;
                GameManager.THIS.dataBaseMenu.inputv12.image.color = Color.white;
                 GameManager.THIS.dataBaseMenu.inputa1.image.color = Color.white;
                 GameManager.THIS.dataBaseMenu.inputa2.image.color = Color.white;
                 GameManager.THIS.dataBaseMenu.inputb1.image.color = Color.white;
                GameManager.THIS.dataBaseMenu.inputb2.image.color = Color.white;
                GameManager.THIS.dataBaseMenu.inputThickness.image.color = Color.white;
                GameManager.THIS.dataBaseMenu.inputLongitudinalTensileStrength.image.color = Color.white;
                GameManager.THIS.dataBaseMenu.inputLongitudinalCompressiveStrength.image.color = Color.white;
                GameManager.THIS.dataBaseMenu.inputTransverseTensileStrength.image.color = Color.white;
                GameManager.THIS.dataBaseMenu.inputTransverselCompressiveStrength.image.color = Color.white;
                GameManager.THIS.dataBaseMenu.inputShearStrength.image.color = Color.white;
                GameManager.THIS.dataBaseMenu.inputMaterialName.image.color = Color.white;
            }

            else if (GameManager.THIS.macroMechanicsMenu.gameObject.activeSelf)
            {
                GameManager.THIS.macroMechanicsMenu.inputE1.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].E1.ToString().Replace('.', ',');
                GameManager.THIS.macroMechanicsMenu.inputE2.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].E2.ToString().Replace('.', ',');
                GameManager.THIS.macroMechanicsMenu.inputG1.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].G12.ToString().Replace('.', ',');
                GameManager.THIS.macroMechanicsMenu.inputv12.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].v12.ToString().Replace('.', ',');
                GameManager.THIS.macroMechanicsMenu.inputv21.text = ((GameManager.THIS.dataManager.materialPropertiesList[dd.value].E2 / GameManager.THIS.dataManager.materialPropertiesList[dd.value].E1) * GameManager.THIS.dataManager.materialPropertiesList[dd.value].v12).ToString().Replace('.', ','); ;
                GameManager.THIS.macroMechanicsMenu.inputa1.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].a1.ToString().Replace('.', ',');
                GameManager.THIS.macroMechanicsMenu.inputa2.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].a2.ToString().Replace('.', ',');
                GameManager.THIS.macroMechanicsMenu.inputb1.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].b1.ToString().Replace('.', ',');
                GameManager.THIS.macroMechanicsMenu.inputb2.text = GameManager.THIS.dataManager.materialPropertiesList[dd.value].b2.ToString().Replace('.', ',');
                GameManager.THIS.macroMechanicsMenu.inputE1.image.color = Color.white;
                GameManager.THIS.macroMechanicsMenu.inputE2.image.color = Color.white;
                GameManager.THIS.macroMechanicsMenu.inputG1.image.color = Color.white;
                GameManager.THIS.macroMechanicsMenu.inputv12.image.color = Color.white;
                GameManager.THIS.macroMechanicsMenu.inputv21.image.color = Color.white;
                GameManager.THIS.macroMechanicsMenu.inputa1.image.color = Color.white;
                GameManager.THIS.macroMechanicsMenu.inputa2.image.color = Color.white;
                GameManager.THIS.macroMechanicsMenu.inputb1.image.color = Color.white;
                GameManager.THIS.macroMechanicsMenu.inputb2.image.color = Color.white;
            }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
