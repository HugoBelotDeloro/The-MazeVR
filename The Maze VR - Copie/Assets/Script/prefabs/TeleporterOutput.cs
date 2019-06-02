using UnityEngine;

public class TeleporterOutput : MonoBehaviour
{
	[SerializeField] private Teleporter teleporter;
	
	private void OnTriggerEnter(Collider other)
	{
		teleporter.TeleportBack(other.gameObject);
	}
}
