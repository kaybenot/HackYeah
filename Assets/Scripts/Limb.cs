using UnityEngine;

public class Limb : MonoBehaviour
{
	[System.Serializable]
	private struct Bindings
	{
		private const string upperLimbName = "Upper Limb";
		private const string lowerLimbName = "Lower Limb";
		private const string endName = "End";
		private const string modelName = "Model";

		public Transform upper;
		public Transform upperModel;
		public Transform lower;
		public Transform lowerModel;
		public Transform end;
		public Transform endModel;

		public void Bind(Transform transform)
		{
			BindPart(ref upper, ref upperModel, upperLimbName, transform);
			BindPart(ref lower, ref lowerModel, lowerLimbName, upper);
			BindPart(ref end, ref endModel, endName, lower);
		}

		private void BindPart(ref Transform part, ref Transform model, string name, Transform start)
		{
			part = start.Find(name);
			if (part)
				model = part.Find(modelName);
		}
	}

	[SerializeField]
	private Bindings bindings;

	[ContextMenu("Try Bind")]
	private void Bind()
	{
		bindings.Bind(transform);
#if UNITY_EDITOR
		UnityEditor.EditorUtility.SetDirty(this);
#endif
	}
}
