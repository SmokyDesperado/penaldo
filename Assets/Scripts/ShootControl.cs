﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Linq;
using UnityEngine.Windows.Speech;

using UnityEngine.UI;

public class ShootControl : MonoBehaviour
{

protected float posX = 0.0f;
protected float posY = 0.0f;
protected float posZ = 0.0f;

public float StartPosX = 0f;
public float StartPosY = -0.3777281f;
public float StartPosZ = -3.635868f;

public float RespawnTimeInSeconds = 1f;
public float ShowShootResultTimeInSeconds = 1f;
public float shootForce = 10f;

public float shootForceX = 5f;
public float shootForceY = 10f;

private float goalLineYModifier = 1.7f;

public Text points;
public Text highscore;

private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
private KeywordRecognizer keywordRecognizer;

void Start()
{
  
}

public void StartGame()
{
  ResetStartingPosition();

  RespawnTimeInSeconds = 2f;
  ShowShootResultTimeInSeconds = 3f;

  keywordActions.Add("kick", VoiceControlShoot);
  keywordActions.Add("shoot", ShootThere);
  keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
  keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
  SetPoints();
  keywordRecognizer.Start();
}

void FixedUpdate()
{

}

private void ShootThere()
{
  Debug.Log("shoot there: " );
  Vector3 p = Input.mousePosition;
  p.z = -3.635868f;
  Vector3 Pos = Camera.main.ScreenToWorldPoint(p);
  Debug.Log("PosX: " + Pos.x + " | PosY: " + Pos.y + " | PosZ: " + Pos.z);

  float posX = (StartPosX - Pos.x) * shootForceX;
  float posY = StartPosY + ((goalLineYModifier - Pos.y) * shootForceY);

  StartKeeperMovement();
  GetComponent<Rigidbody>().AddForce(posX, posY, shootForce, ForceMode.Impulse);

  Invoke("SHowShootResult", ShowShootResultTimeInSeconds);
}

private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
{
  Debug.Log("Keyword: " + args.text);
  keywordActions[args.text].Invoke();
}

private void VoiceControlShoot()
{
  Debug.Log("shoot ball -- VOICE CONTROL");
  ShootBall();
}

private void ResetStartingPosition()
{
  Debug.Log("ResetStartingPosition");

  GetComponent<Rigidbody>().velocity = new Vector3 (0,0,0);
  GetComponent<Rigidbody>().angularVelocity = new Vector3 (0,0,0);

  transform.position = new Vector3(StartPosX, StartPosY, StartPosZ);
  ResetStartingPositionKeeper();
  ResetGoalDetection();
  ResetResultImagesPosistion();
}

private void ResetGoalDetection()
{
  GameObject goalDetection = GameObject.Find("GoalDetection");
  GoalDetection detection = goalDetection.GetComponent<GoalDetection>();
  detection.Restart();
}

private void SHowShootResult()
{
  GameObject goalDetection = GameObject.Find("GoalDetection");
  GoalDetection detection = goalDetection.GetComponent<GoalDetection>();
  if(detection.GetCollision()) {
    Debug.Log("GOAL!!! " + detection.GetGoals());
    GameObject goalImage = GameObject.Find("ShootResult_Goal");
    goalImage.GetComponent<RectTransform>().position = new Vector3(0f, 1.1f, -5.5f);
    detection.CalculateHighscore();
    SetPoints();
  }
  else {
    Debug.Log("No goal LOOSER!!!");
    GameObject missedImage = GameObject.Find("ShootResult_Missed");
    missedImage.GetComponent<RectTransform>().position = new Vector3(0f, 1.1f, -5.5f);
    detection.SetGoals(0);
    detection.CalculateHighscore();
    SetPoints();
  }

  Invoke("ResetStartingPosition", RespawnTimeInSeconds);
}

private void SetPoints()
{
  GameObject goalDetection = GameObject.Find("GoalDetection");
  GoalDetection detection = goalDetection.GetComponent<GoalDetection>();
  double goals = detection.GetGoals();
  double highscores = detection.GetHighscore();
  points.text = "Goals: " + goals.ToString();
  highscore.text = "Highscore: " + highscores.ToString();
}

private void ResetResultImagesPosistion()
{
  GameObject goalImage = GameObject.Find("ShootResult_Goal");
  goalImage.GetComponent<RectTransform>().position = new Vector3(0, -2, 0);

  GameObject missedImage = GameObject.Find("ShootResult_Missed");
  missedImage.GetComponent<RectTransform>().position = new Vector3(0, -2, 0);
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

void OnMouseDown()
{
  Debug.Log("shoot ball -- MOUSE DOWN");
  ShootBall();
}

private void ShootBall()
{
  GameObject goalline = GameObject.Find("GoalLine");
  AimingControl aiming = goalline.GetComponent<AimingControl>();

  float posX = (StartPosX - aiming.getPosX()) * shootForceX;
  float posY = StartPosY + ((goalLineYModifier - aiming.getPosY()) * shootForceY);
  float posZ = aiming.getPosZ() - StartPosZ;

  // Debug.Log("posX: " + posX + " | posY: " + posY + " | posZ: " + posZ);
  Debug.Log("posX: " + aiming.getPosX() + " | posY: " + aiming.getPosY());

  StartKeeperMovement();
  GetComponent<Rigidbody>().AddForce(posX, posY, shootForce, ForceMode.Impulse);

  Invoke("SHowShootResult", ShowShootResultTimeInSeconds);
}
}