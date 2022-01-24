using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathLayout))]
public class PathLayoutEditor : Editor {

	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;
	private int selectedIndex = -1;

	private PathLayout curve;
	private Transform handleTransform;
	private Quaternion handleRotation;

	public override void OnInspectorGUI()
    {
		DrawDefaultInspector();
		curve = target as PathLayout;
		if(GUILayout.Button("Add Curve"))
        {
			Undo.RecordObject(curve, "Add Curve");
			curve.AddCurve();
			EditorUtility.SetDirty(curve);
        }
		if(curve.points.Length > 4)
        {
			if (GUILayout.Button("Remove Last Curve"))
			{
				Undo.RecordObject(curve, "Remove Last Curve");
				curve.RemoveLastCurve();
				EditorUtility.SetDirty(curve);
			}
		}
	}

	private void OnSceneGUI () {
		curve = target as PathLayout;
		handleTransform = curve.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;
		
		Vector3 p0 = ShowPoint(0);
		for(int i = 1; i < curve.points.Length; i += 3)
        {
			Vector3 p1 = ShowPoint(i);
			Vector3 p2 = ShowPoint(i + 1);
			Vector3 p3 = ShowPoint(i + 2);
			Handles.color = Color.gray;
			Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
			p0 = p3;
		}	
	}

	private Vector3 ShowPoint (int index) {
		Vector3 point = handleTransform.TransformPoint(curve.points[index]);
		Handles.color = Color.white;
		float size = HandleUtility.GetHandleSize(point);
		if(Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotHandleCap))
        {
			selectedIndex = index;
        }
		if(selectedIndex == index)
        {
			EditorGUI.BeginChangeCheck();
			point = Handles.DoPositionHandle(point, handleRotation);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(curve, "Move Point");
				EditorUtility.SetDirty(curve);
				curve.points[index] = handleTransform.InverseTransformPoint(point);
			}
		}
		return point;
	}
}