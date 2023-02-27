using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //���� ������ ��� ������� ������������ (��� ����, ��� ������ ������)
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake() //������� ������� Start �� Awake, ����� ��� ����� �������� ����� ������� (�� ��������� ������, ��������� � ���, ��� ��� ������ ���������� ������, �������� ���)
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    public void Launch(Vector2 direction, float force) //� ������� ������� ����������� ������� (Vector2 - �� ���� � ��������� x � y, direction - ��������� �����������, force - ����, � ������� ����� �������� �������� �� ����, float - ������ ��� ���� �������� �� -1 �� 1)
    {
        rigidbody2d.AddForce(direction * force);  //���. ������ = ���� ����� ������� = ����������� ������� * ���� �������
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f) //������� "���� ������ ������ �� 1000 ������ �� �������� ������, �����:"
        {
            Destroy(gameObject); //�� ������������. ��� ����, ����� ������ ������� �� ������� ������. � �� ���� ������ �������� ��������� � ��� ����� �� ��������� ���� ���������� ������, �� ���� ������������� ������ �����, ���
        }
    }
    void OnCollisionEnter2D(Collision2D other) //�����, ���� ���������� ������������. Collision2D � ������� ������������ ��� ������ ���������� other
    {
        EnemyController e = other.collider.GetComponent<EnemyController>(); //��������� �� ��������� � ������� � ��������� EnemyController ��� ������ ���������� � ��������� "�"
        if (e!=null) //�������: ���� ���������� � = false, �����:
        {
            e.Fix(); //���������� ���������� ������� � ������ Fix (��� ��������� � ������� EnemyController)
        }
        Destroy(gameObject); //� ��� ������ �������� (������� �����)
    }
}
