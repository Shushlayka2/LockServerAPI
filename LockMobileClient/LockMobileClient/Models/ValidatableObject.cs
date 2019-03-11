using LockMobileClient.Validations;
using LockMobileClient.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;

namespace LockMobileClient.Models
{
    public class ValidatableObject<T> : BaseViewModel
    {
        public List<IValidationRule<T>> Validations { get; } = new List<IValidationRule<T>>();
        public T Value { get; set; }

        [DependsOn(nameof(Value))]
        public bool IsValid => Validations.TrueForAll(v => v.Validate(Value));

        readonly Action propertyChangedCallback;

        public ValidatableObject(Action propertyChangedCallback = null, params IValidationRule<T>[] validations)
        {
            this.propertyChangedCallback = propertyChangedCallback;
            foreach (var val in validations)
            {
                Validations.Add(val);
            }
        }

        protected virtual void OnValueChanged() => propertyChangedCallback?.Invoke();
    }
}
