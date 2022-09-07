using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Character.Weapons
{
    interface IInputProvider
    {
        public event UnityAction ActionPerformed;
        public event UnityAction ActionHoldTick;
        public event UnityAction ActionCanceled;

        public bool IsActionHolded { get; }
    }

    public abstract class WeaponsInputProvider : MonoBehaviour, IInputProvider
    {
        public virtual event UnityAction ActionPerformed 
        {
            add => actionPerformed.AddListener(value); 
            remove => actionPerformed.RemoveListener(value);
        }

        public virtual event UnityAction ActionHoldTick
        {
            add => actionHoldTick.AddListener(value);
            remove => actionHoldTick.RemoveListener(value);
        }

        public virtual event UnityAction ActionCanceled
        {
            add => actionCanceled.AddListener(value);
            remove => actionCanceled.RemoveListener(value);
        }

        [SerializeField] protected UnityEvent actionPerformed;
        [SerializeField] protected UnityEvent actionHoldTick;
        [SerializeField] protected UnityEvent actionCanceled;

        public virtual bool IsActionHolded { get; protected set; }
    }
}
