using UnityEngine;
using UnityEngine.SceneManagement;

public class DebrisSpawner : MonoBehaviour
{
	private const float SMALL_TIMER = 0.75f;

	private const float MEDIUM_TIMER = 1f;

	private const float XMEDIUM_TIMER = 1.3f;

	public GameObject smallDebrisPrefab;

	public GameObject mediumDebrisPrefab;

	public GameObject xmediumDebrisPrefab;

	public GameObject largeDebrisPrefab;

	public GameObject xlargeDebrisPrefab;

	public Camera mainCamera;

	private float xMin;

	private float xMax;

	private float lastSmallDebrisSpawn;

	private float lastMediumDebrisSpawn;

	private float lastXMediumDebrisSpawn;

	private void Start()
	{
		Vector3 position = new Vector3(0f, 0f, 0f);
		Vector3 position2 = new Vector3(1f, 1f, 0f);
		xMin = mainCamera.ViewportToWorldPoint(position2).x;
		xMax = mainCamera.ViewportToWorldPoint(position).x;
	}

	private void Update()
	{
		Vector3 position = new Vector3(Random.Range(xMin, xMax), base.transform.position.y, -0.9f);
		if (ChanceToSpawnSmallDebris() && Time.time > lastSmallDebrisSpawn + 0.75f)
		{
			lastSmallDebrisSpawn = Time.time;
			Object.Instantiate(smallDebrisPrefab, position, Quaternion.identity);
		}
		if (ChanceToSpawnMediumDebris() && Time.time > lastMediumDebrisSpawn + 1f)
		{
			lastMediumDebrisSpawn = Time.time;
			Object.Instantiate(mediumDebrisPrefab, position, Quaternion.identity);
		}
		if (ChanceToSpawnMediumDebris() && Time.time > lastXMediumDebrisSpawn + 1.3f)
		{
			lastXMediumDebrisSpawn = Time.time;
			Object.Instantiate(xmediumDebrisPrefab, position, Quaternion.identity);
		}
		if (ChanceToSpawnLargeDebris())
		{
			Object.Instantiate(largeDebrisPrefab, position, Quaternion.identity);
		}
		if (SceneManager.GetActiveScene().name.Equals("P2") && ChanceToSpawnXLargeDebris())
		{
			Object.Instantiate(xlargeDebrisPrefab, position, Quaternion.identity);
		}
	}

	private bool ChanceToSpawnSmallDebris()
	{
		if (Random.Range(0, 100) == 1)
		{
			return true;
		}
		return false;
	}

	private bool ChanceToSpawnMediumDebris()
	{
		if (Random.Range(0, 100) == 1)
		{
			return true;
		}
		return false;
	}

	private bool ChanceToSpawnXMediumDebris()
	{
		if (Random.Range(0, 100) == 1)
		{
			return true;
		}
		return false;
	}

	private bool ChanceToSpawnLargeDebris()
	{
		if (Random.Range(0, 500) == 1)
		{
			return true;
		}
		return false;
	}

	private bool ChanceToSpawnXLargeDebris()
	{
		if (Random.Range(0, 600) == 1)
		{
			return true;
		}
		return false;
	}
}
