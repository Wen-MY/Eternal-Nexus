using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform player;
    public float spawnDistance = 30f;
    public float despawnDistance = 80f;

    private ObjectPooler objectPooler;
    private GameObject currentObject;

    [System.Serializable]
    public class ObjectSpawnInfo
    {
        public GameObject prefab;
        public float spawnPercentage;
    }

    public ObjectSpawnInfo[] objectsToSpawn;

    private void Start()
    {
        objectPooler = FindObjectOfType<ObjectPooler>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Spawn object when the player is within the spawn distance
        if (currentObject == null && distanceToPlayer <= spawnDistance)
        {
            currentObject = ChooseObjectToSpawn();
            if (currentObject != null)
            {
                currentObject.transform.position = transform.position;
                currentObject.SetActive(true);
            }
        }

        // Despawn object when the player is beyond the despawn distance
        if (currentObject != null && distanceToPlayer > despawnDistance)
        {
            currentObject.SetActive(false);
            currentObject = null;
        }
    }

    private GameObject ChooseObjectToSpawn()
    {
        float totalPercentage = 0f;
        foreach (ObjectSpawnInfo objInfo in objectsToSpawn)
        {
            totalPercentage += objInfo.spawnPercentage;
        }

        float randomNumber = Random.Range(0f, totalPercentage);

        foreach (ObjectSpawnInfo objInfo in objectsToSpawn)
        {
            if (randomNumber <= objInfo.spawnPercentage)
            {
                GameObject pooledObject = objectPooler.GetPooledObject(objInfo.prefab);
                return pooledObject;
            }

            randomNumber -= objInfo.spawnPercentage;
        }

        return null;
    }
}
