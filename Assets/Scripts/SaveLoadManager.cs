using UnityEngine;
using UnityEngine.UI;
using YG;
using TMPro;

public class SaveLoadManager : MonoBehaviour
{
    public TMP_Text textProgress;


    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }


    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    public void GetLoad()
    {
        // �������� ������ �� ������� � ������ � ���� ��� �����
        // ��������, �� ����� �������� � ��������� UI.Text ������� ��������:
        // textProgress.text = YandexGame.savesData.progress.ToString();
        textProgress.text = $"��� ������: {YandexGame.savesData.progress}";

    }

    // ��������, ��� ��� ����� ��� ����������
    public void Save()
    {
        // Debug.Log(YandexGame.savesData.progress);
        // ���������� ������ � ������
        // ��������, �� ����� ��������� ������� �������� ������:
        /* ���� ������ ��� ����������� �������� ��������� */
        //YandexGame.savesData.progress = YandexGame.savesData.progress;

        // ������ �������� ��������� ������
        YandexGame.SaveProgress();
    }
}

