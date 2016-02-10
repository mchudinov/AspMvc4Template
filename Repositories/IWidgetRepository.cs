using System;
using System.Collections.Generic;
using Models;

namespace Repositories
{
    public interface IWidgetRepository
    {
        IEnumerable<Widget> GetAllWidgets();

        IEnumerable<Widget> GetWidgets(string filter);

        Widget GetWidget(Guid guid);

        void SaveWidget(Widget widget);
    }
}
