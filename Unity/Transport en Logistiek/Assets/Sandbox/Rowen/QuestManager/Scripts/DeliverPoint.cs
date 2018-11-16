using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using UnityEngine;

public class DeliverPoint : MonoBehaviour
{
    [SerializeField] private float pricePerTrash;

    void Start()
    {

    }

    void Update()
    {

    }



    private void Trigger()
    {
        DeliverTrash();
    }

    private void DeliverTrash()
    {
        var trash = PlayerData.Instance().goods;

        PlayerData.Instance().goods -= trash;

        PlayerData.Instance().euro += trash * pricePerTrash;

        PointSystem.Add(PlayerData.Instance().euro, trash * pricePerTrash);
    }
}