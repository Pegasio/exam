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
        // Получаем данные из плагина и делаем с ними что хотим
        // Например, мы хотим записать в компонент UI.Text текущий прогресс:
        // textProgress.text = YandexGame.savesData.progress.ToString();
        textProgress.text = $"Ваш рекорд: {YandexGame.savesData.progress}";

    }

    // Допустим, это Ваш метод для сохранения
    public void Save()
    {
        // Debug.Log(YandexGame.savesData.progress);
        // Записываем данные в плагин
        // Например, мы хотим сохранить текущий прогресс игрока:
        /* ваша логика для определения текущего прогресса */
        //YandexGame.savesData.progress = YandexGame.savesData.progress;

        // Теперь остается сохранить данные
        YandexGame.SaveProgress();
    }
}

