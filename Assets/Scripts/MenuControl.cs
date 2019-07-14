using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{

private bool gameStart = false;
private AudioSource audioData;

// Start is called before the first frame update
void Start()
{
  audioData = GetComponent<AudioSource>();
}

// Update is called once per frame
void Update()
{

}

public void OnMouseDown()
{
  if(gameStart == false)
  {
    Debug.Log("StartGame");
    StartGame();
  }
  else {
    EndGame();
  }
}

private void StartGame()
{
  GetComponent<Transform>().position = new Vector3(0f, -4f, -5f);

  GameObject football = GameObject.Find("Football");
  ShootControl shooCtrl = football.GetComponent<ShootControl>();
  shooCtrl.StartGame();

  audioData.Play(0);
  gameStart = true;
}

private void EndGame()
{
  GetComponent<Transform>().position = new Vector3(0f, 1.5f, -5f);
  audioData.Stop();
}

}
