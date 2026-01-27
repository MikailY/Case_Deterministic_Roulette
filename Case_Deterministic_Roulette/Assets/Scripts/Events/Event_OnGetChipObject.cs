using System;
using Data;
using UnityEngine;

namespace Events
{
    public class Event_OnGetChipObject
    {
        public readonly ChipSO Chip;
        public readonly Action<ChipObject> OnGetObject;

        public Event_OnGetChipObject(ChipSO chip, Action<ChipObject> onGetObject)
        {
            Chip = chip;
            OnGetObject = onGetObject;
        }
    }
}