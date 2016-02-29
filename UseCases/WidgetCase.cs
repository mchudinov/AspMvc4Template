using System;
using Common;
using Models;
using Repositories;

namespace UseCases
{
    [Log]
    [LogException]
    public class WidgetCase : IWidgetCase
    {
        private readonly IWidgetRepository _repoW;
        private readonly IUserRepository _repoU;

        public WidgetCase(IWidgetRepository repoW, IUserRepository repoU)
        {
            _repoW = repoW;
            _repoU = repoU;
        }

        public Widget GetWidget(Guid id)
        {
            return _repoW.GetWidget(id);
        }

        public Guid CreateWidget(string name, float price, Guid userId)
        {
            var user = _repoU.GetUser(userId);
            var widget = new Widget()
            {
                Name = name,
                Price = price,
                User = user
            };

            _repoW.SaveWidget(widget);
            return widget.Id;
        }

        public void UpdateWidget(Widget widget)
        {
            _repoW.SaveWidget(widget);
        }

        public void DeleteWidget(Guid id)
        {
            _repoW.DeleteWidget(id);
        }
    }
}
