using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
  [SerializeField] private GameObject Player;

  private void OnTriggerStay(Collider other)
  {
    if (other.CompareTag("Player")&& Input.GetAxis("Action")>0)
    {
      Vector3 posi = other.transform.position;
      posi.y += Time.deltaTime*2;
      other.transform.position = posi ;
      
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      other.GetComponent<Rigidbody>().useGravity = false;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      other.GetComponent<Rigidbody>().useGravity = true;
    }
  }
}
