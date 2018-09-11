using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Brain : MonoBehaviour
{
  public Vector3[] destinations;
  int destinationsIndex;
  public Vector3 currentLoc;

  public GameObject endPoint;
  public float fitness = 999;

  public bool active;
  public float stepDistanceNegative, stepDistancePositive;

  private void Start()
  {
    active = true;
    destinationsIndex = 0;
    currentLoc = transform.position;
  }

  private void Update()
  {
    if (destinationsIndex < destinations.Length && active)
    {
      move();
    }
    calculateFitness();
  }

  void move()
  {
    transform.position = Vector3.MoveTowards(transform.position, currentLoc + destinations[destinationsIndex], 0.5f);
    if (Vector3.Distance(transform.position, currentLoc + destinations[destinationsIndex]) <= 0.5f)
    {
      currentLoc = transform.position;
      destinationsIndex++;
    }
  }

  void calculateFitness()
  {
    float currentFitness = Vector3.Distance(endPoint.transform.position, transform.position);
    if (currentFitness < fitness)
    {
      fitness = currentFitness;
    }
  }

  public void initalizeDestinations()
  {
    for (int i = 0; i < destinations.Length; i++)
    {
      float x = Random.Range(stepDistanceNegative, stepDistancePositive);
      float z = Random.Range(stepDistanceNegative, stepDistancePositive);
      destinations[i] = new Vector3(x, 0, z);
    }
  }

  public void mutate(float amount)
  {
    List<int> changes = new List<int>();
    for (int i = 0; i < Random.Range(1, 10); i++)
    {
      changes.Add(Random.Range(0, 101));
    }

    for (int i = 0; i < destinations.Length; i++)
    {
      if (changes.Contains(i))
      {
        float x = Random.Range(-amount, amount);
        float z = Random.Range(-amount, amount);
        destinations[i] = new Vector3(x, 0, z);
      }
    }
  }
}
