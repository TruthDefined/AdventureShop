using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;


public class Util_RefreshScriptableObjects

{
   #if UNITY_EDITOR
   [MenuItem("Scriptable Objects/ Print Lists")]
   public static void PrintLists(){
        CraftingManager manager = GameObject.FindObjectOfType<CraftingManager>();
        manager.Print();
   }
   
   [MenuItem("Scriptable Objects/ Refresh in Game Manager")]
   public static void UpdateObjects() {
        CraftingManager manager = GameObject.FindObjectOfType<CraftingManager>();
        UpdateList<Blueprint>(manager);
        UpdateList<MaterialType>(manager);
        UpdateList<PartType>(manager);
        UpdateList<RawMaterial>(manager);
        Debug.Log("Update Complete");  
   }
    [MenuItem("Scriptable Objects/ Update Textures")]
    public static void UpdateTextures(){
        CraftingManager manager = GameObject.FindObjectOfType<CraftingManager>();
        string[] textureFolder = new string[]{$"Assets/Textures/ScriptableObjects"};

        UpdateTexture<Blueprint>(manager.blueprints, textureFolder[0] + "/blueprints");
        UpdateTexture<RawMaterial>(manager.rawMaterials, textureFolder[0] + "/rawMaterials");
        UpdateTexture<PartType>(manager.partTypes, textureFolder[0] + "/partTypes");
        UpdateTexture<MaterialType>(manager.materialTypes, textureFolder[0] + "/materialTypes");

        // foreach (Blueprint data in manager.blueprints){
        //     string[] guid = AssetDatabase.FindAssets(data.name + assetType, textureFolder);
        //     if( guid.Length > 0 ){
        //         if(guid.Length == 1){
        //             string path = AssetDatabase.GUIDToAssetPath(guid[0]);
        //             data.icon = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        //         }
        //         else{
        //             Debug.Log("Found too many textures for " + data.name);
        //         }
                
        //     }
        //     else{
        //         Debug.Log("Failed to find teture for " + data.name);
        //     }
            
        // }

    }

    private static void UpdateTexture<T>(List<T> input, string folder) where T : DataEntity{
        string assetType = " t:sprite";
        string[] folders = new string[]{folder};
        foreach (T data in input){
            string[] guid = AssetDatabase.FindAssets(data.name + assetType, folders);
            if( guid.Length > 0 ){
                if(guid.Length == 1){
                    string path = AssetDatabase.GUIDToAssetPath(guid[0]);
                    data.icon = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                }
                else{
                    Debug.Log("Found too many textures for " + data.name);
                }
                
            }
            else{
                Debug.Log("Failed to find teture for " + data.name);
            }
        }
    }



    private static void UpdateList<T>(CraftingManager m) where T : DataEntity{
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
//}
