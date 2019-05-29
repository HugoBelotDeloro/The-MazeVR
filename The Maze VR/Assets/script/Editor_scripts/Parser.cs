using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public string mapToCode(int[,] map)
    {
        int h = map.GetLength(0);
        int l = map.GetLength(1);
        string code = Convert.ToString(Math.Floor(Math.Log10(h) + 1)) + Convert.ToString(h) + Convert.ToString(Math.Floor(Math.Log10(l) + 1)) + Convert.ToString(l);
        
        
        
        
        
        
        
        
        return code;
    }

    public int[,] codeToMap(string code)
    {
        int[,] map = new int[,];
        return map;
    }
    
    
    
    
}
