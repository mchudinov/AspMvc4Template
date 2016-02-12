using System;
using Common;
using Models;
using Repositories;

namespace UseCases
{
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

        public IFormattable CreateWidget(string name, float price, IFormattable userId)
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
    }
}
