using UnityEngine;
namespace ZGGame
{
	public class VectorUtil
	{
		public static Vector3 pointToWorld(Vector2 vec2,Transform target)
		{
			Vector3 vecp = vec2;
			Vector3 targetPos = Camera.main.WorldToScreenPoint(target.position);
			vecp.z = targetPos.z;
			Vector3 vec3 = Camera.main.ScreenToWorldPoint(vecp);
			return vec3;
		}
	}
}

