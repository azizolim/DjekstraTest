using System;
using System.Collections.Generic;
using UnityEngine;

public class Djekstra : MonoBehaviour
{
    [SerializeField] private Point[] points;
    private Dictionary<int, int> _distances;
    private Dictionary<int, int> _previousPoints;
    private List<Point> _unvisitedPoints;

    void Start()
    {
        _distances = new Dictionary<int, int>();
        _previousPoints = new Dictionary<int, int>();
        _unvisitedPoints = new List<Point>();

        foreach (Point point in points)
        {
            _distances[point.ID] = int.MaxValue;
            _previousPoints[point.ID] = -1;
            _unvisitedPoints.Add(point);
        }

        _distances[1] = 0;

        while (_unvisitedPoints.Count > 0)
        {
            Point currentPoint = GetClosestPoint();
            if (_distances[currentPoint.ID] == int.MaxValue) break;
            foreach (Point neighbor in currentPoint.Neighbours)
            {
                int distance = (int)Vector3.Distance(currentPoint.transform.position, neighbor.transform.position);
                int altDistance = _distances[currentPoint.ID] + distance;
                if (altDistance < _distances[neighbor.ID])
                {
                    _distances[neighbor.ID] = altDistance;
                    _previousPoints[neighbor.ID] = currentPoint.ID;
                }
            }
        }

        foreach (Point point in points)
        {
            if (point.ID != 1)
            {
                string path = GetPath(point.ID);
                Debug.Log("Shortest path from Point 1 to Point " + point.ID + ": " + path);
            }
        }
    }

    private Point GetClosestPoint()
    {
        Point closestPoint = null;
        int minDistance = int.MaxValue;
        foreach (Point point in _unvisitedPoints)
        {
            if (_distances[point.ID] < minDistance)
            {
                closestPoint = point;
                minDistance = _distances[point.ID];
            }
        }
        _unvisitedPoints.Remove(closestPoint);
        return closestPoint;
    }

    private string GetPath(int pointID)
    {
        string path = "";
        while (pointID != -1)
        {
            path = pointID + " -> " + path;
            pointID = _previousPoints[pointID];
        }
        return path;
    }
}