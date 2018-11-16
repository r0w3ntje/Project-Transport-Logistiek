using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using UnityEngine;

public class DeliverPoint : MonoBehaviour
{
    [SerializeField] private float pricePerGood;

    [SerializeField] private Vector2 pricePerGoodRange = new Vector2(1, 10);

    public bool isActive = false;

    private void Awake()
    {
        DeliverEvent.OnDeliver += NewPrice;
    }

    private void Start()
    {
        NewPrice();
    }

    private void NewPrice()
    {
        pricePerGood = Random.Range(pricePerGoodRange.x, pricePerGoodRange.y);
    }

    [ContextMenu("Deliver")]
    private void DeliverGoods()
    {
        var goods = PlayerData.Instance().goods;

        PlayerData.Instance().Add(ref PlayerData.Instance().goods, -goods);
        PlayerData.Instance().Add(ref PlayerData.Instance().money, goods * pricePerGood);

        DeliverEvent.CallEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            if (isActive) DeliverGoods();
    }
}