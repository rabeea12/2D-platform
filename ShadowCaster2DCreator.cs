using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

#if UNITY_EDITOR

[RequireComponent(typeof(CompositeCollider2D))]
public class ShadowCaster2DCreator : MonoBehaviour
{
    [SerializeField]
    private bool selfShadows = true;

    private CompositeCollider2D tilemapCollider;

    static readonly FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo shapePathHashField = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo shadowMeshField = typeof(ShadowCaster2D).GetField("m_ShadowMesh", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo forceRebuildField = typeof(ShadowCaster2D).GetField("m_ForceShadowMeshRebuild", BindingFlags.NonPublic | BindingFlags.Instance);

    public void Create()
    {
        DestroyOldShadowCasters();
        tilemapCollider = GetComponent<CompositeCollider2D>();

        Debug.Log($"CompositeCollider2D pathCount: {tilemapCollider.pathCount}");

        for (int i = 0; i < tilemapCollider.pathCount; i++)
        {
            int pointCount = tilemapCollider.GetPathPointCount(i);
            Debug.Log($"Path {i} point count: {pointCount}");

            if (pointCount == 0)
            {
                Debug.LogWarning($"Skipping path {i} because it has zero points.");
                continue;
            }

            Vector2[] pathVertices = new Vector2[pointCount];
            tilemapCollider.GetPath(i, pathVertices);
            GameObject shadowCaster = new GameObject("shadow_caster_" + i);
            shadowCaster.transform.parent = gameObject.transform;
            ShadowCaster2D shadowCasterComponent = shadowCaster.AddComponent<ShadowCaster2D>();
            shadowCasterComponent.selfShadows = this.selfShadows;

            Vector3[] testPath = new Vector3[pathVertices.Length];
            for (int j = 0; j < pathVertices.Length; j++)
            {
                testPath[j] = pathVertices[j];
            }

            try
            {
                if (shapePathField != null && shapePathHashField != null && shadowMeshField != null && forceRebuildField != null)
                {
                    shapePathField.SetValue(shadowCasterComponent, testPath);
                    shapePathHashField.SetValue(shadowCasterComponent, UnityEngine.Random.Range(int.MinValue, int.MaxValue));
                    shadowMeshField.SetValue(shadowCasterComponent, Activator.CreateInstance(shadowMeshField.FieldType));
                    forceRebuildField.SetValue(shadowCasterComponent, true);
                }
                else
                {
                    Debug.LogError("Reflection fields are null. Cannot set shadow caster properties.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Exception while setting shadow caster properties for path {i}: {ex.Message}");
            }
        }
    }
    public void DestroyOldShadowCasters()
    {
        var tempList = transform.Cast<Transform>().ToList();
        foreach (var child in tempList)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}

[CustomEditor(typeof(ShadowCaster2DCreator))]
public class ShadowCaster2DTileMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            var creator = (ShadowCaster2DCreator)target;
            creator.Create();
        }

        if (GUILayout.Button("Remove Shadows"))
        {
            var creator = (ShadowCaster2DCreator)target;
            creator.DestroyOldShadowCasters();
        }
        EditorGUILayout.EndHorizontal();
    }

}

#endif
