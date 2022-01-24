﻿using UnityEngine;
using System;

public class PathLayout : MonoBehaviour {

	public PathData pathInfo;
	public Vector3[] points;

	public bool unlocked
    {
		get;
		set;
    }

	private int CurveCount
    {
        get
        {
			return (points.Length - 1) / 3;
        }
    }

	public Vector3 GetPoint (float t) {
		int i;
		if(t >= 1)
        {
			t = 1;
			i = points.Length - 4;
        }
        else
        {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
        }
		return transform.TransformPoint(Bezier.GetPoint(points[i], points[i + 1], points[i + 2], points[i + 3], t));
	}
	
	public Vector3 GetVelocity (float t) {
		int i;
		if (t >= 1)
		{
			t = 1;
			i = points.Length - 4;
		}
		else
		{
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.GetFirstDerivative(points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
	}
	
	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}

	public void AddCurve()
    {
		Vector3 point = points[points.Length - 1];
		Array.Resize(ref points, points.Length + 3);
		point.x += 1;
		points[points.Length - 3] = point;
		point.x += 1;
		points[points.Length - 2] = point;
		point.x += 1;
		points[points.Length - 1] = point;
	}

	public void RemoveLastCurve()
    {
		Vector3 point = points[points.Length - 1];
		Array.Resize(ref points, points.Length - 3);
		point.x -= 1;
		points[points.Length - 1] = point;
		point.x -= 1;
		points[points.Length - 1] = point;
		point.x -= 1;
		points[points.Length - 1] = point;
	}
}