﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VirtoCommerce.Domain.Cart.Model;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.CartModule.Web.JsonConverters
{
    public class PolymorphicCartJsonConverter : JsonConverter
    {
        public PolymorphicCartJsonConverter()
        {
        }

        public override bool CanWrite { get { return false; } }
        public override bool CanRead { get { return true; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ShoppingCart).IsAssignableFrom(objectType) || objectType == typeof(ShoppingCartSearchCriteria);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object retVal = null;
            var obj = JObject.Load(reader);
            if (typeof(ShoppingCart).IsAssignableFrom(objectType))
            {
                retVal = AbstractTypeFactory<ShoppingCart>.TryCreateInstance();
            }
            else if (objectType == typeof(ShoppingCartSearchCriteria))
            {
                retVal = AbstractTypeFactory<ShoppingCartSearchCriteria>.TryCreateInstance();
            }          
            serializer.Populate(obj.CreateReader(), retVal);
            return retVal;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}