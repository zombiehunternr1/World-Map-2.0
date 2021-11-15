using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDecoration : MonoBehaviour
{
    public int WaitBeforePathDecorating = 3;
    public float WaitDisplayNextPathDecoration;
    public int Frequency;
    public float Spacing = 1;
    public Transform[] DecorationItems;
    //[HideInInspector]
    public bool FirstTime = true;
    private PathLayout PathToDecorate;

    private void Start()
    {
        PathToDecorate = GetComponent<PathLayout>();
        if (PathToDecorate.Unlocked)
        {
            StartCoroutine(DecoratePath());
        }
    }

    private IEnumerator DecoratePath()
    {
        if (Frequency <= 0 || DecorationItems == null || DecorationItems.Length == 0)
        {
            yield return null;
        }
        float StepSize = Spacing / (Frequency * DecorationItems.Length);
        if (FirstTime)
        {
            yield return new WaitForSeconds(WaitBeforePathDecorating);
        }
        for (int p = 1, f = 0; f < Frequency; f++)
        {
            for (int i = 0; i < DecorationItems.Length; i++, p++)
            {
                Transform PathDecorationItem = Instantiate(DecorationItems[i]) as Transform;
                Vector3 Position = PathToDecorate.GetPoint(p * StepSize);
                PathDecorationItem.transform.localPosition = Position;
                PathDecorationItem.transform.LookAt(Position + PathToDecorate.GetDirection(p * StepSize));
                PathDecorationItem.transform.parent = gameObject.transform;
                if (FirstTime)
                {
                    yield return new WaitForSeconds(WaitDisplayNextPathDecoration);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
