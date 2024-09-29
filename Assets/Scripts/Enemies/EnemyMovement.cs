using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Lemurs.Enemies
{
    public interface IEnemyMovementSettings
    {
	    float SlowdownSpeed { get;}
		float MovementSpeed { get; }
		float DistanceFromTree { get; }
		LayerMask TreeLayers { get; }	
    }

    public class EnemyMovement : MonoBehaviour
	{
		[SerializeField]
		private Vector3 raycastOffset;

		private IEnemyMovementSettings _settings;

		private Ray snapRay;
		private bool slowed;
		
		public void Init(IEnemyMovementSettings settings)
		{
			_settings = settings;
		}

		private void FixedUpdate()
		{
			var position = transform.position;

			var dt = Time.deltaTime;
			var moveSpeed = slowed ? _settings.SlowdownSpeed : _settings.MovementSpeed;
			position.y += dt * moveSpeed;

			var snapDistance = _settings.DistanceFromTree;
			var detectionDistance = snapDistance + 1;
			snapRay = new Ray(
				position + raycastOffset - detectionDistance * transform.forward, 
				transform.forward);

			transform.position = position;	
		}

		public void Slowdown()
		{
			slowed = true;
			StartCoroutine(IStopSlowdown());
		}

		public IEnumerator IStopSlowdown()
		{
			yield return new WaitForSeconds(1f);
			slowed = false;
		}

        private void OnDrawGizmosSelected()
        {
			if (Application.isPlaying == false) 
				return;
            
			float snapDistance = _settings.DistanceFromTree;
            float detectionDistance = snapDistance + 1;
            Gizmos.color = Color.red;
			Gizmos.DrawRay(snapRay.origin, 2 * detectionDistance * snapRay.direction);
			Gizmos.DrawSphere(snapRay.origin, 0.1f);
        }
    }
}
