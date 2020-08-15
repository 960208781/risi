using UnityEngine;
using System.Collections;

using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Box : MonoBehaviour
{
    private Mesh mesh;
    public Vector2 size;
    public Vector2 border;
    public float scale;
    // Use this for initialization
    void Start()
    {
        Fill();
    }

#if UNITY_EDITOR
    void Update()
    {
        Fill();
    }
#endif

    private void Fill()
    {
        mesh = new Mesh();
        var left = new Vector3(-0.5f * size.x, -0.5f * size.y);
        var right = new Vector3(0.5f * size.x, 0.5f * size.y);
        var leftBorder = left.x + border.x*scale;
        var rightBorder = right.x - border.x*scale;

        var vertices = new Vector3[]
            {
                     new Vector3(left.x,left.y,0),
                     new Vector3(leftBorder,left.y,0),
                     new Vector3(rightBorder,-right.y,0),
                     new Vector3(right.x,-right.y,0),
                     new Vector3(right.x,right.y,0),
                     new Vector3(rightBorder,right.y,0),
                     new Vector3(leftBorder,right.y,0),
                     new Vector3(left.x,right.y,0)
            };
        var uv = new Vector2[]
            {
                new Vector2(0,0),
                new Vector2(border.x,0),
                new Vector2(1-border.x,0),
                new Vector2(1,0),
                new Vector2(1,1),
                new Vector2(1-border.x,1),
                  new Vector2(border.x,1),
                   new Vector2(0,1)
            };

        var index = new int[]
            {
                0,1,6,
                6,7,0,
                1,2,5,
                5,6,1,
                2,3,4,
                4,5,2
            };

        mesh.vertices = vertices;
        mesh.triangles = index;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        var filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
    }

}
