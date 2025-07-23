using UnityEngine;
using System.Collections.Generic;

public class FuelStand : MonoBehaviour
{
    [SerializeField] private List<FuelTank> _fuelTankList;
    [SerializeField, Range(0.1f, 100.0f)] private float _fuelAdd = 1.0f;

    private void Update()
    {
        foreach (var tank in _fuelTankList)
        {
            tank.AddFuel(_fuelAdd);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FuelTank fuelTank = other.gameObject.GetComponent<FuelTank>();

        if (fuelTank) AddTank(fuelTank);    //  リストに登録する
    }

    private void OnTriggerExit(Collider other)
    {
        FuelTank fuelTank = other.gameObject.GetComponent<FuelTank>();

        if (fuelTank) _fuelTankList.Remove(fuelTank);   // リストから外す
    }

    public void AddTank(FuelTank fuelTank)
    {
        if (_fuelTankList.Contains(fuelTank)) return;   // 同じものがタンクに含まれていた場合に登録せずに終える
        _fuelTankList.Add(fuelTank);
    }
}
