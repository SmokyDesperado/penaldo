using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetection : MonoBehaviour
{

private bool collision = false;
private double goals = 0;

// Start is called before the first frame update
void Start()
{
  goals = 0;
}

// Update is called once per frame
void Update()
{

}

public void Restart()
{
  Debug.Log("restart GoalDetection");
  collision = false;
}

private void OnTriggerEnter(Collider col)
{
  if(col.gameObject.name == "Football" && collision == false)
  {
    goals++;
    collision = true;
  }
}

public double GetGoals()
{
  return goals;
}

public bool GetCollision()
{
  return collision;
}
}
