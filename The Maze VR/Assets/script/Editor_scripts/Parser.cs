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
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < l; j++)
            {
                if (map[j,i]!=-1)
                    code = code + Convert.ToString(Math.Log10(map[j, i]) + 1) + Convert.ToString(map[j, i]);
                else
                {
                    code = code += '0';
                }
            }
        }
        Debug.Log(code);
        code = base10to62(code);
        return code;
    }

    public int[,] codeToMap(string code)
    {
        code = base62to10(code);
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
        for (int j = 0; j < Convert.ToInt32(hs); j++)
        {
            for (int k = 0; k < Convert.ToInt32(ls); k++)
            {
                if (code[i] != 0)
                {
                    int c = Convert.ToInt32(code[i]);
                    string d = "";
                    for (int l = 0; l < c; l++)
                    {
                        d = d + code[i];
                        i++;
                    }

                    map[k, j] = Convert.ToInt32(d);
                }
                else
                {
                    map[k, j] = -1;
                }
            }
        }
        return map;
    }

    private List<char> bases = new List<char> {('0'),('1'),('2'),('3'),('4'),('5'),('6'),('7'),('8'),('9'),('A'),('B'),('C'),('D'),('E'),('F'),('G'),('H'),('I'),('J'),('K'),('L'),('M'),('N'),('O'),('P'),('Q'),('R'),('S'),('T'),('U'),('V'),('W'),('X'),('Y'),('Z'),('a'),('b'),('c'),('d'),('e'),('f'),('g'),('h'),('i'),('j'),('k'),('l'),('m'),('n'),('o'),('p'),('q'),('r'),('s'),('t'),('u'),('v'),('w'),('x'),('y'),('z')};
    
    private string base10to62(string s)
    {
        string r = "";
        int n = Convert.ToInt32(s);
        int i = 0;
        while (Math.Pow(62, i) < n)
            i++;
        while (i != 0)
        {
            r = r + Convert.ToString(bases[(n / (int)Math.Pow(62, i))]);
            n = (int) (n % Math.Pow(62, i));
            i--;
        }
        r = r + Convert.ToString(n);
        return r;
    }

    private string base62to10(string s)
    {
        int o=0;
        int i = s.Length;
        foreach (char c in s)
        {
            int a=0;
            for (int j = 0; j < bases.Count; j++)
            {
                if (bases[j] == c) ;
                a = j;
            }

            o += a * (int)Math.Pow(62, i);
            i--;
        }

        string r = Convert.ToString(o);
        return r;
    }
}
