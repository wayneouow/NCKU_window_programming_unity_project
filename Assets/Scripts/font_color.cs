using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class font_color : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color startColor = Color.red;
    public Color endColor = Color.blue;
    public float duration = 2f;

    private float elapsedTime = 0f;
    private bool isForward = true; // �ΨӼаO��e���ܤ�V
    void Update()
    {
        // �W�[�w�g�L���ɶ�
        elapsedTime += Time.deltaTime;

        // �p�ⴡ���v���A����0��1����
        float t = Mathf.Clamp01(elapsedTime / duration);

        // �p�G�F����w������ɶ��A�������ܤ�V
        if (elapsedTime >= duration)
        {
            elapsedTime = 0f;
            isForward = !isForward; // �������ܤ�V
        }

        // �ھں��ܤ�V�]�w�����v��
        if (isForward)
        {
            t = Mathf.Lerp(0f, 1f, t);
        }
        else
        {
            t = Mathf.Lerp(1f, 0f, t);
        }
    }
}