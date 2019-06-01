using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
public class client : MonoBehaviour
{
    /// <summary>
    ///!!!!!! TEST CLIENT VERSION !!!!!!!
    /// </summary>
    public string S; //0: VR, 1: CTRL
    NetworkClient _client;
    short rdmid = 666;
    public string me; //pseudo
    private List<string> players; //all players looking for a game
    private int gameID;
    private bool ingame;
    // Start is called before the first frame update
    void Start()
    {
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
        _client.Connect("127.0.0.1", 433);
    }

    void OnMessageReceived(NetworkMessage netMessage)
    {
        string message = netMessage.reader.ReadString();
        Debug.Log(me+": message received: " + message);
        string[] evt = message.Split(':');
        switch (evt[0])
        {
            case "pl":
                {

                    players.Add(evt[1]);
                    if (S == "VR")//demo
                    {
                        connectTo(players[0]);
                    }
                    break;
                }
            case "gameid":
                gameID = int.Parse(evt[1]);
                //START THE GAME HERE
                // GameCmd("salut"); //test

                break;
            case "ig":
                GameAction(evt);
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
                sayready(false);
                break;
            //to do : leave game
        }
    }


    void GameAction(string[] cmd)
    {
        //Call game events
        //ig:cmd:paramter1:parameter2...
        switch (cmd[2]) //example
        {
            case "salut":
                GameCmd("Sa va?");
                break;
        }
    }

    void connectTo(string pseudo)
    {
        if (pseudo==null)
        {
            Debug.Log("This client does not exist"); //pbly useless with the interface, will be deleted
        }
        else
        {
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

    void GameCmd(string cmd)
    {
        message("ig:" + gameID + ":" + cmd);
    }

    void GameOver()
    {
        message("go:" + gameID);
        Debug.Log(me + ": game has ended");
    }

    void Win()
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
        //test
        sayready(true);
        Debug.Log("a");
        if (S=="CTRL") //get ctrl players if ur playing vr
        {
            getVRplist();
        }
        else
        {
            getCTRLplist();
        }
        if (players.Count>0)
        {
            connectTo(players[0]);
        }

    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(60);
    }
        void OnApplicationQuit()
    {
        NetworkTransport.Shutdown();
    }

}
