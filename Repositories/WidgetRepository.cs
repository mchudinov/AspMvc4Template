using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public class WidgetRepository : IWidgetRepository
    {
        public Widget GetWidget(Guid id)
        {
            using (var db = new AppDbContext())
            {
                return db.Widgets.First(u => u.Id == (Guid)id);
            }
        }

        public async Task SaveWidget(Widget widget)
        {
            using (var db = new AppDbContext())
            {
                db.Widgets.Add(widget);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteWidget(Guid id)
        {
            using (var db = new AppDbContext())
            {
                var widget = GetWidget(id);
                db.Widgets.Attach(widget);
                db.Widgets.Remove(widget);
                await db.SaveChangesAsync();
            }
        }
    }
}
