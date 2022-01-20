using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacromechanicsAnalysisofLaminate : MonoBehaviour
{
    [SerializeField] public TMPro.TMP_InputField numberOfLaminas = null;
    [SerializeField] public List<TMPro.TMP_InputField> transformationAngleList = null;
    [SerializeField] public List<TMPro.TMP_InputField> thicknessList = null;
    [SerializeField] private TMPro.TextMeshProUGUI Q;
    [SerializeField] private TMPro.TextMeshProUGUI S;
    [SerializeField] private TMPro.TextMeshProUGUI GlobalStrain;
    [SerializeField] private TMPro.TextMeshProUGUI LocalStrain;
    [SerializeField] private TMPro.TextMeshProUGUI Thermal;
    [SerializeField] private GameObject resultScreenHolder;
    [SerializeField] private RectTransform allItemsOfLamina;
    [SerializeField] private RectTransform allItemsOfResult;

    [System.NonSerialized] private int number = 0;
    [System.NonSerialized] private List<double> thicknessListDouble;
    [System.NonSerialized] private List<int> transformationAngleListInt;
    [System.NonSerialized] private List<double> q11List;
    [System.NonSerialized] private List<double> q12q21List;
    //[System.NonSerialized] private List<double> q13q31List; 0
    [System.NonSerialized] private List<double> q22List;
    //[System.NonSerialized] private List<double> q23q32List; 0
    [System.NonSerialized] private List<double> q33List;
    [System.NonSerialized] private List<double> q11BarList;
    [System.NonSerialized] private List<double> q12q21BarList;
    [System.NonSerialized] private List<double> q13q31BarList; 
    [System.NonSerialized] private List<double> q22BarList;
    [System.NonSerialized] private List<double> q23q32BarList; 
    [System.NonSerialized] private List<double> q33BarList;
    [System.NonSerialized] private List<double> zList;
    [System.NonSerialized] private double[][] aMatrix;
    [System.NonSerialized] private double[][] bMatrix;
    [System.NonSerialized] private double[][] dMatrix;
    [System.NonSerialized] private double[][] aMultiplierMatrix;
    [System.NonSerialized] private double[][] bMultiplierMatrix;
    [System.NonSerialized] private double[][] dMultiplierMatrix;
    [System.NonSerialized] private double[][] stiffnessMatrix;
    [System.NonSerialized] private double[][] complianceMatrix;
    [System.NonSerialized] private double[][] aInMatrix;
    [System.NonSerialized] private double[][] a1Matrix;
    [System.NonSerialized] private double[][] bInMatrix;
    [System.NonSerialized] private double[][] aMinusMatrix;
    [System.NonSerialized] private double[][] calcMinusMatrix;
    [System.NonSerialized] private double[][] b1Matrix;
    [System.NonSerialized] private double[][] cInMatrix;
    [System.NonSerialized] private double[][] c1Matrix;
    [System.NonSerialized] private double[][] dInMatrix;
    [System.NonSerialized] private double[][] d1Matrix;
    [System.NonSerialized] private double[][] mMatrix;
    [System.NonSerialized] private double[][] nMatrix;
    [System.NonSerialized] private double[][] EEMatrix;
    [System.NonSerialized] private double[][] KKMatrix;
    [System.NonSerialized] private double[][] EEKKCalcMatrix;
    [System.NonSerialized] private double a11;
    [System.NonSerialized] private double a12a21;
    [System.NonSerialized] private double a13a31;
    [System.NonSerialized] private double a22;
    [System.NonSerialized] private double a23a32;
    [System.NonSerialized] private double a33;
    [System.NonSerialized] private double b11;
    [System.NonSerialized] private double b12;
    [System.NonSerialized] private double b13b31;
    [System.NonSerialized] private double b12b21;
    [System.NonSerialized] private double b22;
    [System.NonSerialized] private double b23b32;
    [System.NonSerialized] private double b33;
    [System.NonSerialized] private double d11;
    [System.NonSerialized] private double d12;
    [System.NonSerialized] private double d13d31;
    [System.NonSerialized] private double d12d21;
    [System.NonSerialized] private double d22;
    [System.NonSerialized] private double d23d32;
    [System.NonSerialized] private double d33;
    private void Awake()
    {
        number = 0;
    }
    private void Update()
    {
        if (allItemsOfLamina.offsetMin.y < 0.0f)
        {
            
            allItemsOfLamina.offsetMin = new Vector2(allItemsOfLamina.offsetMin.x, 0.0f);
            allItemsOfLamina.offsetMax = new Vector2(allItemsOfLamina.offsetMax.x, 0.0f);
          
            
        }
        else if (allItemsOfLamina.offsetMin.y > 50.0f + ((float)number*250))
        {
            allItemsOfLamina.offsetMin = new Vector2(allItemsOfLamina.offsetMin.x, 50.0f + ((float)number * 250));
            allItemsOfLamina.offsetMax = new Vector2(allItemsOfLamina.offsetMax.x, 50.0f + ((float)number * 250));
        }
        if (allItemsOfResult.offsetMin.y < 0.0f)
        {

            allItemsOfResult.offsetMin = new Vector2(allItemsOfResult.offsetMin.x, 0.0f);
            allItemsOfResult.offsetMax = new Vector2(allItemsOfResult.offsetMax.x, 0.0f);


        }
        else if (allItemsOfResult.offsetMin.y > 2000.0f)
        {
            allItemsOfResult.offsetMin = new Vector2(allItemsOfResult.offsetMin.x, 2000.0f);
            allItemsOfResult.offsetMax = new Vector2(allItemsOfResult.offsetMax.x, 2000.0f);
        }
    }
    /*private void Awake()
    {
        thicknessListDouble = new List<double>();
        transformationAngleListInt = new List<int> ();
        q11List = new List<double>(); 
        q12q21List = new List<double>();
        q22List = new List<double>();
        q33List = new List<double>();
        q11BarList = new List<double>();
        q12q21BarList = new List<double>();
        q13q31BarList = new List<double>();
        q22BarList = new List<double>();
        q23q32BarList = new List<double>();
        q33BarList = new List<double>();
        zList = new List<double>();
    }*/

    private int GetValueInt(TMPro.TMP_InputField tmp)
    {
        string text = tmp.text;
        //text = text.Replace(',', '.'); // valuelarý verince tersini yapmak gerekebilira
        int.TryParse(text, out int value);
        return value;

    }
    private double GetValue(TMPro.TMP_InputField tmp)
    {
        string text = tmp.text;
    //text = text.Replace(',', '.'); // valuelarý verince tersini yapmak gerekebilir
        double.TryParse(text, out double value);
        return value;
    }
    public void ShowMaterialInputs()
    {
        if (string.IsNullOrEmpty(numberOfLaminas.text))
        {
            number = 5;
            numberOfLaminas.text = "5";
        }
        else
        {
            number = GetValueInt(numberOfLaminas);
            if (number < 2)
            {
                number = 2;
                numberOfLaminas.text = "2";
            }
            else if (number > 10)
            {
                number = 10;
                numberOfLaminas.text = "10";
            }
        }
        for (int i = 0; i < GameManager.THIS.dropDownMenuLaminate.materialList.Count; i++)
        {
            if (i < number)
            {
                GameManager.THIS.dropDownMenuLaminate.materialList[i].SetActive(true);
            }
            else
            {
                GameManager.THIS.dropDownMenuLaminate.materialList[i].SetActive(false);
            }
        }
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
    private void GetValuesOfListAngle(List<TMPro.TMP_InputField> tmpList)
    {
        for (int i = 0; i < number; i++)
        {
            int angle = 45;
            if (!string.IsNullOrEmpty(tmpList[i].text))
            {
                angle = GetValueInt(tmpList[i]);
            }
            else
            {
                tmpList[i].text = "45";
            }
            transformationAngleListInt.Add(angle);
        }
       
       
    }
    private void GetValuesOfListThickness(List<TMPro.TMP_InputField> tmpList)
    {
        for (int i = 0; i < number; i++)
        {
            double thickness = 6.0/(double)number;
            if (!string.IsNullOrEmpty(tmpList[i].text))
            {
                thickness = GetValue(tmpList[i]);
            }
            else
            {
                tmpList[i].text = thickness.ToString("F3");
            }
            thicknessListDouble.Add(thickness);
        }

    }
    private void GetQ11ListValues()
    {
        double q11;
        double E1;
        double E2;
        double v12;
        double v21;

        for (int i = 0; i < number; i++)
        {
            E1 = GameManager.THIS.dataManager.materialPropertiesList[GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value].E1;
            E2 = GameManager.THIS.dataManager.materialPropertiesList[GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value].E2;
            v12 = GameManager.THIS.dataManager.materialPropertiesList[GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value].v12;
            v21 = (E2 / E1) * v12;
            q11 = (E1 / (1 - v12 * v21));
            
            q11List.Add(q11);
            //Debug.Log(GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value);
        }
        /*for (int i = 0; i <q11List.Count; i++)
        {
            Debug.Log(q11List[i]);
        }*/
    }
    private void GetQ12Q21ListValues()
    {
        double q12;
        double E1;
        double E2;
        double v12;
        double v21;

        for (int i = 0; i < number; i++)
        {
            E1 = GameManager.THIS.dataManager.materialPropertiesList[GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value].E1;
            E2 = GameManager.THIS.dataManager.materialPropertiesList[GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value].E2;
            v12 = GameManager.THIS.dataManager.materialPropertiesList[GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value].v12;
            v21 = (E2 / E1) * v12;
            q12 = (v12 * E2 / (1 - v12 * v21));
            q12q21List.Add(q12);
            
        }
    }
    private void GetQ22ListValues()
    {
        double q22;
        double E1;
        double E2;
        double v12;
        double v21;

        for (int i = 0; i < number; i++)
        {
            E1 = GameManager.THIS.dataManager.materialPropertiesList[GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value].E1;
            E2 = GameManager.THIS.dataManager.materialPropertiesList[GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value].E2;
            v12 = GameManager.THIS.dataManager.materialPropertiesList[GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value].v12;
            v21 = (E2 / E1) * v12;
            q22 = (E2 / (1 - v12 * v21));
            
            q22List.Add(q22);
        }
    }
    private void GetQ33ListValues()
    {
        double q33;
        double G1;

        for (int i = 0; i < number; i++)
        {
            G1 = GameManager.THIS.dataManager.materialPropertiesList[GameManager.THIS.dropDownMenuLaminate.dropDownList[i].value].G12;
            q33 = G1;
            q33List.Add(q33);
        }
    }
    private void GetQ11BarListValues()
    {
        double q11bar;
        float cos;
        float sin;
        double q66;
        for (int i = 0; i< number; i++)
        {
            q66 = q33List[i];
            cos = Mathf.Cos((float)transformationAngleListInt[i] * Mathf.PI / 180.0f);
            sin = Mathf.Sin((float)transformationAngleListInt[i] * Mathf.PI / 180.0f);
            q11bar = q11List[i] * Mathf.Pow(cos, 4) + 2 * (q12q21List[i] + 2 * q66) * Mathf.Pow(sin, 2) * Mathf.Pow(cos, 2) + q22List[i] * Mathf.Pow(sin, 4);
            q11BarList.Add(q11bar);
           
        }
        
    }
    private void GetQ12Q21BarListValues()
    {
        double q12bar;
        float cos;
        float sin;
        double q66;
        for (int i = 0; i < number; i++)
        {
            q66 = q33List[i];
            cos = Mathf.Cos((float)transformationAngleListInt[i] * Mathf.PI / 180.0f);
            sin = Mathf.Sin((float)transformationAngleListInt[i] * Mathf.PI / 180.0f);
            q12bar = q12q21List[i] * (Mathf.Pow(sin, 4) + Mathf.Pow(cos, 4)) + (q11List[i] + q22List[i] - (4 * q66)) * Mathf.Pow(sin, 2) * Mathf.Pow(cos, 2);
            q12q21BarList.Add(q12bar);
        }
        
    }
    private void GetQ13Q31BarListValues()
    {
        double q13bar;
        float cos;
        float sin;
        double q66;
        for (int i = 0; i < number; i++)
        {
            q66 = q33List[i];
            cos = Mathf.Cos(transformationAngleListInt[i] * Mathf.PI / 180.0f);
            sin = Mathf.Sin(transformationAngleListInt[i] * Mathf.PI / 180.0f);
            q13bar = (q11List[i] - q12q21List[i] - (2 * q66)) * sin * Mathf.Pow(cos, 3) + (q12q21List[i] - q22List[i] - (2 * q66)) * Mathf.Pow(sin, 3) * cos;
            q13q31BarList.Add(q13bar);
        }
        

    }
    private void GetQ22BarListValues()
    {
        double q22bar;
        float cos;
        float sin;
        double q66;
        for (int i = 0; i < number; i++)
        {
            q66 = q33List[i];
            cos = Mathf.Cos(transformationAngleListInt[i] * Mathf.PI / 180.0f);
            sin = Mathf.Sin(transformationAngleListInt[i] * Mathf.PI / 180.0f);
            q22bar = (q11List[i] * Mathf.Pow(sin, 4)) + 2 * (q12q21List[i] + 2 * q66) * Mathf.Pow(sin, 2) * Mathf.Pow(cos, 2) + q22List[i] * Mathf.Pow(cos, 4);
            q22BarList.Add(q22bar);
        }

    }
    private void GetQ23Q32BarListValues()
    {
        double q23bar;
        float cos;
        float sin;
        double q66;
        for (int i = 0; i < number; i++)
        {
            q66 = q33List[i];
            cos = Mathf.Cos(transformationAngleListInt[i] * Mathf.PI / 180.0f);
            sin = Mathf.Sin(transformationAngleListInt[i] * Mathf.PI / 180.0f);
            q23bar = (q11List[i] - q12q21List[i] - 2 * q66) * cos * Mathf.Pow(sin, 3) + (q12q21List[i] - q22List[i] - 2 * q66) * Mathf.Pow(cos, 3) * sin;
            q23q32BarList.Add(q23bar);
        }
        

    }
    private void GetQ33BarListValues()
    {
        double q33bar;
        float cos;
        float sin;
        double q66;
        for (int i = 0; i < number; i++)
        {
            q66 = q33List[i];
            cos = Mathf.Cos(transformationAngleListInt[i] * Mathf.PI / 180.0f);
            sin = Mathf.Sin(transformationAngleListInt[i] * Mathf.PI / 180.0f);
            q33bar = (q11List[i] + q22List[i] - 2 * q12q21List[i] - 2 * q66) * Mathf.Pow(sin, 2) * Mathf.Pow(cos, 2) + q66 * (Mathf.Pow(sin, 4) + Mathf.Pow(cos, 4));
            q33BarList.Add(q33bar);
        }
        

    }
    private void GetZList()
    {
        double totalThickness = 0.0;
        for (int i = 0; i < number; i++)
        {
            totalThickness += thicknessListDouble[i];
        }
        for (int i = 0; i < number; i++)
        {
            zList.Add((i - (float)number / 2 ) * totalThickness/1000);
        }
        Debug.Log(totalThickness);
            
        zList.Add((float)number * totalThickness / 2000);
        foreach (double d in zList)
        {
            Debug.Log(d);
        }
        /*double totalThickness = 0.0;
        for (int i = 0; i < number; i++)
        {
            totalThickness += thicknessListDouble[i];
        }
        //zList[0] = -totalThickness / 2.0;
        zList.Add(-totalThickness / 2.0);
        
        double thickness = 0.0;
        for (int i = 1; i < number; i++)
        {
            //zList[i] = thickness + thicknessListDouble[i-1];
            zList.Add(thickness + thicknessListDouble[i-1]);
            thickness += zList[i-1];
        }
        //zList[number] = totalThickness / 2.0;
        zList.Add(totalThickness / 2.0);
        //zList[number] = number * thicknessListDouble[0] / 2 hangi thickness*/


    }
    private void CalculateABDMatrixValues()
    {
        b11 = 0.0; b12b21 = 0.0; b13b31 = 0.0; b22 = 0.0; b23b32 = 0.0; b33 = 0.0;
        a11 = 0.0; a12a21 = 0.0; a13a31 = 0.0; a22 = 0.0; a23a32 = 0.0; a33 = 0.0;
        d11 = 0.0; d12d21 = 0.0; d13d31 = 0.0; d22 = 0.0; d23d32 = 0.0; d33 = 0.0;
        for (int i = 0; i < number; i++)
        {
            a11 = q11BarList[i] * (zList[i + 1]- zList[i]) + a11;
            a12a21 = q12q21BarList[i] * (zList[i + 1]- zList[i]) + a12a21;
            a13a31 = q13q31BarList[i] * (zList[i + 1]- zList[i]) + a13a31;
            a22 = q22BarList[i] * (zList[i + 1]- zList[i]) + a22;
            a23a32 = q23q32BarList[i] * (zList[i + 1] - zList[i]) + a23a32;
            a33 = q33BarList[i] * (zList[i + 1] - zList[i]) + a33;
            b11 = 0.5 * q11BarList[i] * (Mathf.Pow((float)zList[i + 1], 2) - Mathf.Pow((float)zList[i], 2)) + b11;
            b12b21 = 0.5 * q12q21BarList[i] * (Mathf.Pow((float)zList[i + 1], 2) - Mathf.Pow((float)zList[i], 2)) + b12b21;
            b13b31 = 0.5 * q13q31BarList[i] * (Mathf.Pow((float)zList[i + 1], 2) - Mathf.Pow((float)zList[i], 2)) + b13b31;
            b22 = 0.5 * q22BarList[i] * (Mathf.Pow((float)zList[i + 1],2) - Mathf.Pow((float)zList[i], 2)) + b22;
            b23b32 = 0.5 * q23q32BarList[i] * (Mathf.Pow((float)zList[i + 1], 2) - Mathf.Pow((float)zList[i], 2)) + b23b32;
            b33 = 0.5 * q33BarList[i] * (Mathf.Pow((float)zList[i + 1], 2) - Mathf.Pow((float)zList[i], 2)) + b33;
            d11 = (1.0 / 3.0) * q11BarList[i] * (Mathf.Pow((float)zList[i + 1], 3) - Mathf.Pow((float)zList[i], 3)) + d11;
            d12d21 = (1.0 / 3.0) * q12q21BarList[i] * (Mathf.Pow((float)zList[i + 1], 3) - Mathf.Pow((float)zList[i], 3)) + d12d21;
            d13d31 = (1.0 / 3.0) * q13q31BarList[i] * (Mathf.Pow((float)zList[i + 1], 3) - Mathf.Pow((float)zList[i], 3)) + d13d31;
            d22 = (1.0 / 3.0) * q22BarList[i] * (Mathf.Pow((float)zList[i + 1], 3) - Mathf.Pow((float)zList[i], 3)) + d22;
            d23d32 = (1.0 / 3.0) * q23q32BarList[i] * (Mathf.Pow((float)zList[i + 1], 3) - Mathf.Pow((float)zList[i], 3)) + d23d32;
            d33 = (1.0 / 3.0) * q33BarList[i] * (Mathf.Pow((float)zList[i + 1], 3) - Mathf.Pow((float)zList[i], 3)) + d33;
        }
       
    }
    private void CreateAMatrix()
    {
        aMatrix[0][0] = a11;
        aMatrix[0][1] = a12a21;
        aMatrix[0][2] = a13a31;
        aMatrix[1][0] = a12a21;
        aMatrix[1][1] = a22;
        aMatrix[1][2] = a23a32;
        aMatrix[2][0] = a13a31;
        aMatrix[2][1] = a23a32;
        aMatrix[2][2] = a33;
    }
    private void CreateBMatrix()
    {
        bMatrix[0][0] = b11;
        bMatrix[0][1] = b12b21;
        bMatrix[0][2] = b13b31;
        bMatrix[1][0] = b12b21;
        bMatrix[1][1] = b22;
        bMatrix[1][2] = b23b32;
        bMatrix[2][0] = b13b31;
        bMatrix[2][1] = b23b32;
        bMatrix[2][2] = b33;
    }
    private void CreateDMatrix()
    {
        dMatrix[0][0] = d11;
        dMatrix[0][1] = d12d21;
        dMatrix[0][2] = d13d31;
        dMatrix[1][0] = d12d21;
        dMatrix[1][1] = d22;
        dMatrix[1][2] = d23d32;
        dMatrix[2][0] = d13d31;
        dMatrix[2][1] = d23d32;
        dMatrix[2][2] = d33;
    }
    public void Calculate()
    {
        if (number > 1) {
            resultScreenHolder.SetActive(true);
            thicknessListDouble = new List<double>();
            transformationAngleListInt = new List<int>();
            q11List = new List<double>();
            q12q21List = new List<double>();
            q22List = new List<double>();
            q33List = new List<double>();
            q11BarList = new List<double>();
            q12q21BarList = new List<double>();
            q13q31BarList = new List<double>();
            q22BarList = new List<double>();
            q23q32BarList = new List<double>();
            q33BarList = new List<double>();
            zList = new List<double>();
           
            
            GetValuesOfListAngle(transformationAngleList);
            GetValuesOfListThickness(thicknessList);
            GetQ11ListValues();
            GetQ12Q21ListValues();
            GetQ22ListValues();
            GetQ33ListValues();
            GetQ11BarListValues();
            GetQ12Q21BarListValues();
            GetQ13Q31BarListValues();
            GetQ22BarListValues();
            GetQ23Q32BarListValues();
            GetQ33BarListValues();
            aMatrix = MatrixCalc.MatrixCreate(3, 3);
            bMatrix = MatrixCalc.MatrixCreate(3, 3);
            dMatrix = MatrixCalc.MatrixCreate(3, 3);
            aInMatrix = MatrixCalc.MatrixCreate(3, 3);
            a1Matrix = MatrixCalc.MatrixCreate(3, 3);
            bInMatrix = MatrixCalc.MatrixCreate(3, 3);
            aMinusMatrix = MatrixCalc.MatrixCreate(3, 3);
            calcMinusMatrix = MatrixCalc.MatrixCreate(3, 3);
            b1Matrix = MatrixCalc.MatrixCreate(3, 3);
            dInMatrix = MatrixCalc.MatrixCreate(3, 3);
            d1Matrix = MatrixCalc.MatrixCreate(3, 3);
            cInMatrix = MatrixCalc.MatrixCreate(3, 3);
            c1Matrix = MatrixCalc.MatrixCreate(3, 3);
            stiffnessMatrix = MatrixCalc.MatrixCreate(6, 6);
            complianceMatrix = MatrixCalc.MatrixCreate(6, 6);
            nMatrix = MatrixCalc.MatrixCreate(3, 1);
            mMatrix = MatrixCalc.MatrixCreate(3, 1);
            EEMatrix = MatrixCalc.MatrixCreate(3, 1);
            KKMatrix = MatrixCalc.MatrixCreate(3, 1);
            EEKKCalcMatrix = MatrixCalc.MatrixCreate(3, 1);
            GetZList();
            CalculateABDMatrixValues();
            CreateAMatrix();
            aInMatrix = MatrixCalc.MatrixInverse(aMatrix);
            CreateBMatrix();
            for (int i = 0; i<3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    aMinusMatrix[i][j] = -1.0 * aMatrix[i][j];
                }
            }
            bInMatrix = MatrixCalc.MatrixProduct(aMinusMatrix, bMatrix);
            CreateDMatrix();
            cInMatrix = MatrixCalc.MatrixProduct(bMatrix, aInMatrix);
            calcMinusMatrix = MatrixCalc.MatrixProduct(bMatrix, aInMatrix);
            calcMinusMatrix = MatrixCalc.MatrixProduct(calcMinusMatrix, bMatrix);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    calcMinusMatrix[i][j] = -1.0 * calcMinusMatrix[i][j];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    dInMatrix[i][j] = dMatrix[i][j] - calcMinusMatrix[i][j];
                }
            }
            a1Matrix = MatrixCalc.MatrixInverse(dInMatrix);
            a1Matrix = MatrixCalc.MatrixProduct(bInMatrix, a1Matrix);
            a1Matrix = MatrixCalc.MatrixProduct(a1Matrix, cInMatrix);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    a1Matrix[i][j] = aInMatrix[i][j] - a1Matrix[i][j];
                }
            }
            b1Matrix = MatrixCalc.MatrixInverse(dInMatrix);
            b1Matrix = MatrixCalc.MatrixProduct(bInMatrix, b1Matrix);
            c1Matrix = MatrixCalc.MatrixInverse(dInMatrix);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    c1Matrix[i][j] = -1.0* c1Matrix[i][j];
                }
            }
            c1Matrix = MatrixCalc.MatrixProduct(c1Matrix,cInMatrix);
            //NMatrix
            nMatrix[0][0] = 250.0;
            nMatrix[1][0] = 250.0;
            nMatrix[2][0] = 250.0;
            //MMatrix
            mMatrix[0][0] = 250.0;
            mMatrix[1][0] = 250.0;
            mMatrix[2][0] = 250.0;

            EEMatrix = MatrixCalc.MatrixProduct(a1Matrix, nMatrix);
            EEKKCalcMatrix = MatrixCalc.MatrixProduct(b1Matrix, mMatrix);
            for (int i = 0; i < 3; i++)
            {
               
                    EEMatrix[i][0] += EEKKCalcMatrix[i][0];
            }
            KKMatrix = MatrixCalc.MatrixProduct(c1Matrix, nMatrix);
            EEKKCalcMatrix = MatrixCalc.MatrixInverse(dInMatrix);
            EEKKCalcMatrix = MatrixCalc.MatrixProduct(EEKKCalcMatrix,mMatrix);
            for (int i = 0; i < 3; i++)
            {
                
                    KKMatrix[i][0] += EEKKCalcMatrix[i][0];
            }
            /*bool symetric = true;
            
            for (int i = 0; i<number/2; i++)
            {
                if(transformationAngleListInt[i] - transformationAngleListInt[number-1-i] == 0)
                {
                    continue;
                }
                else
                {
                    symetric = false;
                    break;
                }
            }*/
            
                /*if (number % 2 == 0)
                {
                    aMultiplierMatrix = MatrixCalc.MatrixCreate(3, 3);
                    bMultiplierMatrix = MatrixCalc.MatrixCreate(3, 3);
                    dMultiplierMatrix = MatrixCalc.MatrixCreate(3, 3);
                    aMultiplierMatrix[0][0] = 1.0;
                    aMultiplierMatrix[0][1] = 1.0;
                    aMultiplierMatrix[0][2] = 0.0;
                    aMultiplierMatrix[1][0] = 1.0;
                    aMultiplierMatrix[1][1] = 1.0;
                    aMultiplierMatrix[1][2] = 0.0;
                    aMultiplierMatrix[2][0] = 0.0;
                    aMultiplierMatrix[2][1] = 0.0;
                    aMultiplierMatrix[2][2] = 1.0;
                    bMultiplierMatrix[0][0] = 0.0;
                    bMultiplierMatrix[0][1] = 0.0;
                    bMultiplierMatrix[0][2] = 0.0;
                    bMultiplierMatrix[1][0] = 0.0;
                    bMultiplierMatrix[1][1] = 0.0;
                    bMultiplierMatrix[1][2] = 0.0;
                    bMultiplierMatrix[2][0] = 0.0;
                    bMultiplierMatrix[2][1] = 0.0;
                    bMultiplierMatrix[2][2] = 0.0;
                    dMultiplierMatrix[0][0] = 1.0;
                    dMultiplierMatrix[0][1] = 1.0;
                    dMultiplierMatrix[0][2] = 1.0;
                    dMultiplierMatrix[1][0] = 1.0;
                    dMultiplierMatrix[1][1] = 1.0;
                    dMultiplierMatrix[1][2] = 1.0;
                    dMultiplierMatrix[2][0] = 1.0;
                    dMultiplierMatrix[2][1] = 1.0;
                    dMultiplierMatrix[2][2] = 1.0;
                    aMatrix = MatrixCalc.MatrixProduct(aMatrix, aMultiplierMatrix);
                    bMatrix = MatrixCalc.MatrixProduct(bMatrix, bMultiplierMatrix);
                    dMatrix = MatrixCalc.MatrixProduct(dMatrix, dMultiplierMatrix);
                }*/
             Q.text = "[ " + aMatrix[0][0].ToString("F4") + " " + aMatrix[0][1].ToString("F4") + " " + aMatrix[0][2].ToString("F4") + bMatrix[0][0].ToString("F4") + " " + bMatrix[0][1].ToString("F4") + " " + bMatrix[0][2].ToString("F4") + " ]\n" +
                "[ " + aMatrix[1][0].ToString("F4") + " " + aMatrix[1][1].ToString("F4") + " " + aMatrix[1][2].ToString("F4") + bMatrix[1][0].ToString("F4") + " " + bMatrix[1][1].ToString("F4") + " " + bMatrix[1][2].ToString("F4") + " ]\n" +
                "[ " + aMatrix[2][0].ToString("F4") + " " + aMatrix[2][1].ToString("F4") + " " + aMatrix[2][2].ToString("F4") + bMatrix[2][0].ToString("F4") + " " + bMatrix[2][1].ToString("F4") + " " + bMatrix[2][2].ToString("F4") + " ]\n" +
                "[ " + bMatrix[0][0].ToString("F4") + " " + bMatrix[0][1].ToString("F4") + " " + bMatrix[0][2].ToString("F4") + dMatrix[0][0].ToString("F6") + " " + dMatrix[0][1].ToString("F6") + " " + dMatrix[0][2].ToString("F6")+ " ]\n" +
                "[ " +bMatrix[1][0].ToString("F4") + " " + bMatrix[1][1].ToString("F4") + " " + bMatrix[1][2].ToString("F4") + dMatrix[1][0].ToString("F6") + " " + dMatrix[1][1].ToString("F6") + " " + dMatrix[1][2].ToString("F6")+ " ]\n" +
                "[ " + bMatrix[2][0].ToString("F4") + " " + bMatrix[2][1].ToString("F4") + " " + bMatrix[2][2].ToString("F4") + dMatrix[2][0].ToString("F6") + " " + dMatrix[2][1].ToString("F6") + " " + dMatrix[2][2].ToString("F6") + " ]";
            S.text = "[ " + aInMatrix[0][0].ToString("F4") + " " + aInMatrix[0][1].ToString("F4") + " " + aInMatrix[0][2].ToString("F4") + bInMatrix[0][0].ToString("F4") + " " + bInMatrix[0][1].ToString("F4") + " " + bInMatrix[0][2].ToString("F4") + " ]\n" +
                 "[ " + aInMatrix[1][0].ToString("F4") + " " + aInMatrix[1][1].ToString("F4") + " " + aInMatrix[1][2].ToString("F4") + bInMatrix[1][0].ToString("F4") + " " + bInMatrix[1][1].ToString("F4") + " " + bInMatrix[1][2].ToString("F4") + " ]\n" +
                 "[ " + aInMatrix[2][0].ToString("F4") + " " + aInMatrix[2][1].ToString("F4") + " " + aInMatrix[2][2].ToString("F4") + bInMatrix[2][0].ToString("F4") + " " + bInMatrix[2][1].ToString("F4") + " " + bInMatrix[2][2].ToString("F4") + " ]\n" +
                 "[ " + bInMatrix[0][0].ToString("F4") + " " + bInMatrix[0][1].ToString("F4") + " " + bInMatrix[0][2].ToString("F4") + dInMatrix[0][0].ToString("F6") + " " + dInMatrix[0][1].ToString("F6") + " " + dInMatrix[0][2].ToString("F6") + " ]\n" +
                 "[ " + bInMatrix[1][0].ToString("F4") + " " + bInMatrix[1][1].ToString("F4") + " " + bInMatrix[1][2].ToString("F4") + dInMatrix[1][0].ToString("F6") + " " + dInMatrix[1][1].ToString("F6") + " " + dInMatrix[1][2].ToString("F6") + " ]\n" +
                 "[ " + bInMatrix[2][0].ToString("F4") + " " + bInMatrix[2][1].ToString("F4") + " " + bInMatrix[2][2].ToString("F4") + dInMatrix[2][0].ToString("F6") + " " + dInMatrix[2][1].ToString("F6") + " " + dInMatrix[2][2].ToString("F6") + " ]";
            GlobalStrain.text = "[ " + EEMatrix[0][0].ToString("F4")+ " ]\n" +
               "[ " + EEMatrix[1][0].ToString("F4") + " ]\n" +
               "[ " + EEMatrix[2][0].ToString("F4") + " ]";

        }
    }

}
