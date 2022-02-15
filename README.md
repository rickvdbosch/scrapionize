# Scrapionize

Scrapionize (a Scraper for Sessionize) is a NuGet package to scrape a Sessionize CFP page and give you the basic information that's
on there. The information we'll get from the CFP page:

* Name of the event
* Start and End Date of the event
* Location of the event
* URL of the event
* Start and End date of the CFP
* Arrangements around Travel, Accomodation and Event Fee for speakers

## Examples

Using Scrapionize is pretty straightforward. There's an interface you can register and a class providing the implementation. If you're using
.NET dependency injection, there's also an extension method to auto-setup DI.

### Using .NET Dependency Injection

Use the extension method to setup Scrapionize.

```csharp
builder.Services.AddScrapionize();
```

Inject the interface into the constructor of class you want to use it in.

```csharp
private IScraper _scraper;

public SomeClass(IScraper scraper)
{
    _scraper = scraper;
}
```

And use the scraper to get your Sessionize data!

```csharp
var sessionizeData = _scraper.Scrape(new Uri("<YOUR-SESSIONIZE-URL>"));
```

### Using `new`

Not the preferred way of adding dependencies...

```csharp
var scraper = new Scraper(); //new is glue!
var sessionizeData = scraper.Scrape(new Uri("<YOUR-SESSIONIZE-URL>"));
```
