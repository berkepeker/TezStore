using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace TezStore.Binders // Ad alanı eklendi
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != ValueProviderResult.None)
            {
                var value = valueProviderResult.FirstValue;

                // Nokta ve virgülü ele al
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace(",", "."); // Virgülleri nokta ile değiştir
                }

                if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                {
                    bindingContext.Result = ModelBindingResult.Success(result);
                    return Task.CompletedTask;
                }

                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Geçersiz ondalık sayı formatı.");
            }

            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
    }
}