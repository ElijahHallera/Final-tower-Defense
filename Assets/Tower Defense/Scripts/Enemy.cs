using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
  public Path route;
  private Waypoint[] myPathThroughLife;
  public int coinWorth;
  public float health = 100;
  public float speed = .25f;
  private int index = 0;
  private Vector3 nextWaypoint;
  private bool stop = false;
  private float healthPerUnit;

  public Transform healthBar;

  public UnityEvent DeathEvent;

  public GameObject particleEffect;

    void Start()
  {
    healthPerUnit = 100f / health;
    myPathThroughLife = route.path;
    transform.position = myPathThroughLife[index].transform.position;
    Recalculate();
  }

  void Update()
  {
    if (!stop)
    {
      if ((transform.position - myPathThroughLife[index + 1].transform.position).magnitude < .1f)
      {
        index = index + 1;
        Recalculate();
      }


      Vector3 moveThisFrame = nextWaypoint * Time.deltaTime * speed;
      transform.Translate(moveThisFrame);
    }

  }

  void Recalculate()
  {
    if (index < myPathThroughLife.Length -1)
    {
      nextWaypoint = (myPathThroughLife[index + 1].transform.position - myPathThroughLife[index].transform.position).normalized;
    }
    else
    {
      stop = true;
            Debug.Log(" You Lost Health");
            SceneManager.LoadScene(sceneName: "YouLost");
        }
  }

  public void Damage()
  {
    Damage(20);
  }


  public void Damage(float hitAmount)
  {
    health -= hitAmount;
    if (health <= 0)
    {
            GameObject particle = Instantiate(particleEffect, transform.position, Quaternion.identity);
            DeathEvent.Invoke();
            DeathEvent.RemoveAllListeners();
            Destroy(particle, 2f);
            Destroy(this.gameObject);
        }

    float percentage = healthPerUnit * health;
    Vector3 newHealthAmount = new Vector3(percentage / 100f, healthBar.localScale.y, healthBar.localScale.z);
    healthBar.localScale = newHealthAmount;
  }
}
