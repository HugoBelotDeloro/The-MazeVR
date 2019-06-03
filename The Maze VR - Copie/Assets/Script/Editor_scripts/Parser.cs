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
                if (map[i,j]!=-1)
                    code = code + ((int)Math.Log10(map[i,j]) + 1).ToString() + (map[i,j]).ToString();
                else
                {
                    code = code += '0';
                }
            }
        }
        Debug.Log(code);
        code = Base10To(code);
        return code;
    }

    public int[,] codeToMap(string code)
    {
        code = BaseTo10(code);
        int i = 0;
        int hn = (int)char.GetNumericValue(code[i]);
        i++;
        string hs = "";
        for (int j = 0; j < hn; j++)
        {
            hs = hs + code[i];
            i++;
        }
        int ln = (int)char.GetNumericValue(code[i]);
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
                if (code[i] != '0')
                {
                    int c = (int)char.GetNumericValue(code[i]);
                    i++;
                    string d = "";
                    for (int l = 0; l < c; l++)
                    {
                        d = d + code[i];
                        i++;
                    }

                    map[j,k] = Convert.ToInt32(d);
                }
                else
                {
                    map[j,k] = -1;
                    i++;
                }
            }
        }
        return map;
    }
    
    public static List<char> bases = new List<char> {('0'),('1'),('2'),('3'),('4'),('5'),('6'),('7'),('8'),('9'),('A'),('B'),('C'),('D'),('E'),('F'),('G'),('H'),('I'),('J'),('K'),('L'),('M'),('N'),('O'),('P'),('Q'),('R'),('S'),('T'),('U'),('V'),('W'),('X'),('Y'),('Z'),('a'),('b'),('c'),('d'),('e'),('f'),('g'),('h'),('i'),('j'),('k'),('l'),('m'),('n'),('o'),('p'),('q'),('r'),('s'),('t'),('u'),('v'),('w'),('x'),('y'),('z'),('.'),(';'),(','),('?'),(':'),('/'),('!'),('('),(')'),('{'),('}'),('['),(']'),('-'),('_')};
        
        public static string Base10To(string s)
        {
            int k = 0;
            string re = "";
            while (s.Length > k)
            {
                string sr = "";
                for (int j = k; j < k + 8; j++)
                {
                    if (s.Length > j)
                    {
                        sr = sr + s[j];
                    }
                }
                k += 8;
                string r = "";
                int n = Convert.ToInt32(sr);
                int a = (int) (sr.Length - n.ToString().Length);
                int i = 1;
                while (Math.Pow(bases.Count, i) < n)
                    i++;
                i--;
                while (i != 0)
                {
                    int v = (int) (n / Math.Pow(bases.Count, i));
                    r = r + Convert.ToString(bases[(int) v]);
                    double b = v * Math.Pow(bases.Count, i);
                    n = (int) (n - b);
                    i--;
                }
                r = r + Convert.ToString(bases[(int) n]);
                for (int j = 0; j < a; j++)
                {
                    r = '0' + r;
                }
                re = re + Convert.ToString(r.Length) + r;
            }
            return re;
        }
        
        public static string BaseTo10(string s)
        {
            int k = 0;
            string re = "";
            while (s.Length > k)
            {
                string m = "";
                int w = (int)char.GetNumericValue(s[k]);
                k++;
                for (int j = k; j < k+w; j++)
                {
                    if (s.Length > j)
                    {
                        m = m + s[j];
                    }
                }
                k += w;
                int o = 0;
                int i = m.Length - 1;
                int l = i;
                foreach (char c in m)
                {
                    int a = 0;
                    for (int j = 0; j < bases.Count; j++)
                    {
                        if (bases[j] == c)
                            a = j;
                    }

                    o += a * (int) Math.Pow(bases.Count, i);
                    i--;
                }
                string r = Convert.ToString(o);
                if (o == 0)
                {
                    r = "";
                    for (int j = 0; j < w; j++)
                    {
                        r = r + '0';
                    }
                }
                else
                {
                    int g = 0;
                    bool v = true;
                    foreach (char c in m)
                    {
                        if (v)
                        {
                            if (c == '0')
                            {
                                g++;
                            }
                            else
                            {
                                v = false;
                            }
                        }
                    }
                    int b = g;
                    for (int j = 0; j < b; j++)
                    {
                        r = '0' + r;
                    }
                }
                re = re + r;
            }
            return re;
        }
}
