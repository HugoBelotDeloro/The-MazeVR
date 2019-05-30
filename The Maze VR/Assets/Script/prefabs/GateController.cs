using UnityEngine;

public class GateController : MonoBehaviour
{
	[SerializeField] private GameObject leftDoor;
	[SerializeField] private GameObject rightDoor;
	[SerializeField] private Item keyItem;
	private int _timer;
	private Vector3 _leftDoorAxis;
	private Vector3 _rightDoorAxis;
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Inventory inventory = other.GetComponentInChildren<Inventory>();
			if (inventory.RemoveItem(keyItem))
			{
				Rotate();
			}
		}
	}

	private void Start()
	{
		_leftDoorAxis = leftDoor.transform.position + new Vector3(0, 0, 3.6f);
		_rightDoorAxis = rightDoor.transform.position - new Vector3(0, 0, 3.6f);
	}

	private void Update()
	{
		if (_timer > 0)
		{
			_timer--;
			leftDoor.transform.RotateAround(_leftDoorAxis, Vector3.up, -1);
			rightDoor.transform.RotateAround(_rightDoorAxis, Vector3.up, 1);
		}
	}

	private void Rotate()
	{
		_timer = 80;
	}
}
