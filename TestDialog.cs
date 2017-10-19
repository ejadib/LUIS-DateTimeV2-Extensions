namespace LuisDateTimeV2Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;

    [Serializable]
    [LuisModel("yourmodelid", "yoursubscriptionkey")]
    public class TestDialog : LuisDialog<object>
    {
        // The following code shows how to extract values without using the Extension Methods

        //[LuisIntent("TestDateTimeV2")]
        //public async Task TestDateTimeV2Verbose(IDialogContext context, LuisResult result)
        //{
        //    // Check if there is a date entity
        //    if (result.TryFindEntity("builtin.datetimeV2.date", out EntityRecommendation dateEntity))
        //    {
        //        var resolutionValues = (IList<object>)dateEntity.Resolution["values"];

        //        var values = (IDictionary<string, object>)resolutionValues[0];

        //        await context.PostAsync($"Timex: {values["timex"]}");
        //        await context.PostAsync($"Type: {values["type"]}");
        //        await context.PostAsync($"Value: {values["value"]}");
        //    }

        //    // Check if there is a date range entity
        //    if (result.TryFindEntity("builtin.datetimeV2.daterange", out EntityRecommendation dateRangeEntity))
        //    {
        //        var resolutionValues = (IList<object>)dateEntity.Resolution["values"];

        //        var values = (IDictionary<string, object>)resolutionValues[0];

        //        await context.PostAsync($"Timex: {values["timex"]}");
        //        await context.PostAsync($"Type: {values["type"]}");
        //        await context.PostAsync($"Start: {values["start"]}");
        //        await context.PostAsync($"End: {values["end"]}");
        //    }

        //    // Check if there is a time entity
        //    if (result.TryFindEntity("builtin.datetimeV2.time", out EntityRecommendation timeEntity))
        //    {
        //        var resolutionValues = (IList<object>)dateEntity.Resolution["values"];

        //        var values = (IDictionary<string, object>)resolutionValues[0];

        //        await context.PostAsync($"Timex: {values["timex"]}");
        //        await context.PostAsync($"Type: {values["type"]}");
        //        await context.PostAsync($"Value: {values["value"]}");
        //    }

        //    // Check if there is a time range entity
        //    if (result.TryFindEntity("builtin.datetimeV2.timerange", out EntityRecommendation timeRangeEntity))
        //    {
        //        var resolutionValues = (IList<object>)dateEntity.Resolution["values"];

        //        var values = (IDictionary<string, object>)resolutionValues[0];

        //        await context.PostAsync($"Timex: {values["timex"]}");
        //        await context.PostAsync($"Type: {values["type"]}");
        //        await context.PostAsync($"Start: {values["start"]}");
        //        await context.PostAsync($"End: {values["end"]}");
        //    }

        //    // Check if there is a date entity
        //    if (result.TryFindEntity("builtin.datetimeV2.datetime", out EntityRecommendation dateTimeEntity))
        //    {
        //        var resolutionValues = (IList<object>)dateEntity.Resolution["values"];

        //        var values = (IDictionary<string, object>)resolutionValues[0];

        //        await context.PostAsync($"Timex: {values["timex"]}");
        //        await context.PostAsync($"Type: {values["type"]}");
        //        await context.PostAsync($"Value: {values["value"]}");
        //    }

        //    context.Wait(this.MessageReceived);
        //}

        // The following code shows how to extract values using the Extension Methods
        [LuisIntent("TestDateTimeV2")]
        public async Task TestDateTimeV2Extensions(IDialogContext context, LuisResult result)
        {
            // get all the values from the entity
            var values = result.Entities[0].GetDateTimeValues();

            foreach (var keyPair in values)
            {
                await context.PostAsync($"{keyPair.Key}: {keyPair.Value}");
            }

            // or...

            // Try to get the value
            if (result.Entities[0].TryGetValue(out string value))
            {
                await context.PostAsync($"The value extracted is {value}");
            }

            // Try to get the range
            if (result.Entities[0].TryGetRange(out (string start, string end) range))
            {
                await context.PostAsync($"The range extracted is {range.start} - {range.end}");
            }

            var key = "timex";
            // Try to get the value of an specific key
            if (result.Entities[0].TryGetValueFromKey(key, out string keyValue))
            {
                await context.PostAsync($"The value extracted for {key} is {keyValue}");
            }

            context.Wait(this.MessageReceived);
        }
    }
}