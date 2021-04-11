using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
  public Waypoint[] path;

  void Awake()
  {
    Initialize();
  }

  [ContextMenu("Initialize")]
  private void Initialize()
  {
    path = gameObject.GetComponentsInChildren<Waypoint>();
  }
}
