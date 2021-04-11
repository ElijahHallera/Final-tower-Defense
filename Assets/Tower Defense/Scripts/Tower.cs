using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
  public List<Enemy> currentEnemies;
  public Enemy currentTarget;
  public Transform turret;
  private delegate void enemySubscription(Enemy enemy);

  private LineRenderer laser;
  public float hitAmount = 10;

    void Start()
  {
        laser = GetComponent<LineRenderer>();
        laser.SetPosition(0, turret.transform.position);
  }

  void Update()
  {
        if (currentTarget)
        {
            laser.enabled = true;
            laser.SetPosition(1, currentTarget.transform.position);
            currentTarget.Damage(hitAmount * Time.deltaTime);
        }
        else
        {
            laser.enabled = false;
        }
        if(FindObjectOfType<Enemy>() == null)
        {
            Debug.Log("ALL DEAD");
            SceneManager.LoadScene(sceneName: "YouWon");
        }
    }


  void OnTriggerEnter(Collider collider)
  {
    if (collider.GetComponent<Enemy>() != null)
    {
      Enemy newEnemy = collider.GetComponent<Enemy>();
      newEnemy.DeathEvent.AddListener(delegate { BookKeeping(newEnemy);});
      currentEnemies.Add(newEnemy);
            if (currentTarget == null)
            {
            currentTarget = newEnemy;
            }
        }
    }

  void OnTriggerExit(Collider collider)
  {
    if (collider.GetComponent<Enemy>() != null)
    {
      Enemy oldEnemy = collider.GetComponent<Enemy>();
      BookKeeping(oldEnemy);     
       }
    }

  void BookKeeping(Enemy enemy)
  {
    currentEnemies.Remove(enemy);
    currentTarget = (currentEnemies.Count >0) ? currentEnemies[0]: null;
    
  }
}
