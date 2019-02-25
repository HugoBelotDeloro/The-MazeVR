using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
public class client : MonoBehaviour
{
    NetworkClient _client;
    short rdmid = 666;

    // Start is called before the first frame update
    void Start()
    {
        SetupClient();
    }

    public void SetupClient()
    {
        var config = new ConnectionConfig();
        NetworkTransport.Init();
        // Config the Channels we will use
        config.AddChannel(QosType.ReliableFragmented);
        // Create the client ant attach the configuration
        _client = new NetworkClient();
        HostTopology tp = new HostTopology(config, 1);
        NetworkTransport.AddHost(tp, 0);
        _client.Configure(config, 1);

        // Register the handlers for the different network messages
        _client.RegisterHandler(MsgType.Connect, OnConnected);
        _client.RegisterHandler(rdmid, OnMessageReceived);

        // Connect to the server
        _client.Connect("127.0.0.1", 433);
    }
    void OnMessageReceived(NetworkMessage netMessage)
    {
        string message = netMessage.reader.ReadString();
        Debug.Log("message received: " + message);
        //FAIRE DES CHOSES ICI
    }

    void message(string msg)
    {
        var m = new StringMessage(msg);
        _client.Send(rdmid, m);
    }
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }

    void OnApplicationQuit()
    {
        NetworkTransport.Shutdown();
    }

}
