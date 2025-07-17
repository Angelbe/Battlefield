using System.Collections.Generic;
using UnityEngine;

public class UITurnOrder : MonoBehaviour
{
    public GameObject turnOrderSlotPrefab;
    private Dictionary<int, UITurnOrderSlotView> slots = new();

    private void Awake()
    {
        CreateTurnOrderSlots();
    }

    private void CreateTurnOrderSlots()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject slotGO = Instantiate(turnOrderSlotPrefab, transform);
            slotGO.name = $"TurnOrderSlot_{i}";
            var slotView = slotGO.GetComponent<UITurnOrderSlotView>();
            slotView.gameObject.SetActive(false);
            slots[i] = slotView;
        }
    }

    public void Show(List<CreatureController> order)
    {
        for (int i = 0; i < 15; i++)
        {
            if (slots.TryGetValue(i, out var slotView))
            {
                if (i < order.Count)
                {
                    slotView.SetCreature(order[i]);
                    slotView.gameObject.SetActive(true);
                }
                else
                {
                    slotView.gameObject.SetActive(false);
                }
            }
        }
    }
}
