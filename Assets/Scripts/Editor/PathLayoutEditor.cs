using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathLayout))]
public class PathLayoutEditor : Editor {

	private const int lineSteps = 10;
	private const float directionScale = 0.5f;

	private PathLayout curve;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private void OnSceneGUI () {
		curve = target as PathLayout;
		handleTransform = curve.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;
		
		Vector3 p0 = ShowPoint(0);
		Vector3 p1 = ShowPoint(1);
		Vector3 p2 = ShowPoint(2);
		Vector3 p3 = ShowPoint(3);
		
		Handles.color = Color.gray;	
		Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
	}

	private Vector3 ShowPoint (int index) {
		Vector3 point = handleTransform.TransformPoint(curve.points[index]);
		EditorGUI.BeginChangeCheck();
		point = Handles.DoPositionHandle(point, handleRotation);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(curve, "Move Point");
			EditorUtility.SetDirty(curve);
			curve.points[index] = handleTransform.InverseTransformPoint(point);
		}
		return point;
	}
}