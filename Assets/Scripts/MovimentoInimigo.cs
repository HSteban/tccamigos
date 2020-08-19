using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Inimigo))]
public class MovimentoInimigo : MonoBehaviour
{

	private Transform target;
	private int wavepointIndex = 0;

	public Inimigo enemy;

	void Start()
	{
		//enemy = GetComponent<Inimigo>();

		target = Waypoints.points[0];
	}

	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}

		enemy.speed = enemy.speed;
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}
		Transform antigo = Waypoints.points[wavepointIndex];
		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
		
			if (target.transform.position.x < antigo.transform.position.x)
			{
				enemy.transform.rotation = Quaternion.Euler(0, 270, 0);
			}
			if (target.transform.position.x > antigo.transform.position.x)
			{
				enemy.transform.rotation = Quaternion.Euler(0, 90, 0);
			}
	
			if (target.transform.position.z > antigo.transform.position.z)
			{
				enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			if (target.transform.position.z < antigo.transform.position.z)
			{
				enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
			}
		
	}

	void EndPath()
	{
		//PlayerStats.Lives--;
		//WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}

}