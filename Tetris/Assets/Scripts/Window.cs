using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //обязательно для работы с системой событий

public class Window : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //интерфейсы
{

    //стартовая позиция мыши при начале перетаскивания
    //нужна, чтобы окно сразу не дергалось центрируясь по курсору мышки
    //а плавно меняла положение вслед за курсором
    private Vector3 mouseStartingPos;

    //поддается ли перетаскиванию (цепляет ли его курсор)
    public bool isDragging = true;

    //сколько пикселей от конца экрана безопасная зона, куда будет возвращено окно
    //при перетаскивании за край экрана
    int pixeSafeZone = 20;

    //начало перетаскивания
    public void OnBeginDrag(PointerEventData eventData)
    {
        //запоминаем где была мыш на начале перетаскивания
        mouseStartingPos = Input.mousePosition;
    }

    // в процессе перетаскивания (вызывается каждый раз при изменении положении мышки)
    public void OnDrag(PointerEventData eventData)
    {
        //если окно может цеплять курсор (перетаскиваемое)
        if (isDragging)
        {
            //позиция окна изменяется соответственно тому, как изменилась
            //позиция мышки в процессе перетаскивания. При каждом движении вычисляем
            //разницу между прошлым и текущим положением мышки и применяем ее к окну
            transform.position -= mouseStartingPos - Input.mousePosition;
            //запоминаем текущее положение
            mouseStartingPos = Input.mousePosition;
        }
    }



    //в конце перетаскивания
    public void OnEndDrag(PointerEventData eventData)
    {
        //не позволить окнам выходить за разрешение экрана
        CheckOutScreen();

    }

    //не позволяет окнам выходить за разрешение экрана
    private void CheckOutScreen()
    {
        //если окно вышло по иску за пределы экрана вправо
        if (transform.position.x > Screen.width)
        {
            //возвращаем его в пределы экрана
            transform.position = new Vector3(Screen.width - pixeSafeZone, transform.position.y);
        }
        //если окно вышло по иску за пределы экрана влево
        else if (transform.position.x <= 0)
        {
            //возвращаем его в пределы экрана
            transform.position = new Vector3(pixeSafeZone, transform.position.y);
        }

        //если окно вышло по игреку за пределы экрана вправо
        if (transform.position.y > Screen.height)
        {
            //возвращаем его в пределы экрана
            transform.position = new Vector3(transform.position.x, Screen.height - pixeSafeZone);
        }
        //если окно вышло по игреку за пределы экрана влево
        else if (transform.position.y <= 0)
        {
            //возвращаем его в пределы экрана
            transform.position = new Vector3(transform.position.x, pixeSafeZone);
        }
    }

    void Update()
    {

    }

    void Start()
    {
    }

    public void CloseWindow()
    {
        Destroy(transform.root.gameObject);
    }
}