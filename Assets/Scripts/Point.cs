using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
   [SerializeField] private int Id;
   public int ID => Id;
   [SerializeField] private List<Point> neighbors;
   public List<Point> Neighbours => neighbors;
}
