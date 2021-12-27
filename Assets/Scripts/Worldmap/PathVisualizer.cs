using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    public float speed = 1;
    public float progress = 0;
    public float visualizationSpeed = 2;
    [SerializeField]
    private Transform pathObject;
    private PathLayout pathToVisualize;
    private float pathPoint;

    private void Start()
    {
        pathToVisualize = GetComponent<PathLayout>();
        if (pathToVisualize.unlocked)
        {
            StartCoroutine(VisualizePath());
        }
    }

    IEnumerator VisualizePath()
    {
        if (progress < 1)
        {
            while (progress < 1)
            {
                pathPoint += speed * (Time.deltaTime / visualizationSpeed);
                progress += pathPoint / 100;
                if(progress > 1)
                {
                    progress = 1;
                }
                Transform pathToVisualizeObject = Instantiate(pathObject) as Transform;
                Vector3 position = pathToVisualize.GetPoint(pathPoint);
                pathToVisualizeObject.transform.localPosition = position;
                pathToVisualizeObject.transform.LookAt(position + pathToVisualize.GetDirection(pathPoint));
                pathToVisualizeObject.transform.parent = gameObject.transform;
                yield return progress;
            }
        }
        yield return null;
    }
}
