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
        EntityManager manager = GameObject.FindFirstObjectByType<EntityManager>();
        manager.Print();
   }
   
   [MenuItem("Scriptable Objects/ Refresh in Game Manager")]
   public static void UpdateObjects() {
        EntityManager manager = GameObject.FindFirstObjectByType<EntityManager>();
        UpdateList<Blueprint>(manager);
        UpdateList<MaterialType>(manager);
        UpdateList<PartType>(manager);
        UpdateList<RawMaterial>(manager);

        UpdateList<Ability>(manager);
        UpdateList<AdventurerClass>(manager);
        UpdateList<Species>(manager);
        UpdateList<Location>(manager);
        UpdateList<Adventurer>(manager);
        UpdateList<AdventurerParty>(manager);
        UpdateList<Creature>(manager);
        UpdateList<Equipment>(manager);
        UpdateList<Quest>(manager);
        Debug.Log("Update Complete");  
   }
    [MenuItem("Scriptable Objects/ Update Textures")]
    public static void UpdateTextures(){
        EntityManager manager = GameObject.FindFirstObjectByType<EntityManager>();
        string[] textureFolder = new string[]{$"Assets/Textures/ScriptableObjects"};

        UpdateTexture<Blueprint>(manager.blueprints, textureFolder[0] + "/blueprints");
        UpdateTexture<RawMaterial>(manager.rawMaterials, textureFolder[0] + "/rawMaterials");
        UpdateTexture<PartType>(manager.partTypes, textureFolder[0] + "/partTypes");
        UpdateTexture<MaterialType>(manager.materialTypes, textureFolder[0] + "/materialTypes");
        UpdateTexture<Ability>(manager.abilitys, textureFolder[0] + "/abilities");
        UpdateTexture<AdventurerClass>(manager.adventurerClasses, textureFolder[0] + "/adventurerClasses");
        UpdateTexture<Species>(manager.species, textureFolder[0] + "/species");
        UpdateTexture<Location>(manager.locations, textureFolder[0] + "/locations");
        UpdateTexture<Adventurer>(manager.adventurers, textureFolder[0] + "/adventurers");
        UpdateTexture<AdventurerParty>(manager.adventurerParties, textureFolder[0] + "/parties");
        UpdateTexture<Creature>(manager.creatures, textureFolder[0] + "/creatures");
        UpdateTexture<Equipment>(manager.equipment, textureFolder[0] + "/equipment");
        UpdateTexture<Quest>(manager.quests, textureFolder[0] + "/quests");

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



    private static void UpdateList<T>(EntityManager m) where T : DataEntity{
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
        m.CreateLists<T>(list as List<T>);
    }
   }

   #endif
//}
