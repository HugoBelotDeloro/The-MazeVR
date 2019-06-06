using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENDGAME : MonoBehaviour
{
    private GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.Find("Player");
            end();
        }
        
        void end()
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerMovement>().end.GetComponent<Canvas>().enabled = true;
            StartCoroutine(Wait());
        }
 
        IEnumerator Wait()
        {
            while (Input.GetAxis("Action") <= 0 && Input.GetAxis("Echap") <= 0)
            {
                yield return new WaitForSecondsRealtime(0.05f);
            }
 
            if (Input.GetAxis("Action") > 0)
            {
                GameManager.Instance.ResetScene();
            }
            else
            {
                GameManager.Instance.UnloadScene(GameManager.Instance.mainScene);
                GameManager.Instance.LoadScene("MainMenu");
            }
            yield return new WaitForSecondsRealtime(0.5f);
         
        }
    }
}
