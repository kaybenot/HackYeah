using UnityEngine;

public class SetParent : MonoBehaviour
{
	[SerializeField]
	private Transform parent;

	[ContextMenu("Set Parent")]
	public void Set() => transform.SetParent(parent);

	private void Start() => Set();
}
