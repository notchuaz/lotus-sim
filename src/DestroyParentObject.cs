using UnityEngine;

public class DestroyParentObject : MonoBehaviour
{
	public void DestroyParent()
	{
		Object.Destroy(base.gameObject.transform.parent.gameObject);
	}
}
