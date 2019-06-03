using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField] private GameObject output;
	[SerializeField] private string inputScene;
	[SerializeField] private string outputScene;
    [SerializeField] private Light lampIn;
    [SerializeField] private Light lampOut;
    [SerializeField] private Color color;
    [SerializeField] private Item key;
	private int _timer;
	private bool _currentlyTeleporting;
	private Vector3 _destination;
	private AsyncOperation _unloadingScene;
	private GameObject _target;
    private bool Open;

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

		if (_currentlyTeleporting)
		{
			if (_unloadingScene.isDone)
			{
				_target.transform.position = _destination;
				_currentlyTeleporting = false;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
        if (Open)
        {
            Teleport(other.gameObject);
        }
        else if (other.CompareTag("Player") && other.GetComponentInChildren<Inventory>().RemoveItem(key))
        {
            Open = true;
            Teleport(other.gameObject);
        }
	}

    private void Teleport(GameObject entity)
    {
        if (_timer == 0)
        {
            _timer = 80;
            _target = entity;
            GameManager.Instance.LoadScene(outputScene);
            _unloadingScene = GameManager.Instance.UnloadSingleScene(inputScene);
            _currentlyTeleporting = true;
            _destination = output.transform.position;
        }
    }

	public void TeleportBack(GameObject entity)
	{
		if (_timer == 0)
		{
			_timer = 80;
			_target = entity;
			GameManager.Instance.LoadScene(inputScene);
			_unloadingScene = GameManager.Instance.UnloadSingleScene(inputScene);
			_currentlyTeleporting = true;
			_destination = gameObject.transform.position;
		}
	}
}
