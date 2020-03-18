using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public enum ProjectileType
    {
        Basic,
        Explosive,
        Rocket
    }

    [System.Serializable]
    public struct Pool
    {
        public ProjectileType projType;
        public GameObject projPrefab;
        public Transform poolHolder;
        public int poolSize;
        public int scaleModifier;
    }


    public List<Pool> ListOfPools;

    public Dictionary<ProjectileType, Queue<GameObject>> PoolDictionary;

    #region Singleton

    public static BulletPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        PoolDictionary = new Dictionary<ProjectileType, Queue<GameObject>>();

        foreach(var pool in ListOfPools)
        {
            Queue<GameObject> bulletPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.projPrefab, pool.poolHolder);

                if(pool.scaleModifier > 0)
                {
                    obj.transform.localScale *= pool.scaleModifier;
                }

                obj.SetActive(false);

                bulletPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.projType, bulletPool);
        }
    }


    #region Spawning Functions

    public GameObject SpawnToPosRot(ProjectileType key, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (!PoolDictionary.ContainsKey(key))
        {
            Debug.Log("Dictionary does not contain this key: " + key.ToString());
            return null;
        }

        GameObject objToSpawn = null;

        for (int i = 0; i < PoolDictionary[key].Count; i++)
        {
            objToSpawn = PoolDictionary[key].Dequeue();

            if (objToSpawn.activeSelf)
            {
                PoolDictionary[key].Enqueue(objToSpawn);
                objToSpawn = null;
                continue;
            }
            else if (!objToSpawn.activeSelf)
            {
                objToSpawn.SetActive(true);
                objToSpawn.transform.position = spawnPosition;
                objToSpawn.transform.rotation = spawnRotation;
                PoolDictionary[key].Enqueue(objToSpawn);
                break;
            }
        }

        if (objToSpawn == null)
        {
            Debug.Log("No Projectiles available to Spawn");
        }

        return objToSpawn;
    }

    public GameObject SpawnToPos(ProjectileType key, Vector3 spawnPosition)
    {
        if (!PoolDictionary.ContainsKey(key))
        {
            Debug.Log("Dictionary does not contain this key: " + key.ToString());
            return null;
        }

        GameObject objToSpawn = null;

        for (int i = 0; i < PoolDictionary[key].Count; i++)
        {
            objToSpawn = PoolDictionary[key].Dequeue();

            if (objToSpawn.activeSelf)
            {
                PoolDictionary[key].Enqueue(objToSpawn);
                objToSpawn = null;
                continue;
            }
            else if (!objToSpawn.activeSelf)
            {
                objToSpawn.SetActive(true);
                objToSpawn.transform.position = spawnPosition;
                PoolDictionary[key].Enqueue(objToSpawn);
                break;
            }
        }

        if (objToSpawn == null)
        {
            Debug.Log("No Projectiles available to Spawn");
        }

        return objToSpawn;
    }

    public GameObject SpawnObj(ProjectileType key)
    {
        if (!PoolDictionary.ContainsKey(key))
        {
            Debug.Log("Dictionary does not contain this key: " + key.ToString());
            return null;
        }

        GameObject objToSpawn = null;

        for (int i = 0; i < PoolDictionary[key].Count; i++)
        {
            objToSpawn = PoolDictionary[key].Dequeue();

            if (objToSpawn.activeSelf)
            {
                PoolDictionary[key].Enqueue(objToSpawn);
                objToSpawn = null;
                continue;
            }
            else if (!objToSpawn.activeSelf)
            {
                objToSpawn.SetActive(true);
                PoolDictionary[key].Enqueue(objToSpawn);
                break;
            }
        }

        if(objToSpawn == null)
        {
            Debug.Log("No Projectiles available to Spawn");
        }

        return objToSpawn;
    }


    #endregion
}
