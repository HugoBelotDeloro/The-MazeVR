using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class client : MonoBehaviour
{
    public string S; //VR or CTRL
    NetworkClient _client;
    short rdmid = 666;
    public string me; //pseudo
    private List<string> players; //all players looking for a game
    private int gameID;
    private bool ingame;
    void Start()
    {
        DontDestroyOnLoad(gameObject); //keep the client object (in menus ans ingame)
        SetupClient();
    }


    public void SetupClient()
    {
        ingame = false;
        players = new List<string>();
        var config = new ConnectionConfig();
        NetworkTransport.Init();
        // Config the Channels we will use
        config.AddChannel(QosType.ReliableFragmented);
        // Create the client ant attach the configuration
        _client = new NetworkClient();
        HostTopology tp = new HostTopology(config, 10);
        NetworkTransport.AddHost(tp, 0);
        _client.Configure(config, 10);

        // Register the handlers for the different network messages
        _client.RegisterHandler(MsgType.Connect, OnConnected);
        _client.RegisterHandler(rdmid, OnMessageReceived);

        // Connect to the server
        //_client.Connect("90.65.163.70", 433);
        _client.Connect("90.65.163.70", 433);
    }

    public void login(string password, string username)
    {
        Debug.Log("attempting to log in as " + username);
        message("log:" + username + ":" + password);
    }

    void OnMessageReceived(NetworkMessage netMessage)
    {
        string message = netMessage.reader.ReadString();
        Debug.Log(me+": message received: " + message);
        string[] evt = message.Split(':');
        switch (evt[0])
        {
           case "suc": //login success
                Debug.Log("authentification successfull");
                //login successfull
                sayready(true);
                if (S == "CTRL") //get ctrl players if ur playing vr
                {
                    getVRplist();
                }
                else
                {
                    getCTRLplist();
                }
                Thread.Sleep(1000);
                SceneManager.LoadScene("lobby", LoadSceneMode.Single); //get to lobby

                break;
            case "fai":
                Debug.Log("authentification failed");
                //login failed
                break;  
            case "pl":
                {
                    players.Add(evt[1]);
                    //if (S == "VR")//demo
                    // {
                    //    connectTo(players[0]);
                    // }
                    GameObject.Find("MenuPanel").GetComponentInChildren<LobbyManager>().add(evt[1]);
                    break;
                }
            case "gameid":
                gameID = int.Parse(evt[1]);
                sayready(false);
                //START THE GAME HERE
                // GameCmd("salut"); //test
                if (S == "VR")
                {
                    SceneManager.LoadScene("multi J1", LoadSceneMode.Single);
                }
                else
                {
                    SceneManager.LoadScene("multi J2", LoadSceneMode.Single);
                }

                break;
            case "ig":
                GameAction(message.Replace("ig:",""));
                break;
            case "error":
                //Debug.Log(evt[1]);
                break;
            case "connected":
                Debug.Log("connection successfull");
                //this.message("startgame:"+S);                
                break;
            case "cta":
                Debug.Log(evt[1] + " wants to play");
                //To do: accept or decline
                var pu = GameObject.Find("popupfinder").GetComponent<storeobject>().stored;
                if (pu != null)
                {
                    pu.GetComponentInChildren<Text>().text = evt[1] + " wants to play !";
                }
                GameObject.Find("Canvas").SetActive(false);
                pu.SetActive(true);
                pu.GetComponentInChildren<accept>().player = evt[1];
                pu.GetComponentInChildren<Refuse>().player = evt[1];
                break;
            //to do : leave game
        }
    }

    public void ask(string pseudo)
    {
        message("ask:"+S+":"+pseudo);
        Debug.Log("asking if " + pseudo + "wants to play");
    }
    void GameAction(string cmd)
    {
        if (S == "VR")
        {
            GameObject.Find("Game player 1").GetComponent<addtrap>().Receive(cmd);
        }
        else
        {
            GameObject.Find("Game player 2").GetComponent<placetrap>().Receive(cmd);
        }
    }

    public void connectTo(string pseudo)
    {
        if (pseudo==null)
        {
            Debug.Log("This client does not exist"); //pbly useless with the interface, will be deleted
        }
        else
        {

            Debug.Log("connectin with " + pseudo);
            message("connectto:" + S + ":" + pseudo); //attempt to connect with CTRLplayer
        }
    }

 
    void getVRplist()
    {
        message("VRplist:");
    }

    void getCTRLplist()
    {
        message("CTRLplist:");
    }

    public void GameCmd(string cmd)
    {
        message("ig:" + gameID + ":" + cmd);
    }

    public void GameOver()
    {
        message("go:" + gameID);
        Debug.Log(me + ": game has ended");
    }

    public void Win()
    {
        message("win:" + gameID);
        Debug.Log(me + ": won the game");

    }
    void sayready(bool rdy) //become visible to ther players, and see them
    {
        if (rdy)
        {
            message("ready:"+S+":"+me);
            Debug.Log(me+": visible by other players as " + me);
            ingame = true;
        }
        else
        {
            message("notready:" + S + ":" + me);
            Debug.Log(me+": not visible anymore");
            ingame = true;
        }
    }

    void message(string msg)
    {
        var m = new StringMessage(msg);
        _client.Send(rdmid, m);
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log(me+": Connected to server");

    }

    void OnApplicationQuit()
    {
        NetworkTransport.Shutdown();
    }

}
