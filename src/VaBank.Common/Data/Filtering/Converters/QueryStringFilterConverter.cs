﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VaBank.Common.Data.Filtering.Converters
{
    public class QueryStringFilterConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            try
            {
                var stringValue = value as string;
                if (string.IsNullOrEmpty(stringValue))
                {
                    return new EmptyFilter();
                }
                var filterQuery = JsonConvert.DeserializeObject<QueryStringFilter>(stringValue);
                var filter = new DynamicLinqFilter(filterQuery.Query, filterQuery.Parameters);
                return filter;
            }
            catch (Exception)
            {
                return new EmptyFilter();
            }
        }

        private class QueryStringFilter
        {
            [JsonProperty("q")]
            public string Query { get; set; }

            public object[] Parameters
            {
                get { return GetParameters(); }
            }

            private object[] GetParameters()
            {
                var parameters = JsonConvert.DeserializeObject<object[]>(ParametersJson);
                return parameters.Select(x => Visit((dynamic) x)).Cast<object>().ToArray();
            }

            private object Visit(JArray jArray)
            {
                if (jArray == null)
                {
                    return null;
                }
                var array = jArray.Select(x => ((JValue) x).Value).ToList();
                return array;
            }

            private object Visit(object obj)
            {
                return obj;
            }

            [JsonProperty("p")]
            private string ParametersJson { get; set; }
        }
    }
}
