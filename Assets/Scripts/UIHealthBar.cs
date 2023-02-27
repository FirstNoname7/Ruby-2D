using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //���� ������ ����� ��� ���������� ����� �� ����� ���� (��� �������� ����� ������)
    public static UIHealthBar instance { get; private set; } //������������� ���������� � ��������� instance �������� ��������� (static), �� ���� ����� � ����� ������� ��������
    //UIHealthBar.instance � ��������� �������� get. � set � ������� ��� ������ private, ���� ����� �� ��� ������ ��� �������� ����� ����� �������
    //�� ���� ���� �������� � ����� ������� UIHealthBar.instance, ����� ����� �������� �������� ���� ��������
    public Image mask; //����� ��������� ������ �����������  � ����������
    float originalSize; //����� ���������,��� ����������� ������ ���� � ���� ������������ �������
    void Awake() //����� �������
    {
        instance=this; //���������� instance (������� ��� ������� ���� ����) �������� ���������, �� ���� ����� �������������� ��� ������
        //this - ��� ����������� �������� ����� C#, ���������� �������, ������� � ������ ������ ��������� ��� ��������.
    }
    void Start()
    {
        originalSize = mask.rectTransform.rect.width; //mask.rectTransform - �� ������������ ������ ���� ���, ������� ���������� � �����
        //rect.width - ������ ����� �������� (����� �������� �� ��������������)
    }

    // Update is called once per frame
    public void SetValue(float value) //��������� ������� ��� �������� ��������� �������� � ������� ��������� �������� �����.
                                      //���������� ������ SetValue, ����� ������������ �������� �� 0 �� 1 � ������ �� ��� ������ ��� ��������, ������� ������ �����.
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value); //SetSizeWithCurrentAnchors - ����������� ������ �������� � ����� Anchors
        //RectTransform.Axis.Horizontal - �������� ������ ������ �� �����������
        //originalSize * value - ���� ��������� �������: ������������ ������ ���������� �� ��������� ����������, ������� ��������� � ��������� �������� �����
    }
}
