using UnityEngine;

namespace Lemurs.Enemies
{
	public class EnemyController : MonoBehaviour
	{
		[SerializeField]
		private EnemySettings settings;
		[SerializeField]
		private EnemyMovement movement;
		[SerializeField]
		private EnemyStats stats;

		private void OnEnable()
		{
			movement.Init(settings);
			stats.Init(settings);
		}
	}
}
