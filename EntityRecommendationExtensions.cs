namespace Microsoft.Bot.Builder.Luis.Models
{
    using System;
    using System.Collections.Generic;
    public static class EntityRecommendationExtensions
    {
        public static IDictionary<string, object> GetDateTimeValues(this EntityRecommendation entity)
        {
            if (!entity.Type.StartsWith("builtin.datetimeV2"))
            {
                return null;
            }

            var resolutionValues = (IList<object>)entity.Resolution["values"];

            return (IDictionary<string, object>)resolutionValues[0];
        }

        public static bool TryGetValueFromKey(this EntityRecommendation entity, string key, out string value)
        {
            value = null;

            var values = GetDateTimeValues(entity);

            if (values != null && values.ContainsKey(key))
            {
                value = values[key].ToString();
            }

            return value != null;
        }

        public static bool TryGetValue(this EntityRecommendation entity, out string value)
        {
            value = null;

            if (!entity.Type.EndsWith("range"))
            {
                var values = GetDateTimeValues(entity);

                value = values != null ? values["value"].ToString() : null;
            }

            return value != null;
        }

        public static bool TryGetRange(this EntityRecommendation entity, out (string start, string end) range)
        {
            range = default(ValueTuple<string, string>);

            if (entity.Type.EndsWith("range"))
            {
                var values = GetDateTimeValues(entity);
                range = values != null ? (values["start"].ToString(), values["end"].ToString()) : default(ValueTuple<string, string>);

                return true;
            }

            return false;
        }
    }
}