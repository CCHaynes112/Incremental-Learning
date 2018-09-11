using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{

  public GameObject simulationController;

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      GameObject bestAnimal = other.gameObject;
      bestAnimal.transform.parent = null;
      Destroy(GameObject.Find("Population"));
      simulationController.GetComponent<Population>().enabled = false;
    }
  }
}