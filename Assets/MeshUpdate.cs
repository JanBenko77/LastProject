using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshUpdate : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private MeshCollider collidr;

    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        collidr.sharedMesh = null;
        collidr.sharedMesh = colliderMesh;
    }

    private void Start()
    {
        UpdateCollider();
    }
}
