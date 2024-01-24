using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Configuration;
using ProjectsMeasurements.DBContext;

namespace ProjectsMeasurements.WEB.ActionFilters
{
    public class ViewBagActionFilter : ActionFilterAttribute
    {

        #region Members

        private readonly ProjectsMeasurementsDBContext _dbContext;

        #endregion

        #region Constructor

        public ViewBagActionFilter(ProjectsMeasurementsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Event Methods

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if(context.Controller is PageModel)
            {
                var Controller = context.Controller as PageModel;
                if (Controller != null)
                    Controller.ViewData.Add("MeasurementTypes" , _dbContext.MeasurementsTypes.ToList());
            }
            if (context.Controller is Controller)
            {
                var Controller = context.Controller as Controller;
                if (Controller != null)
                    Controller.ViewBag.MeasurementTypes = _dbContext.MeasurementsTypes.ToList();
            }
            base.OnResultExecuting(context);
        }

        #endregion

    }
}