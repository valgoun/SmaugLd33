﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor {

    Grid grid;

    private int oldIndex;

    private bool looping;

    void OnEnable()
    {
        oldIndex = 0;
        grid = (Grid)target;
    }

    [MenuItem("Assets/Create/TileSet")]
    static void CreateTileSet()
    {
        var asset = ScriptableObject.CreateInstance<TileSet>();
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path)!= "")
        {
            path = path.Replace(Path.GetFileName(path), "");
        }
        else
        {
            path += "/";
        }

        var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "TileSet.asset");
        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
        asset.hideFlags = HideFlags.DontSave;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        grid.width = createSlider("Width", grid.width);
        grid.height = createSlider("Height", grid.height);

        grid.color = EditorGUILayout.ColorField("grid Color", grid.color, GUILayout.Width(Screen.width-19));

        //Tile Prefab
        EditorGUI.BeginChangeCheck();
        var newTilePrefab = (Transform)EditorGUILayout.ObjectField("Tile Prefab", grid.tilePrefab, typeof(Transform), false);
        if (EditorGUI.EndChangeCheck()){
            grid.tilePrefab = newTilePrefab;
            Undo.RecordObject(target, "Grid Changed");
        }

        //TileMap
        EditorGUI.BeginChangeCheck();
        var newTileSet = (TileSet)EditorGUILayout.ObjectField("Tile Set", grid.tileSet, typeof(TileSet), false);
        if (EditorGUI.EndChangeCheck()){
            grid.tileSet = newTileSet;
            Undo.RecordObject(target, "Grid Changed");
        }

        if (grid.tileSet != null){
            EditorGUI.BeginChangeCheck();
            var names = new string[grid.tileSet.prefabs.Length];
            var values = new int[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = grid.tileSet.prefabs[i] != null ? grid.tileSet.prefabs[i].name : "";
                values[i] = i;
            }

            var index = EditorGUILayout.IntPopup("Select Tile", oldIndex, names, values);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Grid Changed");
                if (oldIndex != index)
                {
                    oldIndex = index;
                    grid.tilePrefab = grid.tileSet.prefabs[index];

                    float width = grid.tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
                    float height = grid.tilePrefab.GetComponent<SpriteRenderer>().bounds.size.y;

                    grid.width = width;
                    grid.height = height;
                }
            }
        }

    }

    private float createSlider(string labelName, float sliderPosition)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid" + labelName);
        sliderPosition = EditorGUILayout.Slider(sliderPosition, 1f, 100f, null);
        GUILayout.EndHorizontal();

        return sliderPosition;
    }

    void OnSceneGUI()
    {
        int controlId = GUIUtility.GetControlID(FocusType.Passive);
        Event e = Event.current;
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = ray.origin;

       
        if (e.isMouse && e.type == EventType.MouseDown && e.button == 0) 
        {    
                GUIUtility.hotControl = controlId;
                e.Use();

                GameObject gameObject;
                Transform prefab = grid.tilePrefab;

                if (prefab)
                {
                    Undo.IncrementCurrentGroup();

                    Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2.0f,
                                                    Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2.0f, 0.0f);

                    /*
                    if (GetTransformFromPosition(aligned) != null) return;
                    gameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab.gameObject);
                    gameObject.transform.position = aligned;
                    gameObject.transform.parent = grid.transform;
                    Undo.RegisterCreatedObjectUndo(gameObject, "Create" + gameObject.name);

                    */

                    // /*
                    Transform transform = GetTransformFromPosition(aligned);
                    if (transform != prefab)
                    {

                        if (transform != null)
                        {
                            DestroyImmediate(transform.gameObject);
                        }
                        gameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab.gameObject);
                        gameObject.transform.position = aligned;
                        gameObject.transform.parent = grid.transform;
                        Undo.RegisterCreatedObjectUndo(gameObject, "Create" + gameObject.name);
                    }
                    // */
            }

        }

        if (e.isMouse && e.type == EventType.MouseDown && e.button == 1)
        {
            GUIUtility.hotControl = controlId;
            e.Use();
            Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2.0f,
                                                Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2.0f, 0.0f);
            Transform transform = GetTransformFromPosition(aligned);
            if (transform != null)
            {
                DestroyImmediate(transform.gameObject);
            }
        }

        if (e.isMouse && e.type == EventType.MouseUp)
        {
            GUIUtility.hotControl = 0;
        }
    }

    Transform GetTransformFromPosition(Vector3 aligned)
    {
        int i = 0;
        while (i < grid.transform.childCount)
        {
            Transform transform = grid.transform.GetChild(i);
            if (transform.position == aligned)
            {
                return transform;
            }

            i++;
        }

        return null;
    }


    
}
