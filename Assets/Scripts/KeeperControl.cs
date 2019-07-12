using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperControl : MonoBehaviour
{
  public float StartPosX = 0.107064f;
  public float StartPosY = -0.02332067f;
  public float StartPosZ = -1.499923f;

  public float moveForce = 1f;

  private float movingDirection = 0f;
  private float movingDirectionMax = 1f;
  private float movingDirectionMin = -1f;

// Start is called before the first frame update
void Start()
{
  moveForce = 50f;
  ResetStartingPosition();
  // GetComponent<Rigidbody>().constraints  = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
}

public void ResetStartingPosition()
{
  Debug.Log("Keeper ResetStartingPosition");

  GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
  GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 0, 0);
  GetComponent<Rigidbody>().mass = 100f;

  transform.eulerAngles = new Vector3 (0, 0, 0);
  transform.position = new Vector3(StartPosX, StartPosY, StartPosZ);
}

private void StopMovements()
{
  Debug.Log("StopMovements");
  GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
  GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 0, 0);

  // transform.eulerAngles = new Vector3 (0, 0, 0);
  // transform.position = new Vector3(movingDirection, StartPosY, StartPosZ);
}

// Update is called once per frame
void FixedUpdate()
{
  Debug.Log("position.x: " + GetComponent<Rigidbody>().position.x + " | movingDirection: " + movingDirection);

  if((GetComponent<Rigidbody>().position.x > movingDirectionMax && movingDirection > 0) || (GetComponent<Rigidbody>().position.x < movingDirectionMin && movingDirection < 0)) {
    StopMovements();
  }

}

public void MoveKeeper()
{
  // randomized moving direction: 0 => move left, 1 => move right
  float rnd = Random.Range(-1.3f, 1.3f);

  movingDirection = rnd;

  // movingDirection = 1.3f;

  GetComponent<Rigidbody>().velocity = new Vector3 (movingDirection * moveForce, 0, 0);
  // GetComponent<Rigidbody>().MovePosition(new Vector3(StartPosX + movingDirection, StartPosY, StartPosZ));
}

}
