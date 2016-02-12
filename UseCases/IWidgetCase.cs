using System;

namespace UseCases
{
    public interface IWidgetCase
    {
        IFormattable CreateWidget(string name, float price, IFormattable userId);
    }
}
