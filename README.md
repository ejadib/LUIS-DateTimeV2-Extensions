# LUIS-DateTimeV2-Extensions for Bot Framework

Extension methods to extract LUIS DateTimeV2 Prebuilt entity values for your Microsoft Bot Framework bot

The way to do it without the extension methods can be too verbose:

```
if (result.TryFindEntity("builtin.datetimeV2.date", out EntityRecommendation dateEntity))
{
    var resolutionValues = (IList<object>)dateEntity.Resolution["values"];

    var values = (IDictionary<string, object>)resolutionValues[0];

    await context.PostAsync($"Timex: {values["timex"]}");
    await context.PostAsync($"Type: {values["type"]}");
    await context.PostAsync($"Value: {values["value"]}");
}

if (result.TryFindEntity("builtin.datetimeV2.daterange", out EntityRecommendation dateRangeEntity))
{
    var resolutionValues = (IList<object>)dateEntity.Resolution["values"];

    var values = (IDictionary<string, object>)resolutionValues[0];
    
    await context.PostAsync($"Timex: {values["timex"]}");
    await context.PostAsync($"Type: {values["type"]}");
    await context.PostAsync($"Start: {values["start"]}");
    await context.PostAsync($"End: {values["end"]}");
}

```

With the extension methods you can do things like:

```
// get all the values from the entity
entity.GetDateTimeValues();

/ Try to get the value
entity.TryGetValue(out string value)

// Try to get the range
entity.TryGetRange(out (string start, string end) range)

// Try to get the value of an specific key
entity.TryGetValueFromKey('timex', out string keyValue)
```

The extensions methods are in the [EntityRecommendationExtensions.cs](https://github.com/ejadib/LUIS-DateTimeV2-Extensions/blob/master/EntityRecommendationExtensions.cs) class. And there is a sample showing how to use them
