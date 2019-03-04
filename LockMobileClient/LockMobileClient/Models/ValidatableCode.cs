using LockMobileClient.Validations;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LockMobileClient.Models
{
    public class ValidatableCode : ValidatableObject<string>
    {
        string _presentableValue = "";

        public ValidatableCode(Action propertyChangedCallback = null, params IValidationRule<string>[] validations)
            : base(propertyChangedCallback, validations)
        {
        }

        protected override void OnValueChanged(object sender, PropertyChangedEventArgs e)
        {
            lock (_presentableValue)
            {
                _presentableValue = Value;
                var valueLength = _presentableValue.Length;

                // code has only 9 digits
                if (valueLength > 11)
                {
                    _presentableValue = _presentableValue.Remove(valueLength - 1);
                }

                _presentableValue = _presentableValue.ToUpper();

                if ((valueLength == 4 || valueLength == 8))
                {
                    if (_presentableValue[valueLength - 1] == '-')
                    {
                        _presentableValue = _presentableValue.Remove(valueLength - 1);
                    }
                    else
                    {
                        _presentableValue = _presentableValue.Insert(valueLength - 1, "-");
                    }
                }

                Value = _presentableValue;
                base.OnValueChanged(sender, e);
            }
        }
    }
}
