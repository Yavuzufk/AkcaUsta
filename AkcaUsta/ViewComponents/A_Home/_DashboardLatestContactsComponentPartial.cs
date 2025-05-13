using AkcaUsta.Repositories.IRepository;
using AkcaUsta.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.A_Home
{
    public class _DashboardLatestContactsComponentPartial:ViewComponent
    {
        private readonly IStatisticsDal _statisticsDal;

        public _DashboardLatestContactsComponentPartial(IStatisticsDal statisticsDal)
        {
            _statisticsDal = statisticsDal;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _statisticsDal.GetLatestContactsAsync();
            return View(result);
        }
    }
}
