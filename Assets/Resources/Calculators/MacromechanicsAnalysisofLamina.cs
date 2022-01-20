using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacromechanicsAnalysisofLamina : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] public TMPro.TMP_InputField inputE1;
    [SerializeField] public TMPro.TMP_InputField inputE2;
    [SerializeField] public TMPro.TMP_InputField inputG1;
    [SerializeField] public TMPro.TMP_InputField inputv12;
    [SerializeField] public TMPro.TMP_InputField inputv21;
    [SerializeField] public TMPro.TMP_InputField inputa1;
    [SerializeField] public TMPro.TMP_InputField inputa2;
    [SerializeField] public TMPro.TMP_InputField inputb1;
    [SerializeField] public TMPro.TMP_InputField inputb2;
    [SerializeField] public TMPro.TMP_InputField inputAngle;
    //
    [Header("")]
    [SerializeField] public GameObject resultScreen = null;
    [SerializeField] public RectTransform q11GraphContainer = null;
    [SerializeField] public RectTransform q12q21GraphContainer = null;
    [SerializeField] public RectTransform q13q31GraphContainer = null;
    [SerializeField] public RectTransform q22GraphContainer = null;
    [SerializeField] public RectTransform q23q32GraphContainer = null;
    [SerializeField] public RectTransform q33GraphContainer = null;
    [SerializeField] public GameObject circleImage = null;
    [SerializeField] private RectTransform allItemsOfLamina;
    [SerializeField] private RectTransform allItemsOfResult;

    [Header("Outputs")]
    [SerializeField] private TMPro.TextMeshProUGUI Q;
    [SerializeField] private TMPro.TextMeshProUGUI S;
    [SerializeField] private TMPro.TextMeshProUGUI QBar;
    [SerializeField] private TMPro.TextMeshProUGUI QBarText;
    [SerializeField] private TMPro.TextMeshProUGUI SBar;
    [SerializeField] private TMPro.TextMeshProUGUI SBarText;
    [SerializeField] private TMPro.TextMeshProUGUI Thermal;
    [SerializeField] private TMPro.TextMeshProUGUI ThermalText;
    [SerializeField] private TMPro.TextMeshProUGUI Q11Text;
    [SerializeField] private TMPro.TextMeshProUGUI Q12Text;
    [SerializeField] private TMPro.TextMeshProUGUI Q13Text;
    [SerializeField] private TMPro.TextMeshProUGUI Q22Text;
    [SerializeField] private TMPro.TextMeshProUGUI Q23Text;
    [SerializeField] private TMPro.TextMeshProUGUI Q33Text;

    [System.NonSerialized] private double E1;
    [System.NonSerialized] private double E2;
    [System.NonSerialized] private double v12;
    [System.NonSerialized] private double v21;
    [System.NonSerialized] private double G12;
    [System.NonSerialized] private double a1;
    [System.NonSerialized] private double a2;
    [System.NonSerialized] private double b1;
    [System.NonSerialized] private double b2;
    [System.NonSerialized] private int angle;
    [System.NonSerialized] private bool calc = true;
    [System.NonSerialized] private double t;
    [System.NonSerialized] private double q11;
    [System.NonSerialized] private double q12;
    [System.NonSerialized] private double q13;
    [System.NonSerialized] private double q21;
    [System.NonSerialized] private double q22;
    [System.NonSerialized] private double q23;
    [System.NonSerialized] private double q31;
    [System.NonSerialized] private double q32;
    [System.NonSerialized] private double q33;
    [System.NonSerialized] private double q66;
    [System.NonSerialized] private readonly double totST1=0.001;
    [System.NonSerialized] private readonly double totST2 =0.002;
    [System.NonSerialized] private readonly double totST3 =0.0005;
    //[System.NonSerialized] private readonly double temp=-120;
    [System.NonSerialized] private double q11bar;
    [System.NonSerialized] private double q12bar;
    [System.NonSerialized] private double q13bar;
    [System.NonSerialized] private double q21bar;
    [System.NonSerialized] private double q22bar;
    [System.NonSerialized] private double q23bar;
    [System.NonSerialized] private double q31bar;
    [System.NonSerialized] private double q32bar;
    [System.NonSerialized] private double q33bar;
    [System.NonSerialized] private double q66bar;
    [System.NonSerialized] private double q11barPicked;
    [System.NonSerialized] private double q12barPicked;
    [System.NonSerialized] private double q13barPicked;
    [System.NonSerialized] private double q21barPicked;
    [System.NonSerialized] private double q22barPicked;
    [System.NonSerialized] private double q23barPicked;
    [System.NonSerialized] private double q31barPicked;
    [System.NonSerialized] private double q32barPicked;
    [System.NonSerialized] private double q33barPicked;
    [System.NonSerialized] private double q66barPicked;
    [System.NonSerialized] private double[][] qMatrix;//Stiffness 
    [System.NonSerialized] private double[][] qMatrixInverse;//Compliance
    [System.NonSerialized] private double[][] qBarMatrix;//Transformed Stiffness
    [System.NonSerialized] private double[][] qBarMatrixInverse;//Transformed Compliance
    [System.NonSerialized] private double[][] KK;
    [System.NonSerialized] private double[][] K;
    [System.NonSerialized] private double[][] H;
    [System.NonSerialized] private double[][] T1;
    [System.NonSerialized] private double[][] lclT;
    [System.NonSerialized] private double[][] ttlHT;
    [System.NonSerialized] private double[][] tH;
    [System.NonSerialized] private float cos;
    [System.NonSerialized] private float sin;
    [System.NonSerialized] List<Vector2> q11BarPositionList;
    [System.NonSerialized] List<Vector2> q12q21BarPositionList;
    [System.NonSerialized] List<Vector2> q13q31BarPositionList;
    [System.NonSerialized] List<Vector2> q22BarPositionList;
    [System.NonSerialized] List<Vector2> q23q32BarPositionList;
    [System.NonSerialized] List<Vector2> q33BarPositionList;

    private void Update()
    {

        if (allItemsOfLamina.offsetMin.y < 0.0f)
        {

            allItemsOfLamina.offsetMin = new Vector2(allItemsOfLamina.offsetMin.x, 0.0f);
            allItemsOfLamina.offsetMax = new Vector2(allItemsOfLamina.offsetMax.x, 0.0f);


        }
        else if (allItemsOfLamina.offsetMin.y > 650.0f)
        {
            allItemsOfLamina.offsetMin = new Vector2(allItemsOfLamina.offsetMin.x, 650.0f);
            allItemsOfLamina.offsetMax = new Vector2(allItemsOfLamina.offsetMax.x, 650.0f);
        }
        if (allItemsOfResult.offsetMin.y < 0.0f)
        {

            allItemsOfResult.offsetMin = new Vector2(allItemsOfResult.offsetMin.x, 0.0f);
            allItemsOfResult.offsetMax = new Vector2(allItemsOfResult.offsetMax.x, 0.0f);


        }
        else if (allItemsOfResult.offsetMin.y > 5000.0f)
        {
            allItemsOfResult.offsetMin = new Vector2(allItemsOfResult.offsetMin.x, 5000.0f);
            allItemsOfResult.offsetMax = new Vector2(allItemsOfResult.offsetMax.x, 5000.0f);
        }
    }
    private void CreateCircle(Vector2 anchoredPosition, RectTransform rt)
    {
        GameObject gameObject = Instantiate(circleImage, rt.transform);
        gameObject.transform.SetParent(rt, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.anchoredPosition = anchoredPosition;

    }
    private void PutCircle(double xValue, double yValue, double yMin, double yMax, RectTransform rt)
    {
        double yPos = Calc.map((float)yValue, (float)yMin, (float)yMax, 0f, 350f);
        double xSize = 2.6f;
        double xPos = xSize * xValue;
        CreateCircle(new Vector2((float)xPos+142f, (float)yPos), rt);
    }
    private double FindMaximum(List<Vector2> v2)
    {
        if (v2.Count == 0)
        {
            throw new InvalidOperationException("Empty list");
        }
        double maxValue = double.MinValue;
        foreach (Vector2 type in v2)
        {
            if (type.y > maxValue)
            {
                maxValue = type.y;
            }
        }
        return maxValue;
    }
    private double FindMinimum(List<Vector2> v2)
    {
        if (v2.Count == 0)
        {
            throw new InvalidOperationException("Empty list");
        }
        double minValue = double.MaxValue;
        foreach (Vector2 type in v2)
        {
            if (type.y < minValue)
            {
                minValue = type.y;
            }
        }
        return minValue;
    }
    private double GetValue(TMPro.TMP_InputField tmp)
    {
        string text = tmp.text;
        //text = text.Replace(',', '.'); // valuelarý verince tersini yapmak gerekebilir
        double.TryParse(text, out double value);
        return value;

    }
    private int GetValueInt(TMPro.TMP_InputField tmp)
    {
        string text = tmp.text;
        int.TryParse(text, out int value);
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
    public void Calculate()
    {
        calc = false;
        IsNullOrEmpty(inputE1);
        IsNullOrEmpty(inputE2);
        IsNullOrEmpty(inputG1);
        IsNullOrEmpty(inputv12);
        IsNullOrEmpty(inputa1);
        IsNullOrEmpty(inputa2);
        IsNullOrEmpty(inputb1);
        IsNullOrEmpty(inputb2);
        if (string.IsNullOrEmpty(inputAngle.text))
        {
            angle = 45;
        }
        else
        {
            angle = GetValueInt(inputAngle);
        }
        if (!IsNullOrEmpty(inputE1) &&
           !IsNullOrEmpty(inputE2) &&
           !IsNullOrEmpty(inputG1) &&
           !IsNullOrEmpty(inputv12) &&
           !IsNullOrEmpty(inputa1) &&
           !IsNullOrEmpty(inputa2) &&
           !IsNullOrEmpty(inputb1) &&
           !IsNullOrEmpty(inputb2)
          )
        {
            E1 = GetValue(inputE1);
            Debug.Log(E1);
            E2 = GetValue(inputE2);
            Debug.Log(E2);
            v12 = GetValue(inputv12);
            Debug.Log(v12);
            if (string.IsNullOrEmpty(inputv21.text))
            {
                v21 = (E2 / E1) * v12;
            }
            else
            {
                v21 = GetValue(inputv21);

            }

            G12 = GetValue(inputG1);
            a1 = GetValue(inputa1);
            a2 = GetValue(inputa2);
            b1 = GetValue(inputb1);
            b2 = GetValue(inputb2);
            calc = true;
        }
        if (calc)
        {
            q11 = E1 / (1 - (v12 * v21));
            q12 = (v12 * E2 / (1 - v12 * v21));
            q13 = 0;
            q21 = q12;
            q22 = E2 / (1 - v12 * v21);
            q23 = 0;
            q31 = q13;
            q32 = q23;
            q33 = G12;
            q66 = q33;
            Debug.Log(E1);
            Debug.Log(v12);
            Debug.Log(v21);

            Debug.Log(q11);
            //reduced stifness matrix
            qMatrix = MatrixCalc.MatrixCreate(3, 3);
            qMatrix[0][0] = q11;
            qMatrix[0][1] = q12;
            qMatrix[0][2] = q13;
            qMatrix[1][0] = q21;
            qMatrix[1][1] = q22;
            qMatrix[1][2] = q23;
            qMatrix[2][0] = q31;
            qMatrix[2][1] = q32;
            qMatrix[2][2] = q33;

            qMatrixInverse = MatrixCalc.MatrixCreate(3, 3);
            qMatrixInverse = MatrixCalc.MatrixInverse(qMatrix); //0 angle(reduced) compliance matrix

            qBarMatrix = MatrixCalc.MatrixCreate(3, 3);
            q11BarPositionList = new List<Vector2>();
            q12q21BarPositionList = new List<Vector2>();
            q13q31BarPositionList = new List<Vector2>();
            q22BarPositionList = new List<Vector2>();
            q23q32BarPositionList = new List<Vector2>();
            q33BarPositionList = new List<Vector2>();

            KK = MatrixCalc.MatrixCreate(3, 3);
            T1 = MatrixCalc.MatrixCreate(3, 3);
            H = MatrixCalc.MatrixCreate(3, 1);
            K = MatrixCalc.MatrixCreate(3, 3);
            lclT = MatrixCalc.MatrixCreate(3, 1);
            tH = MatrixCalc.MatrixCreate(3, 1);
            ttlHT = MatrixCalc.MatrixCreate(3, 1);
            //transformed matrix
            for (int degree = -90; degree < 91; degree++)
            {
                cos = Mathf.Cos(degree * Mathf.PI / 180.0f);
                sin = Mathf.Sin(degree * Mathf.PI / 180.0f);
                q11bar = q11 * Mathf.Pow(cos, 4) + 2 * (q12 + 2 * q66) * Mathf.Pow(sin, 2) * Mathf.Pow(cos, 2) + q22 * Mathf.Pow(sin, 4);
                q12bar = q12 * (Mathf.Pow(sin, 4) + Mathf.Pow(cos, 4)) + (q11 + q22 - (4 * q66)) * Mathf.Pow(sin, 2) * Mathf.Pow(cos, 2);
                q13bar = (q11 - q12 - (2 * q66)) * sin * Mathf.Pow(cos, 3) + (q12 - q22 - (2 * q66)) * Mathf.Pow(sin, 3) * cos;
                q21bar = q12bar;
                q22bar = (q11 * Mathf.Pow(sin, 4)) + 2 * (q12 + 2 * q66) * Mathf.Pow(sin, 2) * Mathf.Pow(cos, 2) + q22 * Mathf.Pow(cos, 4);
                q23bar = (q11 - q12 - 2 * q66) * cos * Mathf.Pow(sin, 3) + (q12 - q22 - 2 * q66) * Mathf.Pow(cos, 3) * sin;
                q31bar = q13bar;
                q32bar = q23bar;
                q33bar = (q11 + q22 - 2 * q12 - 2 * q66) * Mathf.Pow(sin, 2) * Mathf.Pow(cos, 2) + q66 * (Mathf.Pow(sin, 4) + Mathf.Pow(cos, 4));
                
                q11BarPositionList.Add(new Vector2(degree + 90, (float)q11bar));
                q12q21BarPositionList.Add(new Vector2(degree + 90, (float)q12bar));
                q13q31BarPositionList.Add(new Vector2(degree + 90, (float)q13bar));
                q22BarPositionList.Add(new Vector2(degree + 90, (float)q22bar));
                q23q32BarPositionList.Add(new Vector2(degree + 90, (float)q23bar));
                q33BarPositionList.Add(new Vector2(degree + 90, (float)q33bar));
                if(degree == angle)
                {
                    q11barPicked = q11bar;
                    q12barPicked = q12bar;
                    q13barPicked = q13bar;
                    q21barPicked = q21bar;
                    q22barPicked = q22bar;
                    q23barPicked = q23bar;
                    q31barPicked = q31bar;
                    q32barPicked = q32bar;
                    q33barPicked = q33bar;
                    KK[0][0] = Mathf.Pow(cos, 2);
                    KK[0][1] = Mathf.Pow(sin, 2);
                    KK[0][2] = 2*sin*cos;
                    KK[1][0] = Mathf.Pow(sin, 2);
                    KK[1][1] = Mathf.Pow(cos, 2);
                    KK[1][2] = -2 * sin * cos;
                    KK[2][0] = -sin * cos;
                    KK[2][1] = sin * cos;
                    KK[2][2] = Mathf.Pow(cos, 2) - Mathf.Pow(sin, 2);
                    T1 = MatrixCalc.MatrixInverse(KK);
                    H[0][0] = totST1;
                    H[1][0] = totST2;
                    H[2][0] = totST3/2;
                    K = MatrixCalc.MatrixProduct(T1, H);
                    lclT[0][0] = a1;
                    lclT[1][0] = a2;
                    lclT[2][0] = 0;
                    tH = MatrixCalc.MatrixProduct(T1 , lclT);
                    ttlHT[0][0] = tH[0][0];
                    ttlHT[1][0] = tH[1][0];
                    ttlHT[2][0] = tH[2][0];
                    Thermal.text = "[ " + ttlHT[0][0] +" ]\n" +
                           "[ " + ttlHT[1][0] + " ]\n" +
                           "[ " + ttlHT[2][0] + " ]";
                }
                

            }
            DrawGraph(q11BarPositionList, q11GraphContainer,Q11Text);
            DrawGraph(q12q21BarPositionList, q12q21GraphContainer,Q12Text);
            DrawGraph(q13q31BarPositionList, q13q31GraphContainer,Q13Text);
            DrawGraph(q22BarPositionList, q22GraphContainer,Q22Text);
            DrawGraph(q23q32BarPositionList, q23q32GraphContainer,Q23Text);
            DrawGraph(q33BarPositionList, q33GraphContainer,Q33Text);



            qBarMatrix[0][0] = q11bar;
            qBarMatrix[0][1] = q12bar;
            qBarMatrix[0][2] = q13bar;
            qBarMatrix[1][0] = q21bar;
            qBarMatrix[1][1] = q22bar;
            qBarMatrix[1][2] = q23bar;
            qBarMatrix[2][0] = q31bar;
            qBarMatrix[2][1] = q32bar;
            qBarMatrix[2][2] = q33bar;
            qBarMatrixInverse = MatrixCalc.MatrixCreate(3, 3);
            qBarMatrixInverse = MatrixCalc.MatrixInverse(qBarMatrix);
            QBarText.text = "Transformed Stiffness Matrix Angle " + angle;
            SBarText.text = "Transformed Compliance Matrix Angle " + angle;
            ThermalText.text = "Default mechanical strain values [0.001 / 0.002 / 0.0005], Temperature Change - 120°C\n\n\nThermal Matrix Angle " + angle;
            
            Q.text = "[ " + q11.ToString("F4") + " " + q12.ToString("F4") + " " + q13.ToString("F4") + " ]\n" +
                "[ " + q21.ToString("F4") + " " + q22.ToString("F4") + " " + q23.ToString("F4") + " ]\n" +
                "[ " + q31.ToString("F4") + " " + q32.ToString("F4") + " " + q33.ToString("F4") + " ]";

            S.text = "[ " + qMatrixInverse[0][0].ToString("F4") + " " + qMatrixInverse[0][1].ToString("F4") + " " + qMatrixInverse[0][2].ToString("F4") + " ]\n" +
                "[ " + qMatrixInverse[1][0].ToString("F4") + " " + qMatrixInverse[1][1].ToString("F4") + " " + qMatrixInverse[1][2].ToString("F4") + " ]\n" +
                "[ " + qMatrixInverse[2][0].ToString("F4") + " " + qMatrixInverse[2][1].ToString("F4") + " " + qMatrixInverse[2][2].ToString("F4") + " ]";
            QBar.text = "[ " + q11barPicked.ToString("F4") + " " + q12barPicked.ToString("F4") + " " + q13barPicked.ToString("F4") + " ]\n" +
                "[ " + q21barPicked.ToString("F4") + " " + q22barPicked.ToString("F4") + " " + q23barPicked.ToString("F4") + " ]\n" +
                "[ " + q31barPicked.ToString("F4") + " " + q32barPicked.ToString("F4") + " " + q33barPicked.ToString("F4") + " ]";
            SBar.text = "[ " + qBarMatrixInverse[0][0].ToString("F4") + " " + qBarMatrixInverse[0][1].ToString("F4") + " " + qBarMatrixInverse[0][2].ToString("F4") + " ]\n" +
                "[ " + qBarMatrixInverse[1][0].ToString("F4") + " " + qBarMatrixInverse[1][1].ToString("F4") + " " + qBarMatrixInverse[1][2].ToString("F4") + " ]\n" +
                "[ " + qBarMatrixInverse[2][0].ToString("F4") + " " + qBarMatrixInverse[2][1].ToString("F4") + " " + qBarMatrixInverse[2][2].ToString("F4") + " ]";

            GameManager.THIS.macroMechanicsMenu.resultScreen.SetActive(true);
            GameManager.THIS.dropDownMenuButton.SetActive(false);
        }
    }
    private void DrawGraph(List<Vector2> vectorList,RectTransform rt, TMPro.TextMeshProUGUI tmp)
    {
        double maxQBar = FindMaximum(vectorList);
        double minQBar = FindMinimum(vectorList);
        foreach (Vector2 v2 in vectorList)
        {

            PutCircle(v2.x, v2.y, minQBar, maxQBar, rt);
        }
        tmp.text = "\n" + maxQBar.ToString("F2") + "\n\n\n\n" + "(GPA)" + "\n\n\n\n\n" + minQBar.ToString("F2");
    }
        /*
        for i = 1:3
            for j = 1:3
                qbar((1:3), (1:3))= qbarK((1:3),(1:3));
        A(i, j) = qbar(i, j) * (h(2) - h(1));
        B(i, j) = 1 / 2 * (qbar(i, j) * (h(2) ^ 2 - h(1) ^ 2));
        D(i, j) = 1 / 3 * (qbar(i, j) * (h(2) ^ 3 - h(1) ^ 3));

        for k = 2 : N

        qbar((1:3), (1:3)) = qbarK((3 * k - 2:3 * k) , (1:3) );
        A(i, j) = qbar(i, j) * (h(k + 1) - h(k)) + A(i, j);
        B(i, j) = 1 / 2 * (qbar(i, j) * (h(k + 1) ^ 2 - h(k) ^ 2)) + B(i, j);
        D(i, j) = 1 / 3 * (qbar(i, j) * (h(k + 1) ^ 3 - h(k) ^ 3)) + D(i, j);
        end
    end
        //Matrix4x4
        //Matrix myMatrix = new Matrix(5, 10, 15, 20, 25, 30);
        *(

    }
        /*
            [System.NonSerialized] private double q11;
            [System.NonSerialized] private double q12;
            [System.NonSerialized] private double q13;
            [System.NonSerialized] private double v21;
            [System.NonSerialized] private double G12;
            [System.NonSerialized] private double a1;
            [System.NonSerialized] private double a2;
            [System.NonSerialized] private double b1;
            [System.NonSerialized] private double b2;
            [System.NonSerialized] private double t;

        */
        /* 
         * E1=input('E1 = ');
         E2= input('E2 = ');
         v12= input('v12 = ');
         v21=(E2/E1)*v12;
         G12= input('G12 = ');
         a1= input('a1 = ');
         a2= input('a2 = ');
         tttt=input('t = ');
         t=tttt* e-03;

        STIFNESS     
            q11=(E1/(1-v12*v21));
             q12=(-v12*E2/(1-v12*v21));
             q13=(0);
             q21=q11;
             q22=(E2/1-v12*v21);
             q23=(0);
             q31=q13;
             q32=q23;
             q33=(G12);
             q66=q33;

         Q = [q11     q12     q13  ;
                   q21     q22     q23  ;
                   q31     q32     q33 ];

            %%Complience Matrix elements
            S = inv(Q);
        for k =-90:1:90    -90 ile 90 arasýnda tüm stiffness matrixi hesaplýyor grafik için
                  0 için zaten stiffness kendi matrixi bulunuyor
                c = cosd(k); % 
                s = sind(k); % 

                %Transformed Reduced Stiffness Matrix açýya göre hesaplýyor
                qbar =zeros(3,3);
                qbar(1,1) = q11*(c^4) + 2*(q12 + 2*q66)*(s^2)*(c^2) + q22*(s^4);
                qbar(1,2) = q12*((s^4)+(c^4)) + (q11 + q22 - 4*q66)*(s^2)*(c^2);
                qbar(1,3) = (q11 - q12 -2*q66)*s*(c^3) + (q12 - q22 - 2*q66)*(s^3)*c;
                qbar(2,1) = qbar(1,2);
                qbar(2,2) = q11*(s^4) + 2*(q12 + 2*q66)*(s^2)*(c^2) + q22*(c^4);
                qbar(2,3) = (q11 - q12 -2*q66)*c*(s^3) + (q12 - q22 - 2*q66)*(c^3)*s;
                qbar(3,1) = qbar(1,3);
                qbar(3,2) = qbar(2,3);
                qbar(3,3) = (q11 + q22 - 2*q12 - 2*q66)*(s^2)*(c^2) + q66*((s^4)+(c^4));
                %Qbar(1,6) = Qbar(3,1) = Qbar(1,3)
                %Qbar(2,6) = Qbar(2,3) = Qbar(3,2)


                %disp(Qbar);
                hold on;
                subplot(3,2,1)
                plot(qbar(1,1),k,'-bo');
                title('Qbar_{11}');
                xlabel('Pa(Pascal)');
                ylabel('\theta (degrees)');

                hold on;
                subplot(3,2,2)
                plot(qbar(1,2),k,'-bo');
                title('Qbar_{12}');
                xlabel('Pa(Pascal)');
                ylabel('\theta (degrees)');


                hold on;
                subplot(3,2,3)
                plot(qbar(2,2),k,'-bo');
                title('Qbar_{22}');
                xlabel('Pa(Pascal)');
                ylabel('\theta (degrees)');


                hold on;
                subplot(3,2,4)
                plot(qbar(3,1),k,'-bo');
                title('Qbar_{16}');
                xlabel('Pa(Pascal)');
                ylabel('\theta (degrees)');

                hold on;
                subplot(3,2,5)
                plot(qbar(3,2),k,'-bo');
                title('Qbar_{26}');
                xlabel('Pa(Pascal)');
                ylabel('\theta (degrees)');

                hold on;
                subplot(3,2,6)
                plot(qbar(3,3),k,'-bo');
                title('Qbar_{66}');
                xlabel('Pa(Pascal)');
                ylabel('\theta (degrees)');

             if (k == 0 || k ==-45 || k ==45 || k ==90)   INPUT OLARAK ACI AL SADECE O MATRIXLERI YAZDIR
                fprintf('Qbar/Q matrix of %d degree\n', k);
                disp(qbar);
         a1
          thermal and moisture için thermalde Volume fractiona göre grafik çýkacak








        Laminate-------------------------------------------------
        ----------------------
        ------------------------
        disp('please enter layer stacking, or use default layer stacking as [0/±45/90]s. ');
            disp('---------------------------------------------------------------------------------------------------------')

            prompt = 'Would you like to use your own Layer Stacking ?, Y/N: ';
            str = input(prompt,'s');
            clc       

            if isequal(str, 'Y')

                disp('If the Layer Stacking(LS) is [+-30 0]s enter " [30 -30 0 -30 30] " Do Not Forget to Use Brackets [] !!!')
                l=input('Enter LS >> ');
            else
                l=[0 45 -45 90 90 -45 45 0];
            end
            clc

           angs = l;


           N=length(angs);
           h=zeros(N+1);

           h(N+1)=N*t/2;
           qbarK=zeros(3*N,3);


            for k = 1 : N

                c = cosd (angs(k)); % 
                s = sind (angs(k)); % 

                %Transformed Reduced Stiffness Matrix
                qbar =zeros(3,3);
                qbar(1,1) = q11*(c^4) + 2*(q12 + 2*q66)*(s^2)*(c^2) + q22*(s^4);
                qbar(1,2) = q12*((s^4)+(c^4)) + (q11 + q22 - 4*q66)*(s^2)*(c^2);
                qbar(1,3) = (q11 - q12 -2*q66)*s*(c^3) + (q12 - q22 - 2*q66)*(s^3)*c;
                qbar(2,1) = qbar(1,2);
                qbar(2,2) = q11*(s^4) + 2*(q12 + 2*q66)*(s^2)*(c^2) + q22*(c^4);
                qbar(2,3) = (q11 - q12 -2*q66)*c*(s^3) + (q12 - q22 - 2*q66)*(c^3)*s;
                qbar(3,1) = qbar(1,3);
                qbar(3,2) = qbar(2,3);
                qbar(3,3) = (q11 + q22 - 2*q12 - 2*q66)*(s^2)*(c^2) + q66*((s^4)+(c^4));
                %Qbar(1,6) = Qbar(3,1) = Qbar(1,3)
                %Qbar(2,6) = Qbar(2,3) = Qbar(3,2)

                %allqbars 
                qbarK((3*k-2:3*k),(1:3))=qbar((1:3),(1:3));
                %all h values
                h(k)=(k-N/2-1)*t;
                %
            end

            A=zeros(3,3);
            B=zeros(3,3);
            D=zeros(3,3);

         for i=1:3
            for j=1:3
                qbar((1:3),(1:3))=qbarK((1:3),(1:3));
                A(i,j) = qbar(i,j) * (h(2) - h(1));
                B(i,j) = 1/2*(qbar(i,j) * (h(2)^2 - h(1)^2));
                D(i,j) = 1/3*(qbar(i,j) * (h(2)^3 - h(1)^3));

                for k = 2 : N
                qbar((1:3),(1:3)) = qbarK((3*k-2:3*k) , (1:3) );
                A(i,j) = qbar(i,j) * (h(k+1) - h(k)) + A(i,j);
                B(i,j) = 1/2*(qbar(i,j) * (h(k+1)^2 - h(k)^2)) + B(i,j);
                D(i,j) = 1/3*(qbar(i,j) * (h(k+1)^3 - h(k)^3)) + D(i,j);    
                end
            end

         end

        if rem(length(angs),2)==0
        A= A.*[1 1 0 ; 1 1 0 ; 0 0 1];
        B= B.*[0 0 0 ; 0 0 0 ; 0 0 0];
        D= D.*[1 1 1 ; 1 1 1 ; 1 1 1];


        end
        disp(A)
        disp(B)
        disp(D)


        Q ve q barlarý her seferinde eklenen malzemeye ve açýsýna göre deðiþtirip hesaplancak ve 1 layer olacak



         */








    }
