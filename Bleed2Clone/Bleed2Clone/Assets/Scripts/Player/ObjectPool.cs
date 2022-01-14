using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    public int amountToPool = 20;

    internal List<GameObject> pooledObjects = new List<GameObject>();

    public void Init()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(BulletPrefab, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public void Init(GameObject bulletPrefab)
    {
        BulletPrefab = bulletPrefab;
        Init();
    }
    public GameObject GetPooledObjects()
    {
        return pooledObjects.Where(x => !x.activeInHierarchy).First<GameObject>();
    }
}
