using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.Networking.NetworkSystem;
using System.Collections.Generic;
using System.Linq;

public class Game
{
    public int id;
    public VRplayer vrp;
    public CTRLplayer ctrlp;
    public Game(int id, VRplayer vrp, CTRLplayer ctrlp)
    {
        this.id = id;
        this.vrp = vrp;
        this.ctrlp = ctrlp;
    }
}
public class VRplayer
{
    public string pseudo;
    public NetworkConnection nc;
    public bool ingame;
    public VRplayer(string pseudo, NetworkConnection nc, bool ingame)
    {
        this.pseudo = pseudo;
        this.nc = nc;
        this.ingame = ingame;
    }
}

public class CTRLplayer
{
    public string pseudo;
    public NetworkConnection nc;
    public bool ingame;
    public CTRLplayer(string pseudo, NetworkConnection nc, bool ingame)
    {
        this.pseudo = pseudo;
        this.nc = nc;
        this.ingame = ingame;
    }
}

public class server : MonoBehaviour

{
    private SQLite sqlite; //aux functions for db handling
    private int count; //total amount of games played since the server is alive, used for game id
    public List<VRplayer> VRplayers;
    public List<CTRLplayer> CTRLplayers;
    public List<Game> Games;
    short rdmid = 666;
    void Start()
    {
        Application.runInBackground = true;
        init_server();
        byte[] bytes = new Byte[1024];
        VRplayers = new List<VRplayer>();
        CTRLplayers = new List<CTRLplayer>();
        Games = new List<Game>();
        sqlite = gameObject.GetComponentInChildren<SQLite>();
    }

    void init_server()
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnClientDisconnected);
        NetworkServer.RegisterHandler(rdmid, OnMessageReceived);
        var config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableFragmented);
        var ht = new HostTopology(config, 10);
        NetworkServer.Configure(ht);
        // Start listening on the defined port
        if (NetworkServer.Listen(433))
        {
            Debug.Log("s: Server created, listening on port 433");
        }
        else
        {
            Debug.Log("s: could not listen on port 433");
        }        
    }

    void message(string msg, NetworkConnection conn)
    {       
        var m = new StringMessage(msg);
        NetworkServer.SendToClient(conn.connectionId,666,m);
    }
    void OnClientConnected(NetworkMessage netMessage)
    {
        NetworkConnection session = netMessage.conn;
        Debug.Log("s: New client connected");
    }

    void OnClientDisconnected(NetworkMessage netMessage)
    {
        NetworkConnection session = netMessage.conn;
        Debug.Log("s: Cient disconnected");
            foreach (VRplayer vrp in VRplayers)
            {
                if (vrp.nc == session)
                {
                    VRplayers.Remove(vrp);
                    break;
                }
            }
        foreach (CTRLplayer ctrlp in CTRLplayers)
        {
            if (ctrlp.nc == session)
            {
                CTRLplayers.Remove(ctrlp);
                break;
            }
        }
    }

    void OnMessageReceived(NetworkMessage netMessage)
    {
        NetworkConnection session = netMessage.conn;
        string msg = netMessage.reader.ReadString();
        string[] evt = msg.Split(':');
        if (evt.Length>=3&&evt[2] != "pos")
        {
            Debug.Log("s: message received: " + msg);

        }
        switch (evt[0])
        {
            case "log":
                if (sqlite.CorrectPass(evt[1],evt[2]))
                {
                    message("suc",session);
                    Debug.Log("A user successfully connected");
                }
                else
                {
                    message("fai", session);
                    Debug.Log("A user failed a connection attempt");
                }
                break;
            case "ready":
                if (evt[1] == "VR")
                {
                    VRplayers.Add(new VRplayer(evt[2],session,false));
                    Debug.Log(evt[2] + " is ready (VR)");
                }
                else
                {
                    CTRLplayers.Add(new CTRLplayer(evt[2], session,false));
                    Debug.Log(evt[2] + " is ready (CTRL)");
                }
                Debug.Log("There are " + VRplayers.Count + " VRplayers and " + CTRLplayers.Count + " CTRLplayers connected");
                break;
            case "notready":
                if (evt[1] == "VR")
                {
                    foreach (VRplayer vrp in VRplayers)
                    {
                        if (vrp.nc == session)
                        {
                            VRplayers.Remove(vrp);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (CTRLplayer ctrlp in CTRLplayers)
                    {
                        if (ctrlp.nc == session)
                        {
                            CTRLplayers.Remove(ctrlp);
                            break;
                        }
                    }
                }
                break;
            case "VRplist":
                if (VRplayers.Count==0)
                {
                    message("error:No client connected to play VR",session);
                }
                else
                {
                    foreach (VRplayer vrp in VRplayers)
                    {
                        message("pl:" + vrp.pseudo, session);
                        
                    }
                }
                break;
            case "CTRLplist":
                if (CTRLplayers.Count == 0)
                {
                    message("error:No client connected to play CTRL", session);
                }
                else
                {
                    foreach (CTRLplayer ctrlp in CTRLplayers)
                    {
                        message("pl:" + ctrlp.pseudo, session);
                    }
                }
                break;
            case "ask":
                string whosasking = null;
                if (evt[1] == "VR") //a VR player is asking a ctrl player
                {
                    foreach (VRplayer vrp in VRplayers) //find VRplayer by session
                    {
                        if (vrp.nc == session)
                        {
                            if (vrp.ingame)
                            {
                                Debug.Log(vrp.pseudo + " is already in a match");
                                message("error: " + vrp.pseudo + " is already in a match", session);
                                return;
                            }
                            whosasking = vrp.pseudo;
                            break;
                        }
                    }
                    foreach (CTRLplayer ctrlp in CTRLplayers) //find CTRLplayer by pseudo
                    {
                        if (ctrlp.pseudo == evt[2])
                        {
                            if (ctrlp.ingame)
                            {
                                Debug.Log(ctrlp.pseudo + " is already in a match");
                                message("error: " + ctrlp.pseudo + " is already in a match", session);
                                return;
                            }
                            message("cta:" + whosasking, ctrlp.nc);
                            break;
                        }
                    }
                }
                else //a CTRLplayer is trying to connect to a VRplayer
                {
                    foreach (CTRLplayer ctrlp in CTRLplayers) //find CTRLplayer by session
                    {
                        if (ctrlp.nc == session)
                        {
                            if (ctrlp.ingame)
                            {
                                Debug.Log(ctrlp.pseudo + " is already in a match");
                                message("error: " + ctrlp.pseudo + " is already in a match", session);
                                return;
                            }
                            whosasking = ctrlp.pseudo;
                            break;
                        }
                    }
                    foreach (VRplayer vrp in VRplayers.ToList()) //find VRplayer by pseudo
                    {
                        if (vrp.pseudo == evt[2] && !vrp.ingame)
                        {
                            message("cta:" + whosasking, vrp.nc);
                            break;
                        }
                    }
                }
                break;
            case "connectto":
                Game game = new Game(0, null, null);
                if (evt[1]=="VR") //a VR player is trying  to connect to a ctrl player
                {
                    foreach (CTRLplayer ctrlp in CTRLplayers) //find CTRLplayer by pseudo
                    {
                        if (ctrlp.pseudo == evt[2])
                        {
                            if (ctrlp.ingame)
                            {
                                Debug.Log(ctrlp.pseudo + " is already in a match");
                                message("error: " + ctrlp.pseudo + " is already in a match",session);
                                return;
                            }
                            game.ctrlp = ctrlp;
                            message("connected", session);
                            break;
                        }
                    }
                    foreach (VRplayer vrp in VRplayers) //find VRplayer by session
                    {
                        if (vrp.nc == session)
                        {
                            if (vrp.ingame)
                            {
                                Debug.Log(vrp.pseudo + " is already in a match");
                                message("error: " + vrp.pseudo + " is already in a match", session);
                                return;
                            }
                            game.vrp = vrp;
                             break;
                        }
                    }
                }
                else //a CTRLplayer is trying to connect to a VRplayer
                {
                    foreach (VRplayer vrp in VRplayers.ToList()) //find VRplayer by pseudo
                    {
                        if (vrp.pseudo == evt[2]&&!vrp.ingame)
                        {
                            game.vrp = vrp;
                            Debug.Log("Connected to " + evt[2]);
                            vrp.ingame = true;
                        }
                        break;
                    }
                    foreach (CTRLplayer ctrlp in CTRLplayers.ToList()) //find CTRLplayer by session
                    {
                        if (ctrlp.nc == session&&!ctrlp.ingame)
                        {
                            game.ctrlp = ctrlp;
                            Debug.Log("Connected to " + evt[2]);
                        }
                        break;
                    }
                }
               
                Debug.Log(game.id);
                Debug.Log(game.vrp.pseudo);
                Debug.Log(game.ctrlp.pseudo);
                count += 1;
                game.id = count;
                message("gameid:" + game.id, game.vrp.nc);
                message("gameid:" + game.id, game.ctrlp.nc);                             
                Games.Add(game);
                Debug.Log("s: A new game is about to start: " + game.ctrlp.nc + ": " + game.ctrlp.pseudo + " -> " + game.vrp.nc + ": " + game.vrp.pseudo);
                break;


        //To do: remove game from games when its game over | pbly OK
            case "go": //game over
                Games = Games.Where(g => g.id.ToString() != evt[1]).ToList();
                Debug.Log("game id:" + evt[1] + "has ended");
                break;
            case "win":
                foreach (Game g in Games)//find game by id
                {
  
                    var sql = GetComponent<SQLite>();
                    if (g.id == int.Parse(evt[1]))
                    {
                        if (session == g.vrp.nc) //VR player sending
                        {
                            sql.win_incr(g.vrp.pseudo,true);
                            Debug.Log(g.vrp.pseudo + "won game " + g.id);
                        }
                        else if (session == g.ctrlp.nc) //CTRL player fsending
                        {
                            sql.win_incr(g.ctrlp.pseudo, false);
                            Debug.Log(g.ctrlp.pseudo + "won game " + g.id);
                        }
                    }
                }
                break;
            case "ig": //ingame players
                foreach (Game g in Games)//find game by id
                {
                    if (g.id == int.Parse(evt[1]))
                    {
                        if (session == g.vrp.nc) //VR player sending
                        {
                            message(msg, g.ctrlp.nc); //send command to CTRL player
                        }
                        else if (session == g.ctrlp.nc) //CTRL player fsending
                        {
                            message(msg, g.vrp.nc); //send command to vr player
                        }
                    }             
                }
                break;
        }
    }
    void OnApplicationQuit()
    {
        NetworkServer.Shutdown();      
    }
}

