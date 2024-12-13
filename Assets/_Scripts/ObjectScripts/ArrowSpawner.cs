using UnityEngine;
using System.Collections;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;   
    public int arrowCount = 10;      
    public float spawnInterval = 0.5f; 
    public Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f); 
    public Transform targetArea;    
    public float fireForce = 500f;    
    public float randomRotationRange = 15f; 
    public float startDelay = 2f;     

    private void Start()
    {
        StartCoroutine(StartWithDelay());
    }

    private IEnumerator StartWithDelay()
    {
       
        yield return new WaitForSeconds(startDelay);
       
        StartCoroutine(SpawnArrows());
    }

    private IEnumerator SpawnArrows()
    {
        for (int i = 0; i < arrowCount; i++)
        {
            SpawnArrow();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnArrow()
    {
      
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            spawnAreaSize.y,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        GameObject arrow = Instantiate(arrowPrefab, randomPosition, Quaternion.identity);

        arrow.transform.LookAt(targetArea);
        arrow.transform.rotation *= Quaternion.Euler(
            Random.Range(-randomRotationRange, randomRotationRange),
            Random.Range(-randomRotationRange, randomRotationRange),
            Random.Range(-randomRotationRange, randomRotationRange)
        );

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (targetArea.position - randomPosition).normalized;
            rb.AddForce(direction * fireForce, ForceMode.Impulse);
        }
    }
}
