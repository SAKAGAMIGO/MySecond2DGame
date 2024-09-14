using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveBlock : MonoBehaviour
{
    private Mesh mesh;

    public int segments = 10; // 曲線のセグメント数（頂点数を増やすほど滑らかになる）
    public float width = 1f;  // ブロックの横幅
    public float height = 1f; // ブロックの縦幅
    public float curveAmount = 2f; // 曲がり具合（ラジアン）

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateCurvedMesh();
    }

    void CreateCurvedMesh()
    {
        // 頂点の配列
        Vector3[] vertices = new Vector3[(segments + 1) * 2];
        int[] triangles = new int[segments * 6];
        Vector2[] uvs = new Vector2[(segments + 1) * 2];

        // 頂点を曲げて配置する
        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            float angle = t * curveAmount;  // 曲がり具合を適用
            float x = Mathf.Sin(angle) * width; // 曲線のX座標
            float z = Mathf.Cos(angle) * width; // 曲線のZ座標

            // 下の頂点と上の頂点を設定
            vertices[i * 2] = new Vector3(x, 0, z);
            vertices[i * 2 + 1] = new Vector3(x, height, z);

            // UV座標（テクスチャ貼り付け用）
            uvs[i * 2] = new Vector2(t, 0);
            uvs[i * 2 + 1] = new Vector2(t, 1);

            // 三角形の設定
            if (i < segments)
            {
                int start = i * 2;
                triangles[i * 6] = start;
                triangles[i * 6 + 1] = start + 2;
                triangles[i * 6 + 2] = start + 1;

                triangles[i * 6 + 3] = start + 1;
                triangles[i * 6 + 4] = start + 2;
                triangles[i * 6 + 5] = start + 3;
            }
        }

        // メッシュに頂点、三角形、UVを適用
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
