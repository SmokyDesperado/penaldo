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

void start()
{
  // GoalLine = GameObject.Find("GoalLine");
  // AimCtrl = GoalLine.GetComponent<AimingControl>();
}

void FixedUpdate()
{

}

void OnMouseDown()
{
  GameObject goalline = GameObject.Find("GoalLine");
  AimingControl aiming = goalline.GetComponent<AimingControl>();
  Debug.Log("aiming posX: " + aiming.getPosX() + " | aiming posY: " + aiming.getPosY() + " | aiming posZ: " + aiming.getPosZ());
  
  float posX = aiming.getPosX();
  float posY = aiming.getPosY();
  float posZ = aiming.getPosZ();

  Animation += Time.fixedDeltaTime;
  Animation = Animation % 5f;
  // transform.position = MathParabola.Parabola(Vector3.zero, Vector3.forward *10f, 5f, Animation / 5f);

  transform.position = new Vector3(posX, posY, 2.5f);
  // Debug.Log("posX: " + this.posX + " | posY: " + this.posY);

  // Debug.Log("posX: " + AimCtrl.getPosX() + " | posY: " + AimCtrl.getPosY());

}

}