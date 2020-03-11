# HandlebarsHelpers
Helpers for Handlebars.Net

## General Helpers
### HTTP Basic Auth
Combines parameters into token for HTTP Basic Auth
``` C#
var template = "Authorization: Basic {{auth_token Username Password}}";
var data = new
{
    Username = "api_username",
    Password = "api_password"
};

Handlebars.RegisterHelper("auth_token", GeneralHelpers.HttpBasicAuthHelper);
var hbTemplate = Handlebars.Compile(template);
var authHeader = hbTemplate(data);
// authHeader = Authorization: Basic YXBpX3VzZXJuYW1lOmFwaV9wYXNzd29yZA==
```

## JSON Helpers
Helpers to help with JSON templates

### Bool Helper
The .ToString() method on .NET booleans results in capitalized values, "True" and "False". JSON uses lower case booleans, "true" and "false". This helper makes sure bools are lowered for JSON.

``` C#
var template = "{ \"Enabled\": {{bool_lower IsEnabled}} }";
var data = new { IsEnabled = true};

Handlebars.RegisterHelper("bool_lower", JsonHelpers.BoolHelper);
var hbTemplate = Handlebars.Compile(template);
var json = hbTemplate(data);
// json = { \"Enabled\": true }
```

### List Comma Helper
In a template to build a json file you may what to have an array of items, but json does not permit trailing commas. Use this helper skip writing a comma to the first element.

Here is the formatted template
``` handlebars
{
  "Items": [
    {{#each Items}}
    {{list_comma @index}}"{{this}}"
    {{/each}}
  ]
}
```

and C# code using it
``` C#
var template = "{\"Items\":[{{#each Items}}{{list_comma @index}}\"{{this}}\"{{/each}}]}";
var data = new
{
    Items = new List<string> {"one", "two", "three", "four", "five"}
};

Handlebars.RegisterHelper("list_comma", JsonHelpers.ListCommaHelper);
var hbTemplate = Handlebars.Compile(template);
var json = hbTemplate(data);
```
yields this (formatted)
``` json
{
  "Items": [
    "one"
    ,"two"
    ,"three"
    ,"four"
    ,"five"
  ]
}
```