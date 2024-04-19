using UnityEngine;
using UnityEditor;

public class CoralReefGenerator : EditorWindow
{
    public GameObject coralPrefab;
    public int numberOfCorals = 50;
    public float areaWidth = 50f;
    public float areaDepth = 50f;
    private Terrain terrain; // Reference to the terrain

    [MenuItem("Tools/Coral Reef Generator")]
    static void Init()
    {
        CoralReefGenerator window = (CoralReefGenerator)EditorWindow.GetWindow(typeof(CoralReefGenerator));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Place Corals on Terrain", EditorStyles.boldLabel);
        coralPrefab = (GameObject)EditorGUILayout.ObjectField("Coral Prefab", coralPrefab, typeof(GameObject), false);
        numberOfCorals = EditorGUILayout.IntField("Number of Corals", numberOfCorals);
        areaWidth = EditorGUILayout.FloatField("Area Width", areaWidth);
        areaDepth = EditorGUILayout.FloatField("Area Depth", areaDepth);
        terrain = (Terrain)EditorGUILayout.ObjectField("Terrain", terrain, typeof(Terrain), true);

        if (GUILayout.Button("Generate"))
        {
            GenerateCorals();
        }
    }

    void GenerateCorals()
{
    if (coralPrefab == null || terrain == null)
    {
        EditorUtility.DisplayDialog("Error", "Please assign a coral prefab and a terrain.", "OK");
        return;
    }

    TerrainData terrainData = terrain.terrainData;
    Vector3 terrainSize = terrainData.size;
    Vector3 terrainPosition = terrain.transform.position;

    for (int i = 0; i < numberOfCorals; i++)
    {
        float x = Random.Range(0, terrainSize.x) + terrainPosition.x;
        float z = Random.Range(0, terrainSize.z) + terrainPosition.z;
        float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrainPosition.y;
        
        Vector3 position = new Vector3(x, y, z);
        GameObject coralInstance = Instantiate(coralPrefab, position, Quaternion.identity);
        Undo.RegisterCreatedObjectUndo(coralInstance, "Create " + coralInstance.name);

        // Set scale, I dont think this works, so ignore
        coralInstance.transform.localScale = new Vector3(5f, 5f, 5f);
    }
}
}
