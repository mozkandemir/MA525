using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MaterialProperties 
{
    public double E1;
    public double E2;
    public double v12;
    public double G12;
    public double a1;
    public double a2;
    public double b1;
    public double b2;
    public double thickness;
    public double longitudinalTensileStrength;
    public double longitudinalCompressiveStrength;
    public double transverseTensileStrength;
    public double transverselCompressiveStrength;
    public double shearStrength;
    public string materialName;
    
    public MaterialProperties(
        string metarialName, double E1, double E2, double v12, double G12, double a1, double a2, double b1, double b2,
        double thickness, double longitudinalTensileStrength, double longitudinalCompressiveStrength,
        double transverseTensileStrength, double transverselCompressiveStrength, double shearStrength )
    {
        this.materialName = metarialName;
        this.E1 = E1;
        this.E2 = E2;
        this.v12 = v12;
        this.G12 = G12;
        this.a1 = a1;
        this.a2 = a2;
        this.b1 = b1;
        this.b2 = b2;
        this.thickness = thickness;
        this.longitudinalTensileStrength = longitudinalTensileStrength;
        this.longitudinalCompressiveStrength = longitudinalCompressiveStrength;
        this.transverseTensileStrength = transverseTensileStrength;
        this.transverselCompressiveStrength = transverselCompressiveStrength;
        this.shearStrength = shearStrength;
        
    }
}
