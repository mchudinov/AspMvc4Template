using System;
using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public interface IWidgetRepository
    {
        Widget GetWidget(Guid id);

        Task SaveWidget(Widget widget);

        Task DeleteWidget(Guid id);
    }
}
