using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveBlock : MonoBehaviour
{
    private Mesh mesh;

    public int segments = 10; // �Ȑ��̃Z�O�����g���i���_���𑝂₷�قǊ��炩�ɂȂ�j
    public float width = 1f;  // �u���b�N�̉���
    public float height = 1f; // �u���b�N�̏c��
    public float curveAmount = 2f; // �Ȃ����i���W�A���j

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateCurvedMesh();
    }

    void CreateCurvedMesh()
    {
        // ���_�̔z��
        Vector3[] vertices = new Vector3[(segments + 1) * 2];
        int[] triangles = new int[segments * 6];
        Vector2[] uvs = new Vector2[(segments + 1) * 2];

        // ���_���Ȃ��Ĕz�u����
        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            float angle = t * curveAmount;  // �Ȃ�����K�p
            float x = Mathf.Sin(angle) * width; // �Ȑ���X���W
            float z = Mathf.Cos(angle) * width; // �Ȑ���Z���W

            // ���̒��_�Ə�̒��_��ݒ�
            vertices[i * 2] = new Vector3(x, 0, z);
            vertices[i * 2 + 1] = new Vector3(x, height, z);

            // UV���W�i�e�N�X�`���\��t���p�j
            uvs[i * 2] = new Vector2(t, 0);
            uvs[i * 2 + 1] = new Vector2(t, 1);

            // �O�p�`�̐ݒ�
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

        // ���b�V���ɒ��_�A�O�p�`�AUV��K�p
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
