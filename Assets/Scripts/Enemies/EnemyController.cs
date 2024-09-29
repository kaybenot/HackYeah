using UnityEngine;
using UnityEngine.Animations.Rigging;

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
		[SerializeField]
		private RigBuilder rigBuilder;
		[SerializeField]
		private Animator animator;

		private void OnEnable()
		{
			movement.Init(settings);
			stats.Init(settings);

			rigBuilder.Build();
			animator.Rebind();
			animator.Update(0);
		}
	}
}
