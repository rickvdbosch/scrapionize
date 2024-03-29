[![Build & Publish](https://github.com/rickvdbosch/scrapionize/actions/workflows/build-and-publish.yml/badge.svg)](https://github.com/rickvdbosch/scrapionize/actions/workflows/build-and-publish.yml)

# Scrapionize

Scrapionize (a Scraper for Sessionize) is a NuGet package to scrape a Sessionize CFP page and give you the basic information that's
on the CFP page. The information we'll get from the page:

* Name of the event
* Start and End Date of the event
* Location of the event
* URL of the event
* Start and End date of the CFP
* Arrangements around Travel, Accomodation and Event Fee for speakers

Scrapionize uses the [HtmlAgilityPack package](https://www.nuget.org/packages/HtmlAgilityPack/) to parse the Sessionize CFP page.

## Examples

Using Scrapionize is pretty straightforward. There's a scraper interface you can register, and a class providing the implementation for
that interface. If you're using .NET dependency injection, there's also an extension method to auto-setup DI.

### Example: using .NET Dependency Injection

Use the extension method to setup Scrapionize.

```csharp
builder.Services.AddScrapionize();
```

Inject the interface into the constructor of the class you want to use it in.

```csharp
private IScraper _scraper;

public SomeClass(IScraper scraper)
{
    _scraper = scraper;
}
```

And then use the scraper to get your Sessionize data!

```csharp
var sessionizeData = _scraper.Scrape(new Uri("<YOUR-SESSIONIZE-URL>"));
```

### Example: using `new`

Not the preferred way of adding dependencies, but still an option.

```csharp
var scraper = new Scraper(); //new is glue!
var sessionizeData = scraper.Scrape(new Uri("<YOUR-SESSIONIZE-URL>"));
```

>Hollywood produced an entire movie centered on this principle, in which the LEGO bad-guy was trying to glue pieces together.  
>Don't be that guy.  
(_Inspired by the [New is Glue](https://ardalis.com/new-is-glue/) article on [ardalis.com](https://ardalis.com/)_)
