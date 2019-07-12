using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootControl : MonoBehaviour
{

protected float posX = 0.0f;
protected float posY = 0.0f;
protected float posZ = 0.0f;

public float StartPosX = 0f;
public float StartPosY = -0.3777281f;
public float StartPosZ = -3.635868f;

public float RespawnTimeInSeconds = 1f;
public float shootForce = 10f;

public float shootForceX = 5f;
public float shootForceY = 10f;

private float goalLineYModifier = 1.7f;

void start()
{
  ResetStartingPosition();
}

private void ResetStartingPosition()
{
  Debug.Log("ResetStartingPosition");

  GetComponent<Rigidbody>().velocity = new Vector3 (0,0,0);
  GetComponent<Rigidbody>().angularVelocity = new Vector3 (0,0,0);
  
  transform.position = new Vector3(StartPosX, StartPosY, StartPosZ);
  ResetStartingPositionKeeper();
}

private void ResetStartingPositionKeeper()
{
  GameObject goalie = GameObject.Find("Goalie");
  KeeperControl keeperCtrl = goalie.GetComponent<KeeperControl>();
  keeperCtrl.ResetStartingPosition();
}

private void StartKeeperMovement()
{
  GameObject goalie = GameObject.Find("Goalie");
  KeeperControl keeperCtrl = goalie.GetComponent<KeeperControl>();
  keeperCtrl.MoveKeeper();
}

void FixedUpdate()
{

}

void OnMouseDown()
{
  GameObject goalline = GameObject.Find("GoalLine");
  AimingControl aiming = goalline.GetComponent<AimingControl>();
  
  float posX = (StartPosX - aiming.getPosX()) * shootForceX;
  float posY = StartPosY + ((goalLineYModifier - aiming.getPosY()) * shootForceY);
  float posZ = aiming.getPosZ() - StartPosZ;

  // Debug.Log("posX: " + posX + " | posY: " + posY + " | posZ: " + posZ);

  StartKeeperMovement();
  GetComponent<Rigidbody>().AddForce(posX, posY, shootForce, ForceMode.Impulse);

  Invoke("ResetStartingPosition", RespawnTimeInSeconds);
}

}