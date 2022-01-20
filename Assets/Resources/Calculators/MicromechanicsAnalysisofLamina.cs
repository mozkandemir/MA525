using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicromechanicsAnalysisofLamina : MonoBehaviour
{
    /* YM = Young's Modulus, VF = Volume Fraction, PR = Poisson's Ratio, SM = Shear Modulus, TE = Thermal Expansion, ME = Moisture Expansion
     */
    [Header("Inputs")]
    [SerializeField] public TMPro.TMP_InputField MatrixYM;
    [SerializeField] public TMPro.TMP_InputField MatrixVF;
    [SerializeField] public TMPro.TMP_InputField MatrixPR;
    [SerializeField] public TMPro.TMP_InputField MatrixSM;
    [SerializeField] public TMPro.TMP_InputField MatrixTE;
    [SerializeField] public TMPro.TMP_InputField MatrixME;
    [SerializeField] public TMPro.TMP_InputField MatrixDensity;
    [SerializeField] public TMPro.TMP_InputField FiberYM;
    [SerializeField] public TMPro.TMP_InputField FiberVF;
    [SerializeField] public TMPro.TMP_InputField FiberPR;
    [SerializeField] public TMPro.TMP_InputField FiberSM;
    [SerializeField] public TMPro.TMP_InputField FiberTE;
    [SerializeField] public TMPro.TMP_InputField FiberME;
    [SerializeField] public TMPro.TMP_InputField FiberDensity;

    //
    //
    [SerializeField] public GameObject resultScreen = null;
    [SerializeField] private RectTransform allItemsOfLamina;
    [SerializeField] private RectTransform allItemsOfResult;
    [Header("Outputs")]
    [SerializeField] private TMPro.TextMeshProUGUI YM1;
    [SerializeField] private TMPro.TextMeshProUGUI YM2;
    [SerializeField] private TMPro.TextMeshProUGUI PR12;
    [SerializeField] private TMPro.TextMeshProUGUI PR21;
    [SerializeField] private TMPro.TextMeshProUGUI SM12;
    [SerializeField] private TMPro.TextMeshProUGUI TE1;
    [SerializeField] private TMPro.TextMeshProUGUI TE2;
    [SerializeField] private TMPro.TextMeshProUGUI ME1;
    [SerializeField] private TMPro.TextMeshProUGUI ME2;

    [System.NonSerialized] private double value_MatrixYM;
    [System.NonSerialized] private double value_MatrixVF;
    [System.NonSerialized] private double value_MatrixPR;
    [System.NonSerialized] private double value_MatrixSM;
    [System.NonSerialized] private double value_MatrixTE;
    [System.NonSerialized] private double value_MatrixME;
    [System.NonSerialized] private double value_MatrixDensity;
    [System.NonSerialized] private double value_FiberYM;
    [System.NonSerialized] private double value_FiberVF;
    [System.NonSerialized] private double value_FiberPR;
    [System.NonSerialized] private double value_FiberSM;
    [System.NonSerialized] private double value_FiberTE;
    [System.NonSerialized] private double value_FiberME;
    [System.NonSerialized] private double value_FiberDensity;
    [System.NonSerialized] private double value_CompositeDensity;
    [System.NonSerialized] private double value_YM1;
    [System.NonSerialized] private double value_YM2;
    [System.NonSerialized] private double value_PR12;
    [System.NonSerialized] private double value_PR21;
    [System.NonSerialized] private double value_SM12;
    [System.NonSerialized] private double value_TE1;
    [System.NonSerialized] private double value_TE2;
    [System.NonSerialized] private double value_ME1;
    [System.NonSerialized] private double value_ME2;
    [System.NonSerialized] private bool calc = true;



    private void Update()
    {

        if (allItemsOfLamina.offsetMin.y < 0.0f)
        {

            allItemsOfLamina.offsetMin = new Vector2(allItemsOfLamina.offsetMin.x, 0.0f);
            allItemsOfLamina.offsetMax = new Vector2(allItemsOfLamina.offsetMax.x, 0.0f);


        }
        else if (allItemsOfLamina.offsetMin.y > 1350.0f)
        {
            allItemsOfLamina.offsetMin = new Vector2(allItemsOfLamina.offsetMin.x, 1350.0f);
            allItemsOfLamina.offsetMax = new Vector2(allItemsOfLamina.offsetMax.x, 1350.0f);
        }
        if (allItemsOfResult.offsetMin.y < 0.0f)
        {

            allItemsOfResult.offsetMin = new Vector2(allItemsOfResult.offsetMin.x, 0.0f);
            allItemsOfResult.offsetMax = new Vector2(allItemsOfResult.offsetMax.x, 0.0f);


        }
        else if (allItemsOfResult.offsetMin.y > 750.0f)
        {
            allItemsOfResult.offsetMin = new Vector2(allItemsOfResult.offsetMin.x, 750.0f);
            allItemsOfResult.offsetMax = new Vector2(allItemsOfResult.offsetMax.x, 750.0f);
        }
    }
    private double GetValue(TMPro.TMP_InputField tmp)
    {
        string text = tmp.text;
        //text = text.Replace(',', '.'); // valueları verince tersini yapmak gerekebilir
        double.TryParse(text, out double value);
        return value;

    }
    /*
    private double SetValue(double value)
    {
        string text = tmp.text;
        text = text.Replace('.', ','); // 
        double.TryParse(text, out double value);
        return value;

    }*/
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
    private bool IsNullOrEmptyOptional(TMPro.TMP_InputField tmp)
    {
        if (string.IsNullOrEmpty(tmp.text))
        {
            tmp.image.color = Color.yellow;
            return true;
        }
        else
        {
            tmp.image.color = Color.white;
            return false;
        }
    }

    public void Calculate()
    {
        calc = true;
        IsNullOrEmpty(MatrixYM)  ;
        IsNullOrEmpty(MatrixPR)  ;
        IsNullOrEmpty(MatrixVF)  ;
        IsNullOrEmpty(MatrixSM)  ;
        IsNullOrEmpty(FiberYM)   ;
        IsNullOrEmpty(FiberVF)   ;
        IsNullOrEmpty(FiberPR)   ;
        IsNullOrEmpty(FiberSM);
        if (!IsNullOrEmpty(MatrixYM) &&
            !IsNullOrEmpty(MatrixPR) &&
            !IsNullOrEmpty(MatrixVF) &&
            !IsNullOrEmpty(MatrixSM) &&
            !IsNullOrEmpty(FiberYM) &&
            !IsNullOrEmpty(FiberVF) &&
            !IsNullOrEmpty(FiberPR) &&
            !IsNullOrEmpty(FiberSM)
           )
        {
            value_MatrixYM = GetValue(MatrixYM);
            value_MatrixVF = GetValue(MatrixVF);
            value_MatrixPR = GetValue(MatrixPR);
            value_MatrixSM = GetValue(MatrixSM);

            value_FiberYM = GetValue(FiberYM);
            value_FiberVF = GetValue(FiberVF);
            value_FiberPR = GetValue(FiberPR);
            value_FiberSM = GetValue(FiberSM);

        }
        else
        {
            calc = false;
        }

        IsNullOrEmptyOptional(MatrixTE)        ;
        IsNullOrEmptyOptional(MatrixME)        ;
        IsNullOrEmptyOptional(MatrixDensity)   ;
        IsNullOrEmptyOptional(FiberDensity)    ;
        IsNullOrEmptyOptional(FiberTE);
        IsNullOrEmptyOptional(FiberME);
        if (!IsNullOrEmptyOptional(MatrixTE) &&
            !IsNullOrEmptyOptional(MatrixME) &&
            !IsNullOrEmptyOptional(MatrixDensity) &&
            !IsNullOrEmptyOptional(FiberDensity) &&
            !IsNullOrEmptyOptional(FiberTE) &&
            !IsNullOrEmptyOptional(FiberME)
            )
        {
            value_MatrixTE = GetValue(MatrixTE);
            value_MatrixME = GetValue(MatrixME);
            value_MatrixDensity = GetValue(MatrixDensity);
            value_FiberDensity = GetValue(FiberDensity);
            value_FiberTE = GetValue(FiberTE);
            value_FiberME = GetValue(FiberME);

        }



        // Elasticity Modulus Local Axis 1 > E1= Ef*Vf  + Em*Vm
        value_YM1 = (value_MatrixYM * value_MatrixVF) + (value_FiberYM * value_FiberVF);
        YM1.SetText(value_YM1.ToString());

        // Elasticity Modulus Local Axis 2 > E2 = Ef*Em /(Ef*Vm +  Em*Vf)
        value_YM2 = (value_MatrixYM * value_FiberYM) / ((value_FiberYM * value_MatrixVF) + (value_MatrixYM * value_FiberVF));
        YM2.SetText(value_YM2.ToString());


        // Shear Modulus   > G12=  Gf*Gm/(Gf*Vm +Gm*Vf)
        value_SM12 = (value_MatrixSM * value_FiberSM) / ((value_FiberSM * value_MatrixVF) + (value_MatrixSM * value_FiberVF));
        SM12.SetText(value_SM12.ToString());

        // Major Poisson's Ratio > v12 = vfVf + vmVm
        value_PR12 = (value_MatrixPR * value_MatrixVF) + (value_FiberPR * value_FiberVF);
        PR12.SetText(value_PR12.ToString());

        // Minor Poisson's Ratio > v21 = v12*(E2/E1) 
        value_PR21 = value_PR12 * (value_YM2 / value_YM1);
        PR21.SetText(value_PR21.ToString());
        if (calc)
        {
            //dc = dm*Vm + df*Vf
            value_CompositeDensity = (value_MatrixDensity * value_MatrixVF) + (value_FiberDensity * value_FiberVF);

            // Coeffcient of Thermal Expansion1 > α1 = αfEfVf + αmEm(1-Vf)  / (EfVf + Em(1-Vf))
            value_TE1 = ((value_FiberTE * value_FiberYM * value_FiberVF) + (value_MatrixTE * value_MatrixYM * (1 - value_FiberVF))) / ((value_FiberYM * value_FiberVF) + (value_MatrixYM * (1 - value_FiberVF)));
            TE1.SetText(value_TE1.ToString());

            // Coeffcient of Thermal Expansion2 > α2 = (1 +vf)*αfVf + (1 +vm)αm(1-Vf) - α1v12
            value_TE2 = (1 + value_FiberPR) * (value_FiberTE * value_FiberVF) + ((1 + value_MatrixPR) * value_MatrixTE * (1 - value_FiberVF)) - (value_TE1 * value_PR12);
            TE2.SetText(value_TE2.ToString());

            // Coeffcient of Moisture Expansion 1 > β1 = (βm*Em*dc)/(E1dm)
            value_ME1 = (value_MatrixME * value_MatrixYM * value_CompositeDensity) / (value_YM1 * value_MatrixDensity);
            ME1.SetText(value_ME1.ToString());

            // Coeffcient of Moisture Expansion 2 > β2 = (βm*Em*dc)/(E1dm)
            value_ME2 = (((1 + value_MatrixPR) * (value_MatrixME * value_CompositeDensity)) / value_MatrixDensity) - (value_ME1 * value_PR12);
            ME2.SetText(value_ME2.ToString());
            GameManager.THIS.microMechanicsMenu.resultScreen.SetActive(true);
            GameManager.THIS.dropDownMenuButton.SetActive(false);
        }
    }
}
       
    