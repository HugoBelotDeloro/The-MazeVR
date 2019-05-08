using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    [SerializeField] private GameObject north;
    [SerializeField] private GameObject east;
    [SerializeField] private GameObject south;
    [SerializeField] private GameObject west;

    private enum Orientation
    {
        North,
        East,
        South,
        West
    }

    private Orientation _orientation;

    private void Start()
    {
        _orientation = Orientation.North;
        _map = new Dictionary<Orientation, GameObject>
        {
            {Orientation.North, north},
            {Orientation.East, east},
            {Orientation.South, south},
            {Orientation.West, west}
        };
    }

    private Dictionary<Orientation, GameObject> _map;
    
    void Update()
    {
        Orientation temp = _orientation;
        var rotation = player.transform.rotation.eulerAngles.y;
        if (rotation <= 45 || rotation > 315)
        {
            _orientation = Orientation.North;
        }
        else if (rotation <= 135)
        {
            _orientation = Orientation.East;
        }
        else if (rotation <= 225)
        {
            _orientation = Orientation.South;
        }
        else
        {
            _orientation = Orientation.West;
        }
        if (_orientation != temp)
        {
            if (_map.TryGetValue(temp, out GameObject old) && _map.TryGetValue(_orientation, out GameObject notNew))
            {
                old.SetActive(false);
                notNew.SetActive(true);
            }
        }
    }
}
