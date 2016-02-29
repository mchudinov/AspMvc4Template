using System;
using Models;

namespace UseCases
{
    public interface IWidgetCase
    {
        Widget GetWidget(Guid id);

        Guid CreateWidget(string name, float price, Guid userId);

        void UpdateWidget(Widget widget);

        void DeleteWidget(Guid id);
    }
}
