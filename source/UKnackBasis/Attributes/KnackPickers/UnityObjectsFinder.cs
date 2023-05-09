#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UKnack.Attributes.KnackPickers
{
    public static class UnityObjectsFinder
    {
        public enum ObjectLocation
        {
            Unknown,
            SceneGameobjects,
            SceneComponents,
            Assets
        }

        private static readonly List<GameObject> s_tempRootObjects = new List<GameObject>();
        //private static readonly List<UnityEngine.Object> s_tempBuffer = new List<UnityEngine.Object>();

        public static IEnumerable<UnityEngine.Object> LookUpType(
            System.Type type,
            GameObject owner,
            Predicate<UnityEngine.Object> filter,
            Action<ObjectLocation> onFirstItem = null)
        {
            //Debug.Log($"{owner} {type} {type == typeof(UnityEngine.Object)} {type == typeof(UnityEngine.GameObject) } {typeof(Component).IsAssignableFrom(type)}");
            IEnumerator<UnityEngine.Object> enumerator;
            if (owner != null && 
                (type == typeof(UnityEngine.Object) || 
                type == typeof(UnityEngine.GameObject) || 
                typeof(Component).IsAssignableFrom(type)))
            {
                //Debug.Log("Trying to LookUp in scene");
                enumerator = LookUpInScene(type, owner, filter, s_tempRootObjects, onFirstItem);
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    //if (filter(current.Object))
                        yield return current;
                }
            }
            enumerator = LookupAssetDatabase(type, filter, 300, onFirstItem);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                //if (filter(current.Object))
                    yield return current;
            }
        }

        //[Obsolete("Not tested")]
        private static IEnumerator<UnityEngine.Object> LookUpInScene(
            System.Type type,
            GameObject owner,
            Predicate<UnityEngine.Object> filter,
            List<GameObject> rootBuffer, 
            Action<ObjectLocation> onFirstItem = null)
        {
            if (owner == null)
                yield break;
            rootBuffer.Clear();

            Scene scene = owner.scene;
            scene.GetRootGameObjects(rootBuffer);

            int successulItemsCounter;
            IEnumerator<UnityEngine.Object> enumerator;
            if (type == typeof(UnityEngine.Object) || type == typeof(UnityEngine.GameObject))
            {
               // Debug.Log("Trying to LookUp for Gameobjects in scene");
                successulItemsCounter = 0;
                enumerator = LookupGameobjects(rootBuffer);
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    if (filter(current))
                    {
                        if (successulItemsCounter == 0 && onFirstItem != null)
                            onFirstItem(ObjectLocation.SceneGameobjects);
                        successulItemsCounter++;
                        yield return current;
                    }

                }
            } 
            if (type == typeof(UnityEngine.Object) || typeof(Component).IsAssignableFrom(type)) // || type.IsInterface || type == typeof(Component) || )
            {
                //Debug.Log("Trying to LookUp for Components in scene");
                successulItemsCounter = 0;
                Type componentType = typeof(Component).IsAssignableFrom(type) ? type : typeof(Component);
                enumerator = LookupComponents(componentType, rootBuffer);
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    if (filter(current))
                    {
                        if (successulItemsCounter == 0 && onFirstItem != null)
                            onFirstItem(ObjectLocation.SceneComponents);
                        successulItemsCounter++;
                        yield return current;
                    }
                }
            }

            rootBuffer.Clear();
        }

        //[Obsolete("Not tested")]
        // if unloadAfterUnusedCount less than 1, EditorUtility.UnloadUnusedAssetsImmediate() won't be called
        private static IEnumerator<UnityEngine.Object> LookupAssetDatabase(
            System.Type type, Predicate<UnityEngine.Object> filter, 
            int unloadAfterUnusedCount = 300,
            Action<ObjectLocation> onFirstItem = null)
        {
            int unwantedAssetsCount = 0;
            int successulItemsCounter = 0;
            var guids = AssetDatabase.FindAssets($"t:{type.Name}");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);


                if (path.EndsWith(".unity"))
                {
                    var asset = AssetDatabase.LoadAssetAtPath(path, type);
                    if (asset == null)
                        continue;

                    if (filter(asset))
                    {
                        if (successulItemsCounter == 0 && onFirstItem != null)
                            onFirstItem(ObjectLocation.Assets);
                        successulItemsCounter++;
                        yield return asset;
                    }
                    else
                    {
                        unwantedAssetsCount++;
                    }
                }
                else
                {
                    foreach (UnityEngine.Object asset in AssetDatabase.LoadAllAssetsAtPath(path))
                    {
                        if(asset == null)
                            continue;
                        AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out var loadedGuid, out long _);

                        if (guid == loadedGuid && filter(asset))
                        {
                            if (successulItemsCounter == 0 && onFirstItem != null)
                                onFirstItem(ObjectLocation.Assets);
                            successulItemsCounter++;
                            yield return asset;//, Type = ObjectSourceType.Asset };
                        }
                        else
                        {
                            unwantedAssetsCount++;
                        }
                    }
                }
                if(unloadAfterUnusedCount>0 && unwantedAssetsCount > unloadAfterUnusedCount)
                {
                    EditorUtility.UnloadUnusedAssetsImmediate();
                    unwantedAssetsCount = 0;
                }
            }
            if (unloadAfterUnusedCount > 0 && unwantedAssetsCount > 0)
                EditorUtility.UnloadUnusedAssetsImmediate();
        }

        private static IEnumerator<UnityEngine.Object> LookupComponents(System.Type componentType, List<GameObject> where)
        {
            foreach (var go in where)
            {
                foreach (var component in go.GetComponentsInChildren(componentType, true))
                {
                    yield return component;//, Type = ObjectSourceType.Scene };
                }
            }
        }
        private static IEnumerator<UnityEngine.Object> LookupGameobjects(List<GameObject> where)
        {
            foreach (var rootGameObject in where)
            {
                yield return rootGameObject;

                var children = GetAllChildGameobjects(rootGameObject.transform);
                while (children.MoveNext())
                    yield return children.Current;
            }
        }
        private static IEnumerator<GameObject> GetAllChildGameobjects(Transform root)
        {
            foreach (Transform child in root.transform)
            {
                yield return child.gameObject;
                var childrenEnumerator = GetAllChildGameobjects(child);
                while (childrenEnumerator.MoveNext())
                    yield return childrenEnumerator.Current;
            }
        }
    }
}
#endif