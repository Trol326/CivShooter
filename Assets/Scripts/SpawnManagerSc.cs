using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerSc : MonoBehaviour
{
    [Header("Boxes")]
    [SerializeField]GameObject weaponBoxPrefub;
    [SerializeField]float spawnRadius;
    [SerializeField]List<GameObject> boxSpawnPlaces = new List<GameObject>();
    GameObject currentSpawnPlace;
    [Header("Enemies")]
    [SerializeField]List<GameObject> EnemySpawners = new List<GameObject>();

    // _____________________________________________________________
    private static SpawnManagerSc _instance;
    public static SpawnManagerSc Instance { get => _instance;}
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    // _____________________________________________________________
    public void SpawnNextBox()
    {
        ChooseRandomPlace();
        Vector3 spawnPos = currentSpawnPlace.transform.position + Random.insideUnitSphere*spawnRadius;
        spawnPos.y = 1;
        GameObject.Instantiate(weaponBoxPrefub, spawnPos, currentSpawnPlace.transform.rotation);

    }
    
    void ChooseRandomPlace()
    {
        if(boxSpawnPlaces.Count <= 1)return;
        int nextPos = (int)(Random.value * boxSpawnPlaces.Count);
        if(nextPos > (boxSpawnPlaces.Count-1))nextPos--;
        if(boxSpawnPlaces[nextPos] == currentSpawnPlace)
        {
            ChooseRandomPlace();
            return;
        }
        currentSpawnPlace = boxSpawnPlaces[nextPos];
    }



    // ______________________________________________________________
    #if UNITY_EDITOR
    enum DebugDisplayBoxSpawnRadius
    {
        Never,
        OnSelected,
        Always        
    }

    [Header("Debug")]
    [SerializeField]DebugDisplayBoxSpawnRadius DrawBoxSpawnRadius;    
    
    void OnDrawGizmos()
    {
        if(DrawBoxSpawnRadius == DebugDisplayBoxSpawnRadius.Always)
        {
        foreach(var place in boxSpawnPlaces)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(place.transform.position, spawnRadius);
        }
        }
    }
    void OnDrawGizmosSelected()
    {
        if(DrawBoxSpawnRadius == DebugDisplayBoxSpawnRadius.OnSelected)
        {
            foreach(var place in boxSpawnPlaces)
            {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(place.transform.position, spawnRadius);
            }
        }
    }
    #endif
    // ______________________________________________________________
}
