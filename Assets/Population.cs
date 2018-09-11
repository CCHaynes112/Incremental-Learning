using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
  public GameObject animal;
  public int populationSize;
  public int generations;
  public List<GameObject> population = new List<GameObject>();
  GameObject mostFitAnimal;
  public GameObject populationObj;
  public float mutationRate;

  void Start()
  {
    StartCoroutine(beginEvoloution());
  }

  IEnumerator beginEvoloution()
  {
    Debug.Log("Started Evolution");

    for (int i = 0; i < generations; i++)
    {
      yield return StartCoroutine(createNewGeneration(i));
      yield return StartCoroutine(getMostFitAnimal());
    }

    Debug.Log("Finished Evolution");
  }

  IEnumerator createNewGeneration(int currentGeneration)
  {
    Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~");
    Debug.Log("Current Generation: " + currentGeneration);

    clearAndDelete();

    if (currentGeneration == 0)
    {
      for (int i = 0; i < populationSize; i++)
      {
        GameObject tempAnimal = Instantiate(animal, new Vector3(200, 1, 0), Quaternion.identity);
        tempAnimal.name = "Animal";
        tempAnimal.transform.parent = populationObj.transform;
        population.Add(tempAnimal);
        tempAnimal.GetComponent<Brain>().initalizeDestinations();
      }
    }
    else
    {
      for (int i = 0; i < populationSize; i++)
      {
        GameObject tempAnimal = Instantiate(mostFitAnimal, new Vector3(200, 1, 0), Quaternion.identity);
        tempAnimal.name = "Animal";
        tempAnimal.transform.parent = populationObj.transform;
        tempAnimal.GetComponent<Brain>().mutate(mutationRate);
        tempAnimal.GetComponent<Brain>().enabled = true;
        population.Add(tempAnimal);
      }
    }
    yield return new WaitForSeconds(36);
    Debug.Log("Generation Simulation Complete");
  }

  IEnumerator getMostFitAnimal()
  {
    Debug.Log("Getting most fit animal...");
    mostFitAnimal = population[0];
    for (int i = 0; i < population.Count; i++)
    {
      if (population[i].GetComponent<Brain>().fitness < mostFitAnimal.GetComponent<Brain>().fitness)
      {
        mostFitAnimal = population[i];
      }
    }
    yield return 0;
    Debug.Log("Most fit animal: " + mostFitAnimal.GetComponent<Brain>().fitness);
  }

  void clearAndDelete()
  {
    for (int i = 0; i < population.Count; i++)
    {
      Destroy(population[i]);
    }
    population.Clear();
  }

  bool previousGenerationBetter()
  {
    float bestFitness = 999;
    for (int i = 0; i < population.Count; i++)
    {
      if (population[i].GetComponent<Brain>().fitness < bestFitness)
      {
        bestFitness = population[i].GetComponent<Brain>().fitness;
      }
    }
    if (bestFitness > mostFitAnimal.GetComponent<Brain>().fitness)
      return true;
    return false;
  }
}

/*
    if (previousGenerationBetter())
 */
