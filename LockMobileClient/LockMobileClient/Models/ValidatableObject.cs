using LockMobileClient.Validations;
using LockMobileClient.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LockMobileClient.Models
{
    public class ValidatableObject<T> : BaseViewModel
    {
        public List<IValidationRule<T>> Validations { get; } = new List<IValidationRule<T>>();
        public T Value { get; set; }

        public bool IsValid;

        readonly Action propertyChangedCallback;

        public ValidatableObject(Action propertyChangedCallback = null, params IValidationRule<T>[] validations)
        {
            this.propertyChangedCallback = propertyChangedCallback;
            PropertyChanged += async (sender, e) => await Task.Run(() => OnValueChanged(sender, e));
            foreach (var val in validations)
            {
                Validations.Add(val);
            }
        }

        protected virtual void OnValueChanged(object sender, PropertyChangedEventArgs e)
        {
            IsValid = Validations.TrueForAll(v => v.Validate(Value));
            propertyChangedCallback?.Invoke();
        }
    }
}
