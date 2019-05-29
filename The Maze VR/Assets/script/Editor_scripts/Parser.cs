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
        int i = 0;
        int hn = Convert.ToInt32(code[i]);
        i++;
        string hs = "";
        for (int j = 0; j < hn; j++)
        {
            hs = hs + code[i];
            i++;
        }
        int ln = Convert.ToInt32(code[i]);
        i++;
        string ls = "";
        for (int j = 0; j < ln; j++)
        {
            ls = ls + code[i];
            i++;
        }
        int[,] map = new int[Convert.ToInt32(hs),Convert.ToInt32(ls)];
        return map;
    }
    
    
    
    
}
