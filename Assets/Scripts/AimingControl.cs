using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingControl : MonoBehaviour
{

public float PosX = 0.0f;
public float PosY = 0.0f;
public float PosZ = 0.0f;

void start()
{

}

void OnMouseDown()
{
  Vector3 p = Input.mousePosition;
  p.z = -3.635868f;
  Vector3 Pos = Camera.main.ScreenToWorldPoint(p);
  SetAimingPosition(Pos.x, Pos.y, Pos.z);
  Debug.Log("PosX: " + PosX + " | PosY: " + PosY + " | PosZ: " + PosZ);
}

void SetAimingPosition(float AimingPosX, float AimingPosY, float AimingPosZ)
{
  PosX = AimingPosX;
  PosY = AimingPosY;
  PosZ = AimingPosZ;
}

public float getPosX()
{
  return PosX;
}

public float getPosY()
{
  return PosY;
}

public float getPosZ()
{
  return PosZ;
}

}
