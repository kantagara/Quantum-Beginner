using UnityEditor;
using UnityEngine;

public class QuantumUtilities : EditorWindow
{
    [MenuItem("Quantum/Utilities/Replace Colliders With Quantum Colliders")]
    public static void ReplaceUnityCollidersWithQuantumColliders()
    {
        var colliders = FindObjectsOfType<Collider>();
        for (var i = colliders.Length - 1; i >= 0; i--)
        {
            var collider = colliders[i];

            if (((int)GameObjectUtility.GetStaticEditorFlags(collider.gameObject) & int.MaxValue) == 0) continue;

            switch (collider)
            {
                case SphereCollider sphereCollider:
                {
                    var quantumCollider = collider.gameObject.AddComponent<QuantumStaticSphereCollider3D>();
                    quantumCollider.SourceCollider = sphereCollider;
                    continue;
                }
                case BoxCollider boxCollider:
                {
                    var quantumCollider = collider.gameObject.AddComponent<QuantumStaticBoxCollider3D>();
                    quantumCollider.SourceCollider = boxCollider;
                    continue;
                }
                case MeshCollider meshCollider:
                {
                    var quantumCollider = collider.gameObject.AddComponent<QuantumStaticMeshCollider3D>();
                    quantumCollider.Mesh = meshCollider.sharedMesh;
                    DestroyImmediate(collider);
                    continue;
                }
            }
        }
    }
}