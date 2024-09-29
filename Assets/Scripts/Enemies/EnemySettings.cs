using UnityEngine;

namespace Lemurs.Enemies
{
	[CreateAssetMenu]
	public class EnemySettings : ScriptableObject, IEnemyMovementSettings, IEnemyStatsSettings
	{
		[field: SerializeField]
		public float MovementSpeed { get; set; }
		
		[field: SerializeField]
		public float SlowdownSpeed { get; set; }

		[field: SerializeField]
		public int MaxHealth { get; set; }

		[field: SerializeField]
		public float DistanceFromTree { get; set; }

		[field: SerializeField]
		public LayerMask TreeLayers { get; set; }
	}
}
