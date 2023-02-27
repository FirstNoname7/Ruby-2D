using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    //� ����� ������ ������ �� ������� HealthCollectible, ��� ��� ������� �������� - ��� ������ ����� �������� �����, � ��� - ����������
    void OnTriggerStay2D(Collider2D other) //����� OnTriggerEnter2D �� OnTriggerStay2D, ����� ���� ������� ����� �� ���� ������ �������, ������� �� ����� �� ���
    {
        RubyController controller = other.GetComponent<RubyController>(); //���������� controller ����������. ��� ������������ � ������� RubyController, ����� ��������� ��������� �������:

        if (controller != null) //���� ���������� controller (�� ���� ����) �� ��������� � �������� �� ����� ���� (�� ���� ��������� �� ��������), �����:
        {
            controller.ChangeHealth(-1); //���������� ��. ����� � ������� ������� ������� �� ���������� � �����            
        }
    }
}