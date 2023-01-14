using System;
using Microsoft.AspNetCore.Mvc;
using FacileBudget.Customizations.ModelBinders;

namespace FacileBudget.Models.InputModels
{
    [ModelBinder(BinderType = typeof(SpeseListInputModelBinder))]
    public class SpeseListInputModel
    {
        public SpeseListInputModel(int page, int limit)
        {
            Page = Math.Max(1, page);
            Limit = Math.Max(1, limit);

            Offset = (Page - 1) * Limit;
        }
        public int Page { get; }
        
        public int Limit { get; }
        public int Offset { get; }
    }
}