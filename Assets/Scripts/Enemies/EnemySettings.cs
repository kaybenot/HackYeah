using UnityEngine;

namespace Lemurs.Enemies
{
	[CreateAssetMenu]
	public class EnemySettings : ScriptableObject, IEnemyMovementSettings, IEnemyStatsSettings
	{
		[field: SerializeField]
		public float MovementSpeed { get; set; }

		[field: SerializeField]
		public int MaxHealth { get; set; }
	}
}
