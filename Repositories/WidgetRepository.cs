using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Repositories
{
    public class WidgetRepository : IWidgetRepository
    {
        public IList<Widget> GetAllWidgets()
        {
            using (var db = new AppDbContext())
            {
                return db.Widgets.AsNoTracking().ToList();
            }
        }

        public Widget GetWidget(IFormattable id)
        {
            using (var db = new AppDbContext())
            {
                return db.Widgets.First(u => u.Id == (Guid)id);
            }
        }

        public IList<Widget> GetWidgets(string filter)
        {
            using (var db = new AppDbContext())
            {
                return db.Widgets.AsNoTracking().Where(u => u.Name.ToLower().Contains(filter.ToLower())).ToList();
            }
        }

        public void SaveWidget(Widget widget)
        {
            using (var db = new AppDbContext())
            {
                db.Widgets.Add(widget);
                db.SaveChangesAsync();
            }
        }
    }
}
