﻿using System.Text;
using AutoMapper;
using Newtonsoft.Json.Linq;
using VaBank.Core.Payments.Entities;
using VaBank.Services.Contracts.Payments.Models;

namespace VaBank.Services.Payments
{
    public class PaymentsProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PaymentTemplate, PaymentTemplateModel>()
                .ForMember(x => x.FormTemplate, cfg => cfg.MapFrom(x => JObject.Parse(x.Form)))
                .ForMember(x => x.Name, cfg => cfg.ResolveUsing(Constructors.PaymentTemplateModelName));
        }

        private static class Constructors
        {
            public static string PaymentTemplateModelName(PaymentTemplate obj)
            {
                var sb = new StringBuilder();
                var category = obj.Category;
                while (category.ParentCategory.ParentCategory != null)
                {
                    sb.Insert(0, category.Name);
                    if (category.ParentCategory != null)
                        sb.Insert(0, " / ");
                    category = category.ParentCategory;
                }
                return sb.ToString();
            }
        }
    }
}