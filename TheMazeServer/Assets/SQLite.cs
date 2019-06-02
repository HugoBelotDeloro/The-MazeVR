
using UnityEngine;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System;

public class SQLite : MonoBehaviour
{
    public string DBpath;
    private SQLiteConnection sql_con;
    private SQLiteCommand sql_cmd;
    private SQLiteDataReader reader;
    // Start is called before the first frame update
    void Start()
    {
        sql_con = new SQLiteConnection
    ("Data Source=" + DBpath + ";Version=3;New=False;Compress=True;");
        sql_con.Open();
    }

    public bool CorrectPass(string username, string password)
    {
        string s = rquery("SELECT password FROM auth_user WHERE username='"+username+"'"); //get password hash in db
        using (MD5 md5Hash = MD5.Create())
        {
            return GetMd5Hash(md5Hash, password) == s; //compare with entered password hash
        }
    }

    public void win_incr(string username, bool isVR)
    {
        string uid = rquery("SELECT id FROM auth_user WHERE username='" + username + "'"); //add win
        query("UPDATE stats_win SET wins = wins + 1 WHERE user_id = '" + uid + "'");
        if (isVR)
        {
            query("UPDATE stats_win SET VRwins = VRwins + 1 WHERE user_id = '" + uid + "'"); //add VR win
        }
        else
        {
            query("UPDATE stats_win SET CTRLwins = CTRLwins + 1 WHERE user_id = '" + uid + "'"); //add CTRL win
        }
        Debug.Log(uid);
    }

    public string rquery(string q) //query with output
    {
        sql_cmd = sql_con.CreateCommand();
        sql_cmd.CommandText = q;
        reader = sql_cmd.ExecuteReader();
        while (reader.HasRows)
        {
            while (reader.Read())
            {
                return reader[0].ToString(); //only first output
                //bc we never use more than 1 output
            }
        }
        return null;
    }
    public void query(string q) //query with no output
    {
        sql_cmd = sql_con.CreateCommand();
        sql_cmd.CommandText = q;
        sql_cmd.ExecuteNonQuery();
    }

    void OnApplicationQuit()
    {
        sql_con.Close();
    }

    static string GetMd5Hash(MD5 md5Hash, string input)
    {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        StringBuilder sBuilder = new StringBuilder();

        // format bytes as hexadecimal strings
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();
    }

}

