using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootControl : MonoBehaviour
{

protected float Animation;
protected GameObject GoalLine;
protected AimingControl AimCtrl;

protected float posX = 0.0f;
protected float posY = 0.0f;
protected float posZ = 0.0f;

public float StartPosX = 0f;
public float StartPosY = -0.3777281f;
public float StartPosZ = -3.635868f;

public float RespawnTimeInSeconds = 1f;

void start()
{
  // GoalLine = GameObject.Find("GoalLine");
  // AimCtrl = GoalLine.GetComponent<AimingControl>();
  ResetStartingPosition();
}

private void ResetStartingPosition()
{
  Debug.Log("ResetStartingPosition");
  transform.position = new Vector3(StartPosX, StartPosY, StartPosZ);
}

void FixedUpdate()
{

}

void OnMouseDown()
{
  GameObject goalline = GameObject.Find("GoalLine");
  AimingControl aiming = goalline.GetComponent<AimingControl>();
  // Debug.Log("aiming posX: " + aiming.getPosX() + " | aiming posY: " + aiming.getPosY() + " | aiming posZ: " + aiming.getPosZ());
  // Debug.Log("posX: " + this.posX + " | posY: " + this.posY);

  float posX = aiming.getPosX();
  float posY = aiming.getPosY();
  float posZ = aiming.getPosZ();

  Animation += Time.fixedDeltaTime;
  Animation = Animation % 5f;
  // transform.position = MathParabola.Parabola(Vector3.zero, Vector3.forward *10f, 5f, Animation / 5f);

  transform.position = new Vector3(posX, posY, 2.5f);

  Invoke("ResetStartingPosition", RespawnTimeInSeconds);
}

}