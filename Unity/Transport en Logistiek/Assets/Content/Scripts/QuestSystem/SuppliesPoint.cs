using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using Systems.QuestSystem;
using UnityEngine;
using DG.Tweening;

public class SuppliesPoint : MonoBehaviour
{
    [SerializeField] private float pricePerSupply;
    [SerializeField] private Vector2 PricePerSupplyRange = new Vector2(25, 100);

    [Space(8)]

    [SerializeField] private int suppliesAmount;
    [SerializeField] private Vector2 suppliesAmountRange = new Vector2(1, 10);

    [Space(8)]

    [SerializeField] private GameObject arrow;
    [SerializeField] private float hoverDuration;

    private Coroutine arrowRoutine;

    public bool isActive = false;

    private void Awake()
    {
        DeliverEvent.OnDeliver += Reset;
    }

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        pricePerSupply = Random.Range(PricePerSupplyRange.x, PricePerSupplyRange.y);
        suppliesAmount = Mathf.RoundToInt(Random.Range(suppliesAmountRange.x, suppliesAmountRange.y));

        if (isActive)
        {
            if (arrowRoutine == null)
                arrowRoutine = StartCoroutine(Active());
        }
        else
        {
            StopCoroutine(arrowRoutine);
        }
    }

    [ContextMenu("Pickup Supplies")]
    private void PickupSupplies()
    {
        PlayerData.Instance().Add(ref PlayerData.Instance().supplies, suppliesAmount);

        DeliverEvent.CallEvent();
    }

    [ContextMenu("Deliver Supplies")]
    private void DeliverSupplies()
    {
        var supplies = PlayerData.Instance().supplies;

        PlayerData.Instance().Add(ref PlayerData.Instance().supplies, -supplies);
        PlayerData.Instance().Add(ref PlayerData.Instance().money, supplies * pricePerSupply);

        DeliverEvent.CallEvent();
    }

    [ContextMenu("Trigger")]
    private void Trigger()
    {
        if (isActive)
        {
            switch (QuestManager.Instance().currentTask)
            {
                case Systems.QuestSystem.Action.Pickup:
                    PickupSupplies();
                    break;

                case Systems.QuestSystem.Action.Deliver:
                    DeliverSupplies();
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Trigger();
        }
    }

    public IEnumerator Active()
    {
        if (isActive == true)
        {
            arrow.transform.DOLocalMove(new Vector3(0, transform.localPosition.y + 1, 0), hoverDuration).SetEase(Ease.InOutQuad);
            arrow.transform.DOLocalMove(new Vector3(0, transform.localPosition.y + 0, 0), hoverDuration).SetEase(Ease.InOutQuad).SetDelay(hoverDuration);
            yield return new WaitForSeconds(hoverDuration * 2);

            arrowRoutine = StartCoroutine(Active());
        } else {
            StopCoroutine(arrowRoutine);
        }
    }
}