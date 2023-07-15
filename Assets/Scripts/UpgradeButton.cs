using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Upgrades upgrade; // переменная для хранения ссылки на объект улучшения
    public Player player; // переменная для хранения ссылки на объект игрока
    public bool fill = false; // флаг, указывающий, заполнена ли кнопка улучшения
    public GameObject description; // объект, который будет отображать описание улучшения при наведении на кнопку
    public GameObject tempDes; // временный объект, который будет отображать описание улучшения
    public Canvas mainCanvas; // объект, который будет являться канвасом для отображения описания улучшения

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); // находим объект игрока и сохраняем ссылку на него в переменной player
        fill = false; // устанавливаем флаг fill в false
        if (upgrade != null) // если у кнопки улучшения есть объект улучшения, то:
        {
            SetUpgrade(upgrade); // вызываем метод SetUpgrade для установки улучшения на кнопку
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)// метод, который вызывается при наведении на кнопку мышью
    {
        if (fill)// если кнопка улучшения заполнена, то:
        {
            description.transform.GetChild(0).GetComponent<Image>().sprite = upgrade.icon; // устанавливаем иконку улучшения в изображении
            description.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            description.transform.GetChild(1).GetComponent<Text>().text = upgrade.name;// устанавливаем название улучшения в тексте
            description.transform.GetChild(2).GetComponent<Text>().text = $"Стоимость: {upgrade.price}\n"; // устанавливаем стоимость улучшения в тексте
            if (upgrade.effectOnCapyPerSec != UBigNumber.ConvertToBigNumber(0))// если у улучшения есть эффект на процент капитулы в секунду, то:
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"Весь доход +{upgrade.stringEffectOnCapyPerSec}%\n";// устанавливаем текст с эффектом на процент капитулы в секунду
            }
            if (upgrade.effectOnCapyPerSecOnItem != UBigNumber.ConvertToBigNumber(0))// если у улучшения есть эффект на процент капитулы в секунду для определенного предмета, то:
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"Доход строения {upgrade.GetStringItemType()} +{upgrade.stringEffectOnCapyPerSecOnItem}%\n";// устанавливаем текст с эффектом на процент капитулы в секунду для определенного предмета
            }
            if (upgrade.effectOnTap != UBigNumber.ConvertToBigNumber(0)) // если у улучшения есть эффект на процент дохода за клик, то:
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"Увеличение дохода за клик +{upgrade.stringEffectOnTap}%\n";
            }
            if (upgrade.effectOnTapFromAll != UBigNumber.ConvertToBigNumber(0))// если у улучшения есть эфффект на процент дохода за клик от дохода, то:
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"Увеличение дохода за клик от дохода +{upgrade.stringEffectOnTapFromAll}%\n";// устанавливаем текст с эффектом на процент дохода за клик
            }
            tempDes = Instantiate(description,mainCanvas.transform);// создаем временный объект, который будет отображать описание улучшения
            tempDes.transform.position = new Vector2(Input.mousePosition.x - 352, Input.mousePosition.y - 152);// устанавливаем позицию временного объекта в координатах мыши
            tempDes.transform.SetParent(mainCanvas.transform);// устанавливаем родительский объект временного объекта в канвасе
        }
    }


    public void OnPointerExit(PointerEventData eventData)// метод, который вызывается при выходе курсора мыши с кнопки улучшения
    {
        Destroy(tempDes);// удаляем временный объект, который отображает описание улучшения
    }

    public virtual void OnPointerClick(PointerEventData eventData) // метод, который вызывается при нажатии на кнопку улучшения
    {
        if (fill) // если кнопка улучшения заполнена, то:
        {
            if (upgrade.price <= player.capyPoints) // если у игрока достаточно капитул, то:
            {
                upgrade.active = false; // устанавливаем флаг активности улучшения в false
                upgrade.alreadySet = false; // устанавливаем флаг установки улучшения в false
                player.capyPoints -= upgrade.price; // уменьшаем количество капитул у игрока на стоимость улучшения
                upgrade.DoUpgrade(); // вызываем метод DoUpgrade у объекта улучшения для изменения его параметров
                player.UpdateStatistic(); // вызываем метод UpdateStatistic у объекта игрока для обновления его статистики
                upgrade = null; // устанавливаем объект улучшения в null
                gameObject.GetComponent<Image>().sprite = null; // устанавливаем спрайт кнопки улучшения в null
                gameObject.GetComponent<Image>().color = Color.white; // устанавливаем цвет кнопки улучшения в белый
                fill = false; // устанавливаем флаг fill в false
            }
        }
    }

    public virtual void SetUpgrade(Upgrades up)// метод для установки улучшения на кнопку
    {
        up.price = new UBigNumber(up.stringPrice); // преобразуем строковое значение стоимости улучшения в объект UBigNumber
        up.effectOnCapyPerSec = new UBigNumber(up.stringEffectOnCapyPerSec); // преобразуем строковое значение эффекта на процент капитулы в секунду в объект UBigNumber
        up.effectOnCapyPerSecOnItem = new UBigNumber(up.stringEffectOnCapyPerSecOnItem); // преобразуем строковое значение эффекта на процент капитулы в секунду для определенного предмета в объект UBigNumber
        up.effectOnTap = new UBigNumber(up.stringEffectOnTap); // преобразуем строковое значение эффекта на процент дохода за клик в объект UBigNumber
        up.effectOnTapFromAll = new UBigNumber(up.stringEffectOnTapFromAll); // преобразуем строковое значение эффекта на процент дохода за клик от дохода в объект UBigNumber
        upgrade = up;// устанавливаем объект улучшения в переменную upgrade
        if (upgrade.icon != null)// если у улучшения есть иконка, то:
        {
            GetComponent<Image>().sprite = GetComponent<UpgradeButton>().upgrade.icon;// устанавливаем иконку улучшения на кнопке
        }
        fill = true;// устанавливаем флаг fill в true
    }
}
