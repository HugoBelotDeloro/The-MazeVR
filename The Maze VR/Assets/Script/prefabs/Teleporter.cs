using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField] private GameObject output;
	[SerializeField] private string labyCode;
	[SerializeField] private GameObject labyCreator;
    [SerializeField] private Light lampIn;
    [SerializeField] private Light lampOut;
    [SerializeField] private Color color;
    [SerializeField] private Item key;
    [SerializeField] private GameObject goal;
	private int _timer;
	private bool _open;
	private bool _generated;
	private Vector3 _destination;

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
        lampIn.color = color;
        lampOut.color = color;
	}

	private void Update()
	{
		if (_timer > 0)
		{
			_timer--;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
        if (_open)
        {
            Teleport(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
	        if (key.Type == Equipment.ItemType.Null || other.GetComponentInChildren<Inventory>().RemoveItem(key))
	        {
		        _open = true;
		        Teleport(other.gameObject);
	        }
        }
	}

    private void Teleport(GameObject entity)
    {
        if (_timer == 0)
        {
	        if (!_generated)
            {
	            GameObject laby = Instantiate(labyCreator, output.transform.position, new Quaternion());
	            Generator generator = laby.GetComponent<Generator>();
	            generator.code = labyCode;
	            generator.P[8].prefab = goal;
	            generator.enabled = true;
	            _generated = true;
	            StartCoroutine(SetTeleport(entity));
            }
            else
            {
	            entity.transform.position = output.transform.position;
	            _timer = 80;
            }
        }
    }

    private IEnumerator SetTeleport(GameObject entity)
    {
	    yield return new WaitUntil(() => !(PlzTPMe.LastCallForHelp is null));
	    PlzTPMe plzTpMe = PlzTPMe.LastCallForHelp;
	    Vector3 position = plzTpMe.gameObject.transform.position;
	    output.transform.position = position;
	    Teleport(entity);
    }

	public void TeleportBack(GameObject entity)
	{
		if (_timer == 0)
		{
			_timer = 80;
			entity.transform.position = gameObject.transform.position;
		}
	}
}
