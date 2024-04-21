using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class CombineStaticMeshes : MonoBehaviour
{
    void Start()
    {
        Combine();
    }

    void Combine()
    {
        // Find all MeshFilter components in children of this GameObject
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>(); 
        Material[] materials = new Material[meshRenderers.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            materials[i] = meshRenderers[i].sharedMaterial; 

            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        
        // Set up the Mesh Collider with the new combined mesh
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = transform.GetComponent<MeshFilter>().mesh;
        
        transform.GetComponent<MeshRenderer>().materials = materials;

        transform.gameObject.SetActive(true);

        foreach (var meshFilter in meshFilters)
        {
            Destroy(meshFilter.gameObject);
        }
    }
}
