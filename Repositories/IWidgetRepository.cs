using System;
using System.Collections.Generic;
using Models;

namespace Repositories
{
    public interface IWidgetRepository
    {
        IList<Widget> GetAllWidgets();

        IList<Widget> GetWidgets(string filter);

        Widget GetWidget(Guid guid);

        void SaveWidget(Widget widget);
    }
}
