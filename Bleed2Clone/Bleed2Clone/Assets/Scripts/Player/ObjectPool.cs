using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ObjectPool : MonoBehaviour
{
    List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] int amountToPool = 20;

    public GameObject BulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public void Init()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(BulletPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public GameObject GetPooledObjects()
    {
        return pooledObjects.Where(x => x.activeInHierarchy).First<GameObject>();

        //for (int i = 0; i < pooledObjects.Count; i++)
        //{
        //    if (!pooledObjects[i].activeInHierarchy)
        //    {
        //        return pooledObjects[i];
        //    }
        //}

        //return null;
    }
}
