using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField] private GameObject output;
	[SerializeField] private string inputScene;
	[SerializeField] private string outputScene;
	private int _timer;
	private bool _currentlyTeleporting;
	private Vector3 _destination;
	private AsyncOperation _unloadingScene;
	private GameObject _target;

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
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
		if (_timer == 0)
		{
			_timer = 80;
			_target = other.gameObject;
			GameManager.Instance.LoadScene(outputScene);
			_unloadingScene = GameManager.Instance.UnloadSingleScene(inputScene);
			_currentlyTeleporting = true;
			_destination = output.transform.position;
		}
	}

	public void TeleportBack(GameObject other)
	{
		if (_timer == 0)
		{
			_timer = 80;
			_target = other;
			GameManager.Instance.LoadScene(inputScene);
			_unloadingScene = GameManager.Instance.UnloadSingleScene(inputScene);
			_currentlyTeleporting = true;
			_destination = gameObject.transform.position;
		}
	}
}
