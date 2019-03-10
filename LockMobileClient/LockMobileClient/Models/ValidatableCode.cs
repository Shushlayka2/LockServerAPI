using LockMobileClient.Validations;
using System;
using System.ComponentModel;

namespace LockMobileClient.Models
{
    public class ValidatableCode : ValidatableObject<string>
    {
        public ValidatableCode(Action propertyChangedCallback = null, params IValidationRule<string>[] validations)
            : base(propertyChangedCallback, validations)
        {
        }

        protected override void OnValueChanged()
        {
                var valueLength = Value.Length;

                // code has only 9 digits
                if (valueLength > 11)
                {
                    Value = Value.Remove(valueLength - 1);
                }

                //Value = Value.ToUpper();

                //if ((valueLength == 4 || valueLength == 8))
                //{
                //    if (Value[valueLength - 1] == '-')
                //    {
                //        Value = Value.Remove(valueLength - 1);
                //    }
                //    else
                //    {
                //        Value = Value.Insert(valueLength - 1, "-");
                //    }
                //}
                base.OnValueChanged();
        }
    }
}
