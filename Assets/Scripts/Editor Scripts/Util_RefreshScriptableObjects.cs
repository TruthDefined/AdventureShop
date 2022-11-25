using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Util_RefreshScriptableObjects
{
   #if UNITY_EDITOR
   [MenuItem("Scriptable Objects/ Print Lists")]
   public static void PrintLists(){
        CraftingManager manager = Component.FindObjectOfType<CraftingManager>();
        manager.Print();
   }
   
   [MenuItem("Scriptable Objects/ Refresh in Game Manager")]
   public static void UpdateObjects() {
    {
        CraftingManager manager = Component.FindObjectOfType<CraftingManager>();
        UpdateList<Blueprint>(manager);
        UpdateList<MaterialType>(manager);
        UpdateList<PartType>(manager);
        UpdateList<RawMaterial>(manager);
        Debug.Log("Update Complete");

    }

    void UpdateList<T>(CraftingManager m) where T : UnityEngine.Object{
        List<T> list = new List<T>();
        var searchFolder = new string[]{$"Assets/ScriptableObjects"};
        string[] guid = AssetDatabase.FindAssets("", searchFolder);
        //Debug.Log(guid[0]);
        foreach (string id in guid){
            var path = AssetDatabase.GUIDToAssetPath(id);
            var asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if(asset){
                list.Add(asset);
                //Debug.Log(asset.name);
            }
        }
        m.UpdateLists<T>(list as List<T>);
    }
   }
   #endif
}
