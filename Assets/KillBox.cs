using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      //Destroy(other.transform.gameObject);
      other.gameObject.GetComponent<Brain>().active = false;
    }
  }
}
