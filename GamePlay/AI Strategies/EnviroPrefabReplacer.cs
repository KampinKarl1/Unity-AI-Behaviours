using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroPrefabReplacer : MonoBehaviour
{
    [System.Serializable]
    class ObjectNameAndReplacement 
    {
        public string name = "Enter the name of the object you want to replace";
        public GameObject replacementPrefab = null;
    }

#if UNITY_EDITOR
    [SerializeField, Multiline(6)]
    private string attention = "Hi, this class will help you replace stuff that is a child of this object. Enter the name of the object you " +
        "want to replace and the replacement object you want to instantiate.";
#endif

    [SerializeField] private List<ObjectNameAndReplacement> replaceableObjects = new List<ObjectNameAndReplacement>();

    [SerializeField] private bool doReplacement = false;

    void Start()
    {
        if (doReplacement)
        {
            doReplacement = false;

            Dictionary<string, List<Transform>> namesAndCounts = new Dictionary<string, List<Transform>>();


            foreach (var child in transform.GetComponentsInChildren<Transform>())
            {
                if (!namesAndCounts.ContainsKey(child.name))
                    namesAndCounts.Add(child.name, new List<Transform>());
                namesAndCounts[child.name].Add(child);
            }

            List<string> copyOfNames = new List<string>(replaceableObjects.Count);
            foreach (var item in replaceableObjects)
            {
                copyOfNames.Add(item.name);
            }

            foreach (var kvp in namesAndCounts)
                for (int i = 0; i < copyOfNames.Count; i++)
                {
                    if (kvp.Key == copyOfNames[i])
                    {
                        print("There are " + kvp.Value.Count + " of " + kvp.Key.ToString());
                        break;
                    }
                }

            int cur = 0;
            foreach (var kvp in namesAndCounts)
            {
                if (!copyOfNames.Contains(kvp.Key.ToString()))
                    continue;

                List<Transform> curAssetList = kvp.Value;

                for (int i = 0; i < curAssetList.Count; i++)
                {
                    Transform t = curAssetList[i];

                    Transform rep = new GameObject(replaceableObjects[cur].name).transform;
                    rep.SetPositionAndRotation(t.position, t.rotation);
                    rep.localScale = t.localScale;
                    rep.parent = t.parent;

                    if (replaceableObjects[cur].replacementPrefab != null)
                    {
                        var o = Instantiate(replaceableObjects[cur].replacementPrefab, rep.position, rep.rotation, rep);
                    }

                    Destroy(curAssetList[i].gameObject);
                }
                cur++;
            }
        }
    }

}
