using UnityEngine;
using System.Collections;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;    // Arrow prefab
    public int arrowCount = 10;       // Number of arrows to spawn
    public float spawnInterval = 0.5f; // Spawn interval (seconds)
    public Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f); // Random area size
    public Transform targetArea;     // Target area (where arrows will stick)
    public float fireForce = 500f;    // Launch force
    public float randomRotationRange = 15f; // Randomness in arrow launch angle
    public float startDelay = 2f;     // Delay before spawning starts

    private void Start()
    {
        StartCoroutine(StartWithDelay());
    }

    private IEnumerator StartWithDelay()
    {
        // Wait for the initial delay
        yield return new WaitForSeconds(startDelay);
        // Start spawning arrows
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
        // Determine a random starting position
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            spawnAreaSize.y,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        // Instantiate the arrow at the calculated position
        GameObject arrow = Instantiate(arrowPrefab, randomPosition, Quaternion.identity);

        // Adjust the arrow's initial orientation to point towards the target
        arrow.transform.LookAt(targetArea);
        arrow.transform.rotation *= Quaternion.Euler(
            Random.Range(-randomRotationRange, randomRotationRange),
            Random.Range(-randomRotationRange, randomRotationRange),
            Random.Range(-randomRotationRange, randomRotationRange)
        );

        // Apply force to the arrow to launch it
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (targetArea.position - randomPosition).normalized;
            rb.AddForce(direction * fireForce, ForceMode.Impulse);
        }
    }
}
