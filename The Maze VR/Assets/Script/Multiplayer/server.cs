using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.Networking.NetworkSystem;

public class server : MonoBehaviour

{
    short rdmid = 666;
    void Start()
    {
        Application.runInBackground = true;
        init_server();
        byte[] bytes = new Byte[1024];
        
    }

    void init_server()
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnClientDisconnected);
        NetworkServer.RegisterHandler(rdmid, OnMessageReceived);
        var config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableFragmented);
        var ht = new HostTopology(config, 1);
        NetworkServer.Configure(ht);
        // Start listening on the defined port
        if (NetworkServer.Listen(433))
        {
            Debug.Log("Server created, listening on port 433");
        }
        else
        {
            Debug.Log("No server created, could not listen to the port: 433");
        }        
    }
    void message(string msg)
    {
        
        var m = new StringMessage(msg);
        NetworkServer.SendToAll(rdmid,m);
    }
    void OnClientConnected(NetworkMessage netMessage)
    {
        Debug.Log("Cient connected");
    }

    void OnClientDisconnected(NetworkMessage netMessage)
    {
        Debug.Log("Cient disconnected");
    }
    void OnMessageReceived(NetworkMessage netMessage)
    {

        string message = netMessage.reader.ReadString();
        Debug.Log("message received: "+message);
    }

    void OnApplicationQuit()
    {
        NetworkServer.Shutdown();
        
    }
}

